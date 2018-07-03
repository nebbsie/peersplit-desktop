using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace peersplit_desktop.Controller
{
    public class MathUtilities
    {
       /// <summary>
        /// Take a double and only keep up to a certain amount of digits.
        /// </summary>
        public static double Truncate(double val, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * val) / mult;
            return result;
        }
    }
}
