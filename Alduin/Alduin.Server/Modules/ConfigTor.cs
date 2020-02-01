using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Alduin.Server.Modules
{
    internal static partial class ConfigTor
    {
        public static string Tor = "tor";
        public static string TorrcPath = GetPathes.get_LocalPath() + @"\Tor\Data\Tor\torrc";
        public static string TorFolder = GetPathes.get_LocalPath() + @"\Tor";
        public static string TorPath = GetPathes.get_LocalPath() + @"\Tor\tor.exe";
        public static string AlduinWebPort = "50371";
        public static void StartTor()
        {
            foreach (Process proc in Process.GetProcessesByName(Tor))
            {
                proc.Kill();
            }
            if (!File.Exists(TorrcPath))
            {
                CreateTorrc();
            }
            Process[] p;
            p = Process.GetProcessesByName(Tor);
            if (!(p.Length > 0))
                StartTorProccess();
        }

        private static void StartTorProccess()
        {
            Console.WriteLine("Starting tor...");
            var Process = new Process();
            Process.StartInfo.FileName = TorPath;
            Process.StartInfo.Arguments = "-f " + TorrcPath;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.CreateNoWindow = true;
            Process.StartInfo.WorkingDirectory = TorFolder;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.Start();
            Process.PriorityClass = ProcessPriorityClass.BelowNormal;

            while (!Process.StandardOutput.EndOfStream)
            {
                Console.WriteLine(Process.StandardOutput.ReadLine());
            }
        }
        public static int ToSec(int sec)
        {
            return sec * 1000;
        }
        public static void CreateTorrc()
        {
            string filestring = @"
                        ControlPort 9151
                        DataDirectory " + TorFolder + @"
                        DirPort 9030
                        ExitPolicy reject *:*
                        HashedControlPassword 16:4E1F1599005EB8F3603C046EF402B00B6F74C008765172A774D2853FD4
                        HiddenServiceDir " + TorFolder + @"
                        HiddenServicePort " + AlduinWebPort + @" 127.0.0.1:5557
                        Log notice stdout
                        Nickname Alduin
                        SocksPort 9150";
            FileStream fs = File.Create(TorrcPath);
            var info = new UTF8Encoding(true).GetBytes(filestring);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
        public static void CreateTorrc(string data)
        {
            FileStream fs = File.Create(TorrcPath);
            var info = new UTF8Encoding(true).GetBytes(data);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
    }
}
