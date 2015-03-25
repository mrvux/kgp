using Microsoft.Kinect;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Static class to provide calibration data
    /// </summary>
    public static class CalibrationBuilderUtils
    {
        /// <summary>
        /// Extension to convert camera space point to sharpdx vector3
        /// </summary>
        /// <param name="point">Camera Space Point</param>
        /// <returns>SharpDX Vector3</returns>
        public static Vector3 ToVector3(this CameraSpacePoint point)
        {
            return new Vector3(point.X, point.Y, point.Z);
        }

        /// <summary>
        /// Builds a list of all tracked joints for two Kinect bodies
        /// </summary>
        /// <param name="body1">First body</param>
        /// <param name="body2">Second Body</param>
        /// <returns>Camera to camera point list</returns>
        public static IEnumerable<CameraToCameraPoint> AllTrackedJoints(KinectBody body1, KinectBody body2)
        {
            return body1.Joints.Where(j => j.Value.TrackingState == TrackingState.Tracked
                && body2.Joints[j.Key].TrackingState == TrackingState.Tracked)
                .Select(kvp => new CameraToCameraPoint(kvp.Value.Position.ToVector3(), body2.Joints[kvp.Key].Position.ToVector3()));
        }

        /// <summary>
        /// Returns camera space matching for left hand, if in tracked state for each KinectBody
        /// </summary>
        /// <param name="body1">First body</param>
        /// <param name="body2">Second Body</param>
        /// <param name="jointType">Joint type to check</param>
        /// <returns>Camera to camera point, or empty enumeration if hand is not tracked</returns>
        public static IEnumerable<CameraToCameraPoint> YieldIfTracked(KinectBody body1, KinectBody body2, JointType jointType)
        {
            var j1 = body1.Joints[jointType];
            var j2 = body2.Joints[jointType];

            if (j1.TrackingState == TrackingState.Tracked && j2.TrackingState == TrackingState.Tracked)
            {
                yield return new CameraToCameraPoint(j1.Position.ToVector3(), j2.Position.ToVector3());
            }
        }

        /// <summary>
        /// Returns camera space matching for left hand, if in tracked state for each KinectBody
        /// </summary>
        /// <param name="body1">First body</param>
        /// <param name="body2">Second Body</param>
        /// <returns>Camera to camera point, or empty enumeration if hand is not tracked</returns>
        public static IEnumerable<CameraToCameraPoint> HandLeft(KinectBody body1, KinectBody body2)
        {
            return YieldIfTracked(body1, body2, JointType.HandLeft);
        }

        /// <summary>
        /// Returns camera space matching for right hand, if in tracked state for each KinectBody
        /// </summary>
        /// <param name="body1">First body</param>
        /// <param name="body2">Second Body</param>
        /// <returns>Camera to camera point, or empty enumeration if hand is not tracked</returns>
        public static IEnumerable<CameraToCameraPoint> HandRight(KinectBody body1, KinectBody body2)
        {
            return YieldIfTracked(body1, body2, JointType.HandRight);
        }
    }
}
