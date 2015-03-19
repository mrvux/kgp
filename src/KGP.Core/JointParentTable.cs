using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Dictionary that contains joint->parent relationship
    /// </summary>
    public static class JointParentTable
    {
        private static Dictionary<JointType, JointType> jointParentTable;

        /// <summary>
        /// Gets parent joint Table
        /// </summary>
        public static IReadOnlyDictionary<JointType, JointType> Table
        {
            get
            {
                if (jointParentTable == null)
                {
                    jointParentTable = new Dictionary<JointType, JointType>();

                    jointParentTable.Add(JointType.AnkleLeft, JointType.KneeLeft);
                    jointParentTable.Add(JointType.AnkleRight, JointType.KneeRight);

                    jointParentTable.Add(JointType.ElbowLeft, JointType.ShoulderLeft);
                    jointParentTable.Add(JointType.ElbowRight, JointType.ShoulderRight);

                    jointParentTable.Add(JointType.FootLeft, JointType.AnkleLeft);
                    jointParentTable.Add(JointType.FootRight, JointType.AnkleRight);

                    jointParentTable.Add(JointType.HandLeft, JointType.WristLeft);
                    jointParentTable.Add(JointType.HandRight, JointType.WristRight);

                    jointParentTable.Add(JointType.HandTipLeft, JointType.HandLeft);
                    jointParentTable.Add(JointType.HandTipRight, JointType.HandRight);

                    jointParentTable.Add(JointType.Head, JointType.Neck);

                    jointParentTable.Add(JointType.HipLeft, JointType.SpineBase);
                    jointParentTable.Add(JointType.HipRight, JointType.SpineBase);

                    jointParentTable.Add(JointType.KneeLeft, JointType.HipLeft);
                    jointParentTable.Add(JointType.KneeRight, JointType.HipRight);

                    jointParentTable.Add(JointType.Neck, JointType.SpineShoulder);

                    jointParentTable.Add(JointType.ShoulderLeft, JointType.SpineShoulder);
                    jointParentTable.Add(JointType.ShoulderRight, JointType.SpineShoulder);

                    jointParentTable.Add(JointType.SpineBase, JointType.SpineBase);

                    jointParentTable.Add(JointType.SpineMid, JointType.SpineBase);

                    jointParentTable.Add(JointType.SpineShoulder, JointType.SpineMid);

                    jointParentTable.Add(JointType.ThumbLeft, JointType.HandLeft);
                    jointParentTable.Add(JointType.ThumbRight, JointType.HandRight);

                    jointParentTable.Add(JointType.WristLeft, JointType.ElbowLeft);
                    jointParentTable.Add(JointType.WristRight, JointType.ElbowRight);
                }
                return jointParentTable;
            }
        }
    }
}
