using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.PriorityQueue
{
    public class HeapSort<T> where T : IComparable
    {
        private static bool IsLess(T[] xs, int i, int j) 
        {
            // (i/j - 1) because array is indexed from 0
            return xs[i - 1].CompareTo(xs[j - 1]) < 0;
        }


        private static void Sink(T[] xs, int i, int n)
        {
            while (2 * i <= n)
            {
                int j = 2 * i;
                if (j < n && IsLess(xs, j, j + 1)) { j++; }
                if (IsLess(xs, i, j))
                {
                    Exch(xs, i, j);
                    i = j;
                }
                else
                {
                    break;
                }
            }
        }

        private static void Exch(T[] xs, int i, int j)
        {
            T tmp = xs[--i];    // -- because array is indexed from 0
            xs[i] = xs[--j];    // -- because array is indexed from 0
            xs[j] = tmp;
        }

        public static T[] Sort(T[] xs)
        {
            int n = xs.Length;
            for (int i = n / 2; i >= 1; i--)
            {
                Sink(xs, i, n);
            }

            while (n > 1)
            {
                Exch(xs, 1, n);
                Sink(xs, 1, --n);
            }
            
            return xs;
        }
    }
}
