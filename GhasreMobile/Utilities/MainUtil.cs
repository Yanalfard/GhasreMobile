using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GhasreMobile.Utilities
{
    public static class MainUtil
    {
        public static double Round(double input, int deg)
        {
            double degIn10 = Math.Pow(10, deg);
            input = input / degIn10;
            // input = Math.Round(input);
            input = Math.Floor(input);
            return input * degIn10;
        }
    }
}
