Imports BetterclassifiedsCore


Partial Public Class BodyView
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Select Case Request.QueryString("type").ToLower.Trim
            Case "db"
                divContent.InnerHtml = AdController.GetOnlineAdHtml(Request.QueryString("id"))
            Case "session"
                If BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                    divContent.InnerHtml = BundleBooking.BundleController.BundleCart.OnlineAd.HtmlText
                Else
                    divContent.InnerHtml = BookingController.AdBookCart.Ad.AdDesigns(0).OnlineAds(0).HtmlText
                End If
        End Select
    End Sub
End Class