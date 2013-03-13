Imports Paramount.ApplicationBlock.Configuration
Imports Paramount.Broadcast.Components
Imports Microsoft.Win32

Namespace CRM
    Partial Public Class Email_Options
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        End Sub



        Private Sub emailTemplateSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles emailTemplateSource1.Selected

        End Sub

        Private Sub ddlEmailTemplate1_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles ddlEmailTemplate1.SelectedIndexChanged
            successPanel.Visible = False
            If String.IsNullOrEmpty(e.Value) Then
                updateBtn.Enabled = False
                emailContentBox1.Content = String.Empty
                templateName.Text = String.Empty
                senderBox.Text = String.Empty
                subjectBox.Text = String.Empty
                templateDescription.Text = String.Empty
                Return
            End If

            Me.updateBtn.Enabled = True
            Dim template = EmailBroadcastController.GetTemplate(e.Value)
            Me.emailContentBox1.Content = template.EmailContent
            Me.templateName.Text = template.Name
            Me.senderBox.Text = template.Sender
            subjectBox.Text = template.Subject
            templateDescription.Text = template.Description
        End Sub

        Private Sub updateBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles updateBtn.Click
            Me.successPanel.Visible = False
            Dim template As New EmailTemplateView
            template.Description = templateDescription.Text
            template.EmailContent = emailContentBox1.Content
            template.ClientCode = ConfigSettingReader.ClientCode
            template.Name = templateName.Text
            template.Sender = senderBox.Text
            template.Subject = subjectBox.Text
            EmailBroadcastController.InsertUpdateTemplate(template, True)
            Me.successPanel.Visible = True
        End Sub
    End Class

End Namespace