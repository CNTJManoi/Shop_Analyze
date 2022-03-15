using Microsoft.ML;
using Shoping.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shoping
{
    public partial class OutputForm : Form
    {
        public OutputForm(List<string[]> Data, MLContext mlContext, ITransformer model, PredictionEngine<Shop, ClusterPrediction> predictor)
        {
            InitializeComponent();
            int id = 0;
            //0 - координаты, 1 - доход, 2 - Посетители, 3 - расходы, 4 - персонал
            foreach (var lineSub in Data)
            {
                var profit = SinglePrediction(mlContext, model, new Price { Id = id.ToString(), Income = 0, Visitors = float.Parse(lineSub[2]) });
                var prediction = predictor.Predict(new Shop { profit = profit, visitors = float.Parse(lineSub[2]), expenses = float.Parse(lineSub[3]), staff = float.Parse(lineSub[4]) });
                if(prediction.PredictedClusterId == 1)
                {
                    listBox1.Items.Add("Координаты: " + lineSub[0] + ", Доход: " + lineSub[1] +
                        " Прогнозируемый доход: " + profit + ", Посетители: " + lineSub[2] + ", Расходы: " + lineSub[3] +
                        ", Персонал: " + lineSub[4]);
                }
                else
                {
                    listBox2.Items.Add("Координаты: " + lineSub[0] + ", Доход: " + lineSub[1] +
                        " Прогнозируемый доход: " + profit + ", Посетители: " + lineSub[2] + ", Расходы: " + lineSub[3] +
                        ", Персонал: " + lineSub[4]);
                }
                id++;
            }
        }
        public float SinglePrediction(MLContext mlContext, ITransformer model, Price p)
        {
            var predictionFunction = mlContext.Model.CreatePredictionEngine<Price, PricePredition>(model);
            var prediction = predictionFunction.Predict(p);
            //Console.WriteLine($"**********************************************************************");
            //Console.WriteLine($"Прибыль в текущем месяце: {old.Income:0.####}");
            //Console.WriteLine($"Количество покупателей: {p.Visitors:0.####}");
            //Console.WriteLine($"Прогнозируемая прибыль в следующем месяце: {prediction.Income:0.####}");
            //Console.WriteLine($"**********************************************************************");
            return prediction.Income;
        }
    }
}
