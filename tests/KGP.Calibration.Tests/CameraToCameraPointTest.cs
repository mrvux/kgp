using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;

namespace KGP.Calibration.Tests
{
    [TestClass]
    public class CameraToCameraPointTest
    {
        [TestMethod]
        public void TestCameraToCamera()
        {
            Vector3 src = new Vector3(23,5,6);
            Vector3 dst = new Vector3(12,7,0);
            CameraToCameraPoint cc = new CameraToCameraPoint(src, dst);

            Assert.AreEqual(src, cc.Origin);
            Assert.AreEqual(dst, cc.Destination);
        }

        [TestMethod]
        public void TestCameraToScreen()
        {
            Vector3 src = new Vector3(23, 5, 6);
            Vector2 dst = new Vector2(12, 7);
            CameraToScreenPoint cc = new CameraToScreenPoint(src, dst);

            Assert.AreEqual(src, cc.CameraPoint);
            Assert.AreEqual(dst, cc.ScreenPoint);
        }
    }
}
