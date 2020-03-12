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
            int ReachPort = 50371; //ReachPort
            try
            {
                proxyClient = new Socks5ProxyClient("127.0.0.1", 9150);
                proxyClient.ProxyUserName = "";
                proxyClient.ProxyPassword = "";
                TCP = proxyClient.CreateConnection(Address, ReachPort);
                Write = new StreamWriter(TCP.GetStream());
                Write.Write(MSG);
                Write.Flush();
                Reader = new StreamReader(TCP.GetStream());
                var ResultMessage = "";

                while (Reader.Peek() > -1)
                {
                    ResultMessage += Convert.ToChar(Reader.Read()).ToString();
                }
                return ResultMessage;
            }
            catch (Exception ex)
            {
                return "Error: " + ex;
            }
        }
    }
}
