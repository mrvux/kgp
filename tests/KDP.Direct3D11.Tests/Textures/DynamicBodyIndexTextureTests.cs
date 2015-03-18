using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class DynamicBodyIndexTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;
        

        public DynamicBodyIndexTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DynamicBodyIndexTexture texture = new DynamicBodyIndexTexture(device))
            {
                Assert.AreNotEqual(texture.NormalizedView.NativePointer, IntPtr.Zero);
                Assert.AreNotEqual(texture.RawView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        public void TestCopy()
        {
            using (BodyIndexFrameData frame = new BodyIndexFrameData())
            {
                using (DynamicBodyIndexTexture texture = new DynamicBodyIndexTexture(device))
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
