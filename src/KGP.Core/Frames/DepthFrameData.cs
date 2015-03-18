using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Frames
{
    /// <summary>
    /// Depth frame data store, uses unmanaged memory back end
    /// </summary>
    public class DepthFrameData : IDisposable
    {
        private IntPtr dataPointer;
        private int dataSize;

        /// <summary>
        /// Data pointer for depth frame data
        /// </summary>
        public IntPtr DataPointer
        {
            get 
            {
                if (this.dataPointer == IntPtr.Zero)
                    throw new ObjectDisposedException("DepthFrameData");

                return this.dataPointer; 
            }
        }

        /// <summary>
        /// Size of data frame, in bytes
        /// </summary>
        public int SizeInBytes
        {
            get { return this.dataSize; }
        }

        /// <summary>
        /// Constructor, allocates memory to hold depth frame
        /// </summary>
        public DepthFrameData()
        {
            this.dataSize = Consts.DepthWidth * Consts.DepthHeight * sizeof(ushort);
            this.dataPointer = Marshal.AllocHGlobal(dataSize);
        }

        /// <summary>
        /// Dispose memory for depth frame data
        /// </summary>
        public void Dispose()
        {
            if (this.dataPointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(this.dataPointer);
                this.dataPointer = IntPtr.Zero;
                this.dataSize = 0;
            }   
        }
    }
}
