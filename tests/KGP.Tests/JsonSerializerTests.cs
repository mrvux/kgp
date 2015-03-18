using KGP.Serialization.Body;
using KGP.Tests.Fakes;
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
    public class JsonSerializerTests
    {
        [TestMethod]
        public void TestSerialize()
        {
            KinectBody[] bodies = new KinectBody[]
            {
                FakeBodies.FakeSpineBody(10,true, new CameraSpacePoint() 
                {
                    X = 0.2f
                }),
                FakeBodies.FakeSpineBody(20,true, new CameraSpacePoint() 
                {
                    X = -0.02f //Center is an absolute value, so 1 and 2 should be same distance
                }),
                FakeBodies.FakeSpineBody(30,true, new CameraSpacePoint() 
                {
                    X = 0.02f
                }),
                FakeBodies.FakeSpineBody(40,true, new CameraSpacePoint() 
                {
                    X = 0.7f
                })
            };

            string json = JsonSerializer.ToJson(bodies);

            var deserialize = JsonSerializer.FromJson(json);

            Assert.AreEqual(deserialize.Count(), 4);
        }
    }
}
