using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.StackQueue;

namespace Algorithms.Tests.StackQueue
{
    [TestClass]
    public class QueueTests
    {
        [TestMethod]
        public void IsEmpty_InitializedQueue_True()
        {
            Queue<int> q = new Queue<int>();

            Assert.IsTrue(q.IsEmpty());
        }

        [TestMethod]
        public void IsEmpty_QueueWithOneElement_False()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);

            Assert.IsFalse(q.IsEmpty());
        }

        [TestMethod]
        public void IsEmpty_QueueWithAddedAndRemoved_True()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            q.Dequeue();

            Assert.IsTrue(q.IsEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_InitializedQueue_ThrowsException()
        {
            Queue<int> q = new Queue<int>();

            q.Dequeue();
        }

        [TestMethod]
        public void Dequeue_QueueWithAdded1_Returns1()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);

            Assert.AreEqual(1, q.Dequeue());
        }

        [TestMethod]
        public void Dequeue_QueueWithAdded1And2_Returns1AndThen2()
        {
            Queue<int> q = new Queue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            Assert.AreEqual(1, q.Dequeue());
            Assert.AreEqual(2, q.Dequeue());
        }
    }
}
