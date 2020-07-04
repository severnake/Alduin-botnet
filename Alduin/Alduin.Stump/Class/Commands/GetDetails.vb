Imports System.IO
Imports Newtonsoft.Json

Public Class GetDetails
    <STAThread()>
    Public Shared Function Handler()
        'If File.Exists(GetConfigJson().MainPath & "\hardwares.json") Then
        'Return JsonConvert.SerializeObject(File_reader(GetConfigJson().MainPath & "\hardwares.json"))
        'Else
        Dim hardwares As New HardwareCollector
        Return JsonConvert.SerializeObject(hardwares)
        'End If
    End Function

End Class
