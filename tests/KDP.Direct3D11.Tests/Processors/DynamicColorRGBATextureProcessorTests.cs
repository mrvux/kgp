using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Direct3D11.Processors;
using KGP.Providers;

namespace KDP.Direct3D11.Tests.Textures
{
    public class DummyColorRGBAProvider : IColorRGBAFrameProvider , IDisposable
    {
        private ColorRGBAFrameData frameData;

        public ColorRGBAFrameData FrameData
        {
            get { return this.frameData; }
        }

        public DummyColorRGBAProvider()
        {
            this.frameData = new ColorRGBAFrameData();
        }

        public void PushFrame()
        {
            if (this.FrameReceived!= null)
            {
                this.FrameReceived(this, new ColorRGBAFrameDataEventArgs(this.frameData));
            }
        }

        public void Dispose()
        {
            this.frameData.Dispose();
        }

        public event EventHandler<ColorRGBAFrameDataEventArgs> FrameReceived;
    }


    [TestClass]
    public class DynamicColorRGBATextureProcessorTests : IDisposable
    {
        private SharpDX.Direct3D11.Device device;


        public DynamicColorRGBATextureProcessorTests()
        {
            device = new SharpDX.Direct3D11.Device(SharpDX.Direct3D.DriverType.Reference);
        }

        [TestMethod]
        public void TestCreate()
        {
            using (DummyColorRGBAProvider provider = new DummyColorRGBAProvider())
            {
                using (DynamicColorRGBATextureProcessor textureProcessor = new DynamicColorRGBATextureProcessor(provider, device))
                {
                    Assert.IsFalse(textureProcessor.Texture.ShaderView.NativePointer == IntPtr.Zero);
                    Assert.IsFalse(textureProcessor.NeedUpdate);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullDevice()
        {
            using (DummyColorRGBAProvider provider = new DummyColorRGBAProvider())
            {
                using (DynamicColorRGBATextureProcessor textureProcessor = new DynamicColorRGBATextureProcessor(provider, null))
                {
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNullProvider()
        {
            using (DynamicColorRGBATextureProcessor textureProcessor = new DynamicColorRGBATextureProcessor(null, device))
            {
            }
        }

        [TestMethod]
        public void TestReceive()
        {
            using (DummyColorRGBAProvider provider = new DummyColorRGBAProvider())
            {
                using (DynamicColorRGBATextureProcessor textureProcessor = new DynamicColorRGBATextureProcessor(provider, device))
                {
                    provider.PushFrame();
                    Assert.IsTrue(textureProcessor.NeedUpdate);
                }
            }
        }

        [TestMethod]
        public void TestUpload()
        {
            using (DummyColorRGBAProvider provider = new DummyColorRGBAProvider())
            {
                using (DynamicColorRGBATextureProcessor textureProcessor = new DynamicColorRGBATextureProcessor(provider, device))
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
