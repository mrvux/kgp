using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Body
{
    /// <summary>
    /// Exception thrown when an internal body representation has duplicate joint in list
    /// </summary>
    public class DuplicateJointException : Exception
    {
        private readonly JointType joint;

        /// <summary>
        /// Joint type
        /// </summary>
        public JointType Joint
        {
            get { return this.joint; }
        }

        /// <summary>
        /// Constructs a duplicate joint exception
        /// </summary>
        /// <param name="joint">Affected joint</param>
        public DuplicateJointException(JointType joint)
        {
            this.joint = joint;
        }
    }
}
