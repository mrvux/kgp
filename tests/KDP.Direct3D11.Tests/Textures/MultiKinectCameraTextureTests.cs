using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class MultiKinectCameraTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;

        public MultiKinectCameraTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (MultiKinectCameraTexture texture = new MultiKinectCameraTexture(device, 2))
            {
                Assert.AreNotEqual(texture.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        public void TestInvalid()
        {
            using (MultiKinectCameraTexture texture = new MultiKinectCameraTexture(device, 0))
            {
            }
        }

        public void Dispose()
        {
            device.Dispose();
        }
    }
}
