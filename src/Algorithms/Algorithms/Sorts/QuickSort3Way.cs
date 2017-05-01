using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorts
{
    public class QuickSort3Way
    {
        
        private static void Sort<T>(T[] xs, int lo, int hi) where T : IComparable
        {
            if (hi <= lo) return;

            int lt = lo, 
                gt = hi, 
                i = lo;

            T pivot = xs[lo];

            while (i <= gt)
            {
                int cmp = xs[i].CompareTo(pivot);

                if (cmp < 0) Utils.Exch(xs, i++, lt++);
                else if (cmp > 0) Utils.Exch(xs, i, gt--);
                else i++;
            }

            Sort(xs, lo, lt - 1);
            Sort(xs, gt + 1, hi);
        }

        public static T[] Sort<T>(T[] xs) where T : IComparable
        {
            xs = Shuffling.Shuffle(xs);
            Sort(xs, 0, xs.Length - 1);
            return xs;
        }
    }
}
