Imports System.Net

Module WebFilters
    Public Sub Go(ByVal url As String)
        Dim client As New WebClient()
        client.DownloadString(url)
    End Sub

End Module
