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
    /// Dynamic depth texture
    /// </summary>
    public class DynamicDepthTexture : IKinectDepthTexture, IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView rawView;
        private ShaderResourceView normalizedView;

        /// <summary>
        /// Raw shader view
        /// </summary>
        public ShaderResourceView RawView
        {
            get { return this.rawView; }
        }

        /// <summary>
        /// Normalized shader view
        /// </summary>
        public ShaderResourceView NormalizedView
        {
            get { return this.normalizedView; }
        }

        /// <summary>
        /// Creates a dynamic depth texture, allocates GPU resources
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        public DynamicDepthTexture(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            this.texture = new Texture2D(device, DepthTextureDescriptors.DynamicResource);
            this.rawView = new ShaderResourceView(device,this.texture, DepthTextureDescriptors.RawView);
            this.normalizedView = new ShaderResourceView(device,this.texture, DepthTextureDescriptors.NormalizedView);
        }

        /// <summary>
        /// Copy depth frame to graphics card
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="data">Depth frame data</param>
        public void Copy(DeviceContext context, DepthFrameData data)
        {
            this.texture.Upload(context, data.DataPointer, data.SizeInBytes);
        }

        /// <summary>
        /// Disposes gpu resources
        /// </summary>
        public void Dispose()
        {
            this.rawView.Dispose();
            this.normalizedView.Dispose();
            this.texture.Dispose();
        }
    }
}
