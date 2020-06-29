Imports System.IO

Module ImageSearch
    Public Sub ImageSearcher(ByVal path As String, ByVal disc As String)
        Dim extensions As New ArrayList From {
            ".bmp",
            ".gif",
            ".jpg",
            ".png",
            ".tif"
        }
        Using sw As New IO.StreamWriter(path & "\Images.txt")
            For Each dir As String In GetDirectories(disc)
                If Directory.Exists(dir) Then
                    For Each files As String In GetFiles(dir)
                        If extensions.ToArray().Any(Function(x) x.ToString().Contains(IO.Path.GetExtension(files).ToLower)) Then
                            If File.Exists(files) Then
                                'Console.WriteLine(files.Chars(files.Length - 4))
                                If files.Chars(files.Length - 4) = "." Then
                                    Dim info As New FileInfo(files)
                                    If ByteToKbyte(info.Length) > 100 Then
                                        sw.WriteLine(files)
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If
            Next
        End Using
    End Sub
End Module
