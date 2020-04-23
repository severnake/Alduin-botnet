Imports System.Threading
Module CloseDetect
    Declare Function SetConsoleCtrlHandler Lib "kernel32.dll" (ByVal HandlerRoutine As ControlEventHandler,
                                                               ByVal Add As Boolean) As Int32

    Public Enum ConsoleEvent
        CTRL_C = 0
        CTRL_BREAK = 1
        CTRL_CLOSE = 2
        CTRL_LOGOFF = 5
        CTRL_SHUTDOWN = 6
    End Enum

    Public Delegate Sub ControlEventHandler(ByVal consoleEvent As ConsoleEvent)

    Public Sub OnControlEvent(ByVal consoleEvent As ConsoleEvent)
        'Control-C doesn't debug well
        Threading.Interlocked.Decrement(isRunning)
        Console.WriteLine("Event: {0}", consoleEvent)
        Console.WriteLine("Thread closed gracefully")
        If Main.Config.Variables.Debug Then
            Console.WriteLine("Kill Tor...")
            KillTor()
        Else
            Console.WriteLine("Restart...")
            Process.Start(GetMainFile)
        End If
        Thread.Sleep(500)
    End Sub

    Dim t As New Thread(AddressOf looping)
    Dim isRunning As Long

    Sub Detecter()
        SetConsoleCtrlHandler(New ControlEventHandler(AddressOf OnControlEvent), True)
        isRunning = 1L
        t.Start()
        t.Join()
    End Sub

    Sub looping()
        If Main.Config.Variables.Debug Then
            Console.WriteLine("CloseDetecter working...")
        End If
        Do
            Thread.Sleep(500)
        Loop While Threading.Interlocked.Read(isRunning) > 0L
    End Sub
End Module
