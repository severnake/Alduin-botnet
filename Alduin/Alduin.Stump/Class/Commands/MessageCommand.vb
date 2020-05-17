Imports Newtonsoft.Json

Public Class MessageCommand : Implements ICommand
    <STAThread()>
    Public Shared Function Handler(ByVal model As MessageModel)
        Dim result As LogModel
        Try
            If model.newMessageModel.Closed Then
                Do
                    MsgBox(model.newMessageModel.Msg)
                Loop
            Else
                MsgBox(model.newMessageModel.Msg)
            End If
            result = New LogModel With {
                    .KeyUnique = GetConfigJson().KeyUnique,
                    .Message = "Executed",
                    .Type = "Success"
                }
        Catch ex As Exception
            result = New LogModel With {
                    .KeyUnique = GetConfigJson().KeyUnique,
                    .Message = ex.ToString,
                    .Type = "Error"
                }
        End Try
        Return JsonConvert.SerializeObject(result)
    End Function
End Class
