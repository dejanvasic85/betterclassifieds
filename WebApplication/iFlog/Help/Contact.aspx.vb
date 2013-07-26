Imports BetterclassifiedsCore
Imports Paramount.Broadcast.Components

Partial Public Class Contact
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("submit") = 1 Then
            lblSubmit.Visible = True
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        ' set up the email content 
        If RadCaptcha1.IsValid Then
            Dim sb As New StringBuilder
            sb.AppendLine("*** Classies Support ***")
            sb.AppendLine(String.Format("Enquiry Type: {0}", ddlEnquiryType.SelectedItem.Text))
            sb.AppendLine(String.Format("Full Name: {0}", txtFirstName.Text))
            sb.AppendLine(String.Format("Email: {0}", txtEmail.Text))
            sb.AppendLine(String.Format("Subject: {0}", txtSubject.Text))
            sb.AppendLine()
            sb.AppendLine("*** ENQUIRY DETAILS ***")
            sb.AppendLine(txtComments.Text)

            GeneralController.CreateSupportEnquiry(ddlEnquiryType.SelectedValue, _
                                                   txtFirstName.Text, _
                                                   txtEmail.Text, _
                                                   txtPhone.Text, _
                                                   txtSubject.Text, _
                                                   txtComments.Text)

            ' get email
            Dim emailCollection = ConfigurationManager.AppSettings.Get(ddlEnquiryType.SelectedValue).Split(";")


            Dim mail As New SupportNotification(txtFirstName.Text, sb.ToString, ddlEnquiryType.SelectedItem.Text, emailCollection)
            mail.Send()

            Response.Redirect("~/Help/Contact.aspx?submit=1")
        End If
    End Sub
End Class