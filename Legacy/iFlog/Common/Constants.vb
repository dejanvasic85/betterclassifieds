Namespace Common

    Public Class Constants

        Public Const sessionUsername As String = "sessionUsername"
        Public Const adPreviewLength As Integer = 20
        Public Const onlineAdHeadingCharsLimit As Integer = 20
        Public Const onlineAdTextCharsLimit As Integer = 34

        Public Structure PaymentOption
            Public Const CreditCard As String = "cc"
            Public Const PayPal As String = "pp"
            Private dummy
        End Structure

    End Class

    Public Module ErrorCodes

        Public Const loginFailed As Integer = 4000


    End Module

End Namespace