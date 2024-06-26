using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms_COP_4365
{
    //created a class called Recognizer_Engulfing_Bearish which inherits from its base abstract class, Recognizer
    internal class Recognizer_Harami_Bearish : Recognizer
    {
        //constructor  which inherits from the base class Recognizer
        public Recognizer_Harami_Bearish() : base("Harami Bearish", 2)
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
            SmartCandlestick harami = lscs[index];
            //check if value for recognizer already exists in dictionary
            if (harami.Dictionary_Pattern.TryGetValue(patternName, out bool value))
            {
                //if it does exist, then we return value as we do not need to recalculate
                return value;
            }
            //if not in dictionary
            else
            {
                //checks if not in bounds
                if (index < 1)
                {
                    //if not in bounds then mark false in dictionary
                    harami.Dictionary_Pattern.Add(patternName, false);
                    return false;
                }
                else
                {
                    //retrieve previous candlestick so we can see if the recognizer properties match
                    SmartCandlestick previous = lscs[index - 1];
                    //retrieve bearish bool for both smartcandlesticks
                    bool bearish = previous.open < previous.close & harami.close < harami.open;
                    //retrieve recognizer bool value
                    bool r = harami.topPrice < previous.topPrice & harami.bottomPrice > previous.bottomPrice & bearish;
                    //add recognizer bool to dictionary
                    harami.Dictionary_Pattern.Add(patternName, r);
                    return r;
                }
            }
        }
    }
}