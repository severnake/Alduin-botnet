Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Public Class AllImgToJson : Implements ICommand
    <STAThread()>
    Public Shared Function Handler()
        Dim allImg As New AllImgModel
        If System.IO.File.Exists(GetConfigJson().MainPath & "\Images.txt") = True Then
            Dim reader As New StreamReader(GetConfigJson().MainPath & "\Images.txt")
            Do While reader.Peek() <> -1
                allImg.Images.Add(reader.ReadLine())
            Loop
        End If
        Return JsonConvert.SerializeObject(allImg)
    End Function
End Class
