Imports System.Data
Imports Paramount.Betterclassified.Xavier.Services

<Serializable()> _
Public Class ServiceRequest

    Private _requestDate As Date
    Public Property RequestDate() As Date
        Get
            Return _requestDate
        End Get
        Set(ByVal value As Date)
            _requestDate = value
        End Set
    End Property

    Private _data As DataSet
    Public Property Data() As DataSet
        Get
            Return _data
        End Get
        Set(ByVal value As DataSet)
            _data = value
        End Set
    End Property

    Private _clientInfo As ClientInfo
    Public Property ClientInfor() As ClientInfo
        Get
            Return _clientInfo
        End Get
        Set(ByVal value As ClientInfo)
            _clientInfo = value
        End Set
    End Property

    Private _parameters As List(Of Parameter)
    Public Property Parameters() As List(Of Parameter)
        Get
            Return _parameters
        End Get
        Set(ByVal value As List(Of Parameter))
            _parameters = value
        End Set
    End Property

    Public Sub New()
        _clientInfo = New ClientInfo
        _parameters = New List(Of Parameter)
        _data = New DataSet
    End Sub
End Class
