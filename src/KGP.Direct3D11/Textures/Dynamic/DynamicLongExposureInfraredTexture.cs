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
    /// Dynamic long exposure infrared texture
    /// </summary>
    public class DynamicLongExposureInfraredTexture : IDisposable
    {
        private Texture2D texture;
        private ShaderResourceView shaderView;

        /// <summary>
        /// Shader resource view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.shaderView; }
        }

        /// <summary>
        /// Creates a dynamic depth texture, allocates GPU resources
        /// </summary>
        /// <param name="device">Direct3D Device</param>
        public DynamicLongExposureInfraredTexture(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            this.texture = new Texture2D(device, InfraredTextureDescriptors.DynamicResource);
            this.shaderView = new ShaderResourceView(device, this.texture);
        }

        /// <summary>
        /// Copy depth frame to graphics card
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="data">Long exposure frame data</param>
        public void Copy(DeviceContext context, LongExposureInfraredFrameData data)
        {
            this.texture.Upload(context, data.DataPointer, data.SizeInBytes);
        }

        /// <summary>
        /// Disposes gpu resources
        /// </summary>
        public void Dispose()
        {
            this.shaderView.Dispose();
            this.texture.Dispose();
        }
    }
}
