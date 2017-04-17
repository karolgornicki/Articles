using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.UnionFind
{
    public class ImprovedUnionFind
    {
        private int[] elements;
        private int[] sizes;

        public ImprovedUnionFind(int n)
        {
            elements = new int[n];
            sizes = new int[n];
            for (int i = 0; i < n; i++)
            {
                elements[i] = i;
                sizes[i] = 1;
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

            if (sizes[xRoot] > sizes[yRoot])
            {
                elements[yRoot] = xRoot;
                sizes[xRoot] += sizes[yRoot];
            }
            else
            {
                elements[xRoot] = yRoot;
                sizes[yRoot] += sizes[xRoot];
            }
        }

        public bool AreConnected(int x, int y)
        {
            return GetRoot(x) == GetRoot(y);
        }
    }
}
