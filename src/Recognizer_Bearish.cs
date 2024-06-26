using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_COP_4365
{
    //created a class called Recognizer_Engulfing_Bearish which inherits from its base abstract class, Recognizer
    internal class Recognizer_Bearish : Recognizer
    {
        //constructor  which inherits from the base class Recognizer
        public Recognizer_Bearish() : base("Bearish", 1)
        {

        }
        /// <summary>
        /// The method Recognize determines whether the smartcandlestick is a match for the pattern
        /// </summary>
        /// <param name="lscs">list of smartcandlesticks</param>
        /// <param name="index">index of smartcandlestick to analyze</param>
        /// <returns></returns>
        public override bool Recognize(List<SmartCandlestick> lscs, int index)
        {
            //get the candlestick at the specified index
            SmartCandlestick sc = lscs[index];
            //check if value for recognizer already exists in dictionary
            if (sc.Dictionary_Pattern.TryGetValue(patternName, out bool value))
            {
                //if it does exist, then we return value as we do not need to recalculate
                return value;
            }
            else
            {
                //retrieve recognizer bool value
                bool r = sc.close < sc.open;
                //add recognizer bool value to dictionary
                sc.Dictionary_Pattern.Add(patternName, r);
                return r;
            }
        }
    }
}
