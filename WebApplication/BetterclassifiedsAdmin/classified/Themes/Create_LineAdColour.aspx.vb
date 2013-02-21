Imports BetterclassifiedsCore.DataModel
Imports Telerik.Web.UI
Imports System.Drawing
Imports BetterClassified.UIController

Public Class Create_LineAdColour
    Inherits System.Web.UI.Page

    Private _controller As LineAdController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _controller = New LineAdController()
    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        dtlLineAdColour.InsertItem(True)
    End Sub

    Private Sub dtlLineAdColour_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertEventArgs) Handles dtlLineAdColour.ItemInserting
        ' Add the required fields
        e.Values.Add("IsActive", True)
        e.Values.Add("CreatedDate", DateTime.Now)
        e.Values.Add("CreatedByUser", Membership.GetUser().UserName)

        Dim colourCtrl As RadColorPicker = dtlLineAdColour.FindControl("colourPicker")
        e.Values.Add("ColourCode", ColorTranslator.ToHtml(colourCtrl.SelectedColor))
        e.Values.Add("Cyan", getNumericValue("txtCyanCode"))
        e.Values.Add("Magenta", getNumericValue("txtMagentaCode"))
        e.Values.Add("Yellow", getNumericValue("txtYellowCode"))
        e.Values.Add("KeyCode", getNumericValue("txtKeyCode"))
    End Sub

    Private Sub srcLinq_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceInsertEventArgs) Handles srcLinq.Inserting
        If (e.Exception IsNot Nothing) Then
            e.ExceptionHandled = True
        Else
            Dim lineAdColourCode = DirectCast(e.NewObject, LineAdColourCode)
            If lineAdColourCode IsNot Nothing Then
                _controller.AddLineAdColourCode(lineAdColourCode)
                e.Cancel = True ' cancel the linq source insert method
            End If
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