
Imports System.Web.Profile

Namespace ProfileProvider
    Public Class AppUserProfile
        Inherits ProfileBase
        'Private _baseProfile As ProfileBase
        'Private _firstName As String
        'Private _lastName As String
        'Private _email As String
        'Private _address1 As String
        'Private _address2 As String
        'Private _city As String
        'Private _postcode As String
        'Private _state As String
        'Private _phone As String
        'Private _secondaryPhone As String
        'Private _preferedContact As String
        'Private _businessName As String
        'Private _abn As String
        'Private _industry As String
        'Private _businessCategory As String

        Public Sub New()
            MyBase.New()
        End Sub

        Public Property BusinessCategory() As Integer
            Get
                Return GetPropertyValue("BusinessCategory")
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue("BusinessCategory", value)
            End Set
        End Property

        Public Property Industry() As Integer
            Get
                Return GetPropertyValue("Industry")
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue("Industry", value)
            End Set
        End Property

        Public Property ABN() As String
            Get
                Return GetPropertyValue("ABN")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("ABN", value)
            End Set
        End Property

        Public Property BusinessName() As String
            Get
                Return GetPropertyValue("BusinessName")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("BusinessName", value)
            End Set
        End Property

        Public Property PreferedContact() As String
            Get
                Return GetPropertyValue("PreferedContact")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("PreferedContact", value)
            End Set
        End Property

        Public Property SecondaryPhone() As String
            Get
                Return GetPropertyValue("SecondaryPhone")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("SecondaryPhone", value)
            End Set
        End Property

        Public Property Phone() As String
            Get
                Return GetPropertyValue("Phone")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Phone", value)
            End Set
        End Property

        Public Property State() As String
            Get
                Return GetPropertyValue("State")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("State", value)
            End Set
        End Property

        Public Property Postcode() As String
            Get
                Return GetPropertyValue("PostCode")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("PostCode", value)
            End Set
        End Property

        Public Property City() As String
            Get
                Return GetPropertyValue("City")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("City", value)
            End Set
        End Property

        Public Property Address2() As String
            Get
                Return GetPropertyValue("Address2")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Address2", value)
            End Set
        End Property
        Public Property FirstName() As String
            Get
                Return GetPropertyValue("FirstName")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("FirstName", value)
            End Set
        End Property

        Public Property LastName() As String
            Get
                Return GetPropertyValue("LastName")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("LastName", value)
            End Set
        End Property

        Public Property Email() As String
            Get
                Return GetPropertyValue("Email")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Email", value)
            End Set
        End Property

        Public Property Address1() As String
            Get
                Return GetPropertyValue("Address1")
            End Get
            Set(ByVal value As String)
                SetPropertyValue("Address1", value)
            End Set
        End Property

        Public Property ProfileVersion() As Integer
            Get
                Return GetPropertyValue("ProfileVersion")
            End Get
            Set(ByVal value As Integer)
                SetPropertyValue("ProfileVersion", value)
            End Set
        End Property

        Public Overloads Sub Initialize(ByVal username As String)
            MyBase.Initialize(username, True)
        End Sub
        Private Overloads Sub Initialize(ByVal user As String, ByVal auth As Boolean)
        End Sub
        Public Overridable Function GetProfile(ByVal username As String) As AppUserProfile
            Return CType(ProfileBase.Create(username), AppUserProfile)
        End Function
    End Class
End Namespace