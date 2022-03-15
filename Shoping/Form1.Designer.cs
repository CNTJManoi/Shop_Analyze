namespace Shoping
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.button1 = new System.Windows.Forms.Button();
            this.AddMoneyTextBox = new System.Windows.Forms.TextBox();
            this.PeopleCountTextBox = new System.Windows.Forms.TextBox();
            this.MinusMoneyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.StaffCountTextBox = new System.Windows.Forms.TextBox();
            this.BeginAnalize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(12, 12);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomEnabled = true;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(469, 426);
            this.gMapControl1.TabIndex = 0;
            this.gMapControl1.Zoom = 0D;
            this.gMapControl1.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.MainMap_OnMarkerEnter);
            this.gMapControl1.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.MainMap_OnMarkerLeave);
            this.gMapControl1.Load += new System.EventHandler(this.gMapControl1_Load);
            this.gMapControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseDown);
            this.gMapControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseMove);
            this.gMapControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainMap_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(550, 350);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 33);
            this.button1.TabIndex = 1;
            this.button1.Text = "Добавить метку";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAddMarker_Click);
            // 
            // AddMoneyTextBox
            // 
            this.AddMoneyTextBox.Location = new System.Drawing.Point(567, 72);
            this.AddMoneyTextBox.Name = "AddMoneyTextBox";
            this.AddMoneyTextBox.Size = new System.Drawing.Size(125, 27);
            this.AddMoneyTextBox.TabIndex = 2;
            // 
            // PeopleCountTextBox
            // 
            this.PeopleCountTextBox.Location = new System.Drawing.Point(567, 146);
            this.PeopleCountTextBox.Name = "PeopleCountTextBox";
            this.PeopleCountTextBox.Size = new System.Drawing.Size(125, 27);
            this.PeopleCountTextBox.TabIndex = 3;
            // 
            // MinusMoneyTextBox
            // 
            this.MinusMoneyTextBox.Location = new System.Drawing.Point(567, 220);
            this.MinusMoneyTextBox.Name = "MinusMoneyTextBox";
            this.MinusMoneyTextBox.Size = new System.Drawing.Size(125, 27);
            this.MinusMoneyTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(567, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Количество покупателей";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(567, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Доход";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(567, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Расходы";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(567, 268);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Количество рабочих";
            // 
            // StaffCountTextBox
            // 
            this.StaffCountTextBox.Location = new System.Drawing.Point(567, 291);
            this.StaffCountTextBox.Name = "StaffCountTextBox";
            this.StaffCountTextBox.Size = new System.Drawing.Size(125, 27);
            this.StaffCountTextBox.TabIndex = 8;
            // 
            // BeginAnalize
            // 
            this.BeginAnalize.Location = new System.Drawing.Point(550, 389);
            this.BeginAnalize.Name = "BeginAnalize";
            this.BeginAnalize.Size = new System.Drawing.Size(156, 33);
            this.BeginAnalize.TabIndex = 10;
            this.BeginAnalize.Text = "Начать анализ";
            this.BeginAnalize.UseVisualStyleBackColor = true;
            this.BeginAnalize.Click += new System.EventHandler(this.BeginAnalize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BeginAnalize);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.StaffCountTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MinusMoneyTextBox);
            this.Controls.Add(this.PeopleCountTextBox);
            this.Controls.Add(this.AddMoneyTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gMapControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private Button button1;
        private TextBox AddMoneyTextBox;
        private TextBox PeopleCountTextBox;
        private TextBox MinusMoneyTextBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox StaffCountTextBox;
        private Button BeginAnalize;
    }
}