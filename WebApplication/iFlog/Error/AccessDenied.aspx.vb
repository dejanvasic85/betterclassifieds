Public Partial Class AccessDenied
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lnkContact.NavigateUrl = PageUrl.HelpContact
    End Sub

End Class