using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Body
{
    /// <summary>
    /// Responsible to convert kinect body to an internal and easy to serialize version
    /// </summary>
    public static class KinectBodyConverter
    {
        /// <summary>
        /// Converts body to it's internal representation
        /// </summary>
        /// <param name="body">Body to convert</param>
        /// <returns>Converted body</returns>
        public static KinectBodyInternal Convert(KinectBody body)
        {
            KinectBodyInternal result = new KinectBodyInternal()
            {
                ClippedEdges = body.ClippedEdges,
                HandLeftConfidence = body.HandLeftConfidence,
                HandLeftState = body.HandLeftState,
                HandRightConfidence = body.HandRightConfidence,
                HandRightState = body.HandRightState,
                IsRestricted = body.IsRestricted,
                IsTracked = body.IsTracked,
                JointOrientations = body.JointOrientations.Select(kvp => kvp.Value).ToArray(),
                Joints = body.Joints.Select(kvp => kvp.Value).ToArray(),
                Lean = body.Lean,
                LeanTrackingState = body.LeanTrackingState,
                TrackingId = body.TrackingId
            };
            return result;
        }
    }
}
