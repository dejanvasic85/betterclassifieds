Imports BetterclassifiedsCore

Namespace Master
    Partial Public Class MasterWithRightBar
        Inherits System.Web.UI.MasterPage

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Dim btn As LinkButton = TryCast(ucxKeySearch.FindControl("lnkSearch"), LinkButton)
            'If btn IsNot Nothing Then
            '    Me.Page.Form.DefaultButton = btn.UniqueID
            'End If
        End Sub

        Private Sub LinkPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkPrint.Click
            ' start a new print ad straight from here (go straight to paper selection)
            ' StartBookingStep2(SystemAdType.LINE)
            ' Force to bundled for now
            'General.StartBundleBookingStep2()
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
End Namespace