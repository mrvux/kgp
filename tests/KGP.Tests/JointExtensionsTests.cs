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

        [TestMethod]
        public void TestJointParent()
        {
            Assert.AreEqual(JointType.AnkleLeft.GetParentType(), JointType.KneeLeft);
            Assert.AreEqual(JointType.AnkleRight.GetParentType(), JointType.KneeRight);
            
            Assert.AreEqual(JointType.ElbowLeft.GetParentType(), JointType.ShoulderLeft);
            Assert.AreEqual(JointType.ElbowRight.GetParentType(), JointType.ShoulderRight);
            
            Assert.AreEqual(JointType.FootLeft.GetParentType(), JointType.AnkleLeft);
            Assert.AreEqual(JointType.FootRight.GetParentType(), JointType.AnkleRight);
            
            Assert.AreEqual(JointType.HandLeft.GetParentType(), JointType.WristLeft);
            Assert.AreEqual(JointType.HandRight.GetParentType(), JointType.WristRight);
            
            Assert.AreEqual(JointType.HandTipLeft.GetParentType(), JointType.HandLeft);
            Assert.AreEqual(JointType.HandTipRight.GetParentType(), JointType.HandRight);
            
            Assert.AreEqual(JointType.Head.GetParentType(), JointType.Neck);
            
            Assert.AreEqual(JointType.HipLeft.GetParentType(), JointType.SpineBase);
            Assert.AreEqual(JointType.HipRight.GetParentType(), JointType.SpineBase);
            
            Assert.AreEqual(JointType.KneeLeft.GetParentType(), JointType.HipLeft);
            Assert.AreEqual(JointType.KneeRight.GetParentType(), JointType.HipRight);
            
            Assert.AreEqual(JointType.Neck.GetParentType(), JointType.SpineShoulder);
            
            Assert.AreEqual(JointType.ShoulderLeft.GetParentType(), JointType.SpineShoulder);
            Assert.AreEqual(JointType.ShoulderRight.GetParentType(), JointType.SpineShoulder);

            Assert.AreEqual(JointType.SpineBase.GetParentType(), JointType.SpineBase);

            Assert.AreEqual(JointType.SpineMid.GetParentType(), JointType.SpineBase);

            Assert.AreEqual(JointType.SpineShoulder.GetParentType(), JointType.SpineMid);
            
            Assert.AreEqual(JointType.ThumbLeft.GetParentType(), JointType.HandLeft);
            Assert.AreEqual(JointType.ThumbRight.GetParentType(), JointType.HandRight);

            Assert.AreEqual(JointType.WristLeft.GetParentType(), JointType.ElbowLeft);
            Assert.AreEqual(JointType.WristRight.GetParentType(), JointType.ElbowRight);
        }
    }
}
