using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Buffers;
using KGP;
using SharpDX.Direct3D;

namespace KDP.Direct3D11.Tests.Textures
{
    [TestClass]
    public class CounterPointCloudBufferTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public CounterPointCloudBufferTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (CounterPointCloudBuffer buffer = new CounterPointCloudBuffer(device))
            {
                Assert.AreNotEqual(buffer.ShaderView.NativePointer, IntPtr.Zero);
                Assert.AreNotEqual(buffer.UnorderedView.NativePointer, IntPtr.Zero);
                Assert.IsTrue(buffer.UnorderedView.Description.Buffer.Flags == SharpDX.Direct3D11.UnorderedAccessViewBufferFlags.Counter);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (CounterPointCloudBuffer texture = new CounterPointCloudBuffer(null))
            {
            }
        }

        public void Dispose()
        {
            device.Dispose();
        }
    }
}
