Partial Public Class InsertText
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.DetailsView1.DefaultMode = DetailsViewMode.Insert
    End Sub

End Class