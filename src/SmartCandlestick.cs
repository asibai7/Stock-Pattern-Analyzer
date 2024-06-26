using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_COP_4365
{
    //created a class called SmartCandlestick which inherits from its base class, Candlestick
    public class SmartCandlestick : Candlestick
    {
        //decimal variable to store range value
        public decimal range { get; set; }

        //decimal variable to store top price value
        public decimal topPrice { get; set; }

        //decimal variable to store bottom price value
        public decimal bottomPrice { get; set; }

        //decimal variable to store body range value
        public decimal bodyRange { get; set; }

        //decimal variable to store upper tail value
        public decimal upperTail { get; set; }

        //decimal variable to store lower tail value
        public decimal lowerTail { get; set; }

        //dictionary to store patterns and boolean for each as to whether or not that smartcandlestick is a T or F for that pattern
        public Dictionary<string, bool> Dictionary_Pattern = new Dictionary<string, bool>();
        /// <summary>
        /// SmartCandlestick constructor which creates a smartcandlestick from a csv line
        /// </summary>
        /// <param name="csvLine"></param>
        public SmartCandlestick(string csvLine) : base(csvLine)
        {
            //calls method to compute SmartCandlestick properties
            computeExtraProperties();
            //calls method to compute all single patterns for the smartcandlestick
            computePatternProperties();
        }

        /// <summary>
        /// SmartCandlestick constructor which takes in a Candlestick along with its properties and then creates a SmartCandlestick with more properties
        /// </summary>
        /// <param name="cs"></param>
        public SmartCandlestick(Candlestick cs)
        {
            //setting the SmartCandlesticks open value to the open value of the Candlestick object cs
            open = cs.open;
            //setting the SmartCandlesticks open value to the high value of the Candlestick object cs
            high = cs.high;
            //setting the SmartCandlesticks open value to the low value of the Candlestick object cs
            low = cs.low;
            //setting the SmartCandlesticks open value to the close value of the Candlestick object cs
            close = cs.close;
            //setting the SmartCandlesticks open value to the volume value of the Candlestick object cs
            volume = cs.volume;
            //setting the SmartCandlesticks open value to the date value of the Candlestick object cs
            date = cs.date;
            //calls compute extra properties method to calculate smartcandlestick properties
            computeExtraProperties();
            //calls compute pattern properties to see which patterns are seen in the smart candlestick
            computePatternProperties();
        }
        /// <summary>
        /// method to compute a smart candlesticks properties using properties of a regular candlestick
        /// </summary>
        public void computeExtraProperties()
        {
            //calculates the range for the smart candlestick
            range = high - low;
            //calculates the top price for the smart candlestick
            topPrice = Math.Max(open, close);
            //calculates the bottom price for the smart candlestick
            bottomPrice = Math.Min(open, close);
            //calculates the body range for the smart candlestick
            bodyRange = topPrice - bottomPrice;
            //calculates the upper tail for the smart candlestick
            upperTail = high - topPrice;
            //calculates the lower tail for the smart candlestick
            lowerTail = bottomPrice - low;
        }
        /// <summary>
        /// method to compute a smart candlesticks pattern using the definition of a pattern and a dictionary to store the name of those patterns in addition to a bool value which refers to whether or not that smart candlestick shows that pattern
        /// </summary>
        public void computePatternProperties()
        {
            //instantialized boolean definition of bullish so that we can detect whether or not a smartcandlestick is one or not
            bool bullish = close > open;
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Bullish", bullish);
            //instantialized boolean definition of bearish so that we can detect whether or not a smartcandlestick is one or not
            bool bearish = open > close;
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Bearish", bearish);
            //instantialized boolean definition of neutral so that we can detect whether or not a smartcandlestick is one or not
            bool neutral = bodyRange < (range * 0.1m); //you can also be more specific in using this: open == close;
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Neutral", neutral);
            //instantialized boolean definition of marubozu so that we can detect whether or not a smartcandlestick is one or not
            bool marubozu = bodyRange >= (range * 0.85m);
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Marubozu", marubozu);
            //instantialized boolean definition of hammer so that we can detect whether or not a smartcandlestick is one or not
            bool hammer = (bodyRange < range * 0.3m) & (lowerTail > range * 0.6m);
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Hammer", hammer);
            //instantialized boolean definition of doji so that we can detect whether or not a smartcandlestick is one or not
            bool doji = bodyRange <= (range * .05m);
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Doji", doji);
            //instantialized boolean definition of dragonfly doji so that we can detect whether or not a smartcandlestick is one or not
            bool dragonfly_Doji = doji & (lowerTail > range * 0.7m);
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Dragonfly Doji", dragonfly_Doji);
            //instantialized boolean definition of gravestone doji so that we can detect whether or not a smartcandlestick is one or not
            bool gravestone_Doji = doji & (upperTail > range * 0.7m);
            //added pattern and boolean value as key value pair to the dictionary
            Dictionary_Pattern.Add("Gravestone Doji", gravestone_Doji);
        }
    }
}