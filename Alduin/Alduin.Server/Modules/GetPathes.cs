using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Alduin.Server.Modules
{
    internal partial class GetPathes
    {
        private static string MainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        
        public static string get_LocalPath()
        {
            string local_path = MainPath.Replace(@"file:\", "");
            return local_path;
        }
        public static string get_TorPath()
        {
            return Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory)
                .Parent?.Parent?.Parent?.Parent?.FullName, "Tor");
        }
    }
}
