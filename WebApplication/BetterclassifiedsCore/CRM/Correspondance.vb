Namespace CRM
    Public Class Correspondance
        Private _id As String
        Public Property ID() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property

        Private _title As String
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _type As String
        Public Property Type() As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property

        Private _communicationDate As Date
        Public Property CommunicationDate() As Date
            Get
                Return _communicationdate
            End Get
            Set(ByVal value As Date)
                _communicationdate = value
            End Set
        End Property
    End Class
End Namespace