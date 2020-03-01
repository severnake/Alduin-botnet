Public Interface ICommand
    Function Handle(ByVal model As Object) As Task(Of Object)
End Interface
