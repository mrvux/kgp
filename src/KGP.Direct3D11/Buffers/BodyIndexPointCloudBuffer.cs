using KGP.Direct3D11.Descriptors;
using Microsoft.Kinect;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.Buffers
{
    /// <summary>
    /// Represent an buffer that will hold body index data, no flags required
    /// </summary>
    public unsafe class BodyIndexPointCloudBuffer : IDisposable
    {
        private SharpDX.Direct3D11.Buffer buffer;
        private ShaderResourceView shaderView;
        private UnorderedAccessView unorderedView;

        /// <summary>
        /// Shader view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.shaderView; }
        }

        /// <summary>
        /// Unordered view
        /// </summary>
        public UnorderedAccessView UnorderedView
        {
            get { return this.unorderedView; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device">Direct3D11 Device</param>
        public BodyIndexPointCloudBuffer(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            this.buffer = new SharpDX.Direct3D11.Buffer(device, PointCloudDescriptors.Buffer(4));
            this.shaderView = new ShaderResourceView(device, this.buffer);
            this.unorderedView = new UnorderedAccessView(device, this.buffer, PointCloudDescriptors.WriteableView);
        }

        /// <summary>
        /// Dispose GPU resources
        /// </summary>
        public void Dispose()
        {
            this.shaderView.Dispose();
            this.unorderedView.Dispose();
            this.buffer.Dispose();
        }
    }
}
