using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Kinect.Face;

namespace KGP.Tests
{
    [TestClass]
    public class HdFaceFrameResultEventArgsTests
    {
        [TestMethod]
        public void TestValid()
        {
            FaceModel model = new FaceModel();
            FaceAlignment align = new FaceAlignment();
            HdFaceFrameResultEventArgs args = new HdFaceFrameResultEventArgs(1,model, align);

            Assert.AreEqual(model, args.FaceModel);
            Assert.AreEqual(align, args.FaceAlignment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullModel()
        {
            FaceAlignment align = new FaceAlignment();
            HdFaceFrameResultEventArgs args = new HdFaceFrameResultEventArgs(0,null, align);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullAlignment()
        {
            FaceModel model = new FaceModel();
            HdFaceFrameResultEventArgs args = new HdFaceFrameResultEventArgs(0,model, null);
        }
    }
}
