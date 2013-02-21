Public Partial Class _Default
    Inherits System.Web.UI.Page
    Const url As String = "~/OnlineAds/AdView.aspx?preview=false&id={0}"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub sendMessage1_BackClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles sendMessage1.BackClick
        Response.Redirect(String.Format(url, Request.QueryString("AdNumber")))
    End Sub
End Class