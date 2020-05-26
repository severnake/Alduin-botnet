Imports System.Security.Cryptography
Imports System.Text

Module HashKey
    Public Function GetHash(ByVal hashAlgorithm As HashAlgorithm, ByVal input As String) As String
        Dim data As Byte() = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input))
        Dim sBuilder = New StringBuilder()

        For i As Integer = 0 To data.Length - 1
            sBuilder.Append(data(i).ToString("x2"))
        Next

        Return sBuilder.ToString()
    End Function

    Public Function VerifyHash(ByVal hashAlgorithm As HashAlgorithm, ByVal input As String, ByVal hash As String) As Boolean
        Dim hashOfInput = GetHash(hashAlgorithm, input)
        Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
        Return comparer.Compare(hashOfInput, hash) = 0
    End Function
End Module
