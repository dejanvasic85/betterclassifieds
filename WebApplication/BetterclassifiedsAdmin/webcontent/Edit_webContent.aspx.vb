Public Partial Class Edit_webContent
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Request.QueryString("Id")) <= 0 Then
            Me.DetailsView1.DefaultMode = DetailsViewMode.Insert
            Return
        End If
    End Sub

    Protected Function IsContentText(ByVal type As String) As Boolean
        If String.Compare(type, "Text", True) = 0 Then
            Return True
        End If
        Return False
    End Function

    Protected Function IsContentHtml(ByVal type As String) As Boolean
        If String.Compare(type, "Text", True) = 0 Then
            Return False
        End If
        Return True
    End Function
End Class