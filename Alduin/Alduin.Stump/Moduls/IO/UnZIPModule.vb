Imports System.IO.Compression
Module UnZIPModule
    Public Sub UnZip(ByVal zipFileWidthPath As String, ByVal extractPath As String)
        ZipFile.ExtractToDirectory(zipFileWidthPath, extractPath)
    End Sub
End Module
