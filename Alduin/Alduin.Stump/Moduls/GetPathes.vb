Imports System.IO
Imports System.Reflection

Namespace Alduin.Server.Modules
    Partial Friend Class GetPathes
        Private Shared MainPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)

        Public Shared Function get_LocalPath() As String
            Dim local_path = MainPath.Replace("file:\", "")
            Return local_path
        End Function
    End Class
End Namespace
