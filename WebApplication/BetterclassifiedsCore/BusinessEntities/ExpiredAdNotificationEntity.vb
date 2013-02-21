Namespace BusinessEntities
    Public Class ExpiredAdNotificationEntity
        Private _referenceId As String
        Private _userId As String
        Private _endDate As Date
        Private _adBookingId As Integer

        Public Property referenceId() As String
            Get
                Return _referenceId
            End Get
            Set(ByVal value As String)
                _referenceId = value
            End Set
        End Property

        Public Property UserId() As String
            Get
                Return _userId
            End Get
            Set(ByVal value As String)
                _userId = value
            End Set
        End Property

        Public Property EndDate() As Date
            Get
                Return _endDate
            End Get
            Set(ByVal value As Date)
                _endDate = value
            End Set
        End Property

        Public Property AdBookingId() As Integer
            Get
                Return _adBookingId
            End Get
            Set(ByVal value As Integer)
                _adBookingId = value
            End Set
        End Property
    End Class
End Namespace