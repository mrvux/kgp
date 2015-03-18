using KGP.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Providers
{
    /// <summary>
    /// Event args wrapper for ColorRGBAFrameData
    /// </summary>
    public class ColorRGBAFrameDataEventArgs : EventArgs
    {
        private readonly ColorRGBAFrameData args;

        /// <summary>
        /// Frame data
        /// </summary>
        public ColorRGBAFrameData FrameData
        {
            get { return this.args; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="args">frame data</param>
        public ColorRGBAFrameDataEventArgs(ColorRGBAFrameData args)
        {
            if (args == null)
                throw new ArgumentNullException("args");
            this.args = args;
        }
    }   
        
}
