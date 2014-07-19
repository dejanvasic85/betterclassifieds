'Imports BetterclassifiedsCore
'Imports Paramount.Broadcast.Components
'Imports Paramount.Betterclassifieds.Business.Broadcast
'Imports Microsoft.Practices.Unity

Partial Public Class Contact
    Inherits System.Web.UI.Page

    ' Private _broadcastManager As IBroadcastManager

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.RedirectPermanent("~/Home/ContactUs")

        '_broadcastManager = BetterClassified.Unity.DefaultContainer.Resolve(Of IBroadcastManager)()

        'If Request.QueryString("submit") = 1 Then
        '    lblSubmit.Visible = True
        'End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        ' set up the email content 
        'If RadCaptcha1.IsValid Then
        '    Dim sb As New StringBuilder
        '    sb.AppendFormat("<h3>Classies Support</h3>")
        '    sb.AppendFormat("<p>Enquiry Type: {0}</p>", ddlEnquiryType.SelectedItem.Text)
        '    sb.AppendFormat("<p>Full Name: {0}</p>", txtFirstName.Text)
        '    sb.AppendFormat("<p>Email: {0}</p>", txtEmail.Text)
        '    sb.AppendFormat("<p>Subject: {0}<p>", txtSubject.Text)
        '    sb.AppendLine("<h3> ENQUIRY DETAILS </h3>")
        '    sb.AppendFormat("<p><strong>{0}</strong></p>", txtComments.Text)

        '    GeneralController.CreateSupportEnquiry(ddlEnquiryType.SelectedValue, _
        '                                           txtFirstName.Text, _
        '                                           txtEmail.Text, _
        '                                           txtPhone.Text, _
        '                                           txtSubject.Text, _
        '                                           txtComments.Text)

        '    ' get email
        '    Dim emailCollection = ConfigurationManager.AppSettings.Get(ddlEnquiryType.SelectedValue).Split(";")


        '    Dim supportRequestNotification As New SupportRequest
        '    supportRequestNotification.RequestDetails = sb.ToString

        '    _broadcastManager.SendEmail(Of SupportRequest)(supportRequestNotification, emailCollection)

        '    Response.Redirect("~/Help/Contact.aspx?submit=1")
        'End If
    End Sub
End Class