using System;
using System.IO;
using Starksoft.Aspen.Proxy;
using System.Net.Sockets;
namespace Alduin.Server.Modules
{
    public class Connecter
    {
        public static TcpClient TCP;
        public static StreamWriter Write;
        public static StreamReader Reader;
        private static Socks5ProxyClient proxyClient;

        public static string CreateTcpSend(string Address, object MSG)
        {
            int port = 44359;
            try
            {
                proxyClient = new Socks5ProxyClient("127.0.0.1", 9150);
                TCP = proxyClient.CreateConnection(Address, port);
                Write = new StreamWriter(TCP.GetStream());
                Write.WriteLine(MSG);
                Write.Flush();
                Reader = new StreamReader(TCP.GetStream());
                return Reader.ToString();
            }
            catch (Exception ex)
            {
                return "Error: " + ex;
            }
        }
    }
}
