using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.UnionFind;

namespace Algorithms.Tests.UnionFind
{
    [TestClass]
    public class NaiveUnionFindTests
    {

        [TestMethod]
        public void AreConnected_InitializedModel_ElementIsConnectedWithItself()
        {
            NaiveUnionFind model = new NaiveUnionFind(10);

            Assert.IsTrue(model.AreConnected(1, 1));
        }

        [TestMethod]
        public void AreConnected_InitializedModel_NoTwoElementsAreConnected()
        {
            NaiveUnionFind model = new NaiveUnionFind(10);

            Assert.IsFalse(model.AreConnected(1, 2));
            Assert.IsFalse(model.AreConnected(2, 9));
            Assert.IsFalse(model.AreConnected(5, 6));
        }

        [TestMethod]
        public void AreConnected_1And2And3Connected_AllCombinationOf123AreConnected()
        {
            NaiveUnionFind model = new NaiveUnionFind(10);
            model.Union(1, 2);
            model.Union(2, 3);

            Assert.IsTrue(model.AreConnected(1, 2));
            Assert.IsTrue(model.AreConnected(2, 1));
            Assert.IsTrue(model.AreConnected(1, 3));
            Assert.IsTrue(model.AreConnected(3, 1));
            Assert.IsTrue(model.AreConnected(2, 3));
            Assert.IsTrue(model.AreConnected(3, 2));
            Assert.IsTrue(model.AreConnected(1, 1));
            Assert.IsTrue(model.AreConnected(2, 2));
            Assert.IsTrue(model.AreConnected(3, 3));
        }

        [TestMethod]
        public void AreConnected_1And2And3Connected_1And4AreNotConnected()
        {
            NaiveUnionFind model = new NaiveUnionFind(10);
            model.Union(1, 2);
            model.Union(2, 3);

            Assert.IsFalse(model.AreConnected(1, 4));
        }
    }
}
