Imports System.IO
Imports System.Net.Sockets

Public Class TcpListen
    Private adr As Net.IPAddress = Net.IPAddress.Parse("127.0.0.1")
    Private port As Integer = 25999
    Private client As TcpClient
    Private listener As TcpListener

    Public Sub New()
        listener = New TcpListener(adr, port)
    End Sub

    Public Async Function TcpAsync() As Task
        listener.Start()
        Dim Message As String
        If listener.Pending = True Then
            client = listener.AcceptTcpClient
            Message = ""
            Dim Reader As New StreamReader(client.GetStream())
            'Call commands and wait result
            Dim Writer As New StreamWriter(client.GetStream())
            Writer.WriteLine()
            Writer.Flush()
            client.Close()
        End If
    End Function
End Class
