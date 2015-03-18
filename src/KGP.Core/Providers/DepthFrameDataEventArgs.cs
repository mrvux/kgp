using KGP.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Event args wrapper for a depth frame event
    /// </summary>
    public class DepthFrameDataEventArgs : EventArgs
    {
        private readonly DepthFrameData depthData;

        /// <summary>
        /// Depth frame data
        /// </summary>
        public DepthFrameData DepthData
        {
            get { return this.depthData; }
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="depthData">Depth Frame data</param>
        public DepthFrameDataEventArgs(DepthFrameData depthData)
        {
            if (depthData == null)
                throw new ArgumentNullException("depthData");

            this.depthData = depthData;
        }
    }
}
