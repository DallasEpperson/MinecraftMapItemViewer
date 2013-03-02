using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftMapItemViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Log("Program Started");
            Application.Run(new frmMain());

            Log("Program Exiting...");
        }

        [Flags]
        public enum LoggingLocations{
            None = 0,
            DebugPrint =    1 << 0, //1 There's no real reason to do it this way
            LogFile =       1 << 1, //2 other than to serve as an excercise in
            MessageBox =    1 << 2, //4 bit shifting.
            TheRainforest = 1 << 3, //8

            Default = DebugPrint | LogFile //<-- Config me
        }

        /// <summary>
        /// Log message to various places
        /// </summary>
        /// <param name="strMessage">Message to log</param>
        /// <param name="logLocation">Where to log</param>
        public static void Log(string strMessage, LoggingLocations logLocation)
        {
            if ((logLocation & LoggingLocations.DebugPrint) == LoggingLocations.DebugPrint)
            {
                Debug.WriteLine(strMessage);
            }
            if ((logLocation & LoggingLocations.LogFile) == LoggingLocations.LogFile)
            {
                try
                {
                    string strLogFileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + ".log";
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(strLogFileName, true))
                    {
                        file.WriteLine(DateTime.Now.ToString() + " - " + strMessage);
                    }
                }
                catch
                {
                    MessageBox.Show("Error writing to log file!");
                }
            }
            if ((logLocation & LoggingLocations.MessageBox) == LoggingLocations.MessageBox)
            {
                MessageBox.Show(strMessage);
            }
        }

        /// <summary>
        /// Logs message to default location(s)
        /// </summary>
        /// <param name="strMessage">Message to log</param>
        public static void Log(string strMessage)
        {
            Log(strMessage, LoggingLocations.Default);
        }
    }
}
