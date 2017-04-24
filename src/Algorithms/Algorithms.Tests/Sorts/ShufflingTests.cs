using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Sorts;

namespace Algorithms.Tests.Sorts
{
    [TestClass]
    public class ShufflingTests
    {
        [TestMethod]
        public void Shuffle_LargeSortedArray_ReturnsUnorderedArray()
        {
            // Arrange 
            int[] xs = new int[] { 4, 8, 2, 7, 1, 0, 2, 6, 3, 9, 2, 0, 1, 6, 3, 8, 2, 8, 5 };
            int[] xsSorted = SelectionSort.Sort(xs);

            // Act 
            int[] xsShuffled = Shuffling.Shuffle(xsSorted);

            // Assert
            Assert.IsFalse(Utils.IsSorted(xsShuffled));
        }
    }
}
