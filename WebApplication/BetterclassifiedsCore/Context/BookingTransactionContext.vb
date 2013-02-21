Imports System

Namespace Context
    ''' <summary>
    ''' Base class for the context objects
    ''' </summary>
    <Serializable()> _
Public Class BookingTransactionContext
        Inherits ParamountContext
        Private Shared ReadOnly ContextKeyConstant As String = "BookingTransactionContext"

        ''' <summary>
        ''' This is the key, that implemeting class will use to return key to store in a session
        ''' This key will be used by caller to get or set the session value
        ''' </summary>
        ''' <returns></returns>
        Public Overloads Overrides ReadOnly Property ContextKey() As String
            Get
                Return ContextKeyConstant
            End Get
        End Property

        ''' <summary>
        ''' It will return current context
        ''' </summary>
        Public Shared ReadOnly Property Current() As BookingTransactionContext
            Get
                Return TryCast(GetContext(ContextKeyConstant), BookingTransactionContext)
            End Get
        End Property

        Public Shared ReadOnly Property IsValid() As Boolean
            Get
                Return Current IsNot Nothing
            End Get
        End Property

        Private _DateTimeCreated As DateTime
        Public Property DateTimeCreated() As DateTime
            Get
                Return _DateTimeCreated
            End Get
            Set(ByVal value As DateTime)
                _DateTimeCreated = value
            End Set
        End Property

        Private _TransactionReference As String
        Public Property TransactionReference() As String
            Get
                Return _TransactionReference
            End Get
            Set(ByVal value As String)
                _TransactionReference = value
            End Set
        End Property

        Public Shared Sub [Set](ByVal categoryId As Integer)
            Dim paramountUserContext = New BookingTransactionContext()
            Dim refId = GetBookingReference(categoryId, Not BookingTransactionContext.IsValid)
            paramountUserContext.TransactionReference = refId
            SetContext(paramountUserContext)
        End Sub

        ''' <summary>
        ''' If context is not set an exception will be thrown
        ''' other wise it will return true
        ''' </summary>
        Public Shared Sub ValidateContext()
            If Current Is Nothing Then
                'log error
            End If
        End Sub
    End Class
End Namespace