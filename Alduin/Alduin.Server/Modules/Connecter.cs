using System;
using System.IO;
using Starksoft.Aspen.Proxy;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace Alduin.Server.Modules
{
    public class Connecter
    {
        public static TcpClient TCP;
        public static StreamWriter Write;
        public static StreamReader Reader;
        private static Socks5ProxyClient proxyClient;

        public static string CreateTcpSend(string Address, string model)
        {
            int ReachPort = 50371; //ReachPort
            try
            {
                proxyClient = new Socks5ProxyClient("127.0.0.1", 9150);
                proxyClient.ProxyUserName = "";
                proxyClient.ProxyPassword = "";
                TCP = proxyClient.CreateConnection(Address, ReachPort);
                //TCP.SendBufferSize = 1024;
                Write = new StreamWriter(TCP.GetStream());
                Write.Write(model);
                Write.Flush();
                NetworkStream stream = TCP.GetStream();
                int messageSize = TCP.ReceiveBufferSize;
                int readSoFar = 0;
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
                string responseData = System.Text.Encoding.UTF8.GetString(cutByte(msg), 0, cutByte(msg).Length);
                return responseData;
            }
            catch (Exception ex)
            {
                return "Error: " + ex;
            }
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
    }
}
