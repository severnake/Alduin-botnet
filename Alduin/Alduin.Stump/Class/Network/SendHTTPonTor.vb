Imports System.IO
Imports System.Net.Sockets
Imports System.Text
Imports Newtonsoft.Json
Imports Starksoft.Aspen.Proxy

Public Class SendHTTPonTor
    Private proxyClient As Socks5ProxyClient
    Public Write As StreamWriter
    Public TCP As New TcpClient
    Public Sub TalkChannelHTTP(ByVal msg As Object, ByVal att As String)
        Dim jsonString As String = JsonConvert.SerializeObject(msg, Formatting.Indented)
        Try
            Dim header As String = "POST /api/gate/" & att & " HTTP/1.1
Host: " & LocalIP & ":" & 44359 & "
Content-Type: application/json; charset=UTF-8
Content-Length: " & jsonString.Length & "

" & jsonString
            proxyClient = New Socks5ProxyClient(LocalIP, SocketPort) With {
                .ProxyUserName = "",
                .ProxyPassword = ""
            }
            TCP = proxyClient.CreateConnection(ServerDomain, ServerReachPort)
            Write = New StreamWriter(TCP.GetStream())
            Write.Write(header)
            Write.Flush()

            Dim reader = New StreamReader(TCP.GetStream()) 'Debug
            Console.WriteLine("Send!...")
            Console.WriteLine(header)
            While reader.Peek > -1
                Console.Write(Convert.ToChar(reader.Read()).ToString)
            End While
            TCP.Client.Close()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub
    Public Sub TalkChannelHTTP(ByVal msg As String, ByVal att As String) 'Not ready yet
        Dim jsonString As String = JsonConvert.SerializeObject(msg)
        Try
            Dim header As String = "POST /api/gate/" & att & " HTTP/1.1
Content-Type: application/json

" & jsonString
            proxyClient = New Socks5ProxyClient(LocalIP, SocketPort) With {
                .ProxyUserName = "",
                .ProxyPassword = ""
            }
            TCP = proxyClient.CreateConnection(ServerDomain, ReachPort)
            Write = New StreamWriter(TCP.GetStream())
            Write.Write(header)
            Write.Flush()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub
End Class
