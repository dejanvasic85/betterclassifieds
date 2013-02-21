Namespace CRM
    Public Class Purchase
        Inherits Transaction

        Private Const _type As String = "purchase"

        Public Shared Shadows ReadOnly Property Type()
            Get
                Return _type
            End Get
        End Property

        Private _amount As Decimal
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property
    End Class
End Namespace