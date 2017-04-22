using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.StackQueue
{
    public class Stack<T>
    {
        private Node<T> first;

        public Stack()
        {
            first = null;
        }

        public void Push(T item)
        {
            Node<T> node = new Node<T>() { Value = item, Next = first };
            first = node;
        }

        public T Pop()
        {
            if (first == null)
            {
                throw new InvalidOperationException("You can not pop value from empty stack.");
            }
            T val = first.Value;
            first = first.Next;
            return val;
        }

        public bool IsEmpty()
        {
            return first == null;
        }
    }
}
