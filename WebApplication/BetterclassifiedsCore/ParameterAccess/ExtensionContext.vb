Imports System.Web

Namespace ParameterAccess
    Public Class ExtensionContext
        Private Const ParameterKey As String = "ExtensionContext"

        Public Shared Property BookingReference As String
            Get
                Return GetParameter("BookingReference")
            End Get
            Set(value As String)
                SetParameterValue("BookingReference", value)
            End Set
        End Property

        Public Shared Property TotalCost As Decimal
            Get
                Return GetContext("TotalCost")
            End Get
            Set(value As Decimal)
                SetParameterValue("TotalCost", value)
            End Set
        End Property

        Public Shared Property ExtensionId As Nullable(Of Integer)
            Get
                Return GetContext("ExtensionId")
            End Get
            Set(value As Nullable(Of Integer))
                SetParameterValue("ExtensionId", value)
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