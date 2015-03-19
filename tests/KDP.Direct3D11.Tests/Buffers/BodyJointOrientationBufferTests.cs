using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Buffers;
using KGP;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class BodyJointOrientationBufferTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public BodyJointOrientationBufferTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (BodyJointOrientationBuffer buffer = new BodyJointOrientationBuffer(device))
            {
                Assert.AreNotEqual(buffer.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        public void TestUploadEmpty()
        {
            using (BodyJointOrientationBuffer buffer = new BodyJointOrientationBuffer(device))
            {
                KinectBody[] empty = new KinectBody[0];
                buffer.Copy(device.ImmediateContext, empty);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (BodyJointOrientationBuffer texture = new BodyJointOrientationBuffer(null))
            {
            }
        }

        public void Dispose()
        {
            device.Dispose();
        }
    }
}
