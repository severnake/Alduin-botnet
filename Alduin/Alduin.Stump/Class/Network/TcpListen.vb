Imports System.IO
Imports System.Net.Sockets
Namespace Alduin.Stump.Class.Network
    Public Class TcpListen
        Private adr As Net.IPAddress = Net.IPAddress.Parse("127.0.0.1")
        Private port As Integer = 25999
        Private client As TcpClient
        Private listener As TcpListener
        Private ReadOnly _command As ICommand
        Public Sub New(ByVal command As ICommand)
            listener = New TcpListener(adr, port)
            _command = command
        End Sub

        Public Async Function TcpAsync() As Task
            listener.Start()
            Dim Message
            Dim Result
            If listener.Pending = True Then
                client = listener.AcceptTcpClient
                Message = ""
                Dim Reader As New StreamReader(client.GetStream())
                While Reader.Peek > -1
                    Message = Message + Convert.ToChar(Reader.Read())
                End While
                'Call commands and wait result
                Result = Await _command.Handle(Message)
                Moduls.Network.StreamWrite.StreamWrite(client, Message)
                client.Close()
            End If
        End Function
    End Class
End Namespace

