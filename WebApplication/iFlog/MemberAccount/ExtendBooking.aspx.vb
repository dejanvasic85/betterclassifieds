Imports BetterClassified.UI

Public Class ExtendBooking
    Inherits BasePage(Of Presenters.ExtendBookingPresenter, Views.IExtendBookingView)
    Implements Views.IExtendBookingView

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Presenter.Load()
        End If
    End Sub

    Public ReadOnly Property AdBookingId As Integer Implements Views.IExtendBookingView.AdBookingId
        Get
            Return Me.ReadQueryString(Of Integer)("AdBookingId")
        End Get
    End Property

    Public Sub DataBindEditions(editions As IEnumerable(Of Models.PublicationEditionModel), lastOnlineDate As DateTime, pricePerEdition As Decimal, totalPrice As Decimal) Implements Views.IExtendBookingView.DataBindEditions
        rptEditions.DataSource = editions
        rptEditions.DataBind()

        lblOnlineEndDate.Text = lastOnlineDate.ToString("dd-MMM-yyyy")
        lblPricePerEdition.Text = pricePerEdition.ToString("C")
        lblTotalPrice.Text = totalPrice.ToString("C")
        Me.TotalPrice = totalPrice
        divPayment.Visible = totalPrice > 0
    End Sub

    Public Sub DataBindOptions(insertions As IEnumerable(Of Integer)) Implements Views.IExtendBookingView.DataBindOptions
        ddlEditions.DataSource = insertions
        ddlEditions.DataBind()
    End Sub

    Private Sub ddlEditions_IndexChanged(sender As Object, eventArgs As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlEditions.IndexChanged
        Me.Presenter.Load(eventArgs.Value)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect(PageUrl.MemberBookings)
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Presenter.ProcessExtension()
    End Sub

    Public ReadOnly Property SelectedInsertionCount As Integer Implements Views.IExtendBookingView.SelectedInsertionCount
        Get
            Return ddlEditions.SelectedValue
        End Get
    End Property

    Public Sub SetupOnlineOnlyView() Implements Views.IExtendBookingView.SetupOnlineOnlyView
        ddlEditions.Text = "Days"
        ddlEditions.HelpText = ddlEditions.HelpText.Replace("weeks", "days")
        divPricePerEdition.Visible = False
        divPublications.Visible = False
    End Sub

    Public Sub DisplayExpiredBookingMessage() Implements Views.IExtendBookingView.DisplayExpiredBookingMessage
        lblErrorMessage.Text = "The booking you have requested has already expired. Please click the 'Expired Ads' link on the left hand menu to complete the Booking Process for the expired ad."
        errorDetails.Visible = True
        pnlContent.Visible = False
    End Sub

    Public Sub DisplayBookingDoesNotExist() Implements Views.IExtendBookingView.DisplayBookingDoesNotExist
        lblErrorMessage.Text = "The booking you have requested does not exist."
        errorDetails.Visible = True
        pnlContent.Visible = False
    End Sub

    Public ReadOnly Property IsPaymentRequired As Boolean Implements Views.IExtendBookingView.IsPaymentRequired
        Get
            Return Me.TotalPrice > 0
        End Get
    End Property

    Public Property TotalPrice As Decimal Implements Views.IExtendBookingView.TotalPrice
        Get
            Return ViewState("TotalPrice")
        End Get
        Set(value As Decimal)
            ViewState("TotalPrice") = value
        End Set
    End Property

    Public Property IsOnlineOnly As Boolean Implements Views.IExtendBookingView.IsOnlineOnly
        Get
            Return ViewState("IsOnlineOnly")
        End Get
        Set(value As Boolean)
            ViewState("IsOnlineOnly") = value
        End Set
    End Property

    Public Sub NavigateToPayment(extensionId As Integer) Implements Views.IExtendBookingView.NavigateToPayment
        ' Todo
    End Sub

    Public Sub NavigateToBookings(successful As Boolean) Implements Views.IExtendBookingView.NavigateToBookings
        Response.Redirect(PageUrl.MemberBookings + "?extension=true")
    End Sub
End Class