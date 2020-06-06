Imports System.Text
Imports Microsoft.VisualBasic.Devices

Module ComputerDetails
    '"Win32_Process",
    Private objects As List(Of String) = New List(Of String)(New String() {
"Win32_1394Controller",
"Win32_1394ControllerDevice",
"Win32_AssociatedBattery",
"Win32_BaseBoard",
"Win32_Battery",
"Win32_BIOS",
"Win32_CacheMemory",
"Win32_CDROMDrive",
"Win32_ClientApplicationSetting",
"Win32_ComClassAutoEmulator",
"Win32_ComputerSystem",
"Win32_CurrentProbe",
"Win32_DesktopMonitor",
"Win32_DiskDrive",
"Win32_DMAChannel",
"Win32_DriverVXD",
"Win32_Fan",
"Win32_FloppyController",
"Win32_FloppyDrive",
"Win32_HeatPipe",
"Win32_IDEController",
"Win32_InfraredDevice",
"Win32_Keyboard",
"Win32_MethodParameterClass",
"Win32_MotherboardDevice",
"Win32_NetworkAdapter",
"Win32_NetworkClient",
"Win32_NetworkConnection",
"Win32_OnBoardDevice",
"Win32_OperatingSystem",
"Win32_ParallelPort",
"Win32_PCMCIAController",
"Win32_PerfRawData_RSVP_ACSPerRSVPService",
"Win32_PerfRawData_Tcpip_IP",
"Win32_PerfRawData_Tcpip_TCP",
"Win32_PerfRawData_Tcpip_UDP",
"Win32_PerfRawData_W3SVC_WebService",
"Win32_PnPEntity",
"Win32_PointingDevice",
"Win32_PortableBattery",
"Win32_POTSModem",
"Win32_POTSModemToSerialPort",
"Win32_PowerManagementEvent",
"Win32_Printer",
"Win32_PrinterController",
"Win32_PrinterShare",
"Win32_PrintJob",
"Win32_PrivilegesStatus",
"Win32_Processor",
"Win32_ProcessStartup",
"Win32_Registry",
"Win32_ScheduledJob",
"Win32_SCSIController",
"Win32_SCSIControllerDevice",
"Win32_SecurityDescriptor",
"Win32_SecuritySettingAuditing",
"Win32_SecuritySettingGroup",
"Win32_SecuritySettingOwner",
"Win32_SerialPort",
"Win32_Share",
"Win32_SMBIOSMemory",
"Win32_SoundDevice",
"Win32_SystemDriver",
"Win32_SystemEnclosure",
"Win32_SystemNetworkConnections",
"Win32_SystemSlot",
"Win32_TapeDrive",
"Win32_TemperatureProbe",
"Win32_UninterruptiblePowerSupply",
"Win32_VideoConfiguration",
"Win32_VideoController",
"Win32_VoltageProbe"})

    Public Function GetCPU() As HardwareDetails

        Dim MyOBJ As Object = GetObject("WinMgmts:").instancesof("Win32_Processor")
        Dim cpu As Object
        Dim newHardwareDetails As New HardwareDetails()

        Dim j As Integer = 0
        For Each cpu In MyOBJ
            Try
                newHardwareDetails = New HardwareDetails With {
                    .HardwareName = "Name: " & cpu.Name.ToString,
                    .HardwareType = " Type: CPU",
                    .Performance = "Clock Speed: " & cpu.CurrentClockSpeed.ToString + "Mhz",
                    .OtherInformation = "OtherInformation! L2 Memory size: " & cpu.L2CacheSize.ToString & " and CPU Threads: " & cpu.ThreadCount.ToString
                }
                j += 1
            Catch ex As Exception
                If Config.Variables.Debug Then
                    Console.WriteLine("Get Hardware error: " & ex.ToString)
                End If
            End Try
        Next
        Return newHardwareDetails
    End Function
    Public Function GetVideoCard() As HardwareDetails

        Dim objWMIService As Object = GetObject("winmgmts:\\.\root\cimv2")
        Dim colDevices As Object = objWMIService.ExecQuery("Select * From Win32_VideoController")
        Dim objDevice As Object
        Dim newHardwareDetails As New HardwareDetails()
        Dim j As Integer = 0
        For Each objDevice In colDevices
            Try
                newHardwareDetails = New HardwareDetails With {
                    .HardwareName = "Name: " & objDevice.Name,
                    .HardwareType = "Type: GPU",
                    .Performance = "Memory: " & objDevice.AdapterRAM,
                    .OtherInformation = " OtherInformation! MaxMemory: " & objDevice.MaxMemorySupported
                }
                j += 1
            Catch ex As Exception
                If Config.Variables.Debug Then
                    Console.WriteLine("Get Hardware error: " & ex.ToString)
                End If
            End Try
        Next objDevice
        Return newHardwareDetails
    End Function
    Public Function GetRam() As HardwareDetails
        Dim newHardwareDetails As HardwareDetails
        newHardwareDetails = New HardwareDetails With {
            .HardwareName = "Name: N/A",
            .HardwareType = "Type: Memory",
            .Performance = "Total Memory: " & Math.Round((((My.Computer.Info.TotalPhysicalMemory / 1024) / 1024) / 1024)) & "Gb",
            .OtherInformation = "OtherInformation! VirtualMemory: " & Math.Round((((My.Computer.Info.TotalVirtualMemory / 1024) / 1024) / 1024))
        }
        Return newHardwareDetails
    End Function
    Public Function getOS() As HardwareDetails
        Dim newHardwareDetails As HardwareDetails
        newHardwareDetails = New HardwareDetails With {
            .HardwareName = "Name: " & My.Computer.Info.OSFullName,
            .HardwareType = "Type: OS",
            .Performance = "Performance: " & My.Computer.Info.OSPlatform,
            .OtherInformation = "OtherInformation! Version: " & My.Computer.Info.OSVersion
        }
        Return newHardwareDetails
    End Function
    Public Function GetAllHardware() As List(Of HardwareDetails)
        Dim newHardwareDetails As New List(Of HardwareDetails)
        Dim j As Integer = 0
        For i = 0 To objects.Count - 1
            Dim objWMIService As Object = GetObject("winmgmts:\\.\root\cimv2")
            Dim colDevices As Object = objWMIService.ExecQuery("Select * From " & objects(i))
            If Config.Variables.Debug Then
                Console.WriteLine(i & ": " & objects(i))
            End If
            Try
                For Each objDevice In colDevices

                    newHardwareDetails.Add(New HardwareDetails With {
                        .HardwareName = "Name: " & objDevice.Name,
                        .HardwareType = "Type: " & objects(i),
                        .Performance = "Status: " & objDevice.Status,
                        .OtherInformation = "OtherInformation! Description: " & objDevice.Description
                    })
                    j += 1

                Next objDevice
            Catch ex As Exception
                If Config.Variables.Debug Then
                    Console.WriteLine("Get Hardware error: (" & objects(i) & ") " & ex.ToString)
                End If
                Continue For
            End Try
        Next
        Return newHardwareDetails
    End Function
End Module
