
Imports Newtonsoft.Json

Namespace Alduin.Stump.Class.Commands
    Public Class CommandHandler
        Public Sub New()

        End Sub
        Public Function CommandJsonToObject(ByVal model As String) As String
            Dim method As String = ""
            For i = model.IndexOf("Method") To model.Length - 1
                If model.Chars(i) = "}" Or model.Chars(i) = "," Then
                    Exit For
                End If
                method += model.Chars(i)
            Next
            method = method.Replace("""", "").Replace(":", "").Replace(" ", "").Replace(",", "").Replace("Method", "")
            Select Case method
                Case "Execute"
                    Dim ModelDes As ExecuteModel = JsonConvert.DeserializeAnonymousType(model, New ExecuteModel)
                    Return Handler(ModelDes)
                Case "Website"
            End Select
            Return "Command execute Failed!"
        End Function
    End Class
End Namespace


