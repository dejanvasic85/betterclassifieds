Imports Paramount.Broadcast.Components

Partial Public Class GetPassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.RedirectPermanent(NextGenUrl.LoginAndRegistration)
    End Sub


    'Public Sub SendEmail(ByVal user As MembershipUser)
    '    If (user Is Nothing) Then
    '        Return
    '    End If
    '    Dim email As New ForgottenPasswordNotification(user.UserName, user.GetPassword, user.Email)
    '    email.Send()
    'End Sub


    'Protected Sub SubmitEmail_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SubmitEmail.Click
    '    Dim username = Membership.GetUserNameByEmail(emailBox.Text)
    '    If String.IsNullOrEmpty(username) Then
    '        errorLabel.Text = "Email address does not exist"
    '        Return
    '    Else
    '        errorLabel.Text = ""
    '    End If
    '    Dim user = Membership.GetUser(username)
    '    If user.IsLockedOut = True Then
    '        errorLabel.Text = "You account is locked out. Please contact site administrator to unlock your account"
    '        Return
    '    End If
    '    SendEmail(user)
    '        errorLabel.Text = "You password has been sent to " + emailBox.Text
    'End Sub
End Class