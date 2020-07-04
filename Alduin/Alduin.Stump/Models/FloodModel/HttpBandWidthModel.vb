Public Class HttpBandWidthModel
    Public Property newHttpBandWidthVariables As HttpBandWidthVariables
    Public Property newBaseCommand As BaseCommands
    Public Property newBaseFloodModel As BaseFloodModel
End Class

Public Class HttpBandWidthVariables
    Public Property Port As Integer
    Public Property PostDATA As String
    Public Property RandomFile As Boolean
    Public Property Method As String
End Class
