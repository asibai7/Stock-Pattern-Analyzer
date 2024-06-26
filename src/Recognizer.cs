using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms_COP_4365
{
    //created an abstract class Recongizer which will serve as a blueprint for all recognizer classes
    internal abstract class Recognizer
    {
        //string variable to store pattern name
        public string patternName;
        //string variable to store pattern name
        public int patternLength;
        /// <summary>
        /// constructor which initializes pattern name and length
        /// </summary>
        /// <param name="pN"></param>
        /// <param name="pL"></param>
        public Recognizer(string pN, int pL)
        {
            patternName = pN;
            patternLength = pL;
        }
        //abstract method Recognize which each derived class must have
        public abstract bool Recognize(List<SmartCandlestick> LSCS, int index);
        /// <summary>
        /// method to recognize patterns in all candlesticks in the list
        /// </summary>
        /// <param name="LSCS">list of smartcandlesticks</param>
        public void Recognize_All(List<SmartCandlestick> LSCS)
        {
            //loop through all candlesticks in the list
            for (int index = 0; index < LSCS.Count; index++)
            {
                //call the Recognize method for each smartcandlestick
                Recognize(LSCS, index);
            }
        }
    }
}