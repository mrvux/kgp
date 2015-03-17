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
    /// Utility class to get body index texture descriptors
    /// </summary>
    public static class BodyIndexTextureDescriptors
    {
        public static Texture2DDescription DynamicResource
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R8_Typeless);
            }
        }

        public static Texture2DDescription ImmutableResource
        {
            get
            {
                return DescriptorUtils.GetImmutableTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R8_Typeless);
            }
        }

        /// <summary>
        /// Raw body index shader view, cannot be sampled, so Use Load or [] in shaders to access it
        /// </summary>
        public static ShaderResourceViewDescription RawView
        {
            get
            {
                return DescriptorUtils.FormattedResourceView(Format.R8_UInt);
            }
        }

        /// <summary>
        /// Normalized body index shader view, can be sampled
        /// </summary>
        public static ShaderResourceViewDescription NormalizedView
        {
            get
            {
                return DescriptorUtils.FormattedResourceView(Format.R8_UNorm);
            }
        }
    }
}
