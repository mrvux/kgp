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
    public unsafe class HdFaceIndexBuffer : IDisposable
    {
        private SharpDX.Direct3D11.Buffer buffer;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="device">Direct3D11 Device</param>
        /// <param name="maxFaceCount">Maximum body count</param>
        public HdFaceIndexBuffer(Device device, int maxFaceCount)
        {
            if (device == null)
                throw new ArgumentNullException("device");

            uint[] data = FaceDataTable.RepeatTable(maxFaceCount);
            var desc = DescriptorUtils.ImmutableIndexBufferUint(new BufferElementCount(data.Length));

            fixed (uint* ptr = &data[0])
            {
                DataStream ds = new DataStream(new IntPtr(ptr), data.Length * sizeof(uint), true, true);
                this.buffer = new SharpDX.Direct3D11.Buffer(device, ds, desc);
            }
        }

        /// <summary>
        /// Attach the index buffer to the pipeline
        /// </summary>
        /// <param name="context">Device context</param>
        public void Attach(DeviceContext context)
        {
            context.InputAssembler.PrimitiveTopology = SharpDX.Direct3D.PrimitiveTopology.TriangleList;
            context.InputAssembler.SetIndexBuffer(this.buffer, SharpDX.DXGI.Format.R32_UInt, 0);
        }

        /// <summary>
        /// Attach the index buffer to the pipeline, and provides an ptional InputLayout
        /// </summary>
        /// <remarks>Since null Input Layout is perfectly valid, attaching null is also (in case we only use SV input semantics)</remarks>
        /// <param name="context">Device Context</param>
        /// <param name="layout">Input Layout</param>
        public void AttachWithLayout(DeviceContext context, InputLayout layout = null)
        {
            context.InputAssembler.InputLayout = layout;
            this.Attach(context);
        }

        /// <summary>
        /// Draws our index buffer
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="faceCount">Tracked face count</param>
        public void Draw(DeviceContext context, int faceCount)
        {
            context.DrawIndexed(faceCount * (int)FaceModel.TriangleCount * 3, 0, 0);
        }

        /// <summary>
        /// Draws our index buffer as an instanced version
        /// </summary>
        /// <param name="context">Device context</param>
        /// <param name="faceCount">Tracked face count</param>
        public void DrawInstanced(DeviceContext context, int faceCount)
        {
            context.DrawIndexedInstanced((int)FaceModel.TriangleCount * 3, faceCount, 0, 0, 0);
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
