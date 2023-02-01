using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestMonitorClient
{
    class MonitorClient
    {
        /// <summary>
        /// message to send
        /// </summary>
        private static string message;

        /// <summary>
        ///  Stream Object to handle Stream
        /// </summary>
        private static Stream Mstream;

        /// <summary>
        ///  Stream reader
        /// </summary>
        private static StreamReader Mreader;

        /// <summary>
        /// Stream writer
        /// </summary>
        private static StreamWriter Mwriter;

        /// <summary>
        /// TCP Client Object
        /// </summary>
        private static TcpClient Mclient;

        private static bool ConsoleCancel = false;

        static void Main(string[] args)
        {
            
            try
            {
                Mclient = new TcpClient(IPAddress.Loopback.ToString(), 56000);

                Mstream = Mclient.GetStream();
                Mreader = new StreamReader(Mstream);
                Mwriter = new StreamWriter(Mstream);

                Console.CancelKeyPress += Console_CancelKeyPress;

                while (!ConsoleCancel)
                {
                    message = Console.ReadLine();

                    WriteLine(message);
                }
              
            }
            catch (Exception ex)
            {
                Console.WriteLine("Client Error : " + ex.Message + " " + ex.StackTrace);
                Console.WriteLine("Press Enter to Continue");
                Console.ReadLine();
                Console.WriteLine();

            }
            finally
            {
                if (Mclient != null)
                {
                    Mclient.Close();
                }
            }
          
        }

        /// <summary>
        /// This function writes a message line to the stream 
        /// which sends that message to MServer to display
        /// </summary>
        /// <param name="message"></param>
        public static void WriteLine(string message)
        {
            Mwriter.WriteLine(message);
        }

        /// <summary>
        /// Capture the ^C or Break key event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            ConsoleCancel = true;
            e.Cancel = true;
        }
    }
}
