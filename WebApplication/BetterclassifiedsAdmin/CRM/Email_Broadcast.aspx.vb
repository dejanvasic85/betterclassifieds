Imports BetterclassifiedsCore
Imports BetterclassifiedAdmin.Configuration
Imports System.Net.Mail
Imports Paramount.Broadcast.Components
Imports Paramount.Broadcast.UIController.ViewObjects
Imports Paramount.Utility.Security
Imports Paramount.Common.UIController

Namespace CRM
    Partial Public Class Email_Broadcast
        Inherits Page

#Region "Page Load Events"

        Private selectedTemplateIndex As Integer
        ' enable disable buttons
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            If Not Me.Page.IsPostBack And Not String.IsNullOrEmpty(Request.QueryString("letter")) Then
                Dim _
                    source = _
                        UserAccountProfileController.GetNewsletterUsers(Request.QueryString("letter"), _
                                                                         Me.selectAllUsers.Checked, -1, _
                                                                         UsersGridView.PageSize)
                Me.UsersGridView.DataSource = source.Datasource

                UsersGridView.DataBind()
            End If
            If UsersGridView.Rows.Count = 0 Then
                btnSendEmail.Enabled = False
                btnSelectAll.Enabled = False
                'templatePanel.Visible = False
                btnUnselectAll.Enabled = False
                'te.Visible = False

            End If

        End Sub

#End Region

#Region "Send Email Button Click Event"


        'create subprocedure for send email button click
        Protected Sub btnSendEmail_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim email As New NewsletterEmail(txb_Subject.Text)
            email.Fields.Add(New TemplateItemView With {.Name = "content", .Value = radEditor1.Text})
            For Each row As GridViewRow In UsersGridView.Rows
                Dim cb As CheckBox = DirectCast(row.FindControl("chkRows"), CheckBox)
                If cb IsNot Nothing AndAlso cb.Checked Then
                    Try
                        Dim controller = New AppUserController
                        Dim recipient As New EmailRecipientView()
                        Dim username = UsersGridView.DataKeys(row.RowIndex).Value.ToString()
                        Dim userProfile = AppUserController.GetAppUserProfile(username)
                        Dim _
                            subscribersHanderPath = _
                                String.Format("{0}/Subscribe.axd", ConfigManager.NewsletterSubscribersHandlerPath)
                        recipient.Email = userProfile.Email
                        recipient.TemplateFields.Add( _
                                                      New TemplateItemView _
                                                         With {.Name = "email", .Value = userProfile.Email})
                        recipient.TemplateFields.Add(New TemplateItemView With {.Name = "username", .Value = username})
                        recipient.TemplateFields.Add( _
                                                      New TemplateItemView _
                                                         With {.Name = "firstName", .Value = userProfile.FirstName})
                        recipient.TemplateFields.Add( _
                                                      New TemplateItemView _
                                                         With {.Name = "lastName", .Value = userProfile.LastName})
                        recipient.TemplateFields.Add( _
                                                      New TemplateItemView _
                                                         With {.Name = "businessName", .Value = userProfile.BusinessName _
                                                         })
                        Dim query = New SecureQueryString
                        query.Add(TemplateFields.Username, username)

                        recipient.TemplateFields.Add( _
                                                      New TemplateItemView _
                                                         With { _
                                                         .Name = TemplateFields.UnSubscribe, _
                                                         .Value = _
                                                         QueryStringHelper.GenerateSecureUrl(subscribersHanderPath, _
                                                                                              query)})

                        email.Recipients.Add(recipient)
                        Select Case rbt_Importance.SelectedValue
                            Case "Low"
                                email.Priority = MailPriority.Low
                                Exit Select
                            Case "Normal"
                                email.Priority = MailPriority.Normal
                                Exit Select
                            Case "High"
                                email.Priority = MailPriority.High
                                Exit Select
                        End Select
                        email.IsHtmlBody = rbt_BodyTextType.SelectedValue
                        email.Send()

                        lbl_SendResults.Text = ":  Message sent successfully..."
                    Catch ex As Exception
                        lbl_SendResults.Text += ": ERROR: Problem sending email!" + ex.Message
                    Finally

                    End Try
                End If
            Next

        End Sub

#End Region

#Region "Toggle Checkboxes - select all - unselect all"

        'create private subprocedure for button toggle as a boolean (on/off)
        Private Sub ToggleCheckState(ByVal checkState As Boolean)
            For Each row As GridViewRow In UsersGridView.Rows
                Dim cb As CheckBox = DirectCast(row.FindControl("chkRows"), CheckBox)
                If cb IsNot Nothing Then
                    cb.Checked = checkState
                End If
            Next
        End Sub

        'create subprocedure click event for select all button
        Protected Sub btnSelectAll_Click(ByVal sender As Object, ByVal e As EventArgs)
            ToggleCheckState(True)

        End Sub

        Protected Sub btnUnselectAll_Click(ByVal sender As Object, ByVal e As EventArgs)
            ToggleCheckState(False)
        End Sub

#End Region


        Private Sub UsersGridView_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) _
            Handles UsersGridView.PageIndexChanging
            UsersGridView.PageIndex = e.NewPageIndex
        End Sub

        Private Sub showAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles showAll.Click
            Dim _
                              source = _
                                  UserAccountProfileController.GetNewsletterUsers(Request.QueryString("letter"), _
                                                                                   Me.selectAllUsers.Checked, 1, _
                                                                                   UsersGridView.PageSize)
            Me.UsersGridView.DataSource = source.Datasource

            UsersGridView.DataBind()
        End Sub
    End Class
End Namespace