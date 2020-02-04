using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Alduin.Server.Modules
{
    public class ConfigTor
    {
        private static string Tor = "tor";
        public static string AlduinWebPort = "44359";

        public static string TorBaseFolder = GetPathes.Get_TorPath();
        public static string TorrcPath = TorBaseFolder + @"\Data\Tor\torrc";
        public static string TorPath = TorBaseFolder + @"\tor.exe";
        
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
            ServerFileManager.FileAppendTextWithDate(GetPathes.Get_LogPath() + @"\Log.txt", "Starting tor...\r\n");
            var Process = new Process();
            Process.StartInfo.FileName = TorPath;
            Process.StartInfo.Arguments = "-f " + TorrcPath;
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.StartInfo.CreateNoWindow = true;
            Process.StartInfo.WorkingDirectory = TorBaseFolder;
            Process.StartInfo.RedirectStandardOutput = true;
            Process.Start();
            Process.PriorityClass = ProcessPriorityClass.BelowNormal;

            while (!Process.StandardOutput.EndOfStream)
            {
                WriteOutput(Process.StandardOutput.ReadLine());
            }
        }
        private static void WriteOutput(string data)
        {
            Console.WriteLine(data);
            ServerFileManager.FileAppendTextWithDate(GetPathes.Get_LogPath() + @"\Log.txt", data + "<br>");
        }
        public static int ToSec(int sec)
        {
            return sec * 1000;
        }
        public static void CreateTorrc()
        {
            string filestring = @"
ControlPort 9151
DataDirectory " + TorBaseFolder + @"
DirPort 9030
ExitPolicy reject *:*
HashedControlPassword 16:4E1F1599005EB8F3603C046EF402B00B6F74C008765172A774D2853FD4
HiddenServiceDir " + TorBaseFolder + @"
HiddenServicePort " + AlduinWebPort + @" 127.0.0.1:5557
Log notice stdout
Nickname Alduin
SocksPort 9150";
            FileStream fs = File.Create(TorrcPath);
            var info = new UTF8Encoding(false).GetBytes(filestring);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
        public static void CreateTorrc(string data)
        {
            FileStream fs = File.Create(TorrcPath);
            var info = new UTF8Encoding(false).GetBytes(data);
            fs.Write(info, 0, info.Length);
            fs.Close();
        }
    }
}
