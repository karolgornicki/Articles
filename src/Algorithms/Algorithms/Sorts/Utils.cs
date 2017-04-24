using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorts
{
    public class Utils
    {
        public static bool IsLess(IComparable x, IComparable y)
        {
            return x.CompareTo(y) < 0;
        }

        public static bool IsSorted<T>(T[] xs) where T : IComparable
        {
            for (int i = 0; i < xs.Length - 1; i++)
            {
                if (IsLess(xs[i+1], xs[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static void Exch<T>(T[] xs, int i, int j) //where T : IComparable
        {
            if (i < 0 || j < 0 || i >= xs.Length || j >= xs.Length)
            {
                throw new IndexOutOfRangeException();
            }
            T tmp = xs[i];
            xs[i] = xs[j];
            xs[j] = tmp;
        }
    }
}
