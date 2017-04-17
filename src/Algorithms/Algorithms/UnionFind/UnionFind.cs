using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.UnionFind
{
    public class UnionFind
    {
        private int[] elements;

        public UnionFind(int n)
        {
            elements = new int[n];
            for (int i = 0; i < n; i++)
            {
                elements[i] = i;
            }
        }

        private int GetRoot(int x)
        {
            while (elements[x] != x)
            {
                x = elements[x];
            }
            return x;
        }

        public void Union(int x, int y)
        {
            int xRoot = GetRoot(x);
            int yRoot = GetRoot(y);

            elements[xRoot] = yRoot;
        }

        public bool AreConnected(int x, int y)
        {
            return GetRoot(x) == GetRoot(y);
        }
    }
}
