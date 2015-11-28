Public Class AdminBasePage
    Inherits System.Web.UI.Page

    Protected ReadOnly Property UserMessageControl As UserMessage
        Get
            Return Me.Master.FindControl("ucxUserMessage")
        End Get
    End Property

End Class
