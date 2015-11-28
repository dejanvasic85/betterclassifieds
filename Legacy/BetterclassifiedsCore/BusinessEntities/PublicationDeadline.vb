Namespace BusinessEntities
    Public Class PublicationDeadline
        Private _Title As String
        Public Property Title() As String
            Get
                Return _Title
            End Get
            Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Private _deadline As DateTime
        Public Property Deadline() As DateTime
            Get
                Return _deadline
            End Get
            Set(ByVal value As DateTime)
                _deadline = value
            End Set
        End Property
    End Class
End Namespace
