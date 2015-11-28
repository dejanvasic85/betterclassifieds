Imports BetterclassifiedAdmin.Configuration
Imports BetterclassifiedsCore

Partial Public Class Bookings1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtStartDate.Text = DateTime.Today.ToString("dd-MMM-yyyy")
            txtEndDate.Text = DateTime.Today.AddMonths(1).ToString("dd-MMM-yyyy")
        End If
        lblActionMsgSuccess.Text = ""
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        searchResults.Visible = True
        grdSearchResults.DataBind()
        grdSearchResults.PageIndex = 0
    End Sub

    Private Sub grdSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSearchResults.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dataItem As DataModel.spAdBookingsSearchResult = e.Row.DataItem
            Dim img As Image = e.Row.FindControl("imgStatus")
            If img IsNot Nothing Then
                Select Case dataItem.BookingStatus
                    Case BetterclassifiedsCore.Controller.BookingStatus.BOOKED
                        img.ImageUrl = "~/App_Themes/blue/Images/webdev-ok.png"
                        img.ToolTip = "Booked"
                    Case BetterclassifiedsCore.Controller.BookingStatus.CANCELLED
                        img.ImageUrl = "~/App_Themes/blue/Images/webdev-remove.png"
                        img.ToolTip = "Cancelled"
                End Select
            End If
        End If

    End Sub

    Private Sub btnCancelSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelSelected.Click
        Threading.Thread.Sleep(2000)

        For Each row As GridViewRow In grdSearchResults.Rows
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRows"), CheckBox)
            If cb IsNot Nothing AndAlso cb.Checked Then
                Dim adBookingId As Integer = grdSearchResults.DataKeys(row.RowIndex).Value
                BookingController.CancelExistingBooking(adBookingId)
            End If
        Next
        grdSearchResults.DataBind()
        lblActionMsgSuccess.Text = "Successfully cancelled selected bookings."

    End Sub

    Private Sub btnActivate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActivate.Click
        Threading.Thread.Sleep(2000)

        For Each row As GridViewRow In grdSearchResults.Rows
            Dim cb As CheckBox = DirectCast(row.FindControl("chkRows"), CheckBox)
            If cb IsNot Nothing AndAlso cb.Checked Then
                Dim adBookingId As Integer = grdSearchResults.DataKeys(row.RowIndex).Value
                BookingController.ActiveExistingBooking(adBookingId)
            End If
        Next
        grdSearchResults.DataBind()
        lblActionMsgSuccess.Text = "Successfully activated selected bookings."
    End Sub

    Private Sub searchDataSource_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles searchDataSource.Selected
        lblSearchCount.Text = "Found " + e.ReturnValue.ToString() + " record(s)."
    End Sub

End Class