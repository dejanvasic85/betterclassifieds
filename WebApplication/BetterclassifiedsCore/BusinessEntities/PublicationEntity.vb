Namespace BusinessEntities
    Public Class PublicationEntity
        Private _PublicationId As Integer
        Private _Title As String
        Private _Description As String
        Private _PublicationTypeId As Integer
        Private _ImageUrl As String
        Private _FrequencyType As String
        Private _FrequencyValue As String
        Private _CurrentEditionDate As DateTime
        Private _Deadline As DateTime
        Private _Active As Boolean

        Public Property PublicationId() As Integer
            Get
                Return _PublicationId
            End Get
            Set(ByVal value As Integer)
                _PublicationId = value
            End Set
        End Property

        Public Property Title() As String
            Get
                Return _Title
            End Get
            Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property PublicationTypeId() As Integer
            Get
                Return _PublicationTypeId
            End Get
            Set(ByVal value As Integer)
                _PublicationTypeId = value
            End Set
        End Property

        Public Property ImageUrl() As String
            Get
                Return _ImageUrl
            End Get
            Set(ByVal value As String)
                _ImageUrl = value
            End Set
        End Property

        Public Property FrequencyType() As String
            Get
                Return _FrequencyType
            End Get
            Set(ByVal value As String)
                _FrequencyType = value
            End Set
        End Property

        Public Property FrequencyValue() As String
            Get
                Return _FrequencyValue
            End Get
            Set(ByVal value As String)
                _FrequencyValue = value
            End Set
        End Property

        Public Property CurrentEditionDate() As DateTime
            Get
                Return _CurrentEditionDate
            End Get
            Set(ByVal value As DateTime)
                _CurrentEditionDate = value
            End Set
        End Property

        Public Property Deadline() As String
            Get
                Return _Deadline
            End Get
            Set(ByVal value As String)
                _Deadline = value
            End Set
        End Property

        Public Property Active() As Boolean
            Get
                Return _Active
            End Get
            Set(ByVal value As Boolean)
                _Active = value
            End Set
        End Property

    End Class
End Namespace
