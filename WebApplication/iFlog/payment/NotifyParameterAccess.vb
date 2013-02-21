Namespace payment
    Public Class NotifyParameterAccess
        Private Const PaymentReferenceParam = "payment_reference"
        Private Const ReferenceParam As String = "id"
        Private Const TotalCostParam As String = "totalCost"
        Private Const SessionIdParam As String = "sessionId"
        Private Const TransactionTypeParam As String = "tt"

        Public Shared ReadOnly Property ReferenceId() As String
            Get
                Return GetRequestItem(ReferenceParam)
            End Get
        End Property

        Public Shared ReadOnly Property PaymentReferenceId() As String
            Get
                Return GetRequestItem(PaymentReferenceParam)
            End Get
        End Property

        Public Shared ReadOnly Property Cost() As Decimal
            Get
                Return Convert.ToDecimal(GetRequestItem(TotalCostParam))
            End Get
        End Property

        Public Shared ReadOnly Property TransactionType() As String
            Get
                Return GetRequestItem(TransactionTypeParam)
            End Get
        End Property

        Public Shared ReadOnly Property SessionId() As String
            Get
                Return GetRequestItem(SessionIdParam)
            End Get
        End Property


        Private Shared Function GetRequestItem(ByVal key As String)
            Return HttpContext.Current.Request(key)
        End Function
    End Class
End Namespace