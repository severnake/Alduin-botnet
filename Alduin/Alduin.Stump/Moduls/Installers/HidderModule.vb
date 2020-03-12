Imports System.IO
Module HidderModule
    Public Sub Hide_files(ByVal path As String)
        Dim files() As String = IO.Directory.GetFiles(path)
        For Each file As String In files
            Dim currentfile = New FileInfo(file)
            IO.File.SetAttributes(path & "/" & currentfile.Name, IO.FileAttributes.Hidden)
        Next
    End Sub
    Public Sub Hide_directories(ByVal path As String)
        Dim Directories() As String = IO.Directory.GetDirectories(path)
        For Each Directory As String In Directories
            Dim currentDirectory = New DirectoryInfo(Directory)
            IO.File.SetAttributes(path & "/" & currentDirectory.Name, IO.FileAttributes.Hidden)
        Next
    End Sub
End Module
