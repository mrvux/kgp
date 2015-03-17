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
    /// Utility class to get camera view space texture descriptors
    /// </summary>
    public static class CameraTextureDescriptors
    {
        /// <summary>
        /// RGBA Version for the camera space texture description. 
        /// Conversion and padding must be performed in cpu
        /// </summary>
        public static Texture2DDescription DynamicRGBA
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R32G32B32A32_Float);
            }
        }

        /// <summary>
        /// RGBA Version for the camera space texture description. 
        /// Conversion and padding must be performed in cpu
        /// </summary>
        public static Texture2DDescription ImmutableRGBA
        {
            get
            {
                return DescriptorUtils.GetImmutableTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R32G32B32A32_Float);
            }
        }

        /// <summary>
        /// RenderTarget Version for the camera space texture description. 
        /// This can be used if we wish to perform the ray cast operation in hlsl
        /// </summary>
        public static Texture2DDescription RenderTargetRGBA
        {
            get
            {
                return DescriptorUtils.GetRenderTargetTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R32G32B32A32_Float);
            }
        }

        /// <summary>
        /// RGB Version for the camera space texture description.
        /// <remarks>This format CANNOT be sampled. This can crash your driver if you attempt to do so</remarks>
        /// </summary>
        public static Texture2DDescription DynamicRGB
        {
            get
            {
                return DescriptorUtils.GetDynamicTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R32G32B32_Float);
            }
        }

        /// <summary>
        /// RGB Version for the camera space texture description.
        /// <remarks>This format CANNOT be sampled. This can crash your driver if you attempt to do so</remarks>
        /// </summary>
        public static Texture2DDescription ImmutableRGB
        {
            get
            {
                return DescriptorUtils.GetImmutableTexture(Consts.DepthWidth, Consts.DepthHeight, Format.R32G32B32_Float);
            }
        }
    }
}
