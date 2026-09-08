Option Strict On

Namespace Models

    Public Class ReceiverModel
        Public Property Id As Integer
        Public Property Rin As String
        Public Property Name As String
        Public Property Country As String
        Public Property Governate As String
        Public Property RegionCity As String
        Public Property Street As String
        Public Property BuildingNumber As String
        Public Property CreatedAtUtc As DateTime

        Public Sub New()
            Rin = ""
            Name = ""
            Country = "EG"
            Governate = ""
            RegionCity = ""
            Street = ""
            BuildingNumber = ""
        End Sub
    End Class

End Namespace
