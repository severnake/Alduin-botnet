Public Class ProcessModel
    Public Property Id As Integer
    Public Property ProcessName As String
    Public Property description As String
End Class
Public Class ProcessModelList
    Public Property ProcessModel As List(Of List(Of ProcessModel))
End Class
