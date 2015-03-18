using KGP.Direct3D11.Descriptors;
using KGP.Frames;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Textures
{
    /// <summary>
    /// Dynamic rgba color texture
    /// </summary>
    public class DynamicColorRGBATexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;

        /// <summary>
        /// Raw shader view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.rawView; }
        }

        /// <summary>
        /// Creates a dynamic color texture, allocates GPU resources
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        public DynamicColorRGBATexture(Device device)
        {
            this.texture = new Texture2D(device, ColorTextureDescriptors.DynamicRGBAResource);
            this.rawView = new ShaderResourceView(device,this.texture);
        }

        /// <summary>
        /// Copy depth frame to graphics card
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="data">Color frame data</param>
        public void Copy(DeviceContext context, ColorRGBAFrameData data)
        {
            this.texture.Upload(context, data.DataPointer, data.SizeInBytes);
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
