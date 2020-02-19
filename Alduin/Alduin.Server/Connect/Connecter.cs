using System;
using System.IO;
using Starksoft.Aspen.Proxy;
using System.Net.Sockets;
namespace Alduin.Server.Connecter
{
    public class Connecter
    {
        public static TcpClient TCP;
        public static StreamWriter Write;
        public static StreamReader Reader;
        private readonly int port = 44359;
        private delegate void MessageReceived(string msg);
        private static Socks5ProxyClient proxyClient;

        public Connecter()
        {
        }

        public string CreateTcpSend(string Address, object MSG)
        {
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
                //Log
                return "Error: " + ex;
            }
        }
    }
}
