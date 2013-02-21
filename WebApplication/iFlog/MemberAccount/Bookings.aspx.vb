Imports BetterclassifiedsCore

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
            pnlButton.Visible = (grdBookings.Rows.Count > 0)
        End Using
    End Sub

    Protected Sub CancelBooking(ByVal sender As Object, ByVal e As System.EventArgs)
        For Each row As GridViewRow In grdBookings.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim chk As CheckBox = TryCast(row.FindControl("chkSelect"), CheckBox)
                If chk IsNot Nothing Then
                    If chk.Checked Then
                        Dim hdnBooking As HiddenField = TryCast(row.FindControl("hdnBookingId"), HiddenField)
                        If hdnBooking IsNot Nothing And hdnBooking.Value <> String.Empty Then
                            BookingController.ExpireExistingBooking(hdnBooking.Value)
                        End If
                    End If
                End If
            End If
        Next
        DataBindBookings()
    End Sub

End Class