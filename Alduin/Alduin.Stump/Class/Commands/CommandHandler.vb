
Imports Newtonsoft.Json

Namespace Alduin.Stump.Class.Commands
    Public Class CommandHandler
        Public Sub New()

        End Sub

        Public Async Function JsonDes(model As String) As Task(Of String)
            Dim ModelDes As BaseCommandHandlerModel = JsonConvert.DeserializeAnonymousType(model, CommandModel(model))

            Return "ok" 'not ready yet
            Throw New NotImplementedException()
        End Function
        Public Function CommandModel(ByVal model As String) As Object
            Dim Modeldes As BaseCommandHandlerModel = JsonConvert.DeserializeAnonymousType(model, New BaseCommandHandlerModel)
            Select Case Modeldes.Method
                Case "Execute"
                    Return New ExecuteModel()
            End Select
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace


