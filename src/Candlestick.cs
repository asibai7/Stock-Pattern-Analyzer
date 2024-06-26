using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_COP_4365
{
    //creating class called Candlestick to store a Candlestick's properties using getter and setter
    public class Candlestick
    {
        //decimal variable to store open value
        public decimal open { get; set; }

        //decimal variable to store high value
        public decimal high { get; set; }

        //decimal variable to store low value
        public decimal low { get; set; }

        //decimal variable to store close value
        public decimal close { get; set; }

        //decimal variable to store adjusted close value
        public decimal adjclose { get; set; }

        //unsigned int variable to store volume value
        public ulong volume { get; set; }

        //datetime variable to store date value
        public DateTime date { get; set; }

        /// <summary>
        /// constructor for candlestick class that takes in no parameters so that we can create a candlestick without having to supply initial values for its properties
        /// </summary>
        public Candlestick()
        {

        }

        /// <summary>
        /// constructor for Candlestick class which creates a candlestick from a string of data
        /// </summary>
        /// <param name="rowOfData">A string containing comma-separated values representing candlestick properties in the following order: date, open, high, low, close, adjusted close, volume</param>
        public Candlestick(string rowOfData) 
        {
            //separator which splits based on comma, space, and double quotes
            char[] separators = new char[] {',', ' ', '"'};
            //splits input string into substrings based on separators
            string[] subs = rowOfData.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //gets date string from first element of array
            string dateString = subs[0];
            //parses the date using DateTime class
            date = DateTime.Parse(dateString);

            //initialize empty variable temp of type decimal
            decimal temp;
            //uses tryparse to check if value from second element matches expected input type
            bool success = decimal.TryParse(subs[1], out temp);
            //if tryparse is successful, value is stored in open variable
            if (success) open = temp;

            //uses tryparse to check if value from second element matches expected input type
            success = decimal.TryParse(subs[2], out temp);
            //if tryparse is successful, value is stored in high variable
            if (success) high = temp;

            //uses tryparse to check if value from second element matches expected input type
            success = decimal.TryParse(subs[3], out temp);
            //if tryparse is successful, value is stored in low variable
            if (success) low = temp;

            //uses tryparse to check if value from second element matches expected input type
            success = decimal.TryParse(subs[4], out temp);
            //if tryparse is successful, value is stored in close variable
            if (success) close = temp;

            //uses tryparse to check if value from second element matches expected input type
            success = decimal.TryParse(subs[5], out temp);
            //if tryparse is successful, value is stored in adjusted close variable
            if (success) adjclose = temp;

            //initialize empty variable tempVolume of type ulong
            ulong tempVolume;
            //uses tryparse to check if value from second element matches expected input type
            success = ulong.TryParse(subs[6], out tempVolume);
            //if tryparse is successful, value is stored in volume variable
            if (success) volume = tempVolume; 
        }
    }
}
