using System.IO;

namespace HWP_backend.Services.BuilderServices
{
    public class FileMaker
    {
        public static void WriteToFile(string path, string fileName, string data)
        {
            if (!File.Exists($"{path}\\{fileName}.cpp")) File.Create($"{path}\\{fileName}.cpp").Dispose();

            using TextWriter tw = new StreamWriter($"{path}\\{fileName}.cpp");
            tw.Write(data);
        }
    }
}