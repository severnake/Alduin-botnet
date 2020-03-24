Imports System.IO
Imports System.Reflection
Imports System.Environment

Module GetPathes
    Private ReadOnly MainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)
    Private ReadOnly MainJson As String = "MainJson.json"
    Private ReadOnly appData As String = GetFolderPath(SpecialFolder.ApplicationData)
    Private ReadOnly exePath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
    Public Function Get_LocalPath() As String
        Dim local_path = MainPath.Replace("file:\", "")
        Return local_path
    End Function
    Public Function Get_JsonFilewithPath() As String
        Return GetAppdata() & "\" & MainJson
    End Function
    Public Function GetAppdata() As String
        Return appData
    End Function
    Public Function GetMainFile()
        Return Process.GetCurrentProcess().MainModule.ModuleName
    End Function
    Public Function GetDirectories(ByVal path As String) As List(Of String)
        Dim subfolders As New List(Of String)
        Try
            For Each subfolder As String In IO.Directory.GetDirectories(path)
                subfolders.Add(subfolder)

                Try
                    subfolders.AddRange(GetDirectories(subfolder))
                Catch ex As UnauthorizedAccessException
                    'Ignore this folder and move on.
                End Try
            Next subfolder
        Catch
        End Try
        Return subfolders
    End Function
    Public Function GetFiles(ByVal path As String) As List(Of String)
        Dim files As New List(Of String)
        Try
            For Each file As String In IO.Directory.GetFiles(path)

                files.Add(file)

                Try
                    files.AddRange(GetDirectories(file))
                Catch ex As UnauthorizedAccessException
                    'Ignore this folder and move on.
                End Try
            Next file
        Catch
        End Try
        Return files
    End Function
    Public Function GetLocal_path()
        Dim local_path As String = exePath.Replace("file:\", "")
        Return local_path
    End Function
End Module

