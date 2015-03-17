using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Standard event args wrapper for kinect body events
    /// </summary>
    public class KinectBodyEventArgs : EventArgs
    {
        private readonly KinectBody body;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body">Relevant Kinect body</param>
        public KinectBodyEventArgs(KinectBody body)
        {
            this.body = body;
        }
       
        /// <summary>
        /// Kinect body instance raised by event args
        /// </summary>
        public KinectBody Body
        {
            get { return this.body; }
        } 
    }
}
