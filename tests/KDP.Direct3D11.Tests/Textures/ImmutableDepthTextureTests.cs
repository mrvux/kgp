using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class ImmutableDepthTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;

        public ImmutableDepthTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DepthFrameData frame = new DepthFrameData())
            {
                using (ImmutableDepthTexture texture = new ImmutableDepthTexture(device,frame))
                {
                    Assert.AreNotEqual(texture.NormalizedView.NativePointer, IntPtr.Zero);
                    Assert.AreNotEqual(texture.RawView.NativePointer, IntPtr.Zero);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (DepthFrameData frame = new DepthFrameData())
            {
                using (ImmutableDepthTexture texture = new ImmutableDepthTexture(null, frame))
                {

                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullFrame()
        {
            using (ImmutableDepthTexture texture = new ImmutableDepthTexture(device, null))
            {

            }
        }


        public void Dispose()
        {
            device.Dispose();
        }
    }
}
