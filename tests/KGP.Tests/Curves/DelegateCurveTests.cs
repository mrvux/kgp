using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KGP.Tests.Curves
{
    [TestClass]
    public class DelegateCurveTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestNull()
        {
            DelegateCurve d = new DelegateCurve(null);
        }

        [TestMethod]
        public void TestValid()
        {
            DelegateCurve d = new DelegateCurve((x) => x * 0.5f);

            float sut = d.Apply(2.0f);
            Assert.AreEqual(sut, 1.0f);
        }

        [TestMethod]
        public void TestLinear()
        {
            LinearCurve d = new LinearCurve();

            float sut = d.Apply(0.1f);
            Assert.AreEqual(sut, 0.1f);
        }

        [TestMethod]
        public void TestSquarePositive()
        {
            SquareCurve d = new SquareCurve();
            float sut = d.Apply(0.5f);
            Assert.AreEqual(sut, 0.25f);
        }

        [TestMethod]
        public void TestSquareNegative()
        {
            SquareCurve d = new SquareCurve();
            float sut = d.Apply(-0.5f);
            Assert.AreEqual(sut, -0.25f);
        }

        [TestMethod]
        public void TestSquarZero()
        {
            SquareCurve d = new SquareCurve();
            float sut = d.Apply(0.0f);
            Assert.AreEqual(sut, 0.0f);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGammaInvalid()
        {
            GammaCurve d = new GammaCurve(-1.0f);
        }

        [TestMethod]
        public void TestGammaLowerThan1()
        {
            GammaCurve d = new GammaCurve(0.5f);

            float sut = d.Apply(2.0f);
            Assert.AreEqual(sut, (float)Math.Pow(2.0f, 0.5f));

            sut = d.Apply(-2.0f);
            Assert.AreEqual(sut, -(float)Math.Pow(2.0f, 0.5f));
        }

        
    }
}
