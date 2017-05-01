using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorts
{
    public class QuickSort
    {
        private static int Partition<T>(T[] xs, int lo, int hi) where T : IComparable
        {
            int left = lo, 
                right = hi + 1;

            while (true)
            {
                while (Utils.IsLess(xs[++left], xs[lo]))
                {
                    if (left == hi) break;
                }

                while (Utils.IsLess(xs[lo], xs[--right]))
                {
                    if (right == lo) break;
                }

                if (left >= right) break;
                Utils.Exch(xs, left, right);
            }

            Utils.Exch(xs, lo, right);
            return right;
        }

        private static void Sort<T>(T[] xs, int lo, int hi) where T : IComparable
        {
            if (hi <= lo) return;

            int j = Partition(xs, lo, hi);

            Sort(xs, lo, j - 1);
            Sort(xs, j + 1, hi);
        }

        public static T[] Sort<T>(T[] xs) where T : IComparable
        {
            xs = Shuffling.Shuffle(xs);
            Sort(xs, 0, xs.Length - 1);
            return xs;
        }
    }
}
