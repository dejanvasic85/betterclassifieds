Public Partial Class NavigationButtons
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Event NextStep As NavigateButtonHandler

    Public Delegate Sub NavigateButtonHandler(ByVal sender As Object, ByVal e As System.EventArgs)

    Protected Sub OnNavigationSelected(ByVal sender As Object, ByVal e As EventArgs)
        RaiseEvent NextStep(sender, e)
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
        OnNavigationSelected(sender, e)
    End Sub

    Public Property StepNumber() As String
        Get
            Return ViewState("stepNumber")
        End Get
        Set(ByVal value As String)

            'store it in viewstate for recording purposes
            ViewState("stepNumber") = value

            Select Case value
                Case "1"
                    btnPrevious.Visible = False
                Case "2"
                    btnPrevious.PostBackUrl = "~/Booking/Step1.aspx?action=back"
                Case "3"
                    btnPrevious.PostBackUrl = "~/Booking/Step2.aspx?action=back"
                Case "4"
                    btnPrevious.PostBackUrl = "~/Booking/Step3.aspx?action=back"
                Case "5"
                    btnPrevious.PostBackUrl = "~/Booking/Step4.aspx?action=back"
            End Select

        End Set
    End Property

End Class