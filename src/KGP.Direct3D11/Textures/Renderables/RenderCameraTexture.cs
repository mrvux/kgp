using KGP.Direct3D11.Descriptors;
using SharpDX;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Render target version for camera texture
    /// </summary>
    public class RenderCameraTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;
        private RenderTargetView renderView;

        /// <summary>
        /// Shader resource view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.rawView; }
        }

        /// <summary>
        /// Render target view
        /// </summary>
        public RenderTargetView RenderView
        {
            get { return this.renderView; }
        }

        /// <summary>
        /// Creates a Camera render target texture
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        public RenderCameraTexture(Device device)
        {
            this.texture = new Texture2D(device, CameraTextureDescriptors.RenderTargetRGBA);
            this.rawView = new ShaderResourceView(device, this.texture);
            this.renderView = new RenderTargetView(device, this.texture);
        }

        /// <summary>
        /// Dispose GPU resources
        /// </summary>
        public void Dispose()
        {
            this.rawView.Dispose();
            this.texture.Dispose();
            this.renderView.Dispose();
        }
    }
}
