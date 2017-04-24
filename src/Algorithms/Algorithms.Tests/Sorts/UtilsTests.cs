using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Sorts;

namespace Algorithms.Tests.Sorts
{
    [TestClass]
    public class UtilsTests
    {
        #region [IsLess]
        [TestMethod]
        public void IsLess_1And2_ReturnsTrue()
        {
            Assert.IsTrue(Utils.IsLess(1, 2));
        }

        [TestMethod]
        public void IsLess_1And1_ReturnsFalse()
        {
            Assert.IsFalse(Utils.IsLess(1, 1));
        }

        [TestMethod]
        public void IsLess_2And1_ReturnsFalse()
        {
            Assert.IsFalse(Utils.IsLess(2, 1));
        }
        #endregion

        #region [IsSorted]
        [TestMethod]
        public void IsSorted_EmptyArray_ReturnsTrue()
        {
            int[] xs = new int[] { };

            Assert.IsTrue(Utils.IsSorted(xs));
        }

        [TestMethod]
        public void IsSorted_ArrayWithOneElement_ReturnsTrue()
        {
            int[] xs = new int[] { 1 };

            Assert.IsTrue(Utils.IsSorted(xs));
        }

        [TestMethod]
        public void IsSorted_TwoElementOrderedArray_ReturnsTrue()
        {
            int[] xs = new int[] { 1, 2 };

            Assert.IsTrue(Utils.IsSorted(xs));
        }

        [TestMethod]
        public void IsSorted_TwoElementUnorderedArray_ReturnsFalse()
        {
            int[] xs = new int[] { 2, 1 };

            Assert.IsFalse(Utils.IsSorted(xs));
        }

        [TestMethod]
        public void IsSorted_ArrayWithTwoTheSameElements_ReturnsTrue()
        {
            int[] xs = new int[] { 1, 1 };

            Assert.IsTrue(Utils.IsSorted(xs));
        }
        #endregion

        #region [Exch]
        [TestMethod]
        public void Exch_TheSameValidIndex_ArrayHasTheSameElementAtThatIndex()
        {
            // Arrange 
            int[] xs = new int[] { 1, 2, 3 };

            // Act 
            Utils.Exch(xs, 1, 1);

            // Assert 
            Assert.AreEqual(2, xs[1]);
        }

        [TestMethod]
        public void Exch_DifferentValidIndices_ElementsAreSwapped()
        {
            // Arrange 
            int[] xs = new int[] { 1, 2, 3 };

            // Act 
            Utils.Exch(xs, 1, 2);

            // Assert 
            Assert.AreEqual(3, xs[1]);
            Assert.AreEqual(2, xs[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Exch_OneOutOfRangeIndex_ThrowsException()
        {
            // Arrange 
            int[] xs = new int[] { 1, 2, 3 };

            // Act 
            Utils.Exch(xs, 1, 100);
        }
        #endregion
    }
}
