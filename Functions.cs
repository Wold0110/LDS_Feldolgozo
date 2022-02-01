using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDS_Feldolgozo
{
    internal class Functions
    {
        //másodpercet alakít formázott stringé
        public static string secToString(double time)
        {
            double h = Math.Floor(time / 3600);
            time -= h * 3600;
            double m = Math.Floor(time / 60);
            double s = time - m * 60;
            string value = "";

            if (h < 10)
                value += "0";
            value += h + ":";

            if (m < 10)
                value += "0";
            value += m + ":";

            if (s < 10)
                value += "0";
            value += s;

            return value;
        }
        //percet alakít formázott stringé
        public static string minToString(double time)
        {
            double h = Math.Floor(time / 60);
            time -= h * 60;
            string value = "";

            if (h < 10)
                value += "0";
            value += h + ":";

            if (time < 10)
                value += "0";
            value += time + ":00";

            return value;
        }
    }
}
