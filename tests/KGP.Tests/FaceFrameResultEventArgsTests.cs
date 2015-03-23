using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Kinect.Face;

namespace KGP.Tests
{
    [TestClass]
    public class FaceFrameResultEventArgsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNull()
        {
            FaceFrameResultEventArgs args = new FaceFrameResultEventArgs(0, null);
        }
    }
}
