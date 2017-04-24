using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorts
{
    public class Shuffling
    {
        public static T[] Shuffle<T>(T[] xs)
        {
            for (int i = 0; i < xs.Length; i++)
            {
                Random r = new Random();
                int idxNew = r.Next(i, xs.Length - 1);
                Utils.Exch(xs, i, idxNew);
            }

            return xs;
        }
    }
}
