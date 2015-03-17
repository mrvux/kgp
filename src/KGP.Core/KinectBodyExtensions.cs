using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP
{
    /// <summary>
    /// Extensions methods for Kinect Body
    /// </summary>
    public static class KinectBodyExtensions
    {
        /// <summary>
        /// Simple filter to select only tracked bodies
        /// </summary>
        /// <param name="bodies">Body enumeration</param>
        /// <returns>Enumeration which only contains Bodies with tracking state on</returns>
        public static IEnumerable<KinectBody> TrackedOnly(this IEnumerable<KinectBody> bodies)
        {
            return bodies.Where(kb => kb.IsTracked);
        }

        /// <summary>
        /// Finds the relevant body from a list of bodies. Can be used to match a body id from a different frame
        /// </summary>
        /// <param name="kb">Kinect body to check</param>
        /// <param name="bodyList">List of body</param>
        /// <returns>Matched Kinect body (using id) if found, null otherwise</returns>
        public static KinectBody FindById(this KinectBody kb, IEnumerable<KinectBody> bodyList)
        {
            return bodyList.Where(body => body.TrackingId == kb.TrackingId).FirstOrDefault();
        }

        /// <summary>
        /// Checks if a body tracking Id is contained into our list
        /// </summary>
        /// <param name="kb">Kinect body to check</param>
        /// <param name="bodyList">Body list to check</param>
        /// <returns>true is we found this body Id, false otherwise</returns>
        public static bool ContainsId(this KinectBody kb, IEnumerable<KinectBody> bodyList)
        {
            return kb.FindById(bodyList) != null;
        }
    }
}
