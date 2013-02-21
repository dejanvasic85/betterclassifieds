Imports System.Web

Namespace ParameterAccess
    Public Class BookingProcess
        Const ParameterKey As String = "BookingProcess"
        Const PaymentOptionParam As String = "PaymentOptionParam"
        Const PaymentReferenceIdParam As String = "PaymentReferenceIdParam"

        Public Shared Property PaymentOption() As String
            Get
                Return GetParameter(PaymentOptionParam)
            End Get
            Set(ByVal value As String)
                SetParameterValue(PaymentOptionParam, value)
            End Set
        End Property

        Public Shared Property PaymentReferenceId() As String
            Get
                Return GetParameter(PaymentReferenceIdParam)
            End Get
            Set(ByVal value As String)
                SetParameterValue(PaymentReferenceIdParam, value)
            End Set
        End Property

        Private Shared Function GetContext() As Dictionary(Of String, Object)
            Dim item = TryCast(HttpContext.Current.Session(ParameterKey), Dictionary(Of String, Object))
            If (item Is Nothing) Then
                HttpContext.Current.Session(ParameterKey) = New Dictionary(Of String, Object)
            End If
            Return HttpContext.Current.Session(ParameterKey)
        End Function

        Private Shared Sub SetParameterValue(ByVal key As String, ByVal value As Object)
            If GetContext.ContainsKey(key) Then
                GetContext.Item(key) = value
            Else
                GetContext.Add(key, value)
            End If

            HttpContext.Current.Session(ParameterKey) = GetContext()
        End Sub

        Private Shared Function GetParameter(ByVal key As String) As Object
            If GetContext.Keys.Contains(key) Then
                Return GetContext()(key)
            End If
            Return Nothing
        End Function

        Public Shared Sub Clear()
            HttpContext.Current.Session(ParameterKey) = Nothing
        End Sub


    End Class
End Namespace