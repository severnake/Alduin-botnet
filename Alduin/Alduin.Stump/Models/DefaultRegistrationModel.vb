Public Class DefaultRegistrationModel
    Public Property Username As String
    Public Property Address As String
    Public Property KeyUnique As String
    Public Property KeyCertified As String
    Public Property Domain As String
    Public Property CountryCode As String
    Public Property LastIPAddress As String
    Public Property City As String
    Public Overrides Function ToString() As String
        Return "Username=" & Username &
            "&KeyUnique=" & KeyUnique &
            "&KeyCertified=" & KeyCertified &
            "&Domain=" & Domain &
            "&CountryCode=" & CountryCode &
            "&LastIPAddress=" & LastIPAddress &
            "&City=" & City
    End Function
End Class
