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
    /// Utility for lookup textures.
    /// </summary>
    public static class LookupTableTextureDescriptors
    {
        /// <summary>
        /// Depth to camera ray table. Used to reconstruct depth to camera space in hlsl.
        /// Since this texture is only needed once, we only provide an immutable version
        /// </summary>
        public static Texture2DDescription DepthToCameraRayTable
        {
            get
            {
                return DescriptorUtils.GetImmutableTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R32G32_Float);
            }
        }
    }
}
