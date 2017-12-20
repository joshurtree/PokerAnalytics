using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAnalytics
{
    public static class MathExtensions
    {
        public static double FastStdDev<K>(this Dictionary<K, double> data)
        {
            return data.Values.ToArray().FastStdDev();
        }

        public static double FastStdDev(this List<double> data)
        {
            return ((double[]) data.ToArray()).FastStdDev();
        }

        public static double FastStdDev(this double[] data)
        {
            double stdDev = 0;
            double sumAll = 0;
            double sumAllQ = 0;

            //Sum of x and sum of x²
            for (int i = 0; i < data.Length; i++)
            {
                double x = data[i];
                sumAll += x;
                sumAllQ += x * x;
            }

            //Mean (not used here)
            //decimal mean = 0;
            //mean = sumAll / (decimal)data.Length;

            //Standard deviation
            stdDev = System.Math.Sqrt(
                (sumAllQ -
                (sumAll * sumAll) / data.Length) *
                (1.0d / (data.Length - 1))
                );

            return stdDev;
        }

        public static double StdDev<K>(this Dictionary<K, double> values)
        {
            return values.Values.ToArray().StdDev();
        }
        public static double StdDev(this List<double> values)
        {
            return values.ToArray().StdDev();
        }

        public static double StdDev(this double[] valueList, bool samplePopulation = true)
        {

            double M = 0.0;
            double S = 0.0;
            int k = 1;
            foreach (double value in valueList)
            {
                double tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - (samplePopulation ? 1 : 2)));
        }

        public static double Log2(double d)
        {
            return Math.Log(d) / Math.Log(2);
        }
    }

}
