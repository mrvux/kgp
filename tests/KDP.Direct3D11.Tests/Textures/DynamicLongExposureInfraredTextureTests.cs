using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class DynamicLongInfraredTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public DynamicLongInfraredTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DynamicLongExposureInfraredTexture texture = new DynamicLongExposureInfraredTexture(device))
            {
                Assert.AreNotEqual(texture.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (DynamicLongExposureInfraredTexture texture = new DynamicLongExposureInfraredTexture(null))
            {
            }
        }

        [TestMethod]
        public void TestCopy()
        {
            using (LongExposureInfraredFrameData frame = new LongExposureInfraredFrameData())
            {
                using (DynamicLongExposureInfraredTexture texture = new DynamicLongExposureInfraredTexture(device))
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
