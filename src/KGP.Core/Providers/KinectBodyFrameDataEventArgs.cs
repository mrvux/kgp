using KGP.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{ 
    /// <summary>
    ///  Event args wrapper for <see cref="KinectBody"/>
    /// </summary>
    public class KinectBodyFrameDataEventArgs : EventArgs
    {
        private readonly KinectBody[] args;

        /// <summary>
        /// Receive frame data
        /// </summary>
        public KinectBody[] FrameData
        {
            get { return this.args; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public KinectBodyFrameDataEventArgs(KinectBody[] args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            this.args = args;
        }
    }   
        
}
