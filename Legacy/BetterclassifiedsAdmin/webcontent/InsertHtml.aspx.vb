Partial Public Class InsertHtml
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If (Request.QueryString("Id") >= 0) Then
        Me.DetailsView1.DefaultMode = DetailsViewMode.Insert
        'End If




    End Sub

   
End Class