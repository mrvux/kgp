using KGP.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{ 
    /// <summary>
    ///  Event args wrapper for <see cref="BodyIndexFrameData"/>
    /// </summary>
    public class BodyIndexFrameDataEventArgs : EventArgs
    {
        private readonly BodyIndexFrameData args;

        /// <summary>
        /// Receive frame data
        /// </summary>
        public BodyIndexFrameData FrameData
        {
            get { return this.args; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public BodyIndexFrameDataEventArgs(BodyIndexFrameData args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            this.args = args;
        }
    }   
        
}
