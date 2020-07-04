Imports System.IO
Imports System.Text

Module IOModule

    Public Function File_reader(ByVal files As String) As String
        Return File.ReadAllText(files, System.Text.Encoding.UTF8)
    End Function
    Public Sub Delete_file(ByVal files As String)
        File.Delete(files)
    End Sub

    Public Sub Write_file(ByVal pathwithfile As String, ByVal text As String)
        Dim fs As FileStream = File.Create(pathwithfile)
        Dim info As Byte() = New UTF8Encoding(True).GetBytes(text)
        fs.Write(info, 0, info.Length)
        fs.Close()
    End Sub
    Public Sub CreateDirectory(ByVal path As String)
        Directory.CreateDirectory(path)
    End Sub
End Module
