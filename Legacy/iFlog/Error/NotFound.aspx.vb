Public Partial Class NotFound
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.RedirectPermanent("~/Error/NotFound")
    End Sub

End Class