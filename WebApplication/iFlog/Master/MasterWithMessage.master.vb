Imports BetterclassifiedsCore

Partial Public Class MasterWithMessage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub LinkPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkPrint.Click
        ' start a new print ad straight from here (go straight to paper selection)
        ' StartBookingStep2(SystemAdType.LINE)
        ' Force to bundled for now
        ' General.StartBundleBookingStep2()
        Response.Redirect(PageUrl.BookingStep_1)
    End Sub

    Private Sub LinkOnline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkOnline.Click
        ' start new online ad print from here ( go straight to paper selection)
        'General.StartBookingStep2(SystemAdType.ONLINE)
        Response.Redirect(PageUrl.BookingStep_1)
    End Sub

    Private Sub Linkboth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkboth.Click
        ' start a new bundle booking (print and online) - straight to paper selection
        'General.StartBundleBookingStep2()
        Response.Redirect(PageUrl.BookingStep_1)
    End Sub

End Class