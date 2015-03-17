using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Tests.Fakes
{
    public static class FakeJoints
    {
        public static Joint[] ValidRandomJoints()
        {
            Joint[] result = new Joint[Microsoft.Kinect.Body.JointCount];
            JointType[] jt = (JointType[])Enum.GetValues(typeof(JointType));
            for (int i = 0; i < result.Length;i++)
            {
                result[i].JointType = jt[i];
            }
            return result;
        }

        public static Joint[] ValidRandomJointsSpinePosition(CameraSpacePoint cp)
        {
            Joint[] result = new Joint[Microsoft.Kinect.Body.JointCount];
            JointType[] jt = (JointType[])Enum.GetValues(typeof(JointType));
            for (int i = 0; i < result.Length; i++)
            {
                result[i].JointType = jt[i];
                if (jt[i] == JointType.SpineBase)
                {
                    result[i].Position = cp;
                }
            }
            return result;
        }

        public static Joint[] DuplicateJoint()
        {
            Joint[] result = new Joint[Microsoft.Kinect.Body.JointCount];
            JointType[] jt = (JointType[])Enum.GetValues(typeof(JointType));
            for (int i = 0; i < result.Length; i++)
            {
                result[i].JointType = jt[i];
            }

            //Force a duplicate joint here
            result[1].JointType = jt[0];
            return result;
        }


        public static Joint[] DuplicateHead()
        {
            Joint[] result = new Joint[Microsoft.Kinect.Body.JointCount];
            JointType[] jt = (JointType[])Enum.GetValues(typeof(JointType));
            for (int i = 0; i < result.Length; i++)
            {
                result[i].JointType = jt[i];
            }

            //Force a duplicate joint here
            result[1].JointType = JointType.Head;
            result[3].JointType = JointType.Head;
            return result;
        }
    }
}
