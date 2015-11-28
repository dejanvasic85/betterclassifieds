Public Partial Class MemberHeading
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property HeadingText() As String
        Get
            Return lblHeader.Text
        End Get
        Set(ByVal value As String)
            lblHeader.Text = value
        End Set
    End Property

End Class