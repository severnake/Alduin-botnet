Imports System.IO
Imports System.Net.Sockets
Imports Newtonsoft.Json
Imports Starksoft.Aspen.Proxy

Public Class SendHTTPonTor
    Private proxyClient As Socks5ProxyClient
    Public Write As StreamWriter
    Public TCP As TcpClient
    Public Sub TalkChannelHTTP(ByVal msg As Object, ByVal att As String)
        Dim jsonString As String = JsonConvert.SerializeObject(msg)
        Try
            Dim header As String = "POST /api/gate/" & att & " HTTP/1.1
Content-Type: application/json
Content-Length: " & jsonString.Length & "

" & jsonString
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
