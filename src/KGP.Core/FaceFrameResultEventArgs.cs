using Microsoft.Kinect.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Event args wrapper fro Frace frame result
    /// </summary>
    public class FaceFrameResultEventArgs
    {
        private readonly FaceFrameResult frameResult;

        /// <summary>
        /// Frame result
        /// </summary>
        public FaceFrameResult FrameResult
        {
            get { return this.frameResult; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="frameResult">Frame result</param>
        public FaceFrameResultEventArgs(FaceFrameResult frameResult)
        {
            if (frameResult == null)
                throw new ArgumentNullException("frameResult");

            this.frameResult = frameResult;
        }
    }
}
