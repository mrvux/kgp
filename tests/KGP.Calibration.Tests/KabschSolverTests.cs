using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpDX;
using System.Collections.Generic;

namespace KGP.Calibration.Tests
{
    [TestClass]
    public class KabschSolverTests
    {
        public bool NearEqual(Matrix m1, Matrix m2, float epsilon = 0.0001f)
        {
            for (int i = 0; i < 16; i++)
            {
                if (Math.Abs(m1[i] - m2[i]) > epsilon)
                    return false;
            }
            return true;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNull()
        {
            KabschSolver solver = new KabschSolver();
            solver.Solve(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEmptyList()
        {
            List<CameraToCameraPoint> dataSet = new List<CameraToCameraPoint>();
            KabschSolver solver = new KabschSolver();
            solver.Solve(dataSet);
        }


        [TestMethod]
        public void TestSimpleTranslate()
        {
            Matrix expected = Matrix.Translation(0.2f, 5.0f, 8.0f);
            KabschSolver solver = new KabschSolver();

            List<CameraToCameraPoint> dataSet = new List<CameraToCameraPoint>();

            Vector3 o1 = new Vector3(0.2f,7.0f,3.0f); Vector3 d1 = Vector3.TransformCoordinate(o1, expected);
            Vector3 o2 = new Vector3(0.2f,0.12f,3.0f); Vector3 d2 = Vector3.TransformCoordinate(o2, expected);
            Vector3 o3 = new Vector3(0.2f,0.25f,3.0f); Vector3 d3 = Vector3.TransformCoordinate(o3, expected);
            Vector3 o4 = new Vector3(0.4f,0.50f,3.0f); Vector3 d4 = Vector3.TransformCoordinate(o4, expected);
            Vector3 o5 = new Vector3(0.2f,7.0f,30.0f); Vector3 d5 = Vector3.TransformCoordinate(o5, expected);
            Vector3 o6 = new Vector3(0.8f,70.0f,30.0f); Vector3 d6 = Vector3.TransformCoordinate(o6, expected);

            dataSet.Add(new CameraToCameraPoint(o1, d1));
            dataSet.Add(new CameraToCameraPoint(o2, d2));
            dataSet.Add(new CameraToCameraPoint(o3, d3));
            dataSet.Add(new CameraToCameraPoint(o4, d4));
            dataSet.Add(new CameraToCameraPoint(o5, d5));
            dataSet.Add(new CameraToCameraPoint(o6, d6));

            Matrix actual = Matrix.Invert(solver.Solve(dataSet));

            Assert.IsTrue(NearEqual(actual, expected));
        }

        [TestMethod]
        public void TestRotateOnly()
        {
            Matrix expected = Matrix.RotationYawPitchRoll(0.2f, -3.0f, 1.6f);
            KabschSolver solver = new KabschSolver();

            List<CameraToCameraPoint> dataSet = new List<CameraToCameraPoint>();

            Vector3 o1 = new Vector3(0.2f, 7.0f, 3.0f); Vector3 d1 = Vector3.TransformCoordinate(o1, expected);
            Vector3 o2 = new Vector3(0.2f, 0.12f, 3.0f); Vector3 d2 = Vector3.TransformCoordinate(o2, expected);
            Vector3 o3 = new Vector3(0.2f, 0.25f, 3.0f); Vector3 d3 = Vector3.TransformCoordinate(o3, expected);
            Vector3 o4 = new Vector3(0.4f, 0.50f, 3.0f); Vector3 d4 = Vector3.TransformCoordinate(o4, expected);
            Vector3 o5 = new Vector3(0.2f, 7.0f, 30.0f); Vector3 d5 = Vector3.TransformCoordinate(o5, expected);
            Vector3 o6 = new Vector3(0.8f, 70.0f, 30.0f); Vector3 d6 = Vector3.TransformCoordinate(o6, expected);

            dataSet.Add(new CameraToCameraPoint(o1, d1));
            dataSet.Add(new CameraToCameraPoint(o2, d2));
            dataSet.Add(new CameraToCameraPoint(o3, d3));
            dataSet.Add(new CameraToCameraPoint(o4, d4));
            dataSet.Add(new CameraToCameraPoint(o5, d5));
            dataSet.Add(new CameraToCameraPoint(o6, d6));

            Matrix actual = Matrix.Invert(solver.Solve(dataSet));
            Assert.IsTrue(NearEqual(actual, expected));
        }

        [TestMethod]
        public void TestAffine()
        {
            Matrix expected = Matrix.Translation(0.2f, 5.0f, 8.0f) * Matrix.RotationYawPitchRoll(0.2f, -3.0f, 1.6f);
            KabschSolver solver = new KabschSolver();

            List<CameraToCameraPoint> dataSet = new List<CameraToCameraPoint>();

            Vector3 o1 = new Vector3(0.2f, 7.0f, 3.0f); Vector3 d1 = Vector3.TransformCoordinate(o1, expected);
            Vector3 o2 = new Vector3(0.2f, 0.12f, 3.0f); Vector3 d2 = Vector3.TransformCoordinate(o2, expected);
            Vector3 o3 = new Vector3(0.2f, 0.25f, 3.0f); Vector3 d3 = Vector3.TransformCoordinate(o3, expected);
            Vector3 o4 = new Vector3(0.4f, 0.50f, 3.0f); Vector3 d4 = Vector3.TransformCoordinate(o4, expected);
            Vector3 o5 = new Vector3(0.2f, 7.0f, 30.0f); Vector3 d5 = Vector3.TransformCoordinate(o5, expected);
            Vector3 o6 = new Vector3(0.8f, 70.0f, 30.0f); Vector3 d6 = Vector3.TransformCoordinate(o6, expected);

            dataSet.Add(new CameraToCameraPoint(o1, d1));
            dataSet.Add(new CameraToCameraPoint(o2, d2));
            dataSet.Add(new CameraToCameraPoint(o3, d3));
            dataSet.Add(new CameraToCameraPoint(o4, d4));
            dataSet.Add(new CameraToCameraPoint(o5, d5));
            dataSet.Add(new CameraToCameraPoint(o6, d6));

            Matrix actual = Matrix.Invert(solver.Solve(dataSet));

            Assert.IsTrue(NearEqual(actual, expected));
        }
    }
}
