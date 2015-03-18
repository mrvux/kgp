using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Frames
{
    /// <summary>
    /// Camera data frame data store, uses unmanaged memory back end
    /// </summary>
    public class CameraRGBAFrameData : IDisposable
    {
        private int sizeInBytes;
        private IntPtr dataPointer;

        /// <summary>
        /// Data pointer for frame data
        /// </summary>
        public IntPtr DataPointer
        {
            get
            {
                if (this.dataPointer == IntPtr.Zero)
                    throw new ObjectDisposedException("CameraRGBAFrameData");

                return this.dataPointer;
            }
        }

        /// <summary>
        /// Frame size, in bytes
        /// </summary>
        public int SizeInBytes
        {
            get { return this.sizeInBytes; }
        }

        /// <summary>
        /// Constructor, allocates memory to hold frame data
        /// </summary>
        public CameraRGBAFrameData()
        {
            this.sizeInBytes = Consts.DepthWidth * Consts.DepthHeight * sizeof(float) * 4;
            this.dataPointer = Marshal.AllocHGlobal(this.sizeInBytes);
        }

        /// <summary>
        /// Dispose memory for frame data
        /// </summary>
        public void Dispose()
        {
            if (this.dataPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.dataPointer);
                this.dataPointer = IntPtr.Zero;
                this.sizeInBytes = 0;
            }
        }
    }   
        
}
