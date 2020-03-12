Public Interface ICommand
    Function Handle(ByVal model As Object) As Task(Of Object)
    Function Handle(model As ExecuteModel) As Task(Of Object)
End Interface
