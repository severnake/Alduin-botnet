Module ImageGrab
    Public Sub ImageGraber()
        Dim disc As ArrayList = GetDiscName()
        For i = 0 To disc.Count - 1
            ImageSearcher(GetConfigJson().MainPath, disc(i))
        Next
    End Sub
End Module
