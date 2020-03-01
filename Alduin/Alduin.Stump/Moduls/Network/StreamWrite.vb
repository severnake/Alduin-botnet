Imports System.IO
Imports System.Net.Sockets
Namespace Alduin.Stump.Moduls.Network
    Module StreamWrite
        Public Sub StreamWrite(ByVal client As TcpClient, ByVal msg As String)
            Dim Writer As New StreamWriter(client.GetStream())
            Writer.WriteLine(msg)
            Writer.Flush()
        End Sub
        Public Sub StreamWrite(ByVal client As TcpClient, ByVal msg As Object)
            Dim Writer As New StreamWriter(client.GetStream())
            Writer.Write(msg)
            Writer.Flush()
        End Sub
    End Module
End Namespace


