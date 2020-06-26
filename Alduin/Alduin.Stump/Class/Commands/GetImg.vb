Imports System.Drawing
Imports System.Net.Sockets
Imports Newtonsoft.Json

Public Class GetImg : Implements ICommand
    <STAThread()>
    Public Shared Function Handler(ByVal path As String, ByVal client As TcpClient)
        Try
            Dim img As Image = Bitmap.FromFile(path)
            StreamWriterImg(img, client)
        Catch ex As Exception

        End Try
        Return ""
    End Function
    Public Shared Function Handler(ByVal model As GetImageModel, ByVal client As TcpClient)
        Try
            Dim img As Image = Bitmap.FromFile(model.newVariables.imagePath)
            StreamWriterImgToTCP(img, client)
        Catch ex As Exception

        End Try
        Return ""
    End Function
End Class
