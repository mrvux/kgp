using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Tests
{
    [TestClass]
    public class JointParentTableTests
    {
        [TestMethod]
        public void TestCount()
        {
            var jt = JointParentTable.Table;

            Assert.AreEqual(jt.Count, Microsoft.Kinect.Body.JointCount);
        }


    }
}
