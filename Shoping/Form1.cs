using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System.Reflection;
using Microsoft.ML;
using Shoping.Data;

namespace Shoping
{
    public partial class Form1 : Form
    {
        MLContext mlContext;
        ITransformer model1;
        PredictionEngine<Shop, ClusterPrediction> predictor;
        List<string[]> objects;
        string _dataPathClaster = Path.Combine(Environment.CurrentDirectory, "Data", "shop.data");
        string _modelPathClaster = Path.Combine(Environment.CurrentDirectory, "Data", "ShopClusteringModel.zip");
        string _modelPathReggresion = Path.Combine(Environment.CurrentDirectory, "Data", "Prices.zip");
        string _trainDataPathReggresion = Path.Combine(Environment.CurrentDirectory, "Data", "Prices.csv");
        GMapMarker _currentMarker;
        GMapPolygon _polygon;
        bool _isMouseDown;
        GMapMarkerRect _curentRectMarker;
        readonly GMapOverlay _top = new GMapOverlay();
        internal readonly GMapOverlay Objects = new GMapOverlay("objects");
        internal readonly GMapOverlay Routes = new GMapOverlay("routes");
        internal readonly GMapOverlay Polygons = new GMapOverlay("polygons");
        public Form1()
        {
            InitializeComponent();
            objects = new List<string[]>();
            mlContext = new MLContext(seed: 0);
            model1 = Train(mlContext, _trainDataPathReggresion);
            Evaluate(mlContext, model1);
            IDataView dataView = mlContext.Data.LoadFromTextFile<Shop>(_dataPathClaster, hasHeader: false, separatorChar: ',');
            string featuresColumnName = "Features";
            var pipeline = mlContext.Transforms
                .Concatenate(featuresColumnName, "profit", "visitors", "expenses", "staff")
                .Append(mlContext.Clustering.Trainers.KMeans(featuresColumnName, numberOfClusters: 2));
            var model2 = pipeline.Fit(dataView);
            using (var fileStream = new FileStream(_modelPathClaster, FileMode.Create, FileAccess.Write, FileShare.Write))
            {
                mlContext.Model.Save(model2, dataView.Schema, fileStream);
            }
            predictor = mlContext.Model.CreatePredictionEngine<Shop, ClusterPrediction>(model2);
            //StreamReader r = new StreamReader(_dataPathClaster);
            //string line;
            //while ((line = r.ReadLine()) != null)
            //{
            //    string[] lineSub = line.Split(',');
            //    var profit = SinglePrediction(mlContext, model, new Price { Id = "0", Income = 0, Visitors = float.Parse(lineSub[1]) }, new Price { Id = "0", Income = float.Parse(lineSub[0]), Visitors = float.Parse(lineSub[1]) });
            //    var prediction = predictor.Predict(new Shop { profit = profit, visitors = float.Parse(lineSub[1]), expenses = float.Parse(lineSub[2]), staff = float.Parse(lineSub[3]) });
            //    Console.WriteLine($"Data: {line.Replace(lineSub[0], profit.ToString())}");
            //    Console.WriteLine($" ластер: {prediction.PredictedClusterId}");
            //    Console.WriteLine($"–ассто€ни€: {string.Join(" ", prediction.Distances)}");

            //}
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache; //выбор подгрузки карты Ц онлайн или из ресурсов
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance; //какой провайдер карт используетс€ (в нашем случае гугл) 
            gMapControl1.MinZoom = 2; //минимальный зум
            gMapControl1.MaxZoom = 16; //максимальный зум
            gMapControl1.Zoom = 4; // какой используетс€ зум при открытии
            gMapControl1.Position = new GMap.NET.PointLatLng(66.4169575018027, 94.25025752215694);// точка в центре карты при открытии (центр –оссии)
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter; // как приближает (просто в центр карты или по положению мыши)
            gMapControl1.CanDragMap = true; // перетаскивание карты мышью
            gMapControl1.DragButton = MouseButtons.Middle; // какой кнопкой осуществл€етс€ перетаскивание
            gMapControl1.ShowCenter = false; //показывать или скрывать красный крестик в центре
            gMapControl1.ShowTileGridLines = false; //показывать или скрывать тайлы
            gMapControl1.Overlays.Add(Routes);
            gMapControl1.Overlays.Add(Polygons);
            gMapControl1.Overlays.Add(Objects);
            gMapControl1.Overlays.Add(_top);
            _currentMarker = new GMarkerGoogle(gMapControl1.Position, GMarkerGoogleType.arrow);
            _currentMarker.IsHitTestVisible = false;

            _top.Markers.Add(_currentMarker);

        }
        private void btnAddMarker_Click(object sender, EventArgs e)
        {
            if(AddMoneyTextBox.Text == "" ||
                PeopleCountTextBox.Text == "" ||
                MinusMoneyTextBox.Text == "" ||
                StaffCountTextBox.Text == "")
            {
                MessageBox.Show("«аполните все пол€!");
                return;
            }
            var m = new GMarkerGoogle(_currentMarker.Position, GMarkerGoogleType.green_pushpin);
            var mBorders = new GMapMarkerRect(_currentMarker.Position);
            {
                mBorders.InnerMarker = m;
                if (_polygon != null)
                {
                    mBorders.Tag = _polygon.Points.Count;
                }
                mBorders.ToolTipMode = MarkerTooltipMode.Always;
            }

            Placemark? p = null;

            mBorders.ToolTipText = "ƒоход: " + AddMoneyTextBox.Text + "\n" +
                    "ѕосетители: " + PeopleCountTextBox.Text + "\n" +
                    "–асходы: " + MinusMoneyTextBox.Text + "\n" +
                    "ѕерсонал: " + StaffCountTextBox.Text;

            Objects.Markers.Add(m);
            Objects.Markers.Add(mBorders);

            RegeneratePolygon();
            objects.Add(new string[] { _currentMarker.Position.ToString(), AddMoneyTextBox.Text, PeopleCountTextBox.Text, MinusMoneyTextBox.Text, StaffCountTextBox.Text });
            AddMoneyTextBox.Clear();
            PeopleCountTextBox.Clear();
            MinusMoneyTextBox.Clear();
            StaffCountTextBox.Clear();
        }
        void RegeneratePolygon()
        {
            var polygonPoints = new List<PointLatLng>();

            foreach (var m in Objects.Markers)
            {
                if (m is GMapMarkerRect)
                {
                    m.Tag = polygonPoints.Count;
                    polygonPoints.Add(m.Position);
                }
            }

            if (_polygon == null)
            {
                _polygon = new GMapPolygon(polygonPoints, "polygon test");
                _polygon.IsHitTestVisible = true;
                Polygons.Polygons.Add(_polygon);
            }
            else
            {
                _polygon.Points.Clear();
                _polygon.Points.AddRange(polygonPoints);

                if (Polygons.Polygons.Count == 0)
                {
                    Polygons.Polygons.Add(_polygon);
                }
                else
                {
                    gMapControl1.UpdatePolygonLocalPosition(_polygon);
                }
            }
        }
        void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _isMouseDown)
            {
                if (_curentRectMarker == null)
                {
                    if (_currentMarker.IsVisible)
                    {
                        _currentMarker.Position = gMapControl1.FromLocalToLatLng(e.X, e.Y);
                    }
                }
                else // move rect marker
                {
                    var pnew = gMapControl1.FromLocalToLatLng(e.X, e.Y);

                    var pIndex = (int?)_curentRectMarker.Tag;
                    if (pIndex.HasValue)
                    {
                        if (pIndex < _polygon.Points.Count)
                        {
                            _polygon.Points[pIndex.Value] = pnew;
                            gMapControl1.UpdatePolygonLocalPosition(_polygon);
                        }
                    }

                    if (_currentMarker.IsVisible)
                    {
                        _currentMarker.Position = pnew;
                    }
                    _curentRectMarker.Position = pnew;

                    if (_curentRectMarker.InnerMarker != null)
                    {
                        _curentRectMarker.InnerMarker.Position = pnew;
                    }
                }

                gMapControl1.Refresh(); // force instant invalidation
            }
        }
        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;

                if (_currentMarker.IsVisible)
                {
                    _currentMarker.Position = gMapControl1.FromLocalToLatLng(e.X, e.Y);

                    var px = gMapControl1.MapProvider.Projection.FromLatLngToPixel(_currentMarker.Position.Lat, _currentMarker.Position.Lng, (int)gMapControl1.Zoom);
                    var tile = gMapControl1.MapProvider.Projection.FromPixelToTileXY(px);

                    Debug.WriteLine("MouseDown: geo: " + _currentMarker.Position + " | px: " + px + " | tile: " + tile);
                }
            }
        }
        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = false;
            }
        }
        void MainMap_OnMarkerLeave(GMapMarker item)
        {
            if (item is GMapMarkerRect)
            {
                _curentRectMarker = null;

                var rc = item as GMapMarkerRect;
                rc.Pen.Color = Color.Blue;

                Debug.WriteLine("OnMarkerLeave: " + item.Position);
            }
        }
        void MainMap_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMapMarkerRect)
            {
                var rc = item as GMapMarkerRect;
                rc.Pen.Color = Color.Red;

                _curentRectMarker = rc;
            }
            Debug.WriteLine("OnMarkerEnter: " + item.Position);
        }
        ITransformer Train(MLContext mlContext, string dataPath)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<Price>(dataPath, hasHeader: true, separatorChar: ',');
            var pipeline = mlContext.Transforms.CopyColumns(outputColumnName: "Label", inputColumnName: "Income")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "IdEncoded", inputColumnName: "Id"))
                .Append(mlContext.Transforms.Concatenate("Features", "IdEncoded", "Visitors"))
                .Append(mlContext.Regression.Trainers.FastTree());
            var model = pipeline.Fit(dataView);
            return model;
        }
        void Evaluate(MLContext mlContext, ITransformer model)
        {
            IDataView dataView = mlContext.Data.LoadFromTextFile<Price>(_trainDataPathReggresion, hasHeader: true, separatorChar: ',');
            var predictions = model.Transform(dataView);
            var metrics = mlContext.Regression.Evaluate(predictions, "Label", "Score");
            //Console.WriteLine();
            //Console.WriteLine($"*************************************************");
            //Console.WriteLine($"*       Model quality metrics evaluation         ");
            //Console.WriteLine($"*------------------------------------------------");
            //Console.WriteLine($"*       RSquared Score:      {metrics.RSquared}");
            //Console.WriteLine($"*       Root Mean Squared Error:      {metrics.RootMeanSquaredError:#.##}");
        }
        public float SinglePrediction(MLContext mlContext, ITransformer model, Price p, Price old)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<Price, PricePredition>(model);
            var prediction = predictionFunction.Predict(p);
            //Console.WriteLine($"**********************************************************************");
            //Console.WriteLine($"ѕрибыль в текущем мес€це: {old.Income:0.####}");
            //Console.WriteLine($" оличество покупателей: {p.Visitors:0.####}");
            //Console.WriteLine($"ѕрогнозируема€ прибыль в следующем мес€це: {prediction.Income:0.####}");
            //Console.WriteLine($"**********************************************************************");
            return prediction.Income;
        }

        private void BeginAnalize_Click(object sender, EventArgs e)
        {
            OutputForm op = new OutputForm(objects, mlContext, model1, predictor);
            op.Show();
        }
    }
}