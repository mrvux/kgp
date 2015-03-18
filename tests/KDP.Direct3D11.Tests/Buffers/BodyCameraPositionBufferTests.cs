using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Buffers;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class BodyCameraPositionBufferTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public BodyCameraPositionBufferTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (BodyCameraPositionBuffer buffer = new BodyCameraPositionBuffer(device))
            {
                Assert.AreNotEqual(buffer.ShaderView.NativePointer, IntPtr.Zero);
            }
        }

        public void Dispose()
        {
            device.Dispose();
        }
    }
}
