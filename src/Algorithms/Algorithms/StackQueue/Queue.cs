using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.StackQueue
{
    public class Queue<T>
    {
        private Node<T> first;
        private Node<T> last;

        public Queue()
        {
            first = null;
            last = null;
        }

        public void Enqueue(T item)
        {
            Node<T> node = new Node<T>() { Value = item };
            if (IsEmpty())
            {
                first = node;
                last = node;
            }
            else
            {
                last.Next = node;
                last = node;
            }
        }

        public T Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("You can not dequeue from empty queue.");
            }
            else
            {
                T val = first.Value;
                first = first.Next;
                return val;
            }
        }

        public bool IsEmpty()
        {
            return first == null;
        }
    }
}
