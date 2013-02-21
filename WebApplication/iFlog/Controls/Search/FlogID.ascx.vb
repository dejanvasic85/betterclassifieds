Public Partial Class FlogID
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub lnkSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ' get the online ad details for the reference user has provided and direct to item page
        Response.Redirect(PageUrl.AdViewItem + "?preview=false&type=dsId&id=" + txtIFlogID.Text)
    End Sub
End Class