using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Descriptors
{
    /// <summary>
    /// Texture descriptors to handle multiple kinects
    /// </summary>
    public static class MultiKinectTextureDescriptors
    {
        /// <summary>
        /// Get descriptor for camera space texture. This is a texture array that can be bound as 
        /// render target for fast depth/world conversion
        /// </summary>
        /// <param name="kinectCount">Kinect count</param>
        /// <returns>Texture descriptor</returns>
        public static Texture2DDescription CameraTexture(int kinectCount)
        {
            return DescriptorUtils.GetRenderTargetTextureArray(new TextureSize(Consts.DepthWidth, Consts.DepthHeight), SharpDX.DXGI.Format.R32G32B32A32_Float, kinectCount);
        }

        /// <summary>
        /// Get a rendertarget view for a specific kinect index
        /// </summary>
        /// <param name="index">Kinect index</param>
        /// <returns>Render target view description</returns>
        public static RenderTargetViewDescription CameraRenderTarget(int index)
        {
            return new RenderTargetViewDescription()
            {
                Dimension = RenderTargetViewDimension.Texture2DArray,
                Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                Texture2DArray = new RenderTargetViewDescription.Texture2DArrayResource()
                {
                    ArraySize = 1,
                    FirstArraySlice = index,
                    MipSlice = 0
                }
            };
        }

        /// <summary>
        /// Get a body index texture that can hold several kinects at once
        /// </summary>
        /// <param name="kinectCount">Kinect count</param>
        /// <returns>Texture descriptor</returns>
        public static Texture2DDescription BodyIndexTexture(int kinectCount)
        {
            return DescriptorUtils.GetRenderTargetTextureArray(new TextureSize(Consts.DepthWidth, Consts.DepthHeight), SharpDX.DXGI.Format.R8_Typeless, kinectCount);
        }

        
    }
}
