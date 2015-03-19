using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class DynamicInfraredTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public DynamicInfraredTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DynamicInfraredTexture texture = new DynamicInfraredTexture(device))
            {
                Assert.AreNotEqual(texture.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (DynamicInfraredTexture texture = new DynamicInfraredTexture(null))
            {
            }
        }

        [TestMethod]
        public void TestCopy()
        {
            using (InfraredFrameData frame = new InfraredFrameData())
            {
                using (DynamicInfraredTexture texture = new DynamicInfraredTexture(device))
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
