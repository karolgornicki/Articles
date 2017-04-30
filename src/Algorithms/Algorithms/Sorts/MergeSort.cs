using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorts
{
    public class MergeSort
    {
        private static void Sort<T>(T[] xs, T[] aux, int lo, int hi) where T : IComparable
        {
            if (lo < hi)
            {
                int mi = lo + (hi - lo) / 2;
                Sort(xs, aux, lo, mi);
                Sort(xs, aux, mi + 1, hi);
                Merge(xs, aux, lo, mi, hi);
            }
        }

        private static void Merge<T>(T[] xs, T[] aux, int lo, int mi, int hi) where T : IComparable
        {
            // Copy xs to aux.
            for (int i = lo; i <= hi; i++)
            {
                aux[i] = xs[i];
            }

            // Set iterators for 2 halves.
            int iLeft = lo;
            int iRight = mi + 1;

            // Merge 2 halves.
            for (int i = lo; i <= hi; i++)
            {
                if (iLeft > mi) xs[i] = aux[iRight++];
                else if (iRight > hi) xs[i] = aux[iLeft++];
                else if (Utils.IsLess(aux[iLeft], aux[iRight])) xs[i] = aux[iLeft++];
                else xs[i] = aux[iRight++];
            }
        }

        public static T[] Sort<T>(T[] xs) where T : IComparable
        {
            T[] aux = new T[xs.Length];
            Sort(xs, aux, 0, xs.Length - 1);
            return xs;
        }
    }
}
