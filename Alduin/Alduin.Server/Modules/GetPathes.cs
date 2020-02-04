using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Alduin.Server.Modules
{
    public class GetPathes
    {
        private static string MainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
        private static string SolutionMainPath = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.Parent?.Parent?.FullName;
        public static string Get_LocalPath()
        {
            string local_path = MainPath.Replace(@"file:\", "");
            return local_path;
        }
        public static string Get_SolutionMainPath()
        {
            return SolutionMainPath;
        }
        public static string Get_TorPath()
        {
            return Path.Combine(SolutionMainPath, "Tor");
        }
        public static string Get_LogPath()
        {
            return Path.Combine(SolutionMainPath, "Log");
        }
    }
}
