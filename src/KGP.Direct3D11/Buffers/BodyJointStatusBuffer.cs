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
    /// Represent a Structured Buffer holding body tracking status
    /// </summary>
    public unsafe class BodyJointStatusBuffer : IDisposable
    {
        private SharpDX.Direct3D11.Buffer buffer;
        private ShaderResourceView shaderView;

        /// <summary>
        /// Shader view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.shaderView; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device">Direct3D11 Device</param>
        public BodyJointStatusBuffer(Device device)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            this.buffer = new SharpDX.Direct3D11.Buffer(device, JointBufferDescriptor.DynamicBuffer(new BufferStride(4)));
            this.shaderView = new ShaderResourceView(device, this.buffer);
        }

        /// <summary>
        /// Copies color space joints to gpu
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="body">Body list</param>
        public void Copy(DeviceContext context, IEnumerable<KinectBody> body)
        {
            var jointStatusArray = body.SelectMany(cpj => cpj.Joints.Values.Select(j => (uint)j.TrackingState).ToArray()).ToArray();

            if (jointStatusArray.Length == 0)
                return;

            fixed (uint* cptr = &jointStatusArray[0])
            {
                this.buffer.Upload(context, new IntPtr(cptr), sizeof(uint) * jointStatusArray.Length);
            }
        }

        /// <summary>
        /// Dispose GPU resources
        /// </summary>
        public void Dispose()
        {
            this.shaderView.Dispose();
            this.buffer.Dispose();
        }
    }
}
