using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Small list of extension methods for very basic pose detection
    /// </summary>
    public static class KinectMiniPoseExtensions
    {
        /// <summary>
        /// Compares height between two joints
        /// </summary>
        /// <param name="kb">Kinect body</param>
        /// <param name="first">First kinect joint</param>
        /// <param name="second">Second Kinect joint</param>
        /// <returns>true if both joints are at least inferred and first joint Y > second joint Y, false otherwise (also returns false if one of those joints is not tracked)</returns>
        public static bool CompareHeight(this KinectBody kb, JointType first, JointType second)
        {
            Joint j1 = kb.Joints[first];
            Joint j2 = kb.Joints[second];
            return j1.IsAtLeastInferred() && j2.IsAtLeastInferred() && j1.Position.Y > j2.Position.Y;
        }

        /// <summary>
        /// Checks if left hand is raised (we consider raised as "above head")
        /// </summary>
        /// <param name="kb">Kinect body to check</param>
        /// <returns>true if raised, false otherwise</returns>
        public static bool LeftHandRaised(this KinectBody kb)
        {
            return kb.CompareHeight(JointType.HandLeft, JointType.Head);
        }

        /// <summary>
        /// Checks if rights hand is raised (we consider raised as "above head")
        /// </summary>
        /// <param name="kb">Kinect body to check</param>
        /// <returns>true if raised, false otherwise</returns>
        public static bool RightHandRaised(this KinectBody kb)
        {
            return kb.CompareHeight(JointType.HandRight, JointType.Head);
        }

        /// <summary>
        /// Checks if both left and right hand are raised (we consider raised as "above head")
        /// </summary>
        /// <param name="kb">Kinect body to check</param>
        /// <returns>true if raised, false otherwise</returns>
        public static bool BothHandsRaised(this KinectBody kb)
        {
            return kb.LeftHandRaised() && kb.RightHandRaised();
        }
    }
}
