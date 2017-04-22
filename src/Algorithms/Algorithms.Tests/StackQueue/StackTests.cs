using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.StackQueue;

namespace Algorithms.Tests.StackQueue
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void IsEmpty_InitializedStack_ReturnsTrue()
        {
            Stack<int> s = new Stack<int>();

            Assert.IsTrue(s.IsEmpty());
        }

        [TestMethod]
        public void IsEmpty_StackWithAddedElement_ReturnsFalse()
        {
            Stack<int> s = new Stack<int>();
            s.Push(1);

            Assert.IsFalse(s.IsEmpty());
        }

        [TestMethod]
        public void IsEmpty_StackWithAddedAndRemoved_ReturnsTrue()
        {
            Stack<int> s = new Stack<int>();
            s.Push(1);
            s.Pop();

            Assert.IsTrue(s.IsEmpty());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_InitializedStack_ThrowsException()
        {
            Stack<int> s = new Stack<int>();

            s.Pop();
        }

        [TestMethod]
        public void Pop_StackWithAdded1_Returns1()
        {
            Stack<int> s = new Stack<int>();
            s.Push(1);

            Assert.AreEqual(1, s.Pop());
        }

        [TestMethod]
        public void Pop_StackWithAdded1And2_Returns2AndThen1()
        {
            Stack<int> s = new Stack<int>();
            s.Push(1);
            s.Push(2);

            Assert.AreEqual(2, s.Pop());
            Assert.AreEqual(1, s.Pop());
        }
    }
}
