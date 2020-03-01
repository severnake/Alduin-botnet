
Namespace Alduin.Stump.Class.Handlers
    Public Class ExecuteCommandHandler
        Implements ICommand
        Public Sub New()

        End Sub
        Private Function ICommand_Handle(model As Object) As Task(Of Object) Implements ICommand.Handle
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace


