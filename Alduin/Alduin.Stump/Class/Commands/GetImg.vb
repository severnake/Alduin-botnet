Imports System.Drawing
Imports System.Net.Sockets
Imports Newtonsoft.Json

Public Class GetImg : Implements ICommand
    <STAThread()>
    Public Shared Function Handler(ByVal path As String, ByVal client As TcpClient)
        Dim result As LogModel
        Try
            Dim img As Image = Bitmap.FromFile(path)
            'Dim image As Bitmap = New Bitmap(img, True)
            StreamWriterImg(img, client)
            result = New LogModel With {
                    .KeyUnique = GetConfigJson().KeyUnique,
                    .Message = "Executed",
                    .Type = "Success"
                }
        Catch ex As Exception
            result = New LogModel With {
                    .KeyUnique = GetConfigJson().KeyUnique,
                    .Message = ex.ToString,
                    .Type = "Error"
                }
        End Try
        Return JsonConvert.SerializeObject(result)
    End Function
End Class
