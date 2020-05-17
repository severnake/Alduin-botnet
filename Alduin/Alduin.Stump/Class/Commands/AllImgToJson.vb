Imports System.IO
Imports System.Text
Imports Newtonsoft.Json

Public Class AllImgToJson : Implements ICommand
    <STAThread()>
    Public Shared Function Handler()
        Dim objReader As New System.IO.StreamReader(Get_LocalPath() & "\Images.txt")
        Dim allImg As New AllImgModel
        Do While objReader.Peek() <> -1
            allImg.Imges.Add(objReader.ReadLine())
        Loop
        Return JsonConvert.SerializeObject(allImg)
    End Function
End Class
