Imports System.IO
Imports System.Net.Sockets

Public Class GetSourceFile : Implements ICommand
    <STAThread()>
    Public Shared Function Handler(ByVal model As GetSourceFileModel, ByVal client As TcpClient)
        Try
            Dim sourcefile As Byte() = File.ReadAllBytes(model.newVariables.filePath)
            StreamWriterFileToTCP(sourcefile, client)
        Catch ex As Exception

        End Try
        Return ""
    End Function
End Class
