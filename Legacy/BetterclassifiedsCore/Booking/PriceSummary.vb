Namespace Booking
    <Serializable()> _
    Public Class PriceSummary
        Public Property PaperPrice() As Decimal
        Public Property PaperName() As String
        Public Property CategoryName() As String
        Public Property NumOfWords() As Integer
        Public Property PhotoCharge() As Decimal
        Public Property BoldHeading() As Decimal
        Public Property MinimumCharge() As Decimal
        Public Property MaximumCharge() As Decimal
        Public Property RegularWordCount() As Integer
        Public Property RatePerUnit() As Decimal
        Public Property AdType() As SystemAdType
    End Class
End Namespace