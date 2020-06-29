Imports System.IO.Compression
Module UnZIPModule
    Public Sub UnZip(ByVal zipFileWidthPath As String, ByVal extractPath As String)
        ZipFile.ExtractToDirectory(zipFileWidthPath, extractPath)
    End Sub
    Public Sub InZip(ByVal zipFileWidthPath As String, ByVal extractPath As String)
        ZipFile.CreateFromDirectory(zipFileWidthPath, extractPath)
    End Sub
End Module
