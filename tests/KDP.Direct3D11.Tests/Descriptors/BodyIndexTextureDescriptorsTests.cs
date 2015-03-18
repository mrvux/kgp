using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Direct3D11.Descriptors;
using SharpDX.Direct3D11;

namespace KDP.Direct3D11.Tests.Descriptors
{
    [TestClass]
    public class BodyIndexTextureDescriptorsTests
    {
        [TestMethod]
        public void TestDynamicResource()
        {
            var desc = BodyIndexTextureDescriptors.DynamicResource;

            var expected = new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.Write,
                Format = SharpDX.DXGI.Format.R8_Typeless,
                Height = 424,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = ResourceUsage.Dynamic,
                Width = 512
            };

            Assert.AreEqual(desc, expected);
        }

        [TestMethod]
        public void TestImmutableResource()
        {
            var desc = BodyIndexTextureDescriptors.ImmutableResource;

            var expected = new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = SharpDX.DXGI.Format.R8_Typeless,
                Height = 424,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = ResourceUsage.Immutable,
                Width = 512
            };

            Assert.AreEqual(desc, expected);
        }

        [TestMethod]
        public void TestRawViewDesc()
        {
            var desc = BodyIndexTextureDescriptors.RawView;

            var expected = new ShaderResourceViewDescription()
            {
                Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                Format = SharpDX.DXGI.Format.R8_UInt,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                {
                    MipLevels = 1,
                    MostDetailedMip = 0
                }
            };

            Assert.AreEqual(desc, expected);
        }

        [TestMethod]
        public void TestNormalizedViewDesc()
        {
            var desc = BodyIndexTextureDescriptors.NormalizedView;

            var expected = new ShaderResourceViewDescription()
            {
                Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                Format = SharpDX.DXGI.Format.R8_UNorm,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                {
                    MipLevels = 1,
                    MostDetailedMip = 0
                }
            };

            Assert.AreEqual(desc, expected);
        }
    }


}
