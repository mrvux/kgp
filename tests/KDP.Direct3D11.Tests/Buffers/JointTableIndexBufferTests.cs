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
    public class JointTableIndexBufferTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public JointTableIndexBufferTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (JointTableIndexBuffer buffer = new JointTableIndexBuffer(device, 6))
            {
                
            }
        }

        [TestMethod]
        public void TestShouldBind()
        {
            using (JointTableIndexBuffer buffer = new JointTableIndexBuffer(device,6))
            {
                buffer.Attach(device.ImmediateContext);
                Assert.AreEqual(device.ImmediateContext.InputAssembler.PrimitiveTopology, PrimitiveTopology.LineList);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (JointTableIndexBuffer texture = new JointTableIndexBuffer(null, 6))
            {
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestZero()
        {
            using (JointTableIndexBuffer texture = new JointTableIndexBuffer(device, 0))
            {
            }
        }

        public void Dispose()
        {
            device.Dispose();
        }
    }
}
