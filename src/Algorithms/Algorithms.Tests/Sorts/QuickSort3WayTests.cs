using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Sorts;

namespace Algorithms.Tests.Sorts
{
    [TestClass]
    public class QuickSort3WayTests
    {
        // Function under test
        private Func<int[], int[]> f = QuickSort3Way.Sort;

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
