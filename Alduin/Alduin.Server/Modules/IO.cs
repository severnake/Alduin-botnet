using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Alduin.Server.Modules
{
    public class ServerFile
    {
        public static string FileReader(string file)
        {
            return File.ReadAllText(file, Encoding.UTF8);
        }
        public static void FileWriter(string path, string data)
        {
            File.WriteAllText(path, data, Encoding.UTF8);
        }
        public static void FileDelete(string file)
        {
            File.Delete(file);
        }
    }
}
