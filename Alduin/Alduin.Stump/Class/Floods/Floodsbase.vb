Public Class Floodsbase
    Private _Message As String
    Private __AttackStrengOnByte As Integer

    Public Sub New(ByVal model As Object)
    End Sub
    Public Sub selectMethod()

    End Sub
    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function

    Public Function Get_AttackStrengOnByte() As Integer
        Return __AttackStrengOnByte
    End Function

    Public Sub Set_AttackStrengOnByte(AutoPropertyValue As Integer)
        __AttackStrengOnByte = AutoPropertyValue
    End Sub

    Public Function GetMessage() As String
        Return _Message
    End Function
    Public Function GetAttackStrengOnByteOnSec(ByVal stopwatch As Stopwatch)
        Return Get_AttackStrengOnByte() / stopwatch.Elapsed.Seconds
    End Function
    Public Sub SetMessage(AutoPropertyValue As String)
        _Message = AutoPropertyValue
    End Sub
End Class
