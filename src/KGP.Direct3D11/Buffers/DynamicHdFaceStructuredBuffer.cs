using KGP.Direct3D11.DataTables;
using KGP.Direct3D11.Descriptors;
using Microsoft.Kinect;
using Microsoft.Kinect.Face;
using SharpDX;
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
    /// Represent an index buffer for Hd face.
    /// <remarks>We can prefix several buffers in order to batch data more easily when required</remarks>
    /// </summary>
    public unsafe class DynamicHdFaceStructuredBuffer : IDisposable
    {
        private SharpDX.Direct3D11.Buffer buffer;
        private ShaderResourceView shaderView;

        /// <summary>
        /// Shader resource view
        /// </summary>
        public ShaderResourceView ShaderView
        {
            get { return this.shaderView; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device">Direct3D11 Device</param>
        /// <param name="maxFaceCount">Maximum body count</param>
        public DynamicHdFaceStructuredBuffer(Device device, int maxFaceCount)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            var desc = DescriptorUtils.DynamicStructuredBuffer(new BufferElementCount(maxFaceCount * (int)Microsoft.Kinect.Face.FaceModel.VertexCount), new BufferStride(12));
            this.buffer = new SharpDX.Direct3D11.Buffer(device, desc);
            this.shaderView = new ShaderResourceView(device, this.buffer);
        }

        /// <summary>
        /// Copies camera space points from face to the gpu
        /// </summary>
        /// <param name="context"></param>
        /// <param name="points"></param>
        public void Copy(DeviceContext context, CameraSpacePoint[] points)
        {
            if (points.Length == 0)
                return;

            fixed (CameraSpacePoint* cptr = &points[0])
            {
                this.buffer.Upload(context, new IntPtr(cptr), points.Length * 12);
            }

            
        }

        /// <summary>
        /// Dispose GPU resources
        /// </summary>
        public void Dispose()
        {
            this.buffer.Dispose();
        }
    }
}
