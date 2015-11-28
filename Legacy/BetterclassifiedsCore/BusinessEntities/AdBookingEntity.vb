Namespace BusinessEntities

    <Serializable()> _
    Public Class AdBookingEntity

        Private _AdBookingId As Integer

        Private _StartDate As System.Nullable(Of Date)

        Private _EndDate As System.Nullable(Of Date)

        Private _TotalPrice As System.Nullable(Of Decimal)

        Private _BookReference As String

        Private _AdId As System.Nullable(Of Integer)

        Private _UserId As String

        Private _BookingStatus As String

        Private _MainCategoryId As System.Nullable(Of Integer)

        Public Property AdBookingId() As Integer
            Get
                Return Me._AdBookingId
            End Get
            Set(ByVal value As Integer)
                _AdBookingId = value
            End Set
        End Property

        Public Property StartDate() As System.Nullable(Of Date)
            Get
                Return Me._StartDate
            End Get
            Set(ByVal value As System.Nullable(Of Date))
                Me._StartDate = value
            End Set
        End Property

        Public Property EndDate() As System.Nullable(Of Date)
            Get
                Return Me._EndDate
            End Get
            Set(ByVal value As System.Nullable(Of Date))
                _EndDate = value
            End Set
        End Property

        Public Property TotalPrice() As System.Nullable(Of Decimal)
            Get
                Return Me._TotalPrice
            End Get
            Set(ByVal value As System.Nullable(Of Decimal))
                _TotalPrice = value
            End Set
        End Property

        Public Property BookReference() As String
            Get
                Return Me._BookReference
            End Get
            Set(ByVal value As String)
                _BookReference = value
            End Set
        End Property

        Public Property AdId() As System.Nullable(Of Integer)
            Get
                Return Me._AdId
            End Get
            Set(ByVal value As System.Nullable(Of Integer))
                _AdId = value
            End Set
        End Property

        Public Property UserId() As String
            Get
                Return Me._UserId
            End Get
            Set(ByVal value As String)
                _UserId = value
            End Set
        End Property

        Public Property BookingStatus() As String
            Get
                Return Me._BookingStatus
            End Get
            Set(ByVal value As String)
                _BookingStatus = value
            End Set
        End Property

        Public Property MainCategoryId() As System.Nullable(Of Integer)
            Get
                Return Me._MainCategoryId
            End Get
            Set(ByVal value As System.Nullable(Of Integer))
                _MainCategoryId = value
            End Set
        End Property

    End Class

End Namespace