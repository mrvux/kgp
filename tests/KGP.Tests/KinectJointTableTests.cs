using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Kinect;
using SharpDX;
using System.Collections.Generic;

namespace KGP.Tests
{
    [TestClass]
    public class KinectJointTableTests
    {
        [TestMethod]
        public void TestConstructorRef()
        {
            ulong trackingId = 125;
            Dictionary<JointType, Vector3> data = new Dictionary<JointType, Vector3>();
            data.Add(JointType.AnkleLeft, new Vector3(2, 4, 5));
            data.Add(JointType.ElbowLeft, new Vector3(7, 4, 5));
            data.Add(JointType.FootRight, new Vector3(-5, 4, 5));

            KinectJointTable table = new KinectJointTable(trackingId, data);

            Assert.AreEqual(trackingId, table.TrackingId);
            Assert.AreEqual(table.Joints, data);
        }

        [TestMethod]
        public void TestConstructorData()
        {
            ulong trackingId = 125;
            Dictionary<JointType, Vector3> data = new Dictionary<JointType,Vector3>();
            data.Add(JointType.AnkleLeft, new Vector3(2,4,5));
            data.Add(JointType.ElbowLeft, new Vector3(7,4,5));
            data.Add(JointType.FootRight, new Vector3(-5,4,5));

            KinectJointTable table = new KinectJointTable(trackingId, data);

            Assert.AreEqual(trackingId, table.TrackingId);
            Assert.AreEqual(table.Joints.Count, data.Count);
            foreach (JointType jt in data.Keys)
            {
                Assert.AreEqual(data[jt], table.Joints[jt]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullData()
        {
            ulong trackingId = 125;
            KinectJointTable table = new KinectJointTable(trackingId, null);
        }

        [TestMethod]
        public void TesTransform()
        {
            ulong trackingId = 125;
            Dictionary<JointType, Vector3> data = new Dictionary<JointType, Vector3>();
            data.Add(JointType.AnkleLeft, new Vector3(2, 4, 5));
            data.Add(JointType.ElbowLeft, new Vector3(7, 4, 5));
            data.Add(JointType.FootRight, new Vector3(-5, 4, 5));

            KinectJointTable table = new KinectJointTable(trackingId, data);

            Matrix m = Matrix.Translation(-4.0f, 5.0f, 6.0f);

            KinectJointTable transformed = table.Transform(m);

            foreach (JointType jt in data.Keys)
            {
                Assert.AreEqual(Vector3.TransformCoordinate(data[jt], m), transformed.Joints[jt]);
            }
        }
    }
}
