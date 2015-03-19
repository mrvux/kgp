using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.DataTables
{
    /// <summary>
    /// Joint data tables, used to construct gpu buffers
    /// </summary>
    public static class JointParentTable
    {
        /// <summary>
        /// Returns table for a single body as uint, do not include base/base tuple
        /// </summary>
        public static uint[] FullTableUint
        {
            get
            {
                uint[] result = new uint[(Consts.MaxJointCount-1) * 2];

                JointType[] joints = (JointType[])Enum.GetValues(typeof(JointType));
                joints = joints.Where(jt => jt != JointType.SpineBase).ToArray();

                for (int i = 0; i < joints.Length; i++)
                {
                    result[i * 2] = (uint)joints[i];
                    result[i * 2 + 1] = (uint)joints[i].GetParentType();
                }

                return result;
            }
        }

        /// <summary>
        /// Returns a prefixed summed table so we can hold multiple bodies
        /// </summary>
        public static uint[] RepeatTableUInt(int maxBodyCount)
        {
            if (maxBodyCount < 1)
                throw new ArgumentOutOfRangeException("maxBodyCount", "We must have at least one body");

            uint[] baseTable = FullTableUint;

            uint[] result = new uint[baseTable.Length * maxBodyCount];

            uint prefix = 0;
            uint counter = 0;
            for (uint i = 0; i < maxBodyCount; i++)
            {
                for (uint j = 0; j < baseTable.Length; j++)
                {
                    result[counter] = baseTable[j] + prefix;
                    counter++;
                }
                prefix += (uint)baseTable.Length;
            }
            return result;
        }

        /// <summary>
        /// Returns table for a single body as ushort, do not include base/base tuple
        /// </summary>
        public static ushort[] FullTableUShort
        {
            get
            {
                ushort[] result = new ushort[(Consts.MaxJointCount - 1) * 2];

                JointType[] joints = (JointType[])Enum.GetValues(typeof(JointType));
                joints = joints.Where(jt => jt != JointType.SpineBase).ToArray();

                for (int i = 0; i < joints.Length; i++)
                {
                    result[i * 2] = (ushort)joints[i];
                    result[i * 2 + 1] = (ushort)joints[i].GetParentType();
                }
                return result;
            }
        }


    }
}
