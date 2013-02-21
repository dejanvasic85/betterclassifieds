Public Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub grdSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSearchResults.RowDataBound
        ' method is used to display an automatic placeholder if image doesn't exist from datasource.
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim i As Image = e.Row.FindControl("imgDocument")

            If Not i Is Nothing Then
                If e.Row.DataItem.DocumentId Is Nothing Then
                    i.ImageUrl = "../image_placeholder.gif"
                Else
                    i.ImageUrl = "~/dsl/document.ashx?id=" + e.Row.DataItem.DocumentId
                End If
            End If

            Dim p As Label = e.Row.FindControl("lblPrice")
            If Not p Is Nothing Then
                If e.Row.DataItem.Price > 0 Then
                    p.Text = String.Format("{0:C}", e.Row.DataItem.Price)
                Else
                    p.Text = ""
                End If
            End If
        End If
    End Sub
End Class