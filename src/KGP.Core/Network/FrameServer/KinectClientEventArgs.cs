using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KGP.Network.FrameServer
{
    /// <summary>
    /// Event args raised when we have a new kinect client
    /// </summary>
    public class KinectClientEventArgs : EventArgs
    {
        private readonly TcpClient client;
        private readonly NetworkStream stream;

        /// <summary>
        /// Constructs event args
        /// </summary>
        /// <param name="client">TCP Client</param>
        public KinectClientEventArgs(TcpClient client)
        {
            this.client = client;
            this.stream = client.GetStream();
        }

        /// <summary>
        /// Tcp Client
        /// </summary>
        public TcpClient Client
        {
            get { return this.client; }
        }

        /// <summary>
        /// Network stream to receive and send data
        /// </summary>
        public NetworkStream NetworkStream
        {
            get { return this.stream; }
        }
    }
}
