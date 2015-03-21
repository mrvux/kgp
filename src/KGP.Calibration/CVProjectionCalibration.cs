using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Calibration
{
    public unsafe class CVProjectionCalibration : ICameraToScreenSolver
    {
        [DllImport("OCV_CalibrationUtils")]
        private static extern int CalibrateCamera(void* imgPts, void* objPts, int pointCount, int w, int h, int flags, out IntPtr mat, out IntPtr dist, out IntPtr t, out IntPtr r);

        [DllImport("OCV_CalibrationUtils")]
        private static extern int FreeMemory(IntPtr ptr);


        public ProjectorCalibrationResult Solve(ProjectorCalibrationData data, IReadOnlyList<CameraToScreenPoint> points)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (points == null)
                throw new ArgumentNullException("points");
            if (points.Count < 5)
                throw new ArgumentOutOfRangeException("points", "Calibration data should contain at least 4 points");

            Vector3[] worldPoints = points.Select(cp => cp.CameraPoint).ToArray();
            Vector2[] imagePoints = points.Select(cp => cp.ScreenPoint).ToArray();


            IntPtr result;
            IntPtr dist, t, r;

            Vector3[] dst = ObjectPoints(worldPoints);

            fixed (Vector2* pImg = &imagePoints[0])
            {
                fixed (Vector3* pDst = &dst[0])
                {
                    int res = CalibrateCamera(pImg, pDst, worldPoints.Length, (int)data.SensorSize.X, (int)data.SensorSize.Y, 0, out result, out dist, out t, out r);
                }
            }

            float* trans = (float*)t;
            Matrix view = Matrix.Identity;
            view.M41 = trans[0];
            view.M42 = trans[1];
            view.M43 = trans[2];

            float* rot = (float*)r;

            view.M11 = rot[0];
            view.M21 = rot[1];
            view.M31 = rot[2];

            view.M12 = rot[3];
            view.M22 = rot[4];
            view.M32 = rot[5];

            view.M13 = rot[6];
            view.M23 = rot[7];
            view.M33 = rot[8];
            view = FlipMatrix(view);

            float* proj = (float*)result;

            Matrix p = Matrix.Identity;

            p.M11 = proj[0];
            p.M21 = proj[1];
            p.M31 = proj[2];

            p.M12 = proj[3];
            p.M22 = proj[4];
            p.M32 = proj[5];

            p.M13 = proj[6];
            p.M23 = proj[7];
            p.M33 = proj[8];
            p.M34 = 1.0f;

            p.M44 = 0;

            float invx = 2.0f / data.SensorSize.X;
            float invy = 2.0f / data.SensorSize.Y;

            Matrix camView = Matrix.Translation(-1.0f, 1.0f, 0.0f);
            camView = Matrix.Scaling(invx, -invy, 1.0f) * camView;

            Matrix pcam = p * camView * Matrix.Scaling(1, -1, 1);

            float n = data.Near;
            float f = data.Far;
            float q = f / (f - n);
            float qf = -2.0f * q * n;

            pcam.M33 = q;
            pcam.M43 = qf;

            FreeMemory(result);
            FreeMemory(dist);
            FreeMemory(t);
            FreeMemory(r);

            return new ProjectorCalibrationResult(view, pcam);
        }

        private Matrix FlipMatrix(Matrix OpenCVMatrix)
        {
            var coordShift = Matrix.Scaling(1, -1, -1);
            return coordShift * OpenCVMatrix;
        }

        private Vector3[] ObjectPoints(Vector3[] input)
        {
            Vector3[] objectPoints = new Vector3[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                objectPoints[i].X = input[i].X;
                objectPoints[i].Y = -input[i].Y;
                objectPoints[i].Z = -input[i].Z;
            }
            return objectPoints;
        }
    }
}
