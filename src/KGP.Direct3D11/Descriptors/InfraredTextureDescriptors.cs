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
    public static class InfraredTextureDescriptors
    {
        /// <summary>
        /// Dynamic descriptor for infrared texture
        /// </summary>
        public static Texture2DDescription DynamicResource
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(new TextureSize(Consts.DepthWidth, Consts.DepthHeight), Format.R16_UNorm);
            }
        }

        /// <summary>
        /// Immutable descriptor for infrared texture
        /// </summary>
        public static Texture2DDescription ImmutableResource
        {
            get
            {
                return DescriptorUtils.GetImmutableTexture(new TextureSize(Consts.DepthWidth, Consts.DepthHeight), Format.R16_UNorm);
            }
        }
    }
}
