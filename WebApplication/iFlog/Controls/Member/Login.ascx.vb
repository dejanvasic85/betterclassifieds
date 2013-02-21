Public Partial Class Login
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub Login1_Authenticate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.AuthenticateEventArgs) Handles Login1.Authenticate

        If (Membership.Provider.ValidateUser(Login1.UserName, Login1.Password) = True) Then
            e.Authenticated = True

            ' store the username into the session
            Session(Common.Constants.sessionUsername) = Login1.UserName
        End If
    End Sub

    Public Property InstructionText() As String
        Get
            Return Login1.InstructionText
        End Get
        Set(ByVal value As String)
            Login1.InstructionText = value
        End Set
    End Property

    Public ReadOnly Property LoginControl() As System.Web.UI.WebControls.Login
        Get
            Return Login1
        End Get
    End Property

End Class