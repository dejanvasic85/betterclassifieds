Imports System.ComponentModel
Imports Telerik.Web.UI
Imports BetterClassified.UIController
Imports BetterclassifiedsCore.DataModel

Public Class CreateRatecardControl
    Inherits System.Web.UI.UserControl

    Private _controller As RateController
    Private _newId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _controller = New RateController()
    End Sub

    Public Function CreateRatecard() As Integer
        dtlRatecard.InsertItem(True)
        Return _newId
    End Function


    Private Sub linqSourceRatecard_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceInsertEventArgs) Handles linqSourceRatecard.Inserting
        ' Handle the inserting ourselves
        If e.Exception IsNot Nothing Then
            Throw e.Exception
        End If

        Dim rateCard As Ratecard = e.NewObject

        'Get the title of the base rate
        Dim baseRateTitle As String = TryCast(dtlRatecard.FindControl("txtRatecardName"), RadTextBox).Text
        Dim baseRate As New BaseRate With {.Title = baseRateTitle}

        _newId = _controller.AddRateCard(baseRate, rateCard)

        e.Cancel = True
    End Sub

    Private Sub dtlRatecard_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertEventArgs) Handles dtlRatecard.ItemInserting
        e.Values.Add("MinCharge", getControlValue("txtMinimumCharge"))
        e.Values.Add("MaxCharge", getControlValue("txtMaximumCharge"))
        e.Values.Add("MeasureUnitLimit", getControlValue("txtFreeWords", GetType(Integer)))
        e.Values.Add("RatePerMeasureUnit", getControlValue("txtRatePerWord"))
        e.Values.Add("PhotoCharge", getControlValue("txtLineAdMainImageValue"))
        e.Values.Add("BoldHeading", getControlValue("txtLineAdHeadingValue"))
        'e.Values.Add("OnlineEditionBundle", getControlValue("txtOnlineAdBundleCharge"))
        e.Values.Add("LineAdBoldHeading", getControlValue("txtLineAduperBoldHeadingValue"))
        e.Values.Add("LineAdColourHeading", getControlValue("txtLineAdColourHeadingValue"))
        e.Values.Add("LineAdColourBorder", getControlValue("txtLineAdColourBorderValue"))
        e.Values.Add("LineAdColourBackground", getControlValue("txtLineAdColourBackgroundValue"))
        e.Values.Add("LineAdExtraImage", getControlValue("txtLineAdExtraImage"))

        e.Values.Add("CreatedDate", DateTime.Now)
        e.Values.Add("CreatedByUser", Membership.GetUser().UserName)
    End Sub

    Private Function getControlValue(ByVal controlName As String) As Object
        Return getControlValue(controlName, GetType(Decimal))
    End Function

    Private Function getControlValue(ByVal controlName As String, ByVal objectType As Type) As Object
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