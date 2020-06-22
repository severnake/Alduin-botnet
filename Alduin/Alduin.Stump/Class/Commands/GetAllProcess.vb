Imports Newtonsoft.Json

Public Class GetAllProcess : Implements ICommand
    <STAThread()>
    Public Shared Function Handler()
        Return JsonConvert.SerializeObject(GetProcess())
    End Function
    Public Shared Function GetProcess() As List(Of ProcessModel)

        Dim MyOBJ As Object = GetObject("WinMgmts:").instancesof("Win32_Process")
        Dim obj As Object
        Dim newHardwareDetails As New List(Of ProcessModel)

        Dim j As Integer = 0
        For Each obj In MyOBJ
            Try
                newHardwareDetails.Add(New ProcessModel With {
                    .Id = obj.ProcessId.ToString,
                    .ProcessName = obj.Name.ToString,
                    .description = obj.Description.ToString
                })
                j += 1
            Catch ex As Exception
                If Config.Variables.Debug Then
                    Console.WriteLine("Get Hardware error: " & ex.ToString)
                End If
            End Try
        Next
        Return newHardwareDetails
    End Function
End Class
