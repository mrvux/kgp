using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KGP.Network.FrameServer
{
    /// <summary>
    /// Listener class for kinect clients
    /// </summary>
    public class KinectClientListener : IDisposable
    {
        private TcpListener listener;
        private Thread listenThread;

        private bool isListening = false;

        /// <summary>
        /// Raised when a client connected
        /// </summary>
        public event EventHandler<KinectClientEventArgs> ClientConnected;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Server port</param>
        public KinectClientListener(int port)
        {
            this.listener = new TcpListener(IPAddress.Any, port);
            this.StartListening();
        }

        public void StartListening()
        {
            this.listener.Start();
            this.listenThread = new Thread(new ThreadStart(this.ListenForClients));
            this.listenThread.Start();
            this.isListening = true;
        }

        private void ListenForClients()
        {
            while (this.isListening)
            {
                TcpClient client = this.listener.AcceptTcpClient();

                if (client.Connected)
                {
                    if (this.ClientConnected != null)
                    {
                        this.ClientConnected(this, new KinectClientEventArgs(client));
                    }

                    this.isListening = false;
                    this.listener.Stop();
                }
            }
        }

        /// <summary>
        /// Disposes the client listener, by stopping the listen thread if applicable
        /// </summary>
        public void Dispose()
        {
            try
            {
                this.listenThread.Abort();
            }
            catch
            {

            }
            this.listener.Stop();
        }
    }
}
