using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

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
            Logging.Log("Program Started");
            Application.Run(new frmMain());

            Logging.Log("Program Exiting...");
        }

        public static Boolean IsDirectory(string strFilePath)
        {
            FileAttributes attr = File.GetAttributes(strFilePath);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static byte[] UnGZIPFile(string strFileName)
        {
            Logging.Log("Opening " + strFileName + " ...");
            byte[] compressed = File.ReadAllBytes(strFileName);
            Logging.Log("Compressed size: " + compressed.Length);
            Logging.Log("Decompressing...");
            byte[] decompressed = Decompress(compressed);
            Logging.Log("Decompressed size: " + decompressed.Length);
            return decompressed;
        }

        private static byte[] Decompress(byte[] compressed)
        {
            try
            {
                using (GZipStream stream = new GZipStream(new MemoryStream(compressed), CompressionMode.Decompress))
                {
                    const int size = 4096;
                    byte[] buffer = new byte[size];
                    using (MemoryStream memory = new MemoryStream())
                    {
                        int count = 0;
                        do
                        {
                            count = stream.Read(buffer, 0, size);
                            if (count > 0)
                            {
                                memory.Write(buffer, 0, count);
                            }
                        } while (count > 0);
                        return memory.ToArray();
                    }
                }
            }
            catch (InvalidDataException)
            {
                Logging.Log("File is not GZipped!");
                return compressed;
            }
        }

        
    }
}
