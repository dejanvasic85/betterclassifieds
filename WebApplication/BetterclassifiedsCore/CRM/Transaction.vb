Namespace CRM
    Public Class Transaction
        Private Const _type As String = "transaction"
        Private _id As String
        Public Property ID() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                _id = value
            End Set
        End Property

        Private _referenceNumber As String
        Public Property ReferenceNumber() As String
            Get
                Return _referenceNumber
            End Get
            Set(ByVal value As String)
                _referenceNumber = value
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

        Public Shared ReadOnly Property Type() As String
            Get
                Return _type
            End Get
        End Property

        Private _transactionDate As Date
        Public Property TransactionDate() As String
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As String)
                _transactionDate = value
            End Set
        End Property


    End Class
End Namespace