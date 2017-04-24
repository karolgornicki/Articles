using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Sorts;

namespace Algorithms.Tests.Sorts
{
    internal class SortAlgoTester
    {
        public static void TestSortingEmptyArray(Func<int[], int[]> funSort)
        {
            // Arrange 
            int[] xs = new int[] { };

            // Act 
            int[] ys = funSort(xs);

            // Assert 
            Assert.AreEqual(0, ys.Length);
        }

        public static void TestSortingOneElementArrayArray(Func<int[], int[]> funSort)
        {
            // Arrange 
            
            int[] xs = new int[] { 1 };

            // Act 
            int[] ys = funSort(xs);

            // Assert 
            Assert.AreEqual(1, ys.Length);
            Assert.AreEqual(1, ys[0]);
        }

        public static void TestSortingTwoElementOrderedArray(Func<int[], int[]> funSort)
        {
            // Arrange 
            int[] xs = new int[] { 1, 2 };

            // Act 
            int[] ys = funSort(xs);

            // Assert 
            Assert.AreEqual(2, ys.Length);
            Assert.IsTrue(Utils.IsSorted(xs));
        }

        public static void TestSortingTwoElementUnorderedArray(Func<int[], int[]> funSort)
        {
            // Arrange 
            int[] xs = new int[] { 2, 1 };

            // Act 
            int[] ys = funSort(xs);

            // Assert 
            Assert.AreEqual(2, ys.Length);
            Assert.IsTrue(Utils.IsSorted(xs));
        }

        public static void TestSortingLargeArray(Func<int[], int[]> funSort)
        {
            // Arrange 
            int[] xs = new int[] { 4, 8, 2, 7, 1, 0, 2, 6, 3, 9, 2, 0, 1, 6, 3, 8, 2, 8, 5 };

            // Act 
            int[] ys = funSort(xs);

            // Assert 
            Assert.AreEqual(xs.Length, ys.Length);
            Assert.AreEqual(0, ys[0]);
            Assert.AreEqual(9, ys[ys.Length - 1]);
            Assert.IsTrue(Utils.IsSorted(ys));
        }

    }
}
