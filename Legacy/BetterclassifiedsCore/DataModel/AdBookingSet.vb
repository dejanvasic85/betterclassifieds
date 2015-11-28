Imports BetterclassifiedsCore.BusinessEntities

Namespace DataModel
    Public Class AdBookingSet
        Private _data As New List(Of AdBookingEntity)

        Public Sub New(ByVal dataTable As DataTable)
            _data = New List(Of AdBookingEntity)
            If (dataTable Is Nothing) Then Return
            For Each item As DataRow In dataTable.Rows
                Dim row As New AdBookingEntity
                row.AdBookingId = item.Field(Of Integer)("AdBookingId")
                row.AdId = item.Field(Of Integer?)("AdId")
                row.BookingStatus = item.Field(Of Integer)("BookingStatus")
                row.BookReference = item.Field(Of String)("BookReference")
                row.EndDate = item.Field(Of Date?)("EndDate")
                row.MainCategoryId = item.Field(Of Integer)("MainCategoryId")
                row.StartDate = item.Field(Of Date?)("StartDate")
                row.TotalPrice = item.Field(Of Decimal?)("TotalPrice")
                row.UserId = item.Field(Of String)("UserId")
                _data.Add(row)
            Next
        End Sub

        Public ReadOnly Property Data() As List(Of AdBookingEntity)
            Get
                Return _data
            End Get
        End Property
    End Class
End Namespace