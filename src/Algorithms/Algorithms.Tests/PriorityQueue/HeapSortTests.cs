using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Tests.Sorts;
using Algorithms.PriorityQueue;

namespace Algorithms.Tests.PriorityQueue
{
    [TestClass]
    public class HeapSortTests
    {
        // Function under test
        private Func<int[], int[]> f = HeapSort<int>.Sort;

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
