Imports System.ComponentModel
Namespace CRM
    <DataObject(True), Serializable()> _
    Public Class Customer

        Public ReadOnly Property FormattedName()
            Get
                Return String.Format("{0} {1}", _firstName, _lastName)
            End Get
        End Property

        Private _username As String
        Public Property Username() As String
            Get
                Return _username
            End Get
            Set(ByVal value As String)
                _username = value
            End Set
        End Property

        Private _refNumber As String
        Public Property RefNumber() As String
            Get
                Return _refNumber
            End Get
            Set(ByVal value As String)
                _refNumber = value
            End Set
        End Property

        Private _firstName As String
        Public Property FirstName() As String
            Get
                Return _firstName
            End Get
            Set(ByVal value As String)
                _firstName = value
            End Set
        End Property

        Private _lastName As String
        Public Property LastName() As String
            Get
                Return _lastName
            End Get
            Set(ByVal value As String)
                _lastName = value
            End Set
        End Property

        Private _email As String
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        Private _address1 As String
        Private _address2 As String
        Public Property Address1() As String
            Get
                Return _address1
            End Get
            Set(ByVal value As String)
                _address1 = value
            End Set
        End Property

        Public Property Address2() As String
            Get
                Return _address2
            End Get
            Set(ByVal value As String)
                _address2 = value
            End Set
        End Property

        Private _city As String
        Public Property City() As String
            Get
                Return _city
            End Get
            Set(ByVal value As String)
                _city = value
            End Set
        End Property


        Private _phone As String
        Public Property Phone() As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property

        Private _abn As String
        Public Property ABN() As String
            Get
                Return _abn
            End Get
            Set(ByVal value As String)
                _abn = value
            End Set
        End Property

        Private _businessName As String
        Public Property BusinessName() As String
            Get
                Return _businessName
            End Get
            Set(ByVal value As String)
                _businessName = value
            End Set
        End Property

        Private _postCode As String
        Public Property PostCode() As String
            Get
                Return _postCode
            End Get
            Set(ByVal value As String)
                _postCode = value
            End Set
        End Property

        Private _industryCode As String
        Public Property IndustryCode() As String
            Get
                Return _industryCode
            End Get
            Set(ByVal value As String)
                _industryCode = value
            End Set
        End Property

        Private _businessCategoryCode As String
        Public Property BusinessCategoryCode() As String
            Get
                Return _businessCategoryCode
            End Get
            Set(ByVal value As String)
                _businessCategoryCode = value
            End Set
        End Property

        Private _state As String
        Public Property State() As String
            Get
                Return _state
            End Get
            Set(ByVal value As String)
                _state = value
            End Set
        End Property


    End Class
End Namespace