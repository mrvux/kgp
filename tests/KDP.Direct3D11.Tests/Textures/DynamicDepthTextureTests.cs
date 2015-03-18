using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class DynamicDepthTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;

        public DynamicDepthTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DynamicDepthTexture texture = new DynamicDepthTexture(device))
            {
                Assert.AreNotEqual(texture.NormalizedView.NativePointer, IntPtr.Zero);
                Assert.AreNotEqual(texture.RawView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        public void TestCopy()
        {
            using (DepthFrameData frame = new DepthFrameData())
            {
                using (DynamicDepthTexture texture = new DynamicDepthTexture(device))
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
