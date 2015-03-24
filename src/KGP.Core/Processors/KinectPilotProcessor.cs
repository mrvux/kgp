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

        private float steering;
        private float push;
        private float elevation;

        public float Steering
        {
            get { return this.steering; }
        }

        public float Push
        {
            get { return this.push; }
        }

        public float Elevation
        {
            get { return this.elevation; }
        }

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

        public static KinectPilotProcessor Default
        {
            get
            {
                LinearCurve linear = new LinearCurve();
                return new KinectPilotProcessor(linear, linear, linear);
            }
        }

        public void Process(KinectJointTable jointTable)
        {
            var leftHand = jointTable.Joints[JointType.HandLeft];
            var rightHand = jointTable.Joints[JointType.HandRight];
            var hipLeft = jointTable.Joints[JointType.HipLeft];
            var hipRight = jointTable.Joints[JointType.HipRight];
            var shoulderLeft = jointTable.Joints[JointType.ShoulderLeft];
            var shoulderRight = jointTable.Joints[JointType.ShoulderRight];

            //First compute elevation, we consider min y at hip and max y at shoulder
            float leftE = ((leftHand.Y - hipLeft.Y) / (shoulderLeft.Y - hipLeft.Y)) * 2.0f - 1.0f;
            float rightE = ((rightHand.Y - hipRight.Y) / (shoulderRight.Y - hipRight.Y)) * 2.0f - 1.0f;

            this.elevation = this.elevationCurve.Apply((leftE + rightE) * 0.5f);

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

            this.steering = this.steeringCurve.Apply(ang);

            //Last computes push ammount
        }
    }
}
