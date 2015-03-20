using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Color space version of kinect body
    /// </summary>
    public class ColorSpaceKinectJoints
    {
        private readonly Dictionary<JointType, ColorSpacePoint> jointPositions;

        /// <summary>
        /// Joint positions
        /// </summary>
        public IReadOnlyDictionary<JointType, ColorSpacePoint> JointPositions
        {
            get { return this.jointPositions; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="body">Kinect body</param>
        /// <param name="coordinateMapper">Coordinate mapper</param>
        public ColorSpaceKinectJoints(KinectBody body, CoordinateMapper coordinateMapper)
        {
            if (body == null)
                throw new ArgumentNullException("body");
            if (coordinateMapper == null)
                throw new ArgumentNullException("coordinateMapper");

            this.jointPositions = new Dictionary<JointType,ColorSpacePoint>();

            foreach (Joint joint in body.Joints.Values)
            {
                this.jointPositions.Add(joint.JointType, coordinateMapper.MapCameraPointToColorSpace(joint.Position));
            }
        }
    }
}
