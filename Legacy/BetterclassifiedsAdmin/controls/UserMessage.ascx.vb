Public Class UserMessage
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub ClearMessage()
        lblUserMessage.Text = String.Empty
        lblUserMessage.Visible = False
    End Sub

    Public Sub PrintUserMessage(ByVal userMessage As String, ByVal isSuccessful As Boolean)
        lblUserMessage.Text = userMessage
        setSuccessColours(isSuccessful)
    End Sub

    Public Sub PrintUserMessage(ByVal msgType As UserMessageType)
        lblUserMessage.Text = UserMessageHelper.GetUserMessage(msgType)
        setSuccessColours(True)
    End Sub

    Public Sub PrintException(ByVal exception As Exception)
        lblUserMessage.Text = String.Format("An error occurred. {0}", exception.Message)
        setSuccessColours(False)
    End Sub

    Private Sub setSuccessColours(ByVal isSuccessful As Boolean)
        lblUserMessage.Visible = True
        lblUserMessage.BorderStyle = BorderStyle.Solid
        lblUserMessage.BorderWidth = Unit.Pixel(2)
        If isSuccessful Then
            lblUserMessage.BorderColor = Drawing.ColorTranslator.FromHtml("#437C17")
            lblUserMessage.BackColor = Drawing.ColorTranslator.FromHtml("#C3FDB8")
        Else
            lblUserMessage.BorderColor = Drawing.ColorTranslator.FromHtml("#E41B17")
            lblUserMessage.BackColor = Drawing.ColorTranslator.FromHtml("#FAAFBE")
        End If
    End Sub
End Class