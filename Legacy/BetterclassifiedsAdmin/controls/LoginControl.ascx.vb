Public Partial Class LoginControl
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub SetFocus()
        Me.Login1.Focus()
        Page.Form.DefaultFocus = Login1.FindControl("UserName").UniqueID
        Page.Form.DefaultButton = Login1.FindControl("LoginButton").UniqueID
    End Sub

    Public Property Width() As Unit
        Get
            Return Login1.Width
        End Get
        Set(ByVal value As Unit)
            Login1.Width = value
        End Set
    End Property

End Class