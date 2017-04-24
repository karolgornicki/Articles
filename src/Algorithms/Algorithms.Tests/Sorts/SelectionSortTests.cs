using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Sorts;

namespace Algorithms.Tests.Sorts
{
    [TestClass]
    public class SelectionSortTestsNonGeneric
    {
        [TestMethod]
        public void Sort_EmptyArray_ReturnsEmptyArray()
        {
            // Arrange 
            int[] xs = new int[] { };

            // Act 
            int[] ys = SelectionSort.Sort(xs);

            // Assert 
            Assert.AreEqual(0, ys.Length);
        }

        [TestMethod]
        public void Sort_OneElementArray_ReturnsTheSameArray()
        {
            // Arrange 
            int[] xs = new int[] { 1 };

            // Act 
            int[] ys = SelectionSort.Sort(xs);

            // Assert 
            Assert.AreEqual(1, ys.Length);
            Assert.AreEqual(1, ys[0]);
        }

        [TestMethod]
        public void Sort_TwoElementOrderedArray_ReturnsTheSameArray()
        {
            // Arrange 
            int[] xs = new int[] { 1, 2 };

            // Act 
            int[] ys = SelectionSort.Sort(xs);

            // Assert 
            Assert.AreEqual(2, ys.Length);
            Assert.IsTrue(Utils.IsSorted(xs));
        }

        [TestMethod]
        public void Sort_TwoElementUnorderedArray_ReturnsSortedArray()
        {
            // Arrange 
            int[] xs = new int[] { 2, 1 };

            // Act 
            int[] ys = SelectionSort.Sort(xs);

            // Assert 
            Assert.AreEqual(2, ys.Length);
            Assert.IsTrue(Utils.IsSorted(xs));
        }
    }

    [TestClass]
    public class SelectionSortTests
    {
        // Function under test
        private Func<int[], int[]> f = SelectionSort.Sort;

        [TestMethod]
        public void Sort_EmptyArray_ReturnsEmptyArray()
        {
            SortAlgoTester.TestSortingEmptyArray(f);
        }

        [TestMethod]
        public void Sort_OneElementArray_ReturnsTheSameArray()
        {
            SortAlgoTester.TestSortingOneElementArrayArray(f);
        }

        [TestMethod]
        public void Sort_TwoElementOrderedArray_ReturnsTheSameArray()
        {
            SortAlgoTester.TestSortingTwoElementOrderedArray(f);
        }

        [TestMethod]
        public void Sort_TwoElementUnorderedArray_ReturnsSortedrray()
        {
            SortAlgoTester.TestSortingTwoElementUnorderedArray(f);
        }

        [TestMethod]
        public void Sort_LargeArray_ReturnsSortedArray()
        {
            SortAlgoTester.TestSortingLargeArray(f);
        }
    }
}
