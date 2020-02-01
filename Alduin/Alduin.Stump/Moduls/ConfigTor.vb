Imports System.IO
Imports System.Text
Imports System.Threading

Namespace Alduin.Server.Modules
    Partial Friend Module ConfigTor
        Private Tor = "tor"
        Private TorrcPath As String = GetPathes.get_LocalPath & "\Tor\Data\Tor\torrc"
        Private TorFolder As String = GetPathes.get_LocalPath & "\Tor"
        Private TorPath As String = GetPathes.get_LocalPath & "\Tor\tor.exe"
        Private AlduinWebPort = "50371"

        Public Sub StartTor()
            For Each proc As Process In Process.GetProcessesByName(Tor)
                proc.Kill()
            Next

            If Not File.Exists(TorrcPath) Then
                CreateTorrc()
            End If

            Dim p As Process()
            p = Process.GetProcessesByName(Tor)
            If Not (p.Length > 0) Then StartTorProccess()
        End Sub

        Private Sub StartTorProccess()
            Console.WriteLine("Starting tor...")
            Dim Process = New Process()
            Process.StartInfo.FileName = TorPath
            Process.StartInfo.Arguments = "-f " & TorrcPath
            Process.StartInfo.UseShellExecute = False
            Process.StartInfo.RedirectStandardOutput = True
            Process.StartInfo.CreateNoWindow = True
            Process.StartInfo.WorkingDirectory = TorFolder
            Process.StartInfo.RedirectStandardOutput = True
            Process.Start()
            Process.PriorityClass = ProcessPriorityClass.BelowNormal

            While Not Process.StandardOutput.EndOfStream
                Console.WriteLine(Process.StandardOutput.ReadLine())
            End While
        End Sub

        Function ToSec(ByVal sec As Integer) As Integer
            Return sec * 1000
        End Function

        Sub CreateTorrc()
            Dim filestring = "
                        ControlPort 9151
                        DataDirectory " & TorFolder & "
                        DirPort 9030
                        ExitPolicy reject *:*
                        HashedControlPassword 16:4E1F1599005EB8F3603C046EF402B00B6F74C008765172A774D2853FD4
                        HiddenServiceDir " & TorFolder & "
                        HiddenServicePort " & AlduinWebPort & " 127.0.0.1:5557
                        Log notice stdout
                        Nickname Alduin
                        SocksPort 9150"
            Dim fs As FileStream = File.Create(TorrcPath)
            Dim info = New UTF8Encoding(True).GetBytes(filestring)
            fs.Write(info, 0, info.Length)
            fs.Close()
        End Sub

        Sub CreateTorrc(ByVal data As String)
            Dim fs As FileStream = File.Create(TorrcPath)
            Dim info = New UTF8Encoding(True).GetBytes(data)
            fs.Write(info, 0, info.Length)
            fs.Close()
        End Sub
    End Module
End Namespace

