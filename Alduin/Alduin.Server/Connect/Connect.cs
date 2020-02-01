using System;
using System.IO;
using System.Linq;
using System.Net;
using com.LandonKey.SocksWebProxy;
using System.Threading;
using Starksoft.Aspen.Proxy;
using System.Net.Sockets;

internal static partial class Connect
{
    public static TcpClient TCP;
    public static StreamWriter Write;
    public static StreamReader Read;
    private static string Server = ".onion";
    private static int port = 44359;
    private delegate void MessageReceived(string msg);
    private static Socks5ProxyClient proxyClient;


    public static void Send(string MSG) // Send MSG
    {
        try
        {
            proxyClient = new Socks5ProxyClient("127.0.0.1", 9150);
            TCP = proxyClient.CreateConnection(Server, port);
            Write = new StreamWriter(TCP.GetStream());
            Write.WriteLine(MSG);
            Write.Flush();
        }
        catch (Exception ex)
        {

        }
    }
}

