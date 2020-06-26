using Alduin.Server.Commands;
using Newtonsoft.Json;
using Starksoft.Aspen.Proxy;
using System;
using System.IO;
using System.Net.Sockets;

namespace Alduin.Server.Services
{
    public class DownloadFileServices
    {
        public static TcpClient TCP;
        public static StreamWriter Write;
        public static StreamReader Reader;
        private static Socks5ProxyClient proxyClient;
        public void DownLoadFileByWebRequest(string host,string path, string filePath)
        {
            int ReachPort = 50371; //ReachPort
            GetImagesVariables variables = new GetImagesVariables
            {
                imagePath = path
            };
            BaseCommands command = new BaseCommands
            {
                Method = "GetImg"
            };
            GetImagesCommand model = new GetImagesCommand
            {
                newBaseCommand = command,
                newVariables = variables
            };
            string json = JsonConvert.SerializeObject(model);
            try
            {
                proxyClient = new Socks5ProxyClient("127.0.0.1", 9150);
                proxyClient.ProxyUserName = "";
                proxyClient.ProxyPassword = "";
                TCP = proxyClient.CreateConnection(host, ReachPort);
                Write = new StreamWriter(TCP.GetStream());
                Write.Write(json);
                Write.Flush();
                //Read
                NetworkStream stream = TCP.GetStream();
                int readSoFar = 0;
                int messageSize = 20000000;
                byte[] msg = new byte[messageSize];
                while (readSoFar < messageSize)
                {
                    var read = stream.Read(msg, readSoFar, msg.Length - readSoFar);
                    readSoFar += read;
                    if (read == 0)
                        break;
                }
                stream.Close();
                TCP.Close();

                if (File.Exists(filePath))
                    File.Delete(filePath);

                FileStream fstr = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                fstr.Write(cutByte(msg), 0, readSoFar);
                fstr.Close();
            }
            catch(Exception e) {
                Console.WriteLine(e.ToString());
            };
        }
        private static Byte[] cutByte(Byte[] inputByte)
        {
            int i = inputByte.Length - 1;
            while (inputByte[i] == 0)
                --i;

            byte[] bar = new byte[i + 1];
            Array.Copy(inputByte, bar, i + 1);
            return bar;
        }
        public static byte[] addByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 1);
            newArray[0] = newByte;
            return newArray;
        }
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}