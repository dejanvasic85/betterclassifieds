Imports BetterClassified.UIController
Imports BetterClassified.UI
Imports Telerik.Web.UI
Imports BetterclassifiedsCore.DataModel

Public Class CreateLineAdTheme
    Inherits System.Web.UI.Page

    Private _controller As LineAdController
    Private _lineAdThemeId As Integer
    Private _isEditing As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _controller = New LineAdController
        If Request.QueryString("mode") = "edit" Then
            _isEditing = True
            _lineAdThemeId = Request.QueryString("lineAdThemeId")
            dtlLineAdThemeDetail.DefaultMode = DetailsViewMode.Edit
        End If
    End Sub

#Region "Event Handling"



    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect(PageUrl.ClassifiedThemes_ManageThemes)
    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        If _isEditing Then
            dtlLineAdThemeDetail.UpdateItem(True)
        Else
            dtlLineAdThemeDetail.InsertItem(True)
        End If
    End Sub

#End Region

#Region "Inserting"

    Private Sub srcLinq_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceInsertEventArgs) Handles srcLinq.Inserting
        ' Handle the creation process
        _controller.AddLineAdTheme(e.NewObject)
        e.Cancel = True ' Cancel rest of the operation
        redirectToThemes()
    End Sub

    Private Sub dtlLineAdThemeDetail_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertEventArgs) Handles dtlLineAdThemeDetail.ItemInserting
        ' Add extra required details

        e.Values.Add("HeaderColourCode", getColourValueFromControl("headerColourPicker"))
        e.Values.Add("BorderColourCode", getColourValueFromControl("borderColourPicker"))
        e.Values.Add("BackgroundColourCode", getColourValueFromControl("backgroundColourPicker"))
        e.Values.Add("IsHeadingSuperBold", True)
        e.Values.Add("IsActive", True)
        e.Values.Add("CreatedDate", DateTime.Now)
        e.Values.Add("CreatedByUser", Membership.GetUser().UserName)

    End Sub
#End Region

#Region "DataBinding"

    Private Sub dtlLineAdThemeDetail_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlLineAdThemeDetail.DataBound
        If _isEditing Then
            Dim dataItem As LineAdTheme = dtlLineAdThemeDetail.DataItem
            ' Set the Colours
            setColourValue("headerColourPicker", dataItem.HeaderColourCode)
            setColourValue("borderColourPicker", dataItem.BorderColourCode)
            setColourValue("backgroundColourPicker", dataItem.BackgroundColourCode)
        End If
    End Sub

    Private Sub srcLinq_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles srcLinq.Selecting
        If _isEditing Then
            e.Result = _controller.GetLineAdTheme(_lineAdThemeId)
        End If
    End Sub
#End Region

#Region "Updating"

    Private Sub dtlLineAdThemeDetail_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles dtlLineAdThemeDetail.ItemUpdating
        e.NewValues.Add("HeaderColourCode", getColourValueFromControl("headerColourPicker"))
        e.NewValues.Add("BorderColourCode", getColourValueFromControl("borderColourPicker"))
        e.NewValues.Add("BackgroundColourCode", getColourValueFromControl("backgroundColourPicker"))
    End Sub

    Private Sub srcLinq_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles srcLinq.Updating
        Dim lineAdTheme As LineAdTheme = e.NewObject
        _controller.UpdateLineAdTheme(_lineAdThemeId, lineAdTheme.HeaderColourCode, lineAdTheme.BorderColourCode, lineAdTheme.BackgroundColourCode)
        e.Cancel = True ' Cancel original transaction
        redirectToThemes()
    End Sub

#End Region

#Region "Helper Methods"
    Private Sub redirectToThemes()
        Response.Redirect(String.Format("{0}?msg={1}", PageUrl.ClassifiedThemes_ManageThemes, Integer.Parse(UserMessageType.ItemSavedSuccessfully)))
    End Sub

    Private Function getColourValueFromControl(ByVal controlName As String) As String
        Dim ctrl As LineAdColourPicker = dtlLineAdThemeDetail.FindControl(controlName)
        If ctrl IsNot Nothing Then
            Return Drawing.ColorTranslator.ToHtml(ctrl.SelectedColor)
        End If
        Return String.Empty
    End Function

    Private Sub setColourValue(ByVal controlName As String, ByVal value As String)
        Dim ctrl As LineAdColourPicker = dtlLineAdThemeDetail.FindControl(controlName)
        If ctrl IsNot Nothing Then
            If ctrl.Contains(value) Then
                ctrl.SelectedColor = Drawing.ColorTranslator.FromHtml(value)
            End If
        End If
    End Sub
#End Region

End Class