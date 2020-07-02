Imports System.IO
Imports Newtonsoft.Json

Public Class AllSourceFileToJson : Implements ICommand
    <STAThread()>
    Public Shared Function Handler()
        Dim allfiles As New GetAllSourceFileModel
        If System.IO.File.Exists(GetConfigJson().MainPath & "\SourceCodes.txt") = True Then
            Dim reader As New StreamReader(GetConfigJson().MainPath & "\SourceCodes.txt")
            Do While reader.Peek() <> -1
                allfiles.Files.Add(reader.ReadLine())
            Loop
        End If
        Return JsonConvert.SerializeObject(allfiles)
    End Function
End Class
