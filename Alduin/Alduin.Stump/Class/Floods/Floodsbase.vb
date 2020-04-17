Public Class Floodsbase
    Private _Message As String
    Private __AttackUpStrengOnByte As Integer
    Private __AttackDownStrengOnByte As Integer
    Private stopwatch As Stopwatch
    Public Sub New(ByVal model As Object)
        stopwatch = Stopwatch.StartNew
    End Sub
    Public Sub selectMethod()

    End Sub
    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
    Public Function GetStopWatch() As Stopwatch
        Return stopwatch
    End Function
    Public Function Get_AttackUpStrengOnByte() As Integer
        Return __AttackUpStrengOnByte
    End Function
    Public Function Get_AttackDownStrengOnByte() As Integer
        Return __AttackDownStrengOnByte
    End Function
    Public Sub Set_AttackUpStrengOnByte(AutoPropertyValue As Integer)
        __AttackUpStrengOnByte = AutoPropertyValue
    End Sub
    Public Sub Set_AttackDownStrengOnByte(AutoPropertyValue As Integer)
        __AttackDownStrengOnByte = AutoPropertyValue
    End Sub
    Public Function GetMessage() As String
        Return _Message
    End Function
    Public Function GetAttackUpStrengOnByteOnSec()
        Return Get_AttackUpStrengOnByte() / GetStopWatch().Elapsed.Seconds
    End Function
    Public Function GetAttackDownStrengOnByteOnSec()
        Return Get_AttackDownStrengOnByte() / GetStopWatch().Elapsed.Seconds
    End Function
    Public Sub SetMessage(AutoPropertyValue As String)
        _Message = AutoPropertyValue
    End Sub
End Class
