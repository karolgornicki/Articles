using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.PriorityQueue;

namespace Algorithms.Tests.PriorityQueue
{
    [TestClass]
    public class MaxPQTests
    {
        #region [IsEmpty]
        [TestMethod]
        public void IsEmpty_InitalizedPQ_ReturnsTrue()
        {
            MaxPQ<int> pq = new MaxPQ<int>(100);

            Assert.IsTrue(pq.IsEmpty());
        }

        [TestMethod]
        public void IsEmpty_PQWithOneElement_ReturnsFalse()
        {
            MaxPQ<int> pq = new MaxPQ<int>(100);
            pq.Insert(1);

            Assert.IsFalse(pq.IsEmpty());
        }
        #endregion
        
        #region [DelMax]
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DelMax_EmptyPQ_ThrowsExceltion()
        {
            MaxPQ<int> pq = new MaxPQ<int>(100);

            pq.DelMax();
        }

        [TestMethod]
        public void DelMax_PQWithOneElement_ReturnsThatElement()
        {
            MaxPQ<int> pq = new MaxPQ<int>(100);
            pq.Insert(1);

            int retVal = pq.DelMax();
            Assert.AreEqual(1, retVal);
        }

        [TestMethod]
        public void DelMax_PQWith3Element_ReturnsGreater()
        {
            MaxPQ<int> pq = new MaxPQ<int>(100);
            pq.Insert(1);
            pq.Insert(3);
            pq.Insert(2);

            Assert.AreEqual(3, pq.DelMax());
            Assert.AreEqual(2, pq.DelMax());
            Assert.AreEqual(1, pq.DelMax());
        }
        #endregion

        #region [DelMax & IsEmpty]
        [TestMethod]
        public void IsEmpty_AfterRemovingLastElement_ReturnsTrue()
        {
            MaxPQ<int> pq = new MaxPQ<int>(100);
            pq.Insert(1);
            pq.DelMax();

            Assert.IsTrue(pq.IsEmpty());
        }
        #endregion
    }
}
