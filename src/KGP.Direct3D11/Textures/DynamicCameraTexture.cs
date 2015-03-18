using KGP.Direct3D11.Descriptors;
using KGP.Frames;
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
    /// Dynamic camera space texture
    /// </summary>
    public class DynamicCameraTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;

        /// <summary>
        /// Shader resource view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.rawView; }
        }

        /// <summary>
        /// Creates a dynamic camera texture
        /// </summary>
        /// <param name="device">Direct3D11 device</param>
        public DynamicCameraTexture(Device device)
        {
            this.texture = new Texture2D(device, CameraTextureDescriptors.DynamicRGBA);
            this.rawView = new ShaderResourceView(device, this.texture);
        }

        /// <summary>
        /// Copies frame data to GPU
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="frame">Frame data</param>
        public void Copy(DeviceContext context, CameraRGBAFrameData frame)
        {
            this.texture.Upload(context, frame.DataPointer, frame.SizeInBytes);
        }

        /// <summary>
        /// Disposes gpu resources
        /// </summary>
        public void Dispose()
        {
            this.rawView.Dispose();
            this.texture.Dispose();
        }
    }
}
