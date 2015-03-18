using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Body
{
    /// <summary>
    /// Internal mutable Kinect body class, used for serialization only
    /// </summary>
    public class KinectBodyInternal
    {
        /// <see cref="Microsoft.Kinect.Body.ClippedEdges"/>
        public FrameEdges ClippedEdges;

        /// <see cref="Microsoft.Kinect.Body.HandLeftConfidence"/>
        public TrackingConfidence HandLeftConfidence;

        /// <see cref="Microsoft.Kinect.Body.HandLeftState"/>
        public HandState HandLeftState;

        /// <see cref="Microsoft.Kinect.Body.HandRightConfidence"/>
        public TrackingConfidence HandRightConfidence;

        /// <see cref="Microsoft.Kinect.Body.HandRightState"/>
        public HandState HandRightState;

        /// <see cref="Microsoft.Kinect.Body.IsRestricted"/>
        public bool IsRestricted;

        /// <see cref="Microsoft.Kinect.Body.IsTracked"/>
        public bool IsTracked;

        /// <see cref="Microsoft.Kinect.Body.JointOrientations"/>
        public JointOrientation[] JointOrientations;

        /// <see cref="Microsoft.Kinect.Body.Joints"/>
        public Joint[] Joints;

        /// <see cref="Microsoft.Kinect.Body.Lean"/>
        public PointF Lean;

        /// <see cref="Microsoft.Kinect.Body.LeanTrackingState"/>
        public TrackingState LeanTrackingState;

        /// <see cref="Microsoft.Kinect.Body.TrackingId"/>
        public ulong TrackingId;

        /// <summary>
        /// Validates our internal model
        /// <exception cref="System.ArgumentNullException">Thrown when either JointOrientations or Joints property is null</exception>
        /// <exception cref="System.ArgumentException">Thrown when either Joint or Joint property is not valid, which can happen when joint data is incomplete or a joint is registered several times</exception>
        /// </summary>
        public void Validate()
        {
            if (JointOrientations == null)
                throw new ArgumentNullException("JointOrientations");
            
            if (Joints == null)
                throw new ArgumentNullException("Joints");

            if (JointOrientations.Length != Microsoft.Kinect.Body.JointCount)
                throw new ArgumentException("JointOrientations", "Joint orientations count should match kinect joint count");

            if (Joints.Length != Microsoft.Kinect.Body.JointCount)
                throw new ArgumentException("Joints", "Joint count should match kinect joint count");

            HashSet<JointType> set = new HashSet<JointType>();
            for (int i = 0; i < Joints.Length; i++)
            {
                JointType type = this.Joints[i].JointType;
                if (!set.Contains(type))
                {
                    set.Add(type);
                }
                else
                {
                    throw new DuplicateJointException(type); 
                }
            }
        }
    }
}
