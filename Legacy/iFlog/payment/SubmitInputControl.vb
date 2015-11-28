Imports System.Web.UI.WebControls
Namespace Payment
    Public Class SubmitInputControl
        Inherits CompositeControl
        Private _refundPolicyUrl As String
        Private _vendorName As String
        Private _paymentAlertEmail As String
        Private _informationFields As List(Of String)
        Private _paymentReference As String
        Private _customerEmailAddress As String
        Private _gstRate As Decimal
        Private _gstIncluded As Boolean
        Private _returnUrl As String
        Private _notifyUrl As String
        Private _returnUrlText As String
        Private _hiddenFields As String

        Private ReadOnly refundPolicyUrlInput As HtmlGenericControl
        Private ReadOnly vendorNameInput As HtmlGenericControl
        Private ReadOnly paymentAlertEmailInput As HtmlGenericControl



        Public Sub New()
            _informationFields = New List(Of String)
        End Sub
        Public Property RefundPolicyUrl() As String
            Get
                Return _refundPolicyUrl
            End Get
            Set (ByVal value As String)
                _refundPolicyUrl = value
            End Set
        End Property

        Public Property VendorName() As String
            Get
                Return _vendorName
            End Get
            Set (ByVal value As String)
                _vendorName = value
            End Set
        End Property

        Public Property PaymentAlertEmail() As String
            Get
                Return _paymentAlertEmail
            End Get
            Set (ByVal value As String)
                _paymentAlertEmail = value
            End Set
        End Property

        Public Property InformationFields() As List(Of String)
            Get
                Return _informationFields
            End Get
            Set (ByVal value As List(Of String))
                _informationFields = value
            End Set
        End Property

        Public Property PaymentReference() As String
            Get
                Return _paymentReference
            End Get
            Set (ByVal value As String)
                _paymentReference = value
            End Set
        End Property

        Public Property CustomerEmailAddress() As String
            Get
                Return _customerEmailAddress
            End Get
            Set (ByVal value As String)
                _customerEmailAddress = value
            End Set
        End Property

        Public Property GstRate() As Decimal
            Get
                Return _gstRate
            End Get
            Set (ByVal value As Decimal)
                _gstRate = value
            End Set
        End Property

        Public Property GstIncluded() As Boolean
            Get
                Return _gstIncluded
            End Get
            Set (ByVal value As Boolean)
                _gstIncluded = value
            End Set
        End Property

        Public Property ReturnUrl() As String
            Get
                Return _returnUrl
            End Get
            Set (ByVal value As String)
                _returnUrl = value
            End Set
        End Property

        Public Property NotifyUrl() As String
            Get
                Return _notifyUrl
            End Get
            Set (ByVal value As String)
                _notifyUrl = value
            End Set
        End Property

        Public Property ReturnUrlText() As String
            Get
                Return _returnUrlText
            End Get
            Set (ByVal value As String)
                _returnUrlText = value
            End Set
        End Property

        Public Property HiddenFields() As String
            Get
                Return _hiddenFields
            End Get
            Set (ByVal value As String)
                _hiddenFields = value
            End Set
        End Property

        Protected Overrides Sub CreateChildControls()
            Me.Controls.Add(Me.CreateHiddenfield("vendor_name", VendorName))
            Me.Controls.Add(Me.CreateHiddenfield("refund_policy_url", RefundPolicyUrl))
            Me.Controls.Add(Me.CreateHiddenfield("payment_alert", PaymentAlertEmail))
            Me.Controls.Add(Me.CreateHiddenfield("payment_reference", PaymentReference))
            Me.Controls.Add(Me.CreateHiddenfield("receipt_address", CustomerEmailAddress))
            Me.Controls.Add(Me.CreateHiddenfield("gst_rate", GstRate))
            Me.Controls.Add(Me.CreateHiddenfield("gst_added", GstIncluded))
            Me.Controls.Add(Me.CreateHiddenfield("return_link_url", ReturnUrl))
            Me.Controls.Add(Me.CreateHiddenfield("reply_link_url", NotifyUrl))
            Me.Controls.Add(Me.CreateHiddenfield("return_link_text", ReturnUrlText))
            ' Add information Fields
            For Each item In Me.InformationFields
                Me.Controls.Add(Me.CreateHiddenfield("information_fields", item))
            Next

        End Sub

        Public Sub AddProduct(ByVal description As String, ByVal value As Decimal)
            Me.Controls.Add(Me.CreateHiddenfield(description, value))
        End Sub

        Private Function CreateHiddenfield(ByVal name As String, ByVal value As String) As HtmlGenericControl
            Dim g = New HtmlGenericControl("input")
            g.Attributes.Add("name", name)
            g.Attributes.Add("type", "hidden")
            g.Attributes.Add("value", value)
            Return g
        End Function


    End Class
End Namespace