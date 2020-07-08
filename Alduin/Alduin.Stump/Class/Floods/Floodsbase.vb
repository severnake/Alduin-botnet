Public Class Floodsbase
    Private _Message As String
    Private _AttackUpStrengOnByte As Integer = 1
    Private _AttackDownStrengOnByte As Integer = 1
    Private _AttackCount As Integer = 0
    Private _stopwatch As Stopwatch
    Public Function Get_AttackCount() As Integer
        Return _AttackCount
    End Function

    Public Sub Set_AttackCount(ByVal AutoPropertyValue As Integer)
        _AttackCount = AutoPropertyValue
    End Sub
    Public Sub New()
        SetStopWatch(Stopwatch.StartNew)
        SetAttackUpStrengOnByte(1)
        SetAttackDownStrengOnByte(1)
        Set_AttackCount(0)
    End Sub
    Public Overrides Function ToString() As String
        Return MyBase.ToString()
    End Function
    Public Function GetStopWatch() As Stopwatch
        Return _stopwatch
    End Function
    Private Sub SetStopWatch(ByVal stopwatch As Stopwatch)
        _stopwatch = stopwatch
    End Sub
    Public Function GetAttackUpStrengOnByte() As Integer
        Return _AttackUpStrengOnByte
    End Function
    Public Function GetAttackDownStrengOnByte() As Integer
        Return _AttackDownStrengOnByte
    End Function
    Public Sub SetAttackUpStrengOnByte(ByVal AutoPropertyValue As Integer)
        If AutoPropertyValue > 0 Then
            _AttackUpStrengOnByte += AutoPropertyValue
        End If
    End Sub
    Public Sub SetAttackDownStrengOnByte(ByVal AutoPropertyValue As Integer)
        If AutoPropertyValue > 0 Then
            _AttackDownStrengOnByte += AutoPropertyValue
        End If
    End Sub
    Public Function GetMessage() As String
        Return _Message
    End Function
    Public Function GetAttackUpStrengOnByteOnSec() As Double
        Return GetAttackUpStrengOnByte() / GetStopWatch().Elapsed.Seconds
    End Function
    Public Function GetAttackDownStrengOnByteOnSec() As Double
        Return GetAttackDownStrengOnByte() / GetStopWatch().Elapsed.Seconds
    End Function
    Public Sub SetMessage(ByVal AutoPropertyValue As String)
        _Message = AutoPropertyValue
    End Sub
    Public Sub Reset()
        SetMessage("")
        SetAttackUpStrengOnByte(1)
        SetAttackDownStrengOnByte(1)
        SetStopWatch(Stopwatch.StartNew)
        Set_AttackCount(0)
    End Sub
    Public Sub SetEnd()
        _stopwatch.Stop()
        'Set_AttackCount(0)
        'SetMessage("No Attack running")
        'SetAttackUpStrengOnByte(0)
        'SetAttackDownStrengOnByte(0)
    End Sub
End Class
