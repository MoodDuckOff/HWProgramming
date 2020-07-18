using System;
using System.IO;

namespace HWP_backend.Services.BuilderServices
{
    public class FileMaker
    {
        public static void WriteToFile(string path, string fileName, string data, bool isLinux)
        {
            if (isLinux)
            {
                if (!File.Exists($"{path}/{fileName}.cpp")) 
                    File.Create($"{path}/{fileName}.cpp").Dispose();

                using TextWriter tw = new StreamWriter($"{path}/{fileName}.cpp");
                tw.Write(data);
            }
            else
            {
                if (!File.Exists($"{path}\\{fileName}.cpp")) 
                    File.Create($"{path}\\{fileName}.cpp").Dispose();

                using TextWriter tw = new StreamWriter($"{path}\\{fileName}.cpp");
                tw.Write(data);
            }

        }

        public static void DeleteFile(string path, string filename, bool isLinux)
        {

            try
            {

                if (isLinux)
                {
                    if (File.Exists($"{path}/{filename}.cpp"))
                        File.Delete($"{path}/{filename}.cpp");
                    if (File.Exists($"{path}/{filename}"))
                        File.Delete($"{path}/{filename}");
                }
                else
                {
                    if (File.Exists($"{path}\\{filename}.cpp"))
                        File.Delete($"{path}\\{filename}.cpp");
                    if (File.Exists($"{path}\\{filename}.exe"))
                        File.Delete($"{path}\\{filename}.exe");
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\nDELETE FILE ERROR");
            }
        }
    }
}