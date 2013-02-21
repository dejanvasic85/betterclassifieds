Imports BetterclassifiedsCore
Imports Paramount.Modules.Logging.UIController

Partial Public Class BodyView
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Select Case Request.QueryString("type").ToLower.Trim
                Case "db"
                    divContent.InnerHtml = AdController.GetOnlineAdHtml(Request.QueryString("id"))
                Case "session"
                    If BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                        divContent.InnerHtml = BundleBooking.BundleController.BundleCart.OnlineAd.HtmlText
                    ElseIf BookingController.BookingType = Booking.BookingAction.SpecialBooking Then
                        divContent.InnerHtml = BookingController.SpecialBookCart.OnlineAd.HtmlText
                    Else
                        divContent.InnerHtml = BookingController.AdBookCart.Ad.AdDesigns(0).OnlineAds(0).HtmlText
                    End If
            End Select
        Catch ex As Exception
            ' show an error 
            divContent.InnerHtml = "<b>Unable to display ad details at this time.</b>"
            ' write a log entry into the database
            ExceptionLogController(Of Exception).AuditException(ex)
        End Try
    End Sub
End Class