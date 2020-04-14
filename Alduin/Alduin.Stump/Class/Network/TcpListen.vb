Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports Starksoft.Aspen.Proxy
Namespace Alduin.Stump.Class.Network
    Public Class TcpListen
        Private ReadOnly adr As Net.IPAddress = Net.IPAddress.Parse(IPAddress.Loopback.ToString())
        Private client As TcpClient
        Private ReadOnly listener As TcpListener
        Private ReadOnly _command As ICommand


        Public Reader As StreamReader

        Public Sub New()
            listener = New TcpListener(adr, Config.Variables.ListenerPort)
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
                    Message += Convert.ToChar(Reader.Read())
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

