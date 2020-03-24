Imports System.IO

Module CopyModule
    Public Sub Copy_directories(ByVal path As String)
        Dim Directories() As String = IO.Directory.GetDirectories(GetLocal_path())
        For Each Directory As String In Directories
            Dim currentDirectory = New DirectoryInfo(Directory)
            currentDirectory.MoveTo(path & "\" & currentDirectory.Name)
        Next
    End Sub
    Public Sub Copy_files(ByVal DestPath As String)
        Dim files() As String = IO.Directory.GetFiles(GetLocal_path())
        For Each file As String In files
            Dim currentfile = New FileInfo(file)
            currentfile.CopyTo(DestPath & "\" & currentfile.Name)
        Next
    End Sub
    Public Sub Copy_filesExept(ByVal DestPath As String, ByVal exept As ArrayList)
        Dim files() As String = IO.Directory.GetFiles(GetLocal_path())
        For Each file As String In files
            Dim result As Boolean = False
            Dim currentfile = New FileInfo(file)
            For i = 0 To exept.Count - 1
                If currentfile.Name = exept(i) Then
                    result = True
                End If
            Next
            If Not result Then
                currentfile.CopyTo(DestPath & "\" & currentfile.Name)
            End If
        Next
    End Sub
End Module
