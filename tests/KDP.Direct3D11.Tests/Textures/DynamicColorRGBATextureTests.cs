using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class DynamicColorRGBATextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;

        public DynamicColorRGBATextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DynamicColorRGBATexture texture = new DynamicColorRGBATexture(device))
            {
                Assert.AreNotEqual(texture.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (DynamicColorRGBATexture texture = new DynamicColorRGBATexture(null))
            {
            }
        }

        [TestMethod]
        public void TestCopy()
        {
            using (ColorRGBAFrameData frame = new ColorRGBAFrameData())
            {
                using (DynamicColorRGBATexture texture = new DynamicColorRGBATexture(device))
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
