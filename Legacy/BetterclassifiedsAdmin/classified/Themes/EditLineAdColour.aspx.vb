Imports Telerik.Web.UI
Imports System.Drawing
Imports BetterClassified.UIController.ViewObjects

Imports BetterClassified.UIController

Public Class EditLineAdColour
    Inherits AdminBasePage

    Private _lineAdColourId As Integer
    Private _lineAdController As LineAdController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserMessageControl.ClearMessage()
        _lineAdColourId = Request.QueryString("id")
        _lineAdController = New LineAdController()
    End Sub

    Private Sub srcLineAdColour_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles srcLineAdColour.Selecting
        e.InputParameters.Add("lineAdColourId", _lineAdColourId)
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        dtlLineAdColour.UpdateItem(True)
    End Sub

    Private Sub srcLineAdColour_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles srcLineAdColour.Updating

        ' Fetch the original first to update with non editable properties
        Dim originalObject = _lineAdController.GetLineAdColour(_lineAdColourId)

        Dim lineAdColourVo As New LineAdColourVo() With
            {.LineAdColourId = _lineAdColourId,
             .LineAdColourName = e.InputParameters("LineAdColourName"),
             .ColourCode = ColorTranslator.ToHtml(DirectCast(dtlLineAdColour.FindControl("colourPicker"), RadColorPicker).SelectedColor),
             .Cyan = getNumericValue("txtCyanCode"),
             .Magenta = getNumericValue("txtMagentaCode"),
             .Yellow = getNumericValue("txtYellowCode"),
             .KeyCode = getNumericValue("txtKeyCode"),
             .SortOrder = getNumericValue("txtSortOrder"),
             .IsActive = originalObject.IsActive,
             .CreatedByUser = originalObject.CreatedByUser,
             .CreatedDate = originalObject.CreatedDate}
        e.InputParameters.Clear()
        e.InputParameters.Add("lineAdColourVo", lineAdColourVo)
    End Sub

    Private Sub dtlLineAdColour_ItemCreated(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlLineAdColour.ItemCreated
        ' Databind the selected colour
        Dim lineAdColourVo = DirectCast(dtlLineAdColour.DataItem, LineAdColourVo)
        If lineAdColourVo IsNot Nothing Then
            Dim colourCtrl = DirectCast(dtlLineAdColour.FindControl("colourPicker"), RadColorPicker)
            colourCtrl.SelectedColor = ColorTranslator.FromHtml(lineAdColourVo.ColourCode)
        End If
    End Sub

    Private Function getNumericValue(ByVal controlName As String) As Object
        Return getNumericValue(controlName, GetType(Integer))
    End Function

    Private Function getNumericValue(ByVal controlName As String, ByVal objectType As Type) As Object
        Dim textBox As RadNumericTextBox = dtlLineAdColour.FindControl(controlName)
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