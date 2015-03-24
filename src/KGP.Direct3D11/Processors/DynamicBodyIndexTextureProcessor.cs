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
    /// Simple wrapper to provide a body index texture upload workflow
    /// </summary>
    public class DynamicBodyIndexTextureProcessor : IDisposable
    {
        private readonly IBodyIndexFrameProvider bodyIndexFrameProvider;
        private BodyIndexFrameData currentFrameData;
        private DynamicBodyIndexTexture bodyIndexTexture;
        private bool upload;

        /// <summary>
        /// Texture
        /// </summary>
        public DynamicBodyIndexTexture Texture
        {
            get { return this.bodyIndexTexture; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bodyIndexFrameProvider">Body index frame provider</param>
        /// <param name="device">Direct3D11 device</param>
        public DynamicBodyIndexTextureProcessor(IBodyIndexFrameProvider bodyIndexFrameProvider, Device device)
        {
            this.bodyIndexFrameProvider = bodyIndexFrameProvider;
            this.bodyIndexFrameProvider.FrameReceived += FrameReceived;
            this.bodyIndexTexture = new DynamicBodyIndexTexture(device);
        }

        private void FrameReceived(object sender, BodyIndexFrameDataEventArgs e)
        {
            this.currentFrameData = e.FrameData;
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
                this.bodyIndexTexture.Copy(context, this.currentFrameData);
                upload = false;
            }
        }
        
        /// <summary>
        /// Disposes resources
        /// </summary>
        public void Dispose()
        {
            this.bodyIndexTexture.Dispose();
            this.bodyIndexFrameProvider.FrameReceived -= FrameReceived;       
        }
    }
}
