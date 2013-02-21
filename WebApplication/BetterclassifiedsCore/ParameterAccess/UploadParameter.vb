Imports System.Web

Namespace ParameterAccess
    Public Class UploadParameter
        Const ParameterKey As String = "UploadParameter"
        Const AdDesignIdKey As String = "AdDesignId"
        Const AdBookingIdKey As String = "AdBookingId"
        Const IsOnlineAdUploadKey As String = "IsOnlineAdUpload"
        Const IsPrintAdUploadKey As String = "IsPrintAdUpload"
        Const BookingReferenceKey As String = "BookingReference"

        Public Shared Property AdDesignId() As Integer
            Get
                Return GetParameter(AdDesignIdKey)
            End Get
            Set(ByVal value As Integer)
                SetParameterValue(AdDesignIdKey, value)
            End Set
        End Property

        Public Shared Property AdBookingId() As Integer
            Get
                Return GetParameter(AdBookingIdKey)
            End Get
            Set(ByVal value As Integer)
                SetParameterValue(AdBookingIdKey, value)
            End Set
        End Property

        Public Shared Property IsOnlineAdUpload() As Boolean
            Get
                Return GetParameter(IsOnlineAdUploadKey)
            End Get
            Set(ByVal value As Boolean)
                SetParameterValue(IsOnlineAdUploadKey, value)
            End Set
        End Property

        Public Shared Property BookingReference() As String
            Get
                Return GetParameter(BookingReferenceKey)
            End Get
            Set(ByVal value As String)
                SetParameterValue(BookingReferenceKey, value)
            End Set
        End Property

        Public Shared Property IsPrintAdUpload() As Boolean
            Get
                Return GetParameter(IsPrintAdUploadKey)
            End Get
            Set(ByVal value As Boolean)
                SetParameterValue(IsPrintAdUploadKey, value)
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