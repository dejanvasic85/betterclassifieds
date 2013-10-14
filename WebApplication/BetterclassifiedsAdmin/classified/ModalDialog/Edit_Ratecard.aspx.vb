Imports BetterclassifiedsCore
Imports BetterClassified.UIController.ViewObjects

Imports Telerik.Web.UI

Partial Public Class Edit_Ratecard
    Inherits AdminBasePage

    Private _ratecardId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _ratecardId = Request.QueryString("ratecardId")
        ucxAssignRates.RateCardId = _ratecardId
        UserMessageControl.ClearMessage()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        dtlRatecard.UpdateItem(True)
    End Sub

    Private Sub btnUpdatePubCategories_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdatePubCategories.Click
        ucxAssignRates.AssignRates(_ratecardId)
        UserMessageControl.PrintUserMessage("Ratecard has been assigned successfully", True)
    End Sub

    Private Sub objSourceRateCard_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles objSourceRateCard.Selecting
        e.InputParameters.Add("rateCardId", _ratecardId)
    End Sub

    Private Sub objSourceRateCard_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles objSourceRateCard.Updating
        ' Extract the ratecard details
        Dim rateCardVo As New RateCardVo() With
            {.RateCardId = _ratecardId,
             .RateCardName = DirectCast(dtlRatecard.FindControl("txtRatecardName"), RadTextBox).Text,
             .MinCharge = getNumericValue("txtMinimumCharge"),
             .MaxCharge = getNumericValue("txtMaximumCharge"),
             .MeasureUnitLimit = getNumericValue("txtFreeWords", GetType(Integer)),
             .RatePerMeasureUnit = getNumericValue("txtRatePerWord"),
             .BoldHeading = getNumericValue("txtLineAdHeadingValue"),
             .LineAdSuperBoldHeading = getNumericValue("txtLineAduperBoldHeadingValue"),
             .LineAdColourHeading = getNumericValue("txtLineAdColourHeadingValue"),
             .LineAdColourBorder = getNumericValue("txtLineAdColourBorderValue"),
             .LineAdColourBackground = getNumericValue("txtLineAdColourBackgroundValue"),
             .PhotoCharge = getNumericValue("txtLineAdMainImageValue"),
             .LineAdExtraImage = getNumericValue("txtLineAdExtraImage")}

        e.InputParameters.Add("rateCardVo", rateCardVo)
    End Sub

    Private Function getNumericValue(ByVal controlName As String) As Object
        Return getNumericValue(controlName, GetType(Decimal))
    End Function

    Private Function getNumericValue(ByVal controlName As String, ByVal objectType As Type) As Object
        Dim textBox As RadNumericTextBox = dtlRatecard.FindControl(controlName)
        If textBox.Value IsNot Nothing Then
            If objectType.Equals(GetType(Integer)) Then
                Return Convert.ToInt32(textBox.Value)
            Else
                Return Convert.ToDecimal(textBox.Value)
            End If
        Else
            If Not objectType.Equals(GetType(Integer)) Then
                Return Convert.ToDecimal(0)
            End If
            Return 0
        End If
    End Function
End Class