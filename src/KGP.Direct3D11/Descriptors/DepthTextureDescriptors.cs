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
    /// Utility class to get depth texture descriptors
    /// </summary>
    public static class DepthTextureDescriptors
    {
        /// <summary>
        /// Get a dynamic texture description for depth texture. Please not format is set as typeless,
        /// which allows us to provide both a raw view and a normalized view
        /// </summary>
        public static Texture2DDescription DynamicResource
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R16_Typeless);
            }
        }

        /// <summary>
        /// Get an immutable resource description for depth texture, format set as typeless
        /// </summary>
        public static Texture2DDescription ImmutableResource
        {
            get
            {
                return DescriptorUtils.GetImmutableTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R16_Typeless);
            }
        }

        /// <summary>
        /// Raw depth texture view, cannot be sampled and must beused as uint typed Texture2D
        /// </summary>
        public static ShaderResourceViewDescription RawView
        {
            get
            {
                return DescriptorUtils.FormattedResourceView(Format.R16_UInt);
            }
        }

        /// <summary>
        /// Normalized depth view
        /// </summary>
        public static ShaderResourceViewDescription NormalizedView
        {
            get
            {
                return DescriptorUtils.FormattedResourceView(Format.R16_UNorm);
            }
        }
    }
}
