using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KGP.Curves;

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestValid()
        {
            DelegateCurve d = new DelegateCurve((x) => x * 0.5f);

            float sut = d.Apply(2.0f);
            Assert.AreEqual(sut, 1.0f);
        }
    }
}
