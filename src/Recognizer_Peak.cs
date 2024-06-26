using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_COP_4365
{
    //created a class called Recognizer_Valley which inherits from its base abstract class, Recognizer
    internal class Recognizer_Peak : Recognizer
    {
        //constructor  which inherits from the base class Recognizer
        public Recognizer_Peak() : base("Peak", 3)
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
            SmartCandlestick peak = lscs[index];
            //check if value for recognizer already exists in dictionary
            if (peak.Dictionary_Pattern.TryGetValue(patternName, out bool value))
            {
                //if it does exist, then we return value as we do not need to recalculate
                return value;
            }
            else
            {
                //retrieve offset
                int offset = patternLength / 2;
                //check if not in bounds
                if ((index < offset) | (index == lscs.Count() - offset))
                {
                    //if not in bounds then mark false in dictionary
                    peak.Dictionary_Pattern.Add(patternName, false);
                    return false;
                }
                else
                {
                    //retrieve previous candlestick so we can see if the recognizer properties match
                    SmartCandlestick previous = lscs[index - 1];
                    //retrieve next candlestick so we can see if the recognizer properties match
                    SmartCandlestick next = lscs[index + 1];
                    //retrieve recognizer bool value
                    bool r = previous.high < peak.high & peak.high > next.high;
                    //add recognizer bool to dictionary
                    peak.Dictionary_Pattern.Add(patternName, r);
                    return r;
                }
            }
        }
    }
}