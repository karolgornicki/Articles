using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorts
{
    public class InsertSort
    {
        public static T[] Sort<T>(T[] xs) where T : IComparable
        {
            for (int i = 1; i < xs.Length; i++)
            {
                for (int j = i; j >= 1; j--)
                {
                    if (Utils.IsLess(xs[j], xs[j - 1]))
                    {
                        Utils.Exch(xs, j, j - 1);
                    }
                }
            }
            return xs;
        }
    }
}
