using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Tests.Fakes;
using Microsoft.Kinect;

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

        [TestMethod]
        public void TestSingleClosest()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeSpineBody(10,true, new CameraSpacePoint() 
                {
                    Z = 0.2f
                }),
                FakeBodies.FakeSpineBody(20,true, new CameraSpacePoint() 
                {
                    Z = 0.02f
                }),
                FakeBodies.FakeSpineBody(30,true, new CameraSpacePoint() 
                {
                    Z = 0.25f
                }),
                FakeBodies.FakeSpineBody(40,true, new CameraSpacePoint() 
                {
                    Z = 0.7f
                })
            };

            var closest = bodies.ClosestBodies().ToArray();

            Assert.AreEqual(closest.Length, 1);
            Assert.AreEqual(closest[0], bodies[1]);
        }

        [TestMethod]
        public void TestMultipleClosest()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeSpineBody(10,true, new CameraSpacePoint() 
                {
                    Z = 0.2f
                }),
                FakeBodies.FakeSpineBody(20,true, new CameraSpacePoint() 
                {
                    Z = 0.02f
                }),
                FakeBodies.FakeSpineBody(30,true, new CameraSpacePoint() 
                {
                    Z = 0.02f
                }),
                FakeBodies.FakeSpineBody(40,true, new CameraSpacePoint() 
                {
                    Z = 0.7f
                })
            };

            var closest = bodies.ClosestBodies().ToArray();

            Assert.AreEqual(closest.Length, 2);
            Assert.AreEqual(closest.Contains(bodies[1]), true);
            Assert.AreEqual(closest.Contains(bodies[2]), true);
        }

        [TestMethod]
        public void TestSingleCenter()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeSpineBody(10,true, new CameraSpacePoint() 
                {
                    X = 0.2f,
                }),
                FakeBodies.FakeSpineBody(20,true, new CameraSpacePoint() 
                {
                    X = 0.1f,
                }),
                FakeBodies.FakeSpineBody(30,true, new CameraSpacePoint() 
                {
                    X = 0.01f,
                }),
                FakeBodies.FakeSpineBody(40,true, new CameraSpacePoint() 
                {
                    X = 0.7f
                })
            };

            var closest = bodies.CenterBodies().ToArray();

            Assert.AreEqual(closest.Length, 1);
            Assert.AreEqual(closest[0], bodies[2]);
        }

        [TestMethod]
        public void TestMultipleCenter()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeSpineBody(10,true, new CameraSpacePoint() 
                {
                    X = 0.2f
                }),
                FakeBodies.FakeSpineBody(20,true, new CameraSpacePoint() 
                {
                    X = -0.02f //Center is an absolute value, so 1 and 2 should be same distance
                }),
                FakeBodies.FakeSpineBody(30,true, new CameraSpacePoint() 
                {
                    X = 0.02f
                }),
                FakeBodies.FakeSpineBody(40,true, new CameraSpacePoint() 
                {
                    X = 0.7f
                })
            };

            var closest = bodies.CenterBodies().ToArray();

            Assert.AreEqual(closest.Length, 2);
            Assert.AreEqual(closest.Contains(bodies[1]), true);
            Assert.AreEqual(closest.Contains(bodies[2]), true);
        }
    }
}
