using Microsoft.Kinect;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Processors
{
    /// <summary>
    /// Processor for basic body pilot, allows steering, push and up movements
    /// </summary>
    public class KinectPilotProcessor
    {
        private readonly ICurve steeringCurve;
        private readonly ICurve pushCurve;
        private readonly ICurve elevationCurve;

        private float steeringY;
        private float steeringZ;
        private float push;
        private float elevation;

        public float SteeringY
        {
            get { return this.steeringY; }
        }

        public float SterringZ
        {
            get { return this.steeringZ; }
        }
        
        public float Push
        {
            get { return this.push; }
        }

        public float Elevation
        {
            get { return this.elevation; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="steeringCurve">Steering curve</param>
        /// <param name="pushCurve">Push amount curve</param>
        /// <param name="elevationCurve">Elevation curve</param>
        public KinectPilotProcessor(ICurve steeringCurve, ICurve pushCurve, ICurve elevationCurve)
        {
            if (steeringCurve == null)
                throw new ArgumentNullException("steeringCurve");
            if (pushCurve == null)
                throw new ArgumentNullException("pushCurve");
            if (elevationCurve == null)
                throw new ArgumentNullException("elevationCurve");

            this.steeringCurve = steeringCurve;
            this.pushCurve = pushCurve;
            this.elevationCurve = elevationCurve;
        }

        /// <summary>
        /// Simple default with a plain linear curve
        /// </summary>
        public static KinectPilotProcessor Default
        {
            get
            {
                LinearCurve linear = new LinearCurve();
                return new KinectPilotProcessor(linear, linear, linear);
            }
        }

        /// <summary>
        /// Process joint table
        /// </summary>
        /// <param name="jointTable"></param>
        public void Process(KinectJointTable jointTable)
        {
            var leftHand = jointTable.Joints[JointType.HandLeft];
            var rightHand = jointTable.Joints[JointType.HandRight];
            var hipLeft = jointTable.Joints[JointType.HipLeft];
            var hipRight = jointTable.Joints[JointType.HipRight];
            var shoulderLeft = jointTable.Joints[JointType.ShoulderLeft];
            var shoulderRight = jointTable.Joints[JointType.ShoulderRight];
            var elbowLeft = jointTable.Joints[JointType.ElbowLeft];
            var elbowRight = jointTable.Joints[JointType.ElbowRight];

            //First compute elevation, we consider min y at hip and max y at shoulder
            float leftE = ((leftHand.Y - hipLeft.Y) / (shoulderLeft.Y - hipLeft.Y)) * 2.0f - 1.0f;
            float rightE = ((rightHand.Y - hipRight.Y) / (shoulderRight.Y - hipRight.Y)) * 2.0f - 1.0f;

            this.elevation = this.elevationCurve.Apply(MathUtil.Clamp((leftE + rightE) * 0.5f, -1.0f,1.0f));

            float handDirX = leftHand.X - rightHand.X;
            float handDirY = leftHand.Y - rightHand.Y;

            float ang = Convert.ToSingle(Math.Atan2(handDirY, handDirX));
            float pi = (float)Math.PI;
            float halfpi = (float)Math.PI * 0.5f;

            //remap left steer from -halpi to -pi
            if (ang < 0.0f)
            {
                ang = (1.0f - (ang + halfpi) / (-halfpi)) * -1.0f;
            }
            else
            {
                ang = (ang - pi) / (-halfpi);
            }

            this.steeringY = this.steeringCurve.Apply(MathUtil.Clamp(ang, -1.0f, 1.0f));

            //Build z steering as per star wars game, hand angle in xz plane
            float handDirZ = leftHand.Z - rightHand.Z;
            float zang = Convert.ToSingle(Math.Atan2(handDirZ, handDirX));

            if (zang < 0.0f)
            {
                zang = (1.0f - (zang + halfpi) / (-halfpi)) * -1.0f;
            }
            else
            {
                zang = (zang - pi) / (-halfpi);
            }

            this.steeringZ = zang;


            //Last computes push amount, here we say min push is Z next to shoulder and max push is shoulder + arm length
            float leftArmLength = Vector3.Distance(shoulderLeft, elbowLeft) + Vector3.Distance(elbowLeft, leftHand);
            float rightArmLength = Vector3.Distance(shoulderRight, elbowRight) + Vector3.Distance(elbowRight, rightHand);

            float leftP = ((shoulderLeft.Z - leftHand.Z) / (leftArmLength)) * 2.0f - 1.0f;
            float rightP = ((shoulderRight.Z -rightHand.Z) / (rightArmLength)) * 2.0f - 1.0f;

            this.push = this.pushCurve.Apply(MathUtil.Clamp((leftP + rightP) * 0.5f, -1.0f, 1.0f));
        }
    }
}
