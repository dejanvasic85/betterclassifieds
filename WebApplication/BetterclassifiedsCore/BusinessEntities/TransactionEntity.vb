Namespace BusinessEntities
    Public Class TransactionEntity
        Inherits DataModel.Transaction

        Private _gstCharge As Decimal
        Public ReadOnly Property GSTAmount() As Decimal
            Get
                Return Amount * 0.1
            End Get
        End Property

        Private _beforeGST As Decimal
        Public ReadOnly Property AmountBeforeGST() As Decimal
            Get
                Return Amount - (Amount * 0.1)
            End Get
        End Property

    End Class
End Namespace