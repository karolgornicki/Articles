using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.PriorityQueue
{
    public class MaxPQ<T> where T : IComparable
    {
        private T[] _pq;
        private int _n;

        public MaxPQ(int capacity)
        {
            _pq = new T[capacity + 1];
            _n = 0;
        }

        private bool IsLess(int i, int j)
        {
            return _pq[i].CompareTo(_pq[j]) < 0;
        }

        private void Exch(int i, int j)
        {
            T tmp = _pq[i];
            _pq[i] = _pq[j];
            _pq[j] = tmp;
        }

        private void Swim(int n)
        {
            while (n > 1 && IsLess(n / 2, 2))
            {
                Exch(n / 2, n);
                n = n / 2;
            }
        }

        public void Insert(T key)
        {
            _pq[++_n] = key;
            Swim(_n);
        }

        private void Sink(int i)
        {
            while (2 * i <= _n)
            {
                int j = 2 * i;
                if (i < _n && IsLess(j, j + 1)) { j++; }
                if (IsLess(i, j))
                {
                    Exch(i, j);
                    i = j;
                }
                else
                {
                    break;
                }
            }
        }

        public T DelMax()
        {
            if (_n == 0)
            {
                throw new InvalidOperationException("You can't remove element from empty priority queue.");
            }

            T max = _pq[1];
            Exch(1, _n--);
            _pq[_n + 1] = default(T);
            Sink(1);
            return max;
        }

        public bool IsEmpty()
        {
            return _n == 0;
        }

    }
}
