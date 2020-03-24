Public Class DefaultRegistrationModel
    Public Property Username As String
    Public Property Address As String
    Public Property Key As String
    Public Property CountryCode As String
    Public Property LastIPAddress As String
    Public Property City As String
    Public Overrides Function ToString() As String
        Return "Username=" & Username &
            "&Address=" & Address &
            "&Key=" & Key &
            "&CountryCode=" & CountryCode &
            "&LastIPAddress=" & LastIPAddress &
            "&City=" & City
    End Function
End Class
