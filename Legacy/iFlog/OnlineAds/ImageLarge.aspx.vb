Public Partial Class ImageLarge
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strGuid = Server.UrlDecode(Request.QueryString("id"))

        imagePreview.ImageUrl = "~/dsl/document.ashx?id=" + strGuid

    End Sub

End Class