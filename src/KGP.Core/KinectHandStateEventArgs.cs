using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{  
    /// <summary>
    /// Event args 
    /// </summary>
    public class KinectHandStateEventArgs : EventArgs
    {
        private readonly KinectBody body;
        private readonly HandType handType;
        private readonly HandState previousHandState;

        /// <summary>
        /// Relevant Kinect body
        /// </summary>
        public KinectBody Body
        {
            get { return this.body; }
        }

        /// <summary>
        /// Hand type (left or right)
        /// </summary>
        public HandType HandType
        {
            get { return this.handType; }
        }

        /// <summary>
        /// Previous hand state
        /// </summary>
        public HandState PreviousHandState
        {
            get { return this.previousHandState; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body">Kinect body</param>
        /// <param name="handType">Hand type</param>
        /// <param name="previousHandState">Previous hand state</param>
        public KinectHandStateEventArgs(KinectBody body, HandType handType, HandState previousHandState)
        {
            if (body == null)
                throw new ArgumentNullException("body");

            this.body = body;
            this.handType = handType;
            this.previousHandState = previousHandState;
        }
    }   
        
}
