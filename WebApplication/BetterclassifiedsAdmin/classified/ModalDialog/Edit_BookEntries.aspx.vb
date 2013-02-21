Imports BetterclassifiedsCore

Partial Public Class Edit_BookEntries
    Inherits System.Web.UI.Page

    Private _adBookingId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _adBookingId = Request.QueryString("adBookingId")
        Dim query = BookingController.GetBookEntries(_adBookingId)
        grdBookEntries.DataSource = query
        grdBookEntries.DataBind()
    End Sub

End Class