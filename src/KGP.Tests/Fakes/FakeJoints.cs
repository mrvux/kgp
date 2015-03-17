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
    }
}
