using KGP.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Event args wrapper for an infrared frame event
    /// </summary>
    public class InfraredFrameDataEventArgs : EventArgs
    {
        private readonly InfraredFrameData frameData;

        /// <summary>
        /// Infrared frame data
        /// </summary>
        public InfraredFrameData FrameData
        {
            get { return this.frameData; }
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="frameData">Frame data</param>
        public InfraredFrameDataEventArgs(InfraredFrameData frameData)
        {
            if (frameData == null)
                throw new ArgumentNullException("frameData");

            this.frameData = frameData;
        }
    }
}
