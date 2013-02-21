Public Partial Class ErrorList
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub ShowErrors(ByVal errorList As List(Of String))
        If errorList.Count > 0 Then
            ' display the errors in the control
            divError.Visible = True
            bulletErrorList.DataSource = errorList
            bulletErrorList.DataBind()
        Else
            HideErrors()
        End If
    End Sub

    Public Sub HideErrors()
        divError.Visible = False
    End Sub

End Class