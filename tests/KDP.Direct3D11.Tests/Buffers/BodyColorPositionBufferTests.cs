using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Buffers;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class BodyColorPositionBufferTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public BodyColorPositionBufferTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (BodyColorPositionBuffer buffer = new BodyColorPositionBuffer(device))
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
