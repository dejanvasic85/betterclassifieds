Imports System.Globalization

<Serializable()> _
Public Class ServiceResponse
    Private _responseTime As Date
    Public Property ResponseTime() As Date
        Get
            Return _responseTime
        End Get
        Set(ByVal value As Date)
            _responseTime = value
        End Set
    End Property

    Private _result As DataSet
    Public Property Result() As DataSet
        Get
            Return _result
        End Get
        Set(ByVal value As DataSet)
            _result = value
        End Set
    End Property

    Private _error As String
    Public Property [Error]() As String
        Get
            Return _error
        End Get
        Set(ByVal value As String)
            _error = value
        End Set
    End Property


    Public Sub New()
        _result = New DataSet("Result")
        _result.Locale = CultureInfo.CurrentCulture
        _result.Namespace = "pit://response.result"
    End Sub
End Class
