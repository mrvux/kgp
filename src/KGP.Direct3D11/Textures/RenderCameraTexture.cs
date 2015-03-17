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

        public ShaderResourceView ShaderView
        {
            get { return this.rawView; }
        }

        public RenderTargetView RenderView
        {
            get { return this.renderView; }
        }

        public RenderCameraTexture(Device device)
        {
            this.texture = new Texture2D(device, CameraTextureDescriptors.RenderTargetRGBA);
            this.rawView = new ShaderResourceView(device, this.texture);
            this.renderView = new RenderTargetView(device, this.texture);
        }

        public void Dispose()
        {
            this.rawView.Dispose();
            this.texture.Dispose();
            this.renderView.Dispose();
        }
    }
}
