Public Class KillProcessFromId : Implements ICommand
    <STAThread()>
    Public Shared Function Handler(ByVal model As KillProcessModel)
        Dim result As LogModel
        Try
            Dim aProcess As System.Diagnostics.Process
            aProcess = System.Diagnostics.Process.GetProcessById(model.newVariables.Id)
            aProcess.Kill()
            result = New LogModel With {
                                .KeyUnique = GetConfigJson().KeyUnique,
                                .Message = "Process killed",
                                .Type = "Success"
                            }
        Catch ex As Exception
            result = New LogModel With {
                    .KeyUnique = GetConfigJson().KeyUnique,
                    .Message = ex.ToString,
                    .Type = "Error"
                }
        End Try
        Return result
    End Function
End Class
