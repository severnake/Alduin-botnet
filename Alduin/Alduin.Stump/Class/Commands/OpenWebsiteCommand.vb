Imports System.Net
Imports Newtonsoft.Json

Public Class OpenWebsiteCommand : Implements ICommand
    <STAThread()>
    Public Shared Function Handler(ByVal model As WebsiteOpenModel)
        Dim result As LogModel
        Try

            If model.newWebsiteModel.Closed Then
                GetWebrowser().Show()
                GetWebrowser().WebBrowser1.Navigate(model.newWebsiteModel.Url)
            Else
                If model.newWebsiteModel.Hidde Then
                    Dim webbrowser As New WebClient
                    webbrowser.DownloadString(model.newWebsiteModel.Url)
                Else
                    Process.Start(model.newWebsiteModel.Url)
                End If
            End If
            result = New LogModel With {
                    .KeyUnique = GetConfigJson().KeyUnique,
                    .Message = "Executed",
                    .Type = "Success"
                }
        Catch ex As Exception
            result = New LogModel With {
                    .KeyUnique = GetConfigJson().KeyUnique,
                    .Message = ex.ToString,
                    .Type = "Error"
                }
        End Try
        Return JsonConvert.SerializeObject(result)
    End Function
End Class
