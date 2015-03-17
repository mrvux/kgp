using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Serialization.Body
{
    /// <summary>
    /// Simple json serializer for body data
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// Converts a list of kinect body to Json format
        /// </summary>
        /// <param name="bodies">Body list to serialize</param>
        /// <param name="formatting">Json formatting options</param>
        /// <returns>Json string</returns>
        public static string ToJson(IEnumerable<KinectBody> bodies, Formatting formatting = Formatting.Indented)
        {
            KinectBodyInternal[] data = bodies.Select(body => KinectBodyConverter.Convert(body)).ToArray();

            return JsonConvert.SerializeObject(data, formatting);
        }

        /// <summary>
        /// Converts back a kinect body enumeration from a json string
        /// </summary>
        /// <param name="json">Json string</param>
        /// <returns>Kinect body enumeration</returns>
        public static IEnumerable<KinectBody> FromJson(string json)
        {
            KinectBodyInternal[] data = JsonConvert.DeserializeObject<KinectBodyInternal[]>(json);

            return data.Select(kbi => new KinectBody(kbi));
        }
    }
}
