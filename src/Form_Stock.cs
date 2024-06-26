using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsForms_COP_4365 //namespace
{
    /// <summary>
    /// creating class called Form_Stock which inherits from Form
    /// </summary>
    public partial class Form_Stock : Form 
    {
        //declares a list of candlesticks which will store all candlesticks of a ticker when reading a data file
        private List<SmartCandlestick> listOfSmartCandlesticks = null;
        //declares a list which will store the filteredcandlesticks after the filterCandlesticks() method is ran
        private List<SmartCandlestick> filteredSmartCandlesticks = null;
        //declares a binding list of candlesticks which will be used for data binding when working with the UI (data grid and chart), more specifically when the method displayCandlesticks() and normalize() are called
        private BindingList<SmartCandlestick> boundSmartCandlesticks = null;
        //declares a dictionary which will hold key value pairs consisting of recognizer names and the actual recongizer
        private Dictionary<string, Recognizer> Dictionary_Recognizer;
        //Highest total chart value
        private double chartMax;
        //Lowest total chart value
        private double chartMin;

        /// <summary>
        /// constructor for the Form_Stock class which initializes components and creates listofcandlesticks, a list of candlesticks with 1024 candlesticks as max capacity
        /// </summary>
        public Form_Stock() //constructor 
        {
            //initializes components using InitializeComponent method
            InitializeComponent();
            //initalizes all recognizers
            InitializeRecognizer();
            //1024 is the capacity which is the max amount of elements
            listOfSmartCandlesticks = new List<SmartCandlestick>(1024); 
        }
        /// <summary>
        /// constructor for all the child forms when multiple ones are selected
        /// </summary>
        /// <param name="stockPath">pathname for the child stock/param>
        /// <param name="start">start date value</param>
        /// <param name="end">end date value</param>
        public Form_Stock(string stockPath, DateTime start, DateTime end)
        {
            //initializes components using InitializeComponent method
            InitializeComponent();
            //initalizes all recognizers
            InitializeRecognizer();
            //1024 is the capacity which is the max amount of elements, this is done so that the child can have a new list to copy its values into
            listOfSmartCandlesticks = new List<SmartCandlestick>(1024);
            //calls default version of readcandlestickdatafromfile which takes no arguments but then calls the second version of readcandlestickdatafromfile passing arguments: openfiledialog_tickerchooser.filename
            listOfSmartCandlesticks = ReadCandlestickDataFromFile(stockPath);
            //copies listofcandlesticks into bindinglist called boundcandlesticks, this is done so that boundCandlesticks is empty when the child stock attempts to copy its data from loc into it
            boundSmartCandlesticks = new BindingList<SmartCandlestick>(listOfSmartCandlesticks);
            //sets earliest possible start date in the start date time picker according to earliest date in boundcandlesticks
            dateTimePicker_Start.MinDate = boundSmartCandlesticks.Min(cs => cs.date);
            //sets latest possible start date in the start date time picker according to last date in boundcandlesticks
            dateTimePicker_Start.MaxDate = boundSmartCandlesticks.Max(cs => cs.date);
            //sets the start datetimepicker to january 1, 2022 to make it easy for the ta to test
            dateTimePicker_Start.Value = new DateTime(2022, 1, 1);
            //sets earliest possible start date in the end date time picker according to earliest date in boundcandlesticks
            dateTimePicker_End.MinDate = boundSmartCandlesticks.Min(cs => cs.date);
            //sets latest possible end date in the end date time picker according to last date in boundcandlesticks
            dateTimePicker_End.MaxDate = boundSmartCandlesticks.Max(cs => cs.date);
            //sets  datetimepicker_end to last available candlestick
            dateTimePicker_End.Value = boundSmartCandlesticks.Max(cs => cs.date);
            //calls default version of filterCandlesticks which takes no arguments but then calls the second version of filtercandlesticks passing arguments: listOfCandlesticks, dateTimePicker_Start.Value, dateTimePicker_End.Value
            filterSmartCandlesticks();
            //calls default version of displaycandlesticks which takes no arguments but then calls the second version of displaycandlesticks passing arguments: boundecandlesticks
            displayCandlesticks();
        }
        /// <summary>
        /// click event for button_Loader which calls openfiledialog_tickerchooser so that the user can select a file containing stock data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Loader_Click(object sender, EventArgs e) //load button click event
        {
            //opens file dialog so user may choose a stock
            openFileDialog_TickerChooser.ShowDialog();
        }
        /// <summary>
        /// event for when a user selects a file or multiple files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileDialog_TickerChooser_FileOk(object sender, CancelEventArgs e) //openfiledialog filkeok event
        {
            //gets the date of the start date time picker and sets it to startDate
            DateTime startDate = dateTimePicker_Start.Value;
            // gets the date of the end date time picker and sets it to endDate
            DateTime endDate = dateTimePicker_End.Value;
            //calculates the number of files selected by the user using Count
            int numberOfFiles = openFileDialog_TickerChooser.FileNames.Count();
            //loops as many times as their are files selected
            for(int i = 0; i < numberOfFiles; ++i)
            {
                //sets the pathname to the current stock from the ones selected
                string pathName = openFileDialog_TickerChooser.FileNames[i];
                //get its path without extension
                string ticker = Path.GetFileNameWithoutExtension(pathName);
                //variable which holds reference to Form_Stock
                Form_Stock form_Stock;
                //if this is the first file use the current instance of Form_Stock
                if (i == 0)
                {
                    //current instance
                    form_Stock = this;
                    //calls readanddisplay which reads, filters, and displays the data
                    readAndDisplayStock();
                    //title of form is set to Parent + ticker
                    form_Stock.Text = "Parent: " + ticker;
                }
                //if not first file being read
                else
                {
                    //create a new instance of Form_Stock
                    form_Stock = new Form_Stock(pathName, startDate, endDate);
                    //set title of form to Child + ticker
                    form_Stock.Text = "Child: " + ticker;
                }
                //show the form
                form_Stock.Show();
                //bring the form to the front
                form_Stock.BringToFront();
            }
        }
        /// <summary>
        /// method with no parameters which reads, filters, and displays a stocks data
        /// </summary>
        private void readAndDisplayStock()
        {
            //calls default version of readcandlestickdatafromfile which takes no arguments but then calls the second version of readcandlestickdatafromfile passing arguments: openFileDialog_TickerChooser.FileName
            ReadCandlestickDataFromFile();
            //calls default version of filterCandlesticks which takes no arguments but then calls the second version of filtercandlesticks passing arguments: listOfCandlesticks, dateTimePicker_Start.Value, dateTimePicker_End.Value
            filterSmartCandlesticks();
            //calls default version of displaycandlesticks which takes no arguments but then calls the second version of displaycandlesticks passing arguments: boundecandlesticks
            displayCandlesticks();
        }
        /// <summary>
        /// second version of readcandlestickdatafromfile which takes in the filename so that it can read the data from that file into list called candleList, candleList is returned and listofcandlesticks is set to it
        /// </summary>
        /// <param name="filename">name of file which user selects to read data from</param>
        /// <returns></returns>
        private List<SmartCandlestick> ReadCandlestickDataFromFile(string filename) //method to read data from file
        {
            //retrieves filename that user selects
            this.Text = Path.GetFileName(filename);
            //initializes a new candlestick list which we need as this method returns a list
            List<SmartCandlestick> candleList = new List<SmartCandlestick>(1024);
            //reference string which is what the data format will look like when reading from the file
            const string referenceString = "Date,Open,High,Low,Close,Adj Close,Volume";
            //pass file path and file name to StreamReader constructor so file can be read
            using (StreamReader sr = new StreamReader(filename))
            {
                //reads header line
                string line = sr.ReadLine();
                //checks that header matches reference string
                if (line == referenceString)
                {
                    //loops through each line until end of file
                    while ((line = sr.ReadLine()) != null)
                    {
                        //instantiates a smart candlestick from a candlestick and its properties
                        SmartCandlestick scs = new SmartCandlestick(line);
                        //adds a smart candlestick object to list
                        candleList.Add(scs);
                    }
                }
                else
                {
                    //returns this text if the current line being read does not match the reference string
                    Text = "Bad File" + filename;
                }
            }
            //iterates through all recognizers in the recognizer dictionary
            foreach (Recognizer r in Dictionary_Recognizer.Values)
            {
                //Adds dictionary entries for every pattern (recognizer) on every candlestick using Recognize_All
                r.Recognize_All(candleList);
            }

            //returns candleList which has all smart candlestick data points from the file
            return candleList;
        }
        /// <summary>
        /// default version of readcandlestickdatafromfile which has no parameters
        /// </summary>
        private void ReadCandlestickDataFromFile()
        {
            //copies candleList which is returned from readcandlestickdatafromfile into listofcandlesticks
            listOfSmartCandlesticks = ReadCandlestickDataFromFile(openFileDialog_TickerChooser.FileName);
            //copies listofcandlesticks into bindinglist called boundcandlesticks
            boundSmartCandlesticks = new BindingList<SmartCandlestick>(listOfSmartCandlesticks);
            //sets earliest possible start date in the start date time picker according to earliest date in boundcandlesticks
            dateTimePicker_Start.MinDate = boundSmartCandlesticks.Min(cs => cs.date);
            //sets latest possible start date in the start date time picker according to last date in boundcandlesticks
            dateTimePicker_Start.MaxDate = boundSmartCandlesticks.Max(cs => cs.date);
            //sets the start datetimepicker to january 1, 2022 to make it easy for the ta to test
            dateTimePicker_Start.Value = new DateTime(2022, 1, 1);
            //sets earliest possible start date in the end date time picker according to earliest date in boundcandlesticks
            dateTimePicker_End.MinDate = boundSmartCandlesticks.Min(cs => cs.date);
            //sets latest possible end date in the end date time picker according to last date in boundcandlesticks
            dateTimePicker_End.MaxDate = boundSmartCandlesticks.Max(cs => cs.date);
            //sets  datetimepicker_end to last available candlestick
            dateTimePicker_End.Value = boundSmartCandlesticks.Max(cs => cs.date);
        }
        /// <summary>
        /// click event for button_Update which calls filtercandlesticks based on datetimepickers values, then displays filteredcandlesticks in both data grid and chart, then normalizes the chart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Update_Click(object sender, EventArgs e) //update button click event
        {
            if ((listOfSmartCandlesticks.Count != 0) & (dateTimePicker_Start.Value <= dateTimePicker_End.Value))
            {
                //calls default version of filterCandlesticks which takes no arguments but then calls the second version of filtercandlesticks passing arguments: listOfCandlesticks, dateTimePicker_Start.Value, dateTimePicker_End.Value
                filterSmartCandlesticks();
                //calls default version of displaycandlesticks which takes no arguments but then calls the second version of displaycandlesticks passing arguments: boundecandlesticks
                displayCandlesticks();
            }
        }
        /// <summary>
        /// second version of filtercandlesticsk which filters the list of candlesticks based on the values of the date time pickers
        /// </summary>
        /// <param name="loc"> loc is the list which represents listofcandlesticks as that is the first argument passed</param>
        /// <param name="startDate"> startDate is the value of the start date time picker</param>
        /// <param name="endDate"> endDate is the value of the end date time picker</param>
        /// <returns></returns>
        private List<SmartCandlestick> filterSmartCandlesticks(List<SmartCandlestick> loc, DateTime startDate, DateTime endDate)
        {
            //initializes filteredList as a candlestick list with capacity equal to amount that loc.count holds which is how many candlesticks listofcandlesticks has
            List<SmartCandlestick> filteredList = new List<SmartCandlestick>(loc.Count);
            //checks each candlestick in loc which references listofcandlesticks
            foreach (SmartCandlestick cs in loc)
            {
                //checks if current candlesticks date is before startDate and continues to the next candlestick if so because then we need to filter it out
                if (startDate > cs.date)
                    continue;
                //checks if current candlesticks date is after endDate and breaks loop since it and the following candlesticks will also be after endDate, which means they all need to be filtered out
                if (cs.date > endDate)
                    break;
                //if candlestick is between startDate and endDate then we add it to the filteredList
                filteredList.Add(cs);
            }
            //returns filteredList containing candlesticks within specified date range
            return filteredList;
        }
        /// <summary>
        /// default version of filtercandlesticks which has no parameters
        /// </summary>
        private void filterSmartCandlesticks()
        {
            //calls second version of filtercandlesticks with proper parameters and copies the list returned into filteredcandlesticks
            filteredSmartCandlesticks = filterSmartCandlesticks(listOfSmartCandlesticks, dateTimePicker_Start.Value, dateTimePicker_End.Value);
            //copies filteredlist into binding list boundcandlesticks
            boundSmartCandlesticks = new BindingList<SmartCandlestick>(filteredSmartCandlesticks);
            //calls default version of updatecomboxbox which updates the combo box with all the available patterns
            updateComboxBox();
        }
        /// <summary>
        /// second version of displaycandlesticks which uses boundList which references boundcandlesticks to display proper data in data grid and chart
        /// </summary>
        /// <param name="boundList"> boundList references boundcandlesticks as that is the argument that is passed when the default version is called</param>
        private void displayCandlesticks(BindingList<SmartCandlestick> boundList)
        {
            chart_OHLCV.Annotations.Clear();
            //sets the data source of the chart to boundlist (boundcandlesticks) to display proper data
            chart_OHLCV.DataSource = boundList;
            // DataBind() is called to refresh the data displayed in the chart  based on the new data source boundlist (boundcandlesticks)
            chart_OHLCV.DataBind();
            //calls default version of normalize which takes no arguments but then calls the second version of normalize passing arguments: boundecandlesticks
            normalize();
        }
        /// <summary>
        /// default version of displaycandlesticks which has no parameters
        /// </summary>
        private void displayCandlesticks()
        {
            //calls displaycandlesticks with the binding list boundcandlesticks to show data in data grid and chart
            displayCandlesticks(boundSmartCandlesticks);
        }
        /// <summary>
        /// second version of normalize which normalizes the y axis of chart OHLC
        /// </summary>
        /// <param name="boundList"> boundlist references boundcandlesticks which is the binding list argument that is passed when the default version is called</param>
        private void normalize(BindingList<SmartCandlestick> boundList)
        {
            //finds the lowest y axis value by going through boundList's (boundcandlesticks) low values and returning the minimum
            decimal min = boundList.Min(cs => cs.low);
            //finds the max y axis value by going through boundList's (boundcandlesticks) high values and returning the maximum
            decimal max = boundList.Max(cs => cs.high);
            //sets the OHLC charts y axis minimum to the equation which basically multiples min by .98, 2% less, and then floors (rounds down) it so that we can normalize that
            chartMin = chart_OHLCV.ChartAreas[0].AxisY.Minimum = Math.Floor(Decimal.ToDouble(min) * 0.95);
            //sets the OHLC charts y axis maximum to the equation which basically multiples min by 1.02, 2% more, and then ceils (rounds up) it so that we can normalize that
            chartMax = chart_OHLCV.ChartAreas[0].AxisY.Maximum = Math.Ceiling(Decimal.ToDouble(max) * 1.05);
        }
        /// <summary>
        /// default version of normalize which calls second version of normalize with proper argument boundcandlesticks as that is the binding list with the proper data
        /// </summary>
        private void normalize()
        {
            //calls the second version of normalize with the argument boundcandlesticks as that is the binding list with the updated data
            normalize(boundSmartCandlesticks);
        }
        /// <summary>
        /// second version of update combo box which updates items displayed in combo box
        /// </summary>
        /// <param name="bindList">bindList or boundcandlesticks is passed to make sure we have smartcandlesticks</param>
        private void updateComboBox(BindingList<SmartCandlestick> bindList)
        {
            //checks if bindList contains any smartcandlesticks
            if (bindList.Count != 0)
            {
                //clears existing items in combo box
                comboBox_Patterns.Items.Clear();
                //iterate through keys of the dictionary and add recognizers to combox box
                foreach (string key in Dictionary_Recognizer.Keys)
                {
                    //add each key as an item to the combo box
                    comboBox_Patterns.Items.Add(key);
                }
            }
        }
        /// <summary>
        /// default version of update combo box which calls the second version with an argument of boundcandlesticks
        /// </summary>
        private void updateComboxBox()
        {
            //calls the second version of update combo box with passing as an argument the binding list bound candlesticks
            updateComboBox(boundSmartCandlesticks);
        }

        /// <summary>
        /// event handler for when a pattern is selected in the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_Patterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clears all previous annotations on chart
            chart_OHLCV.Annotations.Clear();
            //check if smartcandlesticks exist within boundSmartCandlesticks
            if (boundSmartCandlesticks != null)
            {
                //iterate through all smart candlestick objects in bound candlesticks
                for (int i = 0; i < boundSmartCandlesticks.Count; i++)
                {
                    //retrieve the smart candlestick object at the current index
                    SmartCandlestick scs = (SmartCandlestick)boundSmartCandlesticks[i];
                    //retrieve corresponding datapoint from the chart
                    DataPoint dataPoint = chart_OHLCV.Series[0].Points[i];
                    //get the selected pattern from the combo box
                    string selected = comboBox_Patterns.SelectedItem.ToString();
                    //check if smartcandlestick has the selected pattern
                    if (scs.Dictionary_Pattern[selected])
                    {
                        //store length of pattern
                        int length = Dictionary_Recognizer[selected].patternLength;
                        //annotation specific for multi-smartcandlestick patterns
                        if (length >= 2)
                        {
                            //skips indexes in the list that are out of bounds
                            if (i == 0 | ((i == boundSmartCandlesticks.Count() - 1) & length == 3))
                            {
                                continue;
                            }
                            //initialize rectangle annotation
                            RectangleAnnotation rectangle = new RectangleAnnotation();
                            //set anchor point of rectangle annotation to the datapoint in chart
                            rectangle.SetAnchor(dataPoint);
                            double Ymax, Ymin;
                            //find width based on number of candlesticks
                            double width = (90.0 / boundSmartCandlesticks.Count()) * length;
                            //for 2 candlestick patterns
                            if (length == 2)
                            {
                                //calculate max based on current and previous candlesticks
                                Ymax = (int)(Math.Max(scs.high, boundSmartCandlesticks[i - 1].high));
                                //calculate min based on current and previous candlesticks
                                Ymin = (int)(Math.Min(scs.low, boundSmartCandlesticks[i - 1].low));
                                //calculate anchor offset to position rectangle annotation
                                rectangle.AnchorOffsetX = ((width / length) / 2 - 0.25) * (-1);
                            }
                            //for 3 candlestick patterns
                            else
                            {
                                //calculate max based on current, previous, and next candlesticks
                                Ymax = (int)(Math.Max(scs.high, Math.Max(boundSmartCandlesticks[i + 1].high, boundSmartCandlesticks[i - 1].high)));
                                //calculate min based on current, previous, and next candlesticks
                                Ymin = (int)(Math.Min(scs.low, Math.Min(boundSmartCandlesticks[i + 1].low, boundSmartCandlesticks[i - 1].low)));
                            }
                            //calculate height of rectangle
                            double height = 40.0 * (Ymax - Ymin) / (chartMax - chartMin);
                            //set height
                            rectangle.Height = height;
                            //set width
                            rectangle.Width = width;
                            //set y position
                            rectangle.Y = Ymax;
                            //set backcolor to transparent to see chart
                            rectangle.BackColor = Color.Transparent;     
                            rectangle.LineWidth = 2;                                        
                            rectangle.LineDashStyle = ChartDashStyle.Dash;                  
                            //add annotation to chart
                            chart_OHLCV.Annotations.Add(rectangle);
                        }
                        //create a new arrow annotation
                        ArrowAnnotation arrow = new ArrowAnnotation();
                        //set x axis of arrow annotation to match in chart
                        arrow.AxisX = chart_OHLCV.ChartAreas[0].AxisX;
                        //set x axis of arrow annotation to match in chart
                        arrow.AxisY = chart_OHLCV.ChartAreas[0].AxisY;
                        //set width of arrow
                        arrow.Width = 0;
                        //set height of arrow
                        arrow.Height = -4;
                        //check if pattern selected in combo box is true in this smart candlestick
                        if (scs.Dictionary_Pattern[comboBox_Patterns.SelectedItem.ToString()])
                        {
                            //set anchor point of arrow annotation to the datapoint in chart
                            arrow.SetAnchor(dataPoint);
                            //add the arrow annotation to the chart
                            chart_OHLCV.Annotations.Add(arrow);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// method which initalizes all recognizers
        /// </summary>
        private void InitializeRecognizer()
        {
            //instantiates a new dictionary of type <string, Recognizer> and assigns it to Dictionary_Recognizer
            Dictionary_Recognizer = new Dictionary<string, Recognizer>();
            //Bullish Recognizer
            Recognizer r = new Recognizer_Bullish();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Bearish Recognizer
            r = new Recognizer_Bearish();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Neutral Recognizer
            r = new Recognizer_Neutral();
            Dictionary_Recognizer.Add(r.patternName, r);
            //Marubozu Recognizer
            r = new Recognizer_Marubozu();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Hammer Recognizer
            r = new Recognizer_Hammer();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Doji Recognizer
            r = new Recognizer_Doji();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Dragonfly Doji Recognizer
            r = new Recognizer_DragonflyDoji();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Gravestone Doji Recognizer
            r = new Recognizer_GravestoneDoji();
            Dictionary_Recognizer.Add(r.patternName, r);
            //Harami Bullish Recognizer
            r = new Recognizer_Harami_Bullish();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Harami Bearish Recognizer
            r = new Recognizer_Harami_Bearish();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Engulfing Bullish Recognizer
            r = new Recognizer_Engulfing_Bullish();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Engulfing Bearish Recognizer
            r = new Recognizer_Engulfing_Bearish();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Peak Recognizer
            r = new Recognizer_Peak();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
            //Valley Recognizer
            r = new Recognizer_Valley();
            //adds recognizer to dictionary
            Dictionary_Recognizer.Add(r.patternName, r);
        }
    }
}