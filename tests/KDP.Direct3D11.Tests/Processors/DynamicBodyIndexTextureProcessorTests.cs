using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Processors;
using KGP.Providers;

namespace KDP.Direct3D11.Tests.Textures
{
    public class DummyBodyIndexProvider : IBodyIndexFrameProvider , IDisposable
    {
        private BodyIndexFrameData frameData;

        public BodyIndexFrameData FrameData
        {
            get { return this.frameData; }
        }

        public DummyBodyIndexProvider()
        {
            this.frameData = new BodyIndexFrameData();
        }

        public void PushFrame()
        {
            if (this.FrameReceived!= null)
            {
                this.FrameReceived(this, new BodyIndexFrameDataEventArgs(this.frameData));
            }
        }

        public void Dispose()
        {
            this.frameData.Dispose();
        }

        public event EventHandler<BodyIndexFrameDataEventArgs> FrameReceived;
    }


    [TestClass]
    public class DynamicBodyIndexTextureProcessorTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public DynamicBodyIndexTextureProcessorTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DummyBodyIndexProvider provider = new DummyBodyIndexProvider())
            {
                using (DynamicBodyIndexTextureProcessor textureProcessor = new DynamicBodyIndexTextureProcessor(provider, device))
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
            using (DummyBodyIndexProvider provider = new DummyBodyIndexProvider())
            {
                using (DynamicBodyIndexTextureProcessor textureProcessor = new DynamicBodyIndexTextureProcessor(provider, null))
                {
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullProvider()
        {
            using (DynamicBodyIndexTextureProcessor textureProcessor = new DynamicBodyIndexTextureProcessor(null, device))
            {
            }
        }

        [TestMethod]
        public void TestReceive()
        {
            using (DummyBodyIndexProvider provider = new DummyBodyIndexProvider())
            {
                using (DynamicBodyIndexTextureProcessor textureProcessor = new DynamicBodyIndexTextureProcessor(provider, device))
                {
                    provider.PushFrame();
                    Assert.IsTrue(textureProcessor.NeedUpdate);
                }
            }
        }

        [TestMethod]
        public void TestUpload()
        {
            using (DummyBodyIndexProvider provider = new DummyBodyIndexProvider())
            {
                using (DynamicBodyIndexTextureProcessor textureProcessor = new DynamicBodyIndexTextureProcessor(provider, device))
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
