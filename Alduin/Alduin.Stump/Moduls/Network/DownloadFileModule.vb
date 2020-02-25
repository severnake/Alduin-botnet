Imports System.Net
Module DownloadFileModule
    Public Sub Downloader(ByVal url As String, ByVal filename As String)
        Using client As New WebClient()
            client.DownloadFile(url, filename)
        End Using
    End Sub
End Module
