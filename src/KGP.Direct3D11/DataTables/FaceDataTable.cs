using Microsoft.Kinect.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Direct3D11.DataTables
{
    /// <summary>
    /// HD Face data table, used to build gpu buffers
    /// </summary>
    public static class FaceDataTable
    {
        /// <summary>
        /// Returns an array of face indices from face Model
        /// </summary>
        public static uint[] FaceIndices
        {
            get
            {
                FaceModel faceModel = new FaceModel();
                return faceModel.TriangleIndices.ToArray();
            }
        }

        /// <summary>
        /// Returns an array of face indices from face Model with reversed winding
        /// </summary>
        public static uint[] FaceIndicesReverseWinding
        {
            get
            {
                FaceModel faceModel = new FaceModel();
                var indices = faceModel.TriangleIndices;

                uint[] result = new uint[indices.Count];

                //Swap y and z component to reverse triangle side
                for (int i = 0; i < result.Length / 3; i++)
                {
                    result[i * 3] = indices[i * 3];
                    result[i * 3 + 1] = indices[i * 3 + 1];
                    result[i * 3 + 2] = indices[i * 3 + 2];
                }

                return result;  
            }
        }

        /// <summary>
        /// Returns a prefixed summed table so we can hold multiple face
        /// </summary>
        public static uint[] RepeatTable(int maxFaceCount)
        {
            if (maxFaceCount < 1)
                throw new ArgumentOutOfRangeException("maxFaceCount", "We must have at least one face");

            uint[] baseTable = FaceIndices;

            uint[] result = new uint[baseTable.Length * maxFaceCount];

            uint prefix = 0;
            uint counter = 0;
            for (uint i = 0; i < maxFaceCount; i++)
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
        /// Returns a prefixed summed table so we can hold multiple face, version with reverse winding
        /// </summary>
        public static uint[] RepeatTableReverseWinding(int maxFaceCount)
        {
            if (maxFaceCount < 1)
                throw new ArgumentOutOfRangeException("maxFaceCount", "We must have at least one face");

            uint[] baseTable = FaceIndicesReverseWinding;

            uint[] result = new uint[baseTable.Length * maxFaceCount];

            uint prefix = 0;
            uint counter = 0;
            for (uint i = 0; i < maxFaceCount; i++)
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
    }
}
