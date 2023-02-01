using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TestMonitorClient
{
    public class MonitorClient
    {
        /// <summary>
        /// Private instance of the same class
        /// </summary>
        private static MonitorClient Instance;

        /// <summary>
        ///  Stream Object to handle Stream
        /// </summary>
        private static Stream Stream;

        /// <summary>
        ///  Stream reader
        /// </summary>
        private static StreamReader Sreader;

        /// <summary>
        /// Stream writer
        /// </summary>
        private static StreamWriter Swriter;

        /// <summary>
        /// TCP Client Object
        /// </summary>
        private static TcpClient TcpClient;

        /// <summary>
        /// Synchronizing object used for lock
        /// and make the client thread safe
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// constructor for MonitorClient class
        /// constructor is private and gets single instance of the class logger
        /// </summary>
        private MonitorClient()
        {
            TcpClient = new TcpClient(IPAddress.Loopback.ToString(), 56000);

            Stream = TcpClient.GetStream();
            Sreader = new StreamReader(Stream);
            Swriter = new StreamWriter(Stream);
        }

        public static MonitorClient GetInstance()
        {
            lock (_lock)
            {
                if (Instance == null)
                {
                    Instance = new MonitorClient();
                }
            }

            return Instance;
        }

        /// <summary>
        /// This function writes a message line to the stream
        /// which sends that message to MServer to display
        /// </summary>
        /// <param name="message"></param>
        public void WriteLine(string message)
        {
            lock (_lock)
            {
                Swriter.WriteLine(DateTime.Now + " : " + message);
                Swriter.Flush();
            }
        }

        /// <summary>
        /// This function Sleeps and conveys progress of sleep
        /// </summary>
        /// <param name="Seconds"></param>
        public void Sleep(uint Seconds, uint interval)
        {
            int lapsedSeconds = 0;

            while (lapsedSeconds != Seconds)
            {
                Thread.Sleep(1000);
                lapsedSeconds++;

                if ((lapsedSeconds % interval) == 0)
                {
                    WriteLine("Remaining time = " + (Seconds - lapsedSeconds) / 60 + " minutes "
                              + (Seconds - lapsedSeconds) % 60 + "seconds");
                }
            }
        }

        /// <summary>
        /// Close TCP Connection
        /// </summary>
        public void Close()
        {
            TcpClient.Close();
        }

    }
}