namespace WindowsForms_COP_4365
{
    partial class Form_Stock
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button_Load = new System.Windows.Forms.Button();
            this.openFileDialog_TickerChooser = new System.Windows.Forms.OpenFileDialog();
            this.chart_OHLCV = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dateTimePicker_Start = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_End = new System.Windows.Forms.DateTimePicker();
            this.textBox_Start = new System.Windows.Forms.TextBox();
            this.textBox_End = new System.Windows.Forms.TextBox();
            this.button_Update = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_Patterns = new System.Windows.Forms.ComboBox();
            this.candlestickBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Load
            // 
            this.button_Load.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Load.Location = new System.Drawing.Point(3, 3);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(106, 26);
            this.button_Load.TabIndex = 0;
            this.button_Load.Text = "Load a ticker:";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Loader_Click);
            // 
            // openFileDialog_TickerChooser
            // 
            this.openFileDialog_TickerChooser.FileName = "openFileDialog_TickerChooser";
            this.openFileDialog_TickerChooser.Filter = "All Files|*.csv|Monthly|*-Month.csv|Weekly|*-Week.csv|Daily|*-Day.csv";
            this.openFileDialog_TickerChooser.FilterIndex = 2;
            this.openFileDialog_TickerChooser.InitialDirectory = "C:\\Users\\Ahmad\\Desktop\\Spring 2024\\Software\\Stock Data";
            this.openFileDialog_TickerChooser.Multiselect = true;
            this.openFileDialog_TickerChooser.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_TickerChooser_FileOk);
            // 
            // chart_OHLCV
            // 
            chartArea1.Name = "ChartArea_OHLC";
            chartArea2.AlignWithChartArea = "ChartArea_OHLC";
            chartArea2.Name = "ChartArea_Volume";
            this.chart_OHLCV.ChartAreas.Add(chartArea1);
            this.chart_OHLCV.ChartAreas.Add(chartArea2);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart_OHLCV.Legends.Add(legend1);
            this.chart_OHLCV.Location = new System.Drawing.Point(4, 39);
            this.chart_OHLCV.Name = "chart_OHLCV";
            series1.ChartArea = "ChartArea_OHLC";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.CustomProperties = "PriceDownColor=Red, PriceUpColor=Lime";
            series1.IsXValueIndexed = true;
            series1.Legend = "Legend1";
            series1.Name = "Series_OHLC";
            series1.XValueMember = "date";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueMembers = "high,low,open,close";
            series1.YValuesPerPoint = 4;
            series2.ChartArea = "ChartArea_Volume";
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Series_Volume";
            series2.XValueMember = "date";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueMembers = "volume";
            this.chart_OHLCV.Series.Add(series1);
            this.chart_OHLCV.Series.Add(series2);
            this.chart_OHLCV.Size = new System.Drawing.Size(1119, 621);
            this.chart_OHLCV.TabIndex = 2;
            this.chart_OHLCV.Text = "chart_OHLCV";
            // 
            // dateTimePicker_Start
            // 
            this.dateTimePicker_Start.Location = new System.Drawing.Point(231, 5);
            this.dateTimePicker_Start.Name = "dateTimePicker_Start";
            this.dateTimePicker_Start.Size = new System.Drawing.Size(248, 22);
            this.dateTimePicker_Start.TabIndex = 3;
            // 
            // dateTimePicker_End
            // 
            this.dateTimePicker_End.Location = new System.Drawing.Point(584, 6);
            this.dateTimePicker_End.Name = "dateTimePicker_End";
            this.dateTimePicker_End.Size = new System.Drawing.Size(248, 22);
            this.dateTimePicker_End.TabIndex = 4;
            // 
            // textBox_Start
            // 
            this.textBox_Start.Location = new System.Drawing.Point(153, 5);
            this.textBox_Start.Name = "textBox_Start";
            this.textBox_Start.Size = new System.Drawing.Size(72, 22);
            this.textBox_Start.TabIndex = 5;
            this.textBox_Start.Text = "Start Date:";
            // 
            // textBox_End
            // 
            this.textBox_End.Location = new System.Drawing.Point(508, 5);
            this.textBox_End.Name = "textBox_End";
            this.textBox_End.Size = new System.Drawing.Size(70, 22);
            this.textBox_End.TabIndex = 6;
            this.textBox_End.Text = "End Date:";
            // 
            // button_Update
            // 
            this.button_Update.Location = new System.Drawing.Point(851, 0);
            this.button_Update.Name = "button_Update";
            this.button_Update.Size = new System.Drawing.Size(78, 29);
            this.button_Update.TabIndex = 7;
            this.button_Update.Text = "Update";
            this.button_Update.UseVisualStyleBackColor = true;
            this.button_Update.Click += new System.EventHandler(this.button_Update_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox_Patterns);
            this.panel1.Controls.Add(this.button_Load);
            this.panel1.Controls.Add(this.button_Update);
            this.panel1.Controls.Add(this.textBox_Start);
            this.panel1.Controls.Add(this.dateTimePicker_End);
            this.panel1.Controls.Add(this.textBox_End);
            this.panel1.Controls.Add(this.dateTimePicker_Start);
            this.panel1.Location = new System.Drawing.Point(1, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1103, 31);
            this.panel1.TabIndex = 8;
            // 
            // comboBox_Patterns
            // 
            this.comboBox_Patterns.FormattingEnabled = true;
            this.comboBox_Patterns.Location = new System.Drawing.Point(965, 3);
            this.comboBox_Patterns.Name = "comboBox_Patterns";
            this.comboBox_Patterns.Size = new System.Drawing.Size(131, 24);
            this.comboBox_Patterns.TabIndex = 9;
            this.comboBox_Patterns.Text = "Show a Pattern:";
            this.comboBox_Patterns.SelectedIndexChanged += new System.EventHandler(this.comboBox_Patterns_SelectedIndexChanged);
            // 
            // candlestickBindingSource
            // 
            //this.candlestickBindingSource.DataSource = typeof(WindowsForms_COP_4365.SmartCandlestick);
            // 
            // Form_Stock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1109, 661);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart_OHLCV);
            this.Name = "Form_Stock";
            this.Text = "Form_Stock";
            ((System.ComponentModel.ISupportInitialize)(this.chart_OHLCV)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.candlestickBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.OpenFileDialog openFileDialog_TickerChooser;
        private System.Windows.Forms.BindingSource candlestickBindingSource;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_OHLCV;
        private System.Windows.Forms.DateTimePicker dateTimePicker_Start;
        private System.Windows.Forms.DateTimePicker dateTimePicker_End;
        private System.Windows.Forms.TextBox textBox_Start;
        private System.Windows.Forms.TextBox textBox_End;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox_Patterns;
    }
}