using DotNetMatrix;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    /// <summary>
    /// Kabsch solver version for 3d to 3d point set matrix solver
    /// </summary>
    public class KabschSolver : ICameraToCameraSolver
    {
        /// <summary>
        /// Solves between two point sets
        /// </summary>
        /// <param name="points">Point set</param>
        /// <returns>Affine matrix for each point set</returns>
        public Matrix Solve(IReadOnlyList<CameraToCameraPoint> points)
        {
            if (points == null)
                throw new ArgumentNullException("points");
            if (points.Count < 1)
                throw new ArgumentException("points", "No points provided");

            double[] meanP = new double[3] { 0.0, 0.0, 0.0 }; // mean of first point set
            for (int i = 0; i < points.Count; i++)
            {
                Vector3 orig = points[i].Origin;
                meanP[0] += orig.X;
                meanP[1] += orig.Y;
                meanP[2] += orig.Z;
            }

            double invCount = 1.0 / (double)points.Count;

            meanP[0] *= invCount;
            meanP[1] *= invCount;
            meanP[2] *= invCount;
            double[][] centroidP = new double[3][] { new double[] { meanP[0] }, new double[] { meanP[1] }, new double[] { meanP[2] } };
            GeneralMatrix mCentroidP = new GeneralMatrix(centroidP);

            double[][] arrayP = new double[points.Count][];
            for (int i = 0; i < points.Count; i++)
            {
                Vector3 orig = points[i].Origin;
                arrayP[i] = new double[3];
                arrayP[i][0] = orig.X - meanP[0]; // subtract the mean values from the incoming pointset
                arrayP[i][1] = orig.Y - meanP[1];
                arrayP[i][2] = orig.Z - meanP[2];
            }
            // this is the matrix of the first pointset translated to the origin of the coordinate system
            GeneralMatrix P = new GeneralMatrix(arrayP);

            double[] meanQ = new double[3] { 0.0, 0.0, 0.0 }; // mean of second point set
            for (int i = 0; i < points.Count; i++)
            {
                Vector3 dest = points[i].Destination;
                meanQ[0] += dest.X;
                meanQ[1] += dest.Y;
                meanQ[2] += dest.Z;
            }
            meanQ[0] *= invCount;
            meanQ[1] *= invCount;
            meanQ[2] *= invCount;
            double[][] centroidQ = new double[3][] { new double[] { meanQ[0] }, new double[] { meanQ[1] }, new double[] { meanQ[2] } };
            GeneralMatrix mCentroidQ = new GeneralMatrix(centroidQ);

            double[][] arrayQ = new double[points.Count][];
            for (int i = 0; i < points.Count; i++)
            {
                Vector3 dest = points[i].Destination;
                arrayQ[i] = new double[3];
                arrayQ[i][0] = dest.X - meanQ[0]; // subtract the mean values from the incoming pointset
                arrayQ[i][1] = dest.Y - meanQ[1];
                arrayQ[i][2] = dest.Z - meanQ[2];
            }
            // this is the matrix of the second pointset translated to the origin of the coordinate system
            GeneralMatrix Q = new GeneralMatrix(arrayQ);


            // ======================== STEP2 ========================
            // calculate a covariance matrix A and compute the optimal rotation matrix
            GeneralMatrix A = P.Transpose() * Q;

            SingularValueDecomposition svd = A.SVD();
            GeneralMatrix U = svd.GetU();
            GeneralMatrix V = svd.GetV();

            // calculate determinant for a special reflexion case.
            double det = (V * U.Transpose()).Determinant();
            double[][] arrayD = new double[3][]{ new double[]{1,0,0},
													new double[] {0,1,0},
													new double[] {0,0,1}
				};
            arrayD[2][2] = det < 0 ? -1 : 1; // multiply 3rd column with -1 if determinant is < 0
            GeneralMatrix D = new GeneralMatrix(arrayD);

            // now we can compute the rotation matrix:
            GeneralMatrix R = V * D * U.Transpose();

            // ======================== STEP3 ========================
            // calculate the translation:
            GeneralMatrix T = mCentroidP - R.Inverse() * mCentroidQ;

            return new Matrix((float)(R.Array)[0][0],
               (float)(R.Array)[0][1],
               (float)(R.Array)[0][2],
                0.0f,
               (float)(R.Array)[1][0],
               (float)(R.Array)[1][1],
               (float)(R.Array)[1][2],
               0.0f,
               (float)(R.Array)[2][0],
               (float)(R.Array)[2][1],
               (float)(R.Array)[2][2],
               0.0f,
               (float)(T.Array)[0][0],
               (float)(T.Array)[1][0],
               (float)(T.Array)[2][0],
               1.0f);
	
        }
    }
}
