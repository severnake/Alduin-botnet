
Namespace Alduin.Stump.Class.Commands
    Public Class CommandHandler
        Implements ICommand
        Public Sub New()

        End Sub

        Public Async Function Handle(model As Object) As Task(Of Object) Implements ICommand.Handle
            Console.WriteLine("Runned Object")
            Console.WriteLine(model)
            Return "ok"
            Throw New NotImplementedException()
        End Function
        Public Async Function Handle(model As ExecuteModel) As Task(Of Object) Implements ICommand.Handle
            Console.WriteLine("Runned ExecuteCommand")
            Console.WriteLine(model)
            Return "ok"
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace


