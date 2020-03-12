Imports System.IO
Imports System.Net.Sockets
Namespace Alduin.Stump.Class.Network
    Public Class TcpListen
        Private adr As Net.IPAddress = Net.IPAddress.Parse(LocalIP)
        Private client As TcpClient
        Private listener As TcpListener
        Private ReadOnly _command As ICommand
        Public Sub New()
            listener = New TcpListener(adr, ListenerPort)
        End Sub

        Public Sub TcpAsync()
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
                Console.WriteLine(Message)
                Result = _command.Handle(Message)
                Moduls.Network.StreamWrite.StreamWrite(client, Result)
                client.Close()
            End If
        End Sub
    End Class
End Namespace

