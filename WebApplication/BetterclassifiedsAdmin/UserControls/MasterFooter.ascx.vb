Public Partial Class MasterFooter
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            dateYear.Text = DateTime.Now.Year.ToString()
        End If
    End Sub

End Class