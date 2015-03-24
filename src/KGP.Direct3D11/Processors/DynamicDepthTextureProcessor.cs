using KGP.Direct3D11.Textures;
using KGP.Frames;
using KGP.Providers;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Processors
{
    /// <summary>
    /// Simple wrapper to provide a depth texture upload workflow
    /// </summary>
    public class DynamicDepthTextureProcessor : IDisposable
    {
        private readonly IDepthFrameProvider depthFrameProvider;
        private DepthFrameData currentFrameData;
        private DynamicDepthTexture depthTexture;
        private bool upload;

        /// <summary>
        /// Depth Texture
        /// </summary>
        public DynamicDepthTexture Texture
        {
            get { return this.depthTexture; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="depthFrameProvider">Depth frame provider</param>
        /// <param name="device">Direct3D11 device</param>
        public DynamicDepthTextureProcessor(IDepthFrameProvider depthFrameProvider, Device device)
        {
            this.depthFrameProvider = depthFrameProvider;
            this.depthFrameProvider.FrameReceived += FrameReceived;
            this.depthTexture = new DynamicDepthTexture(device);
        }

        private void FrameReceived(object sender, DepthFrameDataEventArgs e)
        {
            this.currentFrameData = e.DepthData;
            this.upload = true;
        }

        /// <summary>
        /// Copy last frame data to gpu, if applicable
        /// </summary>
        /// <param name="context">Device context</param>
        public void Update(DeviceContext context)
        {
            if (upload)
            {
                this.depthTexture.Copy(context, this.currentFrameData);
                upload = false;
            }
        }
        
        /// <summary>
        /// Disposes resources
        /// </summary>
        public void Dispose()
        {
            this.depthTexture.Dispose();
            this.depthFrameProvider.FrameReceived -= FrameReceived;       
        }
    }
}
