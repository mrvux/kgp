using KGP.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Event args wrapper for a long exposure infrared frame event
    /// </summary>
    public class LongExposureInfraredFrameDataEventArgs : EventArgs
    {
        private readonly LongExposureInfraredFrameData frameData;

        /// <summary>
        /// Infrared frame data
        /// </summary>
        public LongExposureInfraredFrameData FrameData
        {
            get { return this.frameData; }
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="frameData">Frame data</param>
        public LongExposureInfraredFrameDataEventArgs(LongExposureInfraredFrameData frameData)
        {
            if (frameData == null)
                throw new ArgumentNullException("frameData");

            this.frameData = frameData;
        }
    }
}
