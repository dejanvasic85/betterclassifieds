<ComponentModel.DataObject()> _
Public Class CRMController
    Public Shared Function GetEmailTemplate() As IList
        Using db = CRMDataContext.NewContext
            Dim list = db.EmailTemplates.ToList
            list.Insert(0, New EmailTemplate With {.EmailTemplateId = 0, .EmailHeader = "None", .EmailBody = ""})
            Return list
        End Using
    End Function


    Public Shared Function GetEmailTemplate(ByVal id As Integer) As EmailTemplate
        Using db = CRMDataContext.NewContext
            Dim template = From i In db.EmailTemplates Where i.EmailTemplateId = id
            Return template.FirstOrDefault
        End Using
    End Function

    Public Shared Function SerachUserByUsername(ByVal applicationCode As String, ByVal username As String) As IList
        If username = "%" Then
            Return GetAllUser(applicationCode)
        End If

        Using db = CRMDataContext.NewContext
            Dim user = From m In db.aspnet_Memberships Join u In db.aspnet_Users On u.UserId Equals m.UserId _
                        Join a In db.aspnet_Applications On m.ApplicationId Equals a.ApplicationId Where a.ApplicationName = applicationCode And u.LoweredUserName.StartsWith(username.ToLower) Select m.UserId, u.LoweredUserName, _
                        m.LoweredEmail, m.IsApproved, m.IsLockedOut, u.LastActivityDate, m.CreateDate
            Return user.ToList
        End Using
    End Function


    Public Shared Function SerachUserByEmail(ByVal applicationCode As String, ByVal email As String) As IList

        Using db = CRMDataContext.NewContext
            Dim user = From m In db.aspnet_Memberships Join u In db.aspnet_Users On u.UserId Equals m.UserId _
                        Join a In db.aspnet_Applications On m.ApplicationId Equals a.ApplicationId Where a.ApplicationName = applicationCode And m.Email.ToLower = email.ToLower Select m.UserId, u.LoweredUserName, _
                        m.LoweredEmail, m.IsApproved, m.IsLockedOut, u.LastActivityDate, m.CreateDate
            Return user.ToList
        End Using
    End Function

    Public Shared Function GetAllUser(ByVal applicationCode As String) As IList
        Using db = CRMDataContext.NewContext
            Dim users = From m In db.aspnet_Memberships Join u In db.aspnet_Users On u.UserId Equals m.UserId _
                        Join a In db.aspnet_Applications On m.ApplicationId Equals a.ApplicationId Where a.ApplicationName = applicationCode Select m.UserId, u.LoweredUserName, _
                        m.LoweredEmail, m.IsApproved, m.IsLockedOut, u.LastActivityDate, m.CreateDate
            Return users.ToList
        End Using
    End Function

    Public Shared Function GetAllFromEmail() As IList
        Using db = CRMDataContext.NewContext
            Return db.EmailFroms.ToList
        End Using
    End Function

    Public Shared Sub InsertEmailFrom(ByVal emailFromId As Integer, ByVal EmailAddress As String, ByVal Description As String)
        Dim item As EmailFrom
        Using db = CRMDataContext.NewContext
            If (emailFromId > 0) Then
                item = (From i In db.EmailFroms Where i.EmailFromId = emailFromId).FirstOrDefault
                item.EmailAddress = EmailAddress
                item.Description = Description
                db.EmailFroms.Attach(item, True)
            Else
                item = New EmailFrom
                item.EmailAddress = EmailAddress
                item.Description = Description
                db.EmailFroms.InsertOnSubmit(item)
            End If
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub UpdateEmailFrom(ByVal originalemailFromId As Integer, ByVal EmailAddress As String, ByVal Description As String)

        Using db = CRMDataContext.NewContext
            Dim item = (From i In db.EmailFroms Where i.EmailFromId = originalemailFromId).FirstOrDefault
            item.EmailAddress = EmailAddress
            item.Description = Description

            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub DeleteEmailFrom(ByVal originalEmailFromId As Integer)
        Using db = CRMDataContext.NewContext
            If originalEmailFromId > 0 Then
                Dim item = (From g In db.EmailFroms Where g.EmailFromId = originalEmailFromId Select g).FirstOrDefault
                db.EmailFroms.DeleteOnSubmit(item)
                db.SubmitChanges()
            End If
        End Using
    End Sub

    Public Shared Function GetAllSmtpClient() As IList
        Using db = CRMDataContext.NewContext
            Return db.SmtpClients.ToList
        End Using
    End Function


    Public Shared Sub InsertSmtpClient(ByVal smtpClientId As Integer, ByVal smtpClient As String, ByVal username As String, ByVal password As String)
        Using db = CRMDataContext.NewContext
            Dim item = New SmtpClient With {.SmtpClient = smtpClient, .SmtpPassword = password, .SmtpUsername = username}
            db.SmtpClients.InsertOnSubmit(item)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub UpdateSmtpClient(ByVal originalSmptClientID As Integer, ByVal smtpClient As String, ByVal username As String, ByVal password As String)
        Dim item As SmtpClient
        Using db = CRMDataContext.NewContext
            item = (From g In db.SmtpClients Where g.SmtpId = originalSmptClientID Select g).FirstOrDefault
            item.SmtpClient = smtpClient
            item.SmtpPassword = password
            item.SmtpUsername = username

            db.SubmitChanges()
        End Using
    End Sub
    Public Shared Sub DeleteSmtpClient(ByVal originalSmtpClientId As Integer)
        Using db = CRMDataContext.NewContext
            If originalSmtpClientId > 0 Then
                Dim item = (From g In db.SmtpClients Where g.SmtpId = originalSmtpClientId Select g).FirstOrDefault
                db.SmtpClients.DeleteOnSubmit(item)
                db.SubmitChanges()
            End If
        End Using
    End Sub
End Class
