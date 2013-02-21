Imports System.Net.Mail

Partial Public Class ChangePassword
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub ChangePassword1_ChangedPassword(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChangePassword1.ChangedPassword
        Try

        
            Dim mailClient As New SmtpClient
            mailClient.EnableSsl = True

            Dim email As String = Membership.GetUser().Email
            Dim newPassword As String = ChangePassword1.NewPassword

            Dim msg As New MailMessage
            msg.Body = "Recently you have requested to change your password. The new password is " + newPassword
            msg.Subject = "iFlog Password Change Request"
            msg.From = New MailAddress("paramountits@gmail.com")
            msg.To.Add(New MailAddress(email))

            mailClient.Send(msg)
        Catch ex As Net.Mail.SmtpException
            ' todo - log the error that email is down.
        End Try
    End Sub

End Class