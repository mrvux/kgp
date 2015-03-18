using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Descriptors
{
    /// <summary>
    /// Utility class to get texture and shader resource view descriptions
    /// </summary>
    public static partial class DescriptorUtils
    {
        /// <summary>
        /// Gets a dynamic texture description, data must be uploaded using a device context
        /// </summary>
        /// <param name="w">Texture width</param>
        /// <param name="h">Texture height</param>
        /// <param name="format">Texture format</param>
        /// <returns>Direct3D11 Texture description</returns>
        public static Texture2DDescription GetDynamicTexture(int w, int h, Format format)
        {
            return new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.Write,
                Format = format,
                Height = h,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1,0),
                Usage = ResourceUsage.Dynamic,
                Width = w
            };
        }

        /// <summary>
        /// Gets an immutable texture description, data can be uploaded thread free
        /// </summary>
        /// <param name="w">Texture width</param>
        /// <param name="h">Texture height</param>
        /// <param name="format">Texture format</param>
        /// <returns>Direct3D11 Texture description</returns>
        public static Texture2DDescription GetImmutableTexture(int w, int h, Format format)
        {
            return new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = format,
                Height = h,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Immutable,
                Width = w
            };
        }

        /// <summary>
        /// Gets a render target texture description
        /// </summary>
        /// <param name="w">Texture width</param>
        /// <param name="h">Texture height</param>
        /// <param name="format">Texture format</param>
        /// <returns>Direct3D11 Texture description</returns>
        public static Texture2DDescription GetRenderTargetTexture(int w, int h, Format format)
        {
            return new Texture2DDescription()
            {
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = format,
                Height = h,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                Width = w
            };
        }

        /// <summary>
        /// Builds resource description for a render target texture array
        /// </summary>
        /// <param name="w">Texture width</param>
        /// <param name="h">Texture height</param>
        /// <param name="format">Texture format</param>
        /// <param name="arraySize">Texture array element count</param>
        /// <returns>Texture2D Description</returns>
        public static Texture2DDescription GetRenderTargetTextureArray(int w, int h, Format format, int arraySize)
        {
            return new Texture2DDescription()
            {
                ArraySize = arraySize,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = format,
                Height = h,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                Width = w
            };
        }

        /// <summary>
        /// Returns a specific view format for a Direct3D Texture, in case we want a specific view format
        /// </summary>
        /// <param name="format">View format</param>
        /// <returns>Shader resource view for our texture</returns>
        public static ShaderResourceViewDescription FormattedResourceView(Format format)
        {
            return new ShaderResourceViewDescription()
            {
                Dimension = ShaderResourceViewDimension.Texture2D,
                Format = format,
                Texture2D = new ShaderResourceViewDescription.Texture2DResource()
                {
                    MipLevels = 1,
                    MostDetailedMip = 0
                }
            };
        }
    }
}
