using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestMonitorClient
{
    class MonitorClientApp
    {
        /// <summary>
        /// Monitor Client OBject
        /// Note that Monitor CLient is Singleton Class
        /// </summary>
        private static MonitorClient MClient;

        /// <summary>
        /// Console Cancel Flag
        /// </summary>
        private static bool ConsoleCancel = false;

        /// <summary>
        /// Main Console Client Application for testing Server
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string message;
            try
            {
                MClient = MonitorClient.GetInstance();
                Console.CancelKeyPress += Console_CancelKeyPress;

                while (!ConsoleCancel)
                {
                    message = Console.ReadLine();

                    MClient.WriteLine(message);
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
                if (MClient != null)
                {
                    MClient.Close();
                }
            }
          
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
