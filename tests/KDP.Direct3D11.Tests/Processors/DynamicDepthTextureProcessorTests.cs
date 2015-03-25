using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Processors;
using KGP.Providers;

namespace KDP.Direct3D11.Tests.Textures
{
    public class DummyDepthProvider : IDepthFrameProvider , IDisposable
    {
        private DepthFrameData frameData;

        public DepthFrameData FrameData
        {
            get { return this.frameData; }
        }

        public DummyDepthProvider()
        {
            this.frameData = new DepthFrameData();
        }

        public void PushFrame()
        {
            if (this.FrameReceived!= null)
            {
                this.FrameReceived(this, new DepthFrameDataEventArgs(this.frameData));
            }
        }

        public void Dispose()
        {
            this.frameData.Dispose();
        }

        public event EventHandler<DepthFrameDataEventArgs> FrameReceived;
    }


    [TestClass]
    public class DynamicDepthTextureProcessorTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public DynamicDepthTextureProcessorTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DummyDepthProvider provider = new DummyDepthProvider())
            {
                using (DynamicDepthTextureProcessor textureProcessor = new DynamicDepthTextureProcessor(provider, device))
                {
                    Assert.IsFalse(textureProcessor.Texture.NormalizedView.NativePointer == IntPtr.Zero);
                    Assert.IsFalse(textureProcessor.Texture.NormalizedView.NativePointer == IntPtr.Zero);
                    Assert.IsFalse(textureProcessor.NeedUpdate);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (DummyDepthProvider provider = new DummyDepthProvider())
            {
                using (DynamicDepthTextureProcessor textureProcessor = new DynamicDepthTextureProcessor(provider, null))
                {
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullProvider()
        {
            using (DynamicDepthTextureProcessor textureProcessor = new DynamicDepthTextureProcessor(null, device))
            {
            }
        }

        [TestMethod]
        public void TestReceive()
        {
            using (DummyDepthProvider provider = new DummyDepthProvider())
            {
                using (DynamicDepthTextureProcessor textureProcessor = new DynamicDepthTextureProcessor(provider, device))
                {
                    provider.PushFrame();
                    Assert.IsTrue(textureProcessor.NeedUpdate);
                }
            }
        }

        [TestMethod]
        public void TestUpload()
        {
            using (DummyDepthProvider provider = new DummyDepthProvider())
            {
                using (DynamicDepthTextureProcessor textureProcessor = new DynamicDepthTextureProcessor(provider, device))
                {
                    provider.PushFrame();
                    Assert.IsTrue(textureProcessor.NeedUpdate);
                    textureProcessor.Update(device.ImmediateContext);
                    Assert.IsFalse(textureProcessor.NeedUpdate);
                }
            }
        }


        public void Dispose()
        {
            device.Dispose();
        }
    }
}
