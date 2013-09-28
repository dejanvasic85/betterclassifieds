Imports BetterClassified.UI
Imports BetterClassified.UI.Views
Imports BetterClassified
Imports BetterClassified.Models

Partial Public Class Bookings
    Inherits BasePage(Of Presenters.UserBookingsPresenter, Views.IMyBookingsView)
    Implements Views.IMyBookingsView


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Presenter.Load()
        End If
    End Sub

    Public Sub DisplayBookings(ByVal bookings As List(Of UserBookingModel)) Implements Views.IMyBookingsView.DisplayBookings
        lstBookings.DataSource = bookings
        lstBookings.DataBind()
    End Sub

    Private Sub lstBookings_ItemCommand(sender As Object, e As ListViewCommandEventArgs) Handles lstBookings.ItemCommand
        If e.CommandName = "CancelBooking" Then
            Presenter.CancelBooking(e.CommandArgument)
        ElseIf e.CommandName = "BookAgain" Then
            ' todo - this needs to be refactored so that it's part of a refined booking process and go through a presenter
            General.StartBundleBookingStep2(e.CommandArgument)
        End If
    End Sub

    Private Sub lstBookings_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lstBookings.ItemDataBound
        If e.Item.ItemType = ListViewItemType.DataItem Then

            Dim booking = DirectCast(e.Item.DataItem, UserBookingModel)
            e.Item.FindControl(Of Image)("adImage").ImageUrl = Me.Request.ToImageUrl(booking.ImageId)

            Dim row = e.Item.FindControl(Of HtmlTableRow)("row")

            If booking.AboutToExpire Then
                row.Attributes.Add("class", "warning")
            End If

            If booking.Expired Then
                row.Attributes.Add("class", "danger")
                e.Item.FindControl(Of HyperLink)("lnkCancel").Visible = False
                e.Item.FindControl(Of HyperLink)("lnkEditOnlineAd").Visible = False
                e.Item.FindControl(Of HyperLink)("lnkEditLineAd").Visible = False
                e.Item.FindControl(Of LinkButton)("lnkBookAgain").Visible = True
                e.Item.FindControl(Of HyperLink)("lnkExtend").Visible = False
            Else
                e.Item.FindControl(Of HyperLink)("lnkEditLineAd").NavigateUrl = PageUrl.EditLineAd(booking.AdBookingId)
                e.Item.FindControl(Of HyperLink)("lnkEditLineAd").Visible = booking.LineAdId.HasValue
                e.Item.FindControl(Of HyperLink)("lnkEditOnlineAd").Visible = booking.OnlineAdId.HasValue
                e.Item.FindControl(Of HyperLink)("lnkEditOnlineAd").NavigateUrl = PageUrl.EditOnlineAd(booking.AdBookingId)
            End If

            e.Item.FindControl(Of HyperLink)("lnkViewInvoice").Visible = booking.IsPaid
            e.Item.FindControl(Of HyperLink)("lnkViewInvoice").NavigateUrl = PageUrl.ViewInvoice(booking.BookingReference)
            e.Item.FindControl(Of HyperLink)("lnkExtend").NavigateUrl = PageUrl.ExtendBooking(booking.AdBookingId)

        End If

    End Sub

    Public Sub DisplayExpiringBookingsWarning() Implements IMyBookingsView.DisplayExpiringBookingsWarning
        pnlBookingsAboutToExpireAlert.Visible = True
    End Sub

    Public Sub DisplayBookingCancelledAlert() Implements IMyBookingsView.DisplayBookingCancelledAlert
        pnlBookingCancelledAlert.Visible = True
    End Sub

    Public Sub DisplayExtensionCompleteAlert() Implements IMyBookingsView.DisplayExtensionCompleteAlert
        pnlExtensionComplete.Visible = True
    End Sub

    Public ReadOnly Property IsExtensionComplete As Boolean Implements IMyBookingsView.IsExtensionComplete
        Get
            Return Request.QueryStringValue(Of Boolean)("extension")
        End Get
    End Property

    Public ReadOnly Property SelectedViewType As UserBookingViewType Implements IMyBookingsView.SelectedViewType
        Get
            ' Return from view state (priority)
            If ViewState("viewType") IsNot Nothing Then
                Return DirectCast(ViewState("viewType"), UserBookingViewType)
            End If

            ' Return from query string
            If Request.QueryString("view").HasValue Then
                Return [Enum].Parse(GetType(UserBookingViewType), Request.QueryString("view"))
            End If

            ' Default to current
            Return UserBookingViewType.Current
        End Get
    End Property

    Public Sub HideAlerts() Implements IMyBookingsView.HideAlerts
        pnlBookingCancelledAlert.Visible = False
        pnlExtensionComplete.Visible = False
    End Sub

    Public Sub SetViewType(ByVal viewType As UserBookingViewType) Implements IMyBookingsView.SetViewType
        ucxHeading.HeadingText = ucxHeading.HeadingText.Append(" - " + Me.SelectedViewType.ToString)
    End Sub

End Class