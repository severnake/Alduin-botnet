using System;
using System.IO;
using Starksoft.Aspen.Proxy;
using System.Net.Sockets;

internal static partial class Connect
{
    public static TcpClient TCP;
    public static StreamWriter Write;
    private static int port = 50371;
    private delegate void MessageReceived(string msg);
    private static Socks5ProxyClient proxyClient;


    public static async void Send(string MSG, string Server) // Send MSG
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
        //Log
        }
    }


}

