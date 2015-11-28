Imports Telerik.Web.UI

Public Class SetupRatecard
    Inherits AdminBasePage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Private Sub btnCreateRatecard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreateRatecard.Click
        multiPageSetup.SelectedIndex = 1
    End Sub

    Private Sub btnCancelPage1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelPage1.Click, btnCancelPage2.Click
        Response.Redirect(PageUrl.ClassifiedRates_Ratecards)
    End Sub

    Private Sub btnAssignRates_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssignRates.Click
        Try
            ' Perform both (create ratecard and assign rates here)
            Dim rateCardId As Integer = ucxCreateRate.CreateRatecard()
            ucxAssignRates.AssignRates(rateCardId)
            ' Proceed to the next page (success)
            multiPageSetup.SelectedIndex = 2
            ucxUserMessage.PrintUserMessage(UserMessageType.RatecardSetupSuccessful)
        Catch ex As Exception
            ucxUserMessage.PrintException(ex)
        Finally
            multiPageSetup.SelectedIndex = 2
        End Try

    End Sub

    Private Sub btnFinish_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinish.Click
        Response.Redirect(PageUrl.ClassifiedRates_Ratecards)
    End Sub
End Class