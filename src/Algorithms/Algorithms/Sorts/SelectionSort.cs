using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorts
{
    public class SelectionSort
    {
        public static T[] Sort<T>(T[] xs) where T : IComparable
        {
            for (int i = 0; i < xs.Length; i++)
            {
                int iMin = i;
                for (int j = i; j < xs.Length; j++)
                {
                    if (Utils.IsLess(xs[j], xs[iMin]))
                    {
                        iMin = j;
                    }
                }
                Utils.Exch(xs, i, iMin);
            }
            return xs;
        }
    }
}
