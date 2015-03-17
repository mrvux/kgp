using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Tests.Fakes;

namespace KGP.Tests
{
    [TestClass]
    public class KinectBodyExtensionsTests
    {
        [TestMethod]
        public void TestTrackedOnly()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(10,true),
                FakeBodies.FakeRandomBody(20,true),
                FakeBodies.FakeRandomBody(30,false),
                FakeBodies.FakeRandomBody(40,true),
                FakeBodies.FakeRandomBody(50,true),
                FakeBodies.FakeRandomBody(60,false),
            };

            var filtered = bodies.TrackedOnly().ToArray();

            Assert.AreEqual(filtered.Length, 4);
            Assert.AreEqual(filtered.Contains(bodies[0]), true);
            Assert.AreEqual(filtered.Contains(bodies[1]), true);
            Assert.AreEqual(filtered.Contains(bodies[3]), true);
            Assert.AreEqual(filtered.Contains(bodies[4]), true);
        }

        [TestMethod]
        public void TestContainsId()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(10,true),
                FakeBodies.FakeRandomBody(20,true),
                FakeBodies.FakeRandomBody(30,false),
                FakeBodies.FakeRandomBody(40,true),
                FakeBodies.FakeRandomBody(50,true),
                FakeBodies.FakeRandomBody(60,false),
            };

            var match0 = FakeBodies.FakeRandomBody(10, false);
            Assert.AreEqual(match0.ContainsId(bodies), true);

            var match1 = FakeBodies.FakeRandomBody(40, false);
            Assert.AreEqual(match1.ContainsId(bodies), true);
        }

        [TestMethod]
        public void TestContainsIdFalse()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(10,true),
                FakeBodies.FakeRandomBody(20,true),
                FakeBodies.FakeRandomBody(30,false),
                FakeBodies.FakeRandomBody(40,true),
                FakeBodies.FakeRandomBody(50,true),
                FakeBodies.FakeRandomBody(60,false),
            };

            var nomatch = FakeBodies.FakeRandomBody(128, false);
            Assert.AreEqual(nomatch.ContainsId(bodies), false);
        }

        [TestMethod]
        public void TestFindById()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(10,true),
                FakeBodies.FakeRandomBody(20,true),
                FakeBodies.FakeRandomBody(30,false),
                FakeBodies.FakeRandomBody(40,true),
                FakeBodies.FakeRandomBody(50,true),
                FakeBodies.FakeRandomBody(60,false),
            };

            var match0 = FakeBodies.FakeRandomBody(30, false);
            Assert.AreEqual(match0.FindById(bodies), bodies[2]);
        }

        [TestMethod]
        public void TestFindByIdFalse()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeRandomBody(10,true),
                FakeBodies.FakeRandomBody(20,true),
                FakeBodies.FakeRandomBody(30,false),
                FakeBodies.FakeRandomBody(40,true),
                FakeBodies.FakeRandomBody(50,true),
                FakeBodies.FakeRandomBody(60,false),
            };

            var nomatch = FakeBodies.FakeRandomBody(128, false);
            Assert.AreEqual(nomatch.FindById(bodies), null);
        }
    }
}
