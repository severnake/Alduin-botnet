Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Public Class AllImgToJson : Implements ICommand
    <STAThread()>
    Public Shared Function Handler()
        Dim reader As New StreamReader(Get_LocalPath() & "\Images.txt", Encoding.Default)
        Dim allImg As New AllImgModel
        If System.IO.File.Exists(Get_LocalPath() & "\Images.txt") = True Then
            Do While reader.Peek() <> -1
                allImg.Imges.Add(reader.ReadLine())
            Loop
        End If
        Return JsonConvert.SerializeObject(allImg)
    End Function
End Class
