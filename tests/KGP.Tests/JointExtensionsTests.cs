using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Tests
{
    [TestClass]
    public class JointExtensionsTests
    {
        [TestMethod]
        public void TestAtLeastInferred()
        {
            var jnot = new Joint()
            {
                JointType = JointType.Head,
                TrackingState = TrackingState.NotTracked
            };

            var jinferred = new Joint()
            {
                JointType = JointType.Head,
                TrackingState = TrackingState.Inferred
            };

            var jtracked = new Joint()
            {
                JointType = JointType.Head,
                TrackingState = TrackingState.Tracked
            };

            Assert.AreEqual(false, jnot.IsAtLeastInferred());
            Assert.AreEqual(true, jinferred.IsAtLeastInferred());
            Assert.AreEqual(true, jtracked.IsAtLeastInferred());
        }
    }
}
