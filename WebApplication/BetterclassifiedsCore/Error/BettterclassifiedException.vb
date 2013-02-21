
Public Class BettterclassifiedException
    Inherits System.Exception

    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub

    Public Sub New(ByVal message As String, ByVal ex As System.Exception)
        MyBase.New(message, ex)
    End Sub

    Private _errorCode As String
    Public Property ErrorCode()
        Get
            Return _errorCode
        End Get
        Set(ByVal value)
            _errorCode = value
        End Set
    End Property

End Class
