using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class ImmutableInfraredTextureTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;

        public ImmutableInfraredTextureTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (InfraredFrameData frame = new InfraredFrameData())
            {
                using (ImmutableInfraredTexture texture = new ImmutableInfraredTexture(device, frame))
                {
                    Assert.AreNotEqual(texture.ShaderView.NativePointer, IntPtr.Zero);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (InfraredFrameData frame = new InfraredFrameData())
            {
                using (ImmutableInfraredTexture texture = new ImmutableInfraredTexture(null, frame))
                {

                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullFrame()
        {
            using (ImmutableInfraredTexture texture = new ImmutableInfraredTexture(device, null))
            {

            }
        }


        public void Dispose()
        {
            device.Dispose();
        }
    }
}
