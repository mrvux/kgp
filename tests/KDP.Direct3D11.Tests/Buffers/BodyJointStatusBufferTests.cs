using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Buffers;
using KGP;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class BodyJointStatusBufferTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public BodyJointStatusBufferTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (BodyJointStatusBuffer buffer = new BodyJointStatusBuffer(device))
            {
                Assert.AreNotEqual(buffer.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        public void TestUploadEmpty()
        {
            using (BodyJointStatusBuffer buffer = new BodyJointStatusBuffer(device))
            {
                KinectBody[] empty = new KinectBody[0];
                buffer.Copy(device.ImmediateContext, empty);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (BodyJointStatusBuffer texture = new BodyJointStatusBuffer(null))
            {
            }
        }

        public void Dispose()
        {
            device.Dispose();
        }
    }
}
