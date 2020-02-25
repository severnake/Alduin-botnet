Imports System.Diagnostics
Imports System.Environment
Imports System.IO
Imports System.Net
Module IOModule
    Private appData As String = GetFolderPath(SpecialFolder.ApplicationData)
    Private exePath As String = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
    Public Function File_reader(ByVal files As String)
        Return File.ReadAllText(files, System.Text.Encoding.UTF8)
    End Function
    Public Sub Delete_file(ByVal files As String)
        File.Delete(files)
    End Sub
    Public Function getAppdata() As String
        Return appData
    End Function
    Public Function getLocal_path()
        Dim local_path As String = exePath.Replace("file:\", "")
        Return local_path
    End Function
    Public Sub CreateDirectory(ByVal path As String)
        Directory.CreateDirectory(path)
    End Sub
    Public Sub Starter(ByVal exe_file_path As String, ByVal argumentums As String)
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = exe_file_path
        startInfo.Arguments = argumentums
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.Start(startInfo)
    End Sub
End Module
