Imports System.Globalization

<Serializable()> _
Public Class ClientInfo

    Private _applicationCode As String
    Public Property ApplicationCode() As String
        Get
            Return _applicationCode
        End Get
        Set(ByVal value As String)
            _applicationCode = value
        End Set
    End Property

    Private _clientCode As String
    Public Property ClientCode() As String
        Get
            Return _clientCode
        End Get
        Set(ByVal value As String)
            _clientCode = value
        End Set
    End Property

    Private _ipAddress As String
    Public Property IPAddress() As String
        Get
            Return _ipAddress
        End Get
        Set(ByVal value As String)
            _ipAddress = value
        End Set
    End Property

    Private _hostName As String
    Public Property HostName() As String
        Get
            Return _hostName
        End Get
        Set(ByVal value As String)
            _hostName = value
        End Set
    End Property

    Private _hostAccount As String
    Public Property HostAccount() As String
        Get
            Return _hostAccount
        End Get
        Set(ByVal value As String)
            _hostAccount = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return String.Format(CultureInfo.CurrentCulture, "ApplicationCode: {0}, ClientCode: {1}, IPAddress: {2}, HostName: {3}, HostAccount: {4}", _applicationCode, _clientCode, _ipAddress, _hostName, _hostAccount)
    End Function

End Class
