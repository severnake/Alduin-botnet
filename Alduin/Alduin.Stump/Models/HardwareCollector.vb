Public Class HardwareDetails
    Public Property HardwareName As String
    Public Property HardwareType As String
    Public Property Performance As String
    Public Property OtherInformation As String
End Class
Public Class HardwareCollector


    Public Property Cpu As HardwareDetails = GetCPU()
    Public Property Gpu As HardwareDetails = GetVideoCard()
    Public Property Ram As HardwareDetails = GetRam()
    Public Property Os As HardwareDetails = getOS()
    Public Property OtherHarwares As List(Of List(Of HardwareDetails)) = New List(Of List(Of HardwareDetails)) From {GetAllHardware()}
End Class