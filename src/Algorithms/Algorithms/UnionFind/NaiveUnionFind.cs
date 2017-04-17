using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.UnionFind
{
    public class NaiveUnionFind
    {
        private int[] elements;

        public NaiveUnionFind(int n)
        {
            elements = new int[n];
            for (int i = 0; i < n; i++)
            {
                elements[i] = i;
            }
        }

        public void Union(int x, int y)
        {
            // We are going to assign all elements from component x
            // to component y.
            int xComponent = elements[x];
            int yComponent = elements[y];
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == xComponent)
                {
                    elements[i] = yComponent;
                }
            }
        }

        public bool AreConnected(int x, int y)
        {
            return elements[x] == elements[y];
        }
    }
}
