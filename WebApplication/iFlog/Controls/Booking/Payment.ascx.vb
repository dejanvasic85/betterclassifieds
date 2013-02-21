Namespace Controls.Booking
    Partial Public Class Payment
        Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                ' do year expiry
                For i As Integer = 0 To 5
                    ddlYearExpiry.Items.Add(Today.AddYears(i).Year)
                Next
            End If
        End Sub

        Public Property Amount() As Decimal
            Get
                Return CDec(lblAmount.Text)
            End Get
            Set(ByVal value As Decimal)
                lblAmount.Text = String.Format("{0:D}", value)
            End Set
        End Property

        Public Property CreditType() As String
            Get
                Return ddlCreditCardType.SelectedValue
            End Get
            Set(ByVal value As String)
                ddlCreditCardType.SelectedValue = value
            End Set
        End Property

        Public Property CreditCardNumber() As String
            Get
                Return txtCreditCardNumber.Text
            End Get
            Set(ByVal value As String)
                txtCreditCardNumber.Text = value
            End Set
        End Property

        Public Property MonthExpiry() As Integer
            Get
                Return Convert.ToInt32(ddlMonthExpiry)
            End Get
            Set(ByVal value As Integer)
                ddlMonthExpiry.SelectedIndex += value
            End Set
        End Property

        Public Property YearExpiry() As Integer
            Get
                Return Convert.ToInt32(ddlYearExpiry.SelectedValue)
            End Get
            Set(ByVal value As Integer)
                ddlYearExpiry.SelectedValue = value
            End Set
        End Property

        Public Property NameOnCard() As String
            Get
                Return txtNameOnCard.Text
            End Get
            Set(ByVal value As String)
                txtNameOnCard.Text = value
            End Set
        End Property
    End Class
End Namespace