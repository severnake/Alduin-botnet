Public Class Floodsbase
    Private _Message As String
    Private _AttackUpStrengOnByte As Integer
    Private _AttackDownStrengOnByte As Integer
    Private __AttackCount As Integer

    Private Function Get_AttackCount() As Integer
        Return __AttackCount
    End Function

    Private Sub Set_AttackCount(AutoPropertyValue As Integer)
        __AttackCount = AutoPropertyValue
    End Sub

    Private _stopwatch As Stopwatch
    Public Sub New()
        _stopwatch = Stopwatch.StartNew
        _AttackUpStrengOnByte = 0
        _AttackDownStrengOnByte = 0
        Set_AttackCount(0)
    End Sub
    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
    Public Function GetStopWatch() As Stopwatch
        Return _stopwatch
    End Function
    Public Function GetAttackUpStrengOnByte() As Integer
        Return _AttackUpStrengOnByte
    End Function
    Public Function GetAttackDownStrengOnByte() As Integer
        Return _AttackDownStrengOnByte
    End Function
    Public Sub SetAttackUpStrengOnByte(ByVal AutoPropertyValue As Integer)
        _AttackUpStrengOnByte = AutoPropertyValue
    End Sub
    Public Sub SetAttackDownStrengOnByte(ByVal AutoPropertyValue As Integer)
        _AttackDownStrengOnByte = AutoPropertyValue
    End Sub
    Public Function GetMessage() As String
        Return _Message
    End Function
    Public Function GetAttackUpStrengOnByteOnSec()
        Return GetAttackUpStrengOnByte() / GetStopWatch().Elapsed.Seconds
    End Function
    Public Function GetAttackDownStrengOnByteOnSec()
        Return GetAttackDownStrengOnByte() / GetStopWatch().Elapsed.Seconds
    End Function
    Public Sub SetMessage(ByVal AutoPropertyValue As String)
        _Message = AutoPropertyValue
    End Sub
    Public Sub Reset()
        _Message = vbNull
        _AttackUpStrengOnByte = 0
        _AttackDownStrengOnByte = 0
        _stopwatch = Stopwatch.StartNew
        Set_AttackCount(0)
    End Sub
    Public Sub SetEnd()
        Set_AttackCount(0)
        _Message = vbNull
        _AttackUpStrengOnByte = 0
        _AttackDownStrengOnByte = 0
    End Sub
End Class
