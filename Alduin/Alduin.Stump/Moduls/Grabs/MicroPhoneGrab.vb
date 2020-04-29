Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports Newtonsoft.Json

Public Module MicroPhoneGrab
    Private sourceStream As NAudio.Wave.WaveIn
    Private waveOut As NAudio.Wave.DirectSoundOut
    Private waveWriter As NAudio.Wave.WaveFileWriter
    Private DeviceIndex As ArrayList
    Private devicesModel() As MicroPhoneDevicesListModel
    Public Sub Main()
        Dim jsonobject = JsonConvert.SerializeObject(SelectItems())

    End Sub
    Public Sub Main(ByVal index As Integer)
        sourceStream = New NAudio.Wave.WaveIn()
        sourceStream.DeviceNumber = index
        sourceStream.WaveFormat = New NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(index).Channels)
        Dim waveIn As NAudio.Wave.WaveInProvider = New NAudio.Wave.WaveInProvider(sourceStream)
        'waveOut = New NAudio.Wave.DirectSoundOut()
        'waveOut.Init(waveIn)
        'sourceStream.DataAvailable += New EventHandler(Of NAudio.Wave.WaveInEventArgs)(sourceStream_DataAvailable)
        'waveWriter = New NAudio.Wave.WaveFileWriter(save.FileName, sourceStream.WaveFormat)

        sourceStream.StartRecording()
    End Sub
    Private Function SelectItems() As MicroPhoneDevicesListModel()
        Dim sources As List(Of NAudio.Wave.WaveInCapabilities) = New List(Of NAudio.Wave.WaveInCapabilities)()

        For i As Integer = 0 To NAudio.Wave.WaveIn.DeviceCount - 1
            sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i))
        Next

        Dim j As Integer = 0
        For Each source In sources
            devicesModel(j).Index = j
            devicesModel(j).Name = source.ProductName
            j += 1
        Next
        Return devicesModel
    End Function
    Private Sub sourceStream_DataAvailable(ByVal sender As Object, ByVal e As NAudio.Wave.WaveInEventArgs)
        If waveWriter Is Nothing Then Return
        waveWriter.WriteData(e.Buffer, 0, e.BytesRecorded)
        waveWriter.Flush()
    End Sub
    Public Sub Stopped()
        If waveOut IsNot Nothing Then
            waveOut.[Stop]()
            waveOut.Dispose()
            waveOut = Nothing
        End If

        If sourceStream IsNot Nothing Then
            sourceStream.StopRecording()
            sourceStream.Dispose()
            sourceStream = Nothing
        End If

        If waveWriter IsNot Nothing Then
            waveWriter.Dispose()
            waveWriter = Nothing
        End If
    End Sub
End Module
