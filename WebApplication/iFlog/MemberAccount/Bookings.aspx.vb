Imports BetterclassifiedsCore
Imports System.Drawing

Partial Public Class Bookings
    Inherits System.Web.UI.Page

    Private _userId As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _userId = Membership.GetUser.UserName
        If Not Page.IsPostBack Then
            DataBindBookings()
        End If
    End Sub

    Private Sub DataBindBookings()
        Using Controller As New CRM.UserClassController
            grdBookings.DataSource = Controller.GetCurrentAdBookings(_userId, BetterclassifiedsCore.Controller.BookingStatus.BOOKED)
            grdBookings.DataBind()
        End Using
    End Sub

    Private Sub grdBookings_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdBookings.RowCommand
        Select Case e.CommandName
            Case "CancelBooking"
                BookingController.ExpireExistingBooking(e.CommandArgument)
        End Select
    End Sub

    Private Sub grdBookings_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdBookings.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then
            Return
        End If

        Dim item As DataModel.spAdBookingSelectUserActiveResult = e.Row.DataItem
        If item Is Nothing Then
            Return
        End If

        ' Check if the end date is within this week
        If item.EndDate.GetValueOrDefault.AddDays(-7) < DateTime.Today Then
            e.Row.BackColor = ColorTranslator.FromHtml("#FFF8C6")
            e.Row.ToolTip = "This booking will expire this week."
            highlightWarning.Visible = True
        End If

    End Sub


End Class