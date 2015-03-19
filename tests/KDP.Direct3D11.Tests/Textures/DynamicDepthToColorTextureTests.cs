using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class DynamicDepthToColorTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public DynamicDepthToColorTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DynamicDepthToColorTexture texture = new DynamicDepthToColorTexture(device))
            {
                Assert.AreNotEqual(texture.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (DynamicDepthToColorTexture texture = new DynamicDepthToColorTexture(null))
            {
            }
        }

        [TestMethod]
        public void TestCopy()
        {
            using (DepthToColorFrameData frame = new DepthToColorFrameData())
            {
                using (DynamicDepthToColorTexture texture = new DynamicDepthToColorTexture(device))
                {
                    texture.Copy(device.ImmediateContext, frame);
                }
            }
        }

        public void Dispose()
        {
            device.Dispose();
        }
    }
}
