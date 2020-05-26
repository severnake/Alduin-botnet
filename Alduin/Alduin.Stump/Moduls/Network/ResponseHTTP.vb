Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net.Sockets
Module ResponseHTTP
    Public Sub StreamWriterImg(ByVal msg As Bitmap, ByVal Client As TcpClient)
        Dim Writer = New StreamWriter(Client.GetStream())
        Dim MemoryStream = New MemoryStream()
        msg.Save(MemoryStream, ImageFormat.Jpeg)
        Dim byteArray = MemoryStream.ToArray()
        Dim NetworkStream = Client.GetStream()
        Dim str As String = "HTTP/1.1 200 OK
Content-Type: image/jpeg
Content-Length: " & byteArray.Length & "
Connection: keep-alive
Keep-Alive: timeout=15
Date: " & DateTime.UtcNow & "
Server: Apache
Last-Modified: Tue, 11 Mar 2014 15:15:19 GMT
ETag: ""e430-4f4563028b13a""
Accept-Ranges: bytes

"
        Dim headbyte As Byte() = System.Text.Encoding.ASCII.GetBytes(str)
        Console.Write(byteArray)
        NetworkStream.Write(headbyte, 0, headbyte.Length)
        NetworkStream.Write(byteArray, 0, byteArray.Length)
        NetworkStream.Flush()

    End Sub
    Public Sub StreamWriter(ByVal msg As String, ByVal Client As TcpClient)
        Dim Writer = New StreamWriter(Client.GetStream())
        Dim str As String = "HTTP/1.1 200 OK
Date: " & DateTime.UtcNow & "
Server: Apache/2.2.14 (Win32)
Last-Modified: Wed, 22 Jul 2009 19:15:56 GMT
Content-Length: " & msg.Length & "
Content-Type: text/html
Connection: Closed

"
        Writer.Write(str)
        Writer.Write(msg)
        Writer.Flush()
    End Sub
    Public Sub StreamWriterJson(ByVal msg As String, ByVal Client As TcpClient)
        Dim Writer = New StreamWriter(Client.GetStream())
        Dim str As String = "HTTP/1.1 200 OK
Date: " & DateTime.UtcNow & "
Server: Apache/2.2.14 (Win32)
Last-Modified: Wed, 22 Jul 2009 19:15:56 GMT
Content-Length: " & msg.Length & "
Content-Type: application/json
Connection: keep-alive

"
        Writer.Write(str)
        Writer.Write(msg)
        Writer.Flush()
    End Sub
End Module
