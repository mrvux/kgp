using Microsoft.Kinect;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Simple class to hold Kinect joint positions only (with sharpdx vector)
    /// </summary>
    public class KinectJointTable
    {
        private readonly ulong trackingId;
        private readonly IReadOnlyDictionary<JointType, Vector3> joints;

        /// <summary>
        /// Tracking id
        /// </summary>
        public ulong TrackingId
        {
            get { return this.trackingId; }
        }

        /// <summary>
        /// Joint dictionary
        /// </summary>
        public IReadOnlyDictionary<JointType, Vector3> Joints
        {
            get { return this.joints; }
        }

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="trackingId">Tracking id</param>
        /// <param name="joints">Joint table</param>
        public KinectJointTable(ulong trackingId, IReadOnlyDictionary<JointType, Vector3> joints)
        {
            this.trackingId = trackingId;
            this.joints = joints;
        }

        /// <summary>
        /// Returns a new joint table transformed by a matrix
        /// </summary>
        /// <param name="transform">Transformation matrix</param>
        /// <returns>Transformed joint table</returns>
        public KinectJointTable Transform(Matrix transform)
        {
            Dictionary<JointType, Vector3> jointPositions = new Dictionary<JointType,Vector3>();
            foreach (var kvp in this.joints)
            {
                jointPositions.Add(kvp.Key, Vector3.TransformCoordinate(kvp.Value, transform));
            }
            return new KinectJointTable(this.trackingId, jointPositions);
        }
    }
}
