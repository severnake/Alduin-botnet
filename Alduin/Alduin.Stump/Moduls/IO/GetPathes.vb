Imports System.IO
Imports System.Reflection

Namespace Alduin.Stump.Moduls.IO
    Partial Friend Class GetPathes
        Private Shared MainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)
        Private Shared MainJson = "MainJson.json"

        Public Shared Function get_LocalPath() As String
            Dim local_path = MainPath.Replace("file:\", "")
            Return local_path
        End Function
        Public Shared Function get_JsonFilewithPath() As String
            Return getAppdata() & "\" & MainJson
        End Function
    End Class
End Namespace
