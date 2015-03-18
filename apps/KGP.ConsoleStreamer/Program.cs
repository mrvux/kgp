using KGP.Network.FrameServer;
using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGP.ConsoleStreamer
{
    class Program
    {
        static void Main(string[] args)
        {
            KinectSensor sensor = KinectSensor.GetDefault();
            sensor.Open();

            KinectFrameServer frameServer = new KinectFrameServer(32000, sensor);
            frameServer.ClientConnected += frameServer_ClientConnected;
            frameServer.ClientDisconnected += frameServer_ClientDisconnected;


            Console.ReadLine();
            frameServer.Dispose();
        }

        static void frameServer_ClientDisconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Client disconnected");
        }

        static void frameServer_ClientConnected(object sender, EventArgs e)
        {
            Console.WriteLine("Client connected");
        }
    }
}
