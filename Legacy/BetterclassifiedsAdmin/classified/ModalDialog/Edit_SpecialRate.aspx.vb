Imports BetterclassifiedsCore

Partial Public Class Edit_SpecialRate
    Inherits System.Web.UI.Page

    Private _specialRateId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _specialRateId = Request.QueryString("specialRateId")
        UpdateMessage.Text = ""
    End Sub

#Region "Databinding"

    Private Sub linqSourceGeneral_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqSourceGeneral.Selecting
        Dim obj As DataModel.BaseRate = GeneralController.GetBaseRateDetailsBySpecial(_specialRateId)
        e.Result = obj
    End Sub

    Private Sub linqSourceSpecial_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqSourceSpecial.Selecting
        Dim obj As DataModel.SpecialRate = GeneralController.GetSpecialRateById(_specialRateId)
        e.Result = obj
    End Sub

#End Region

#Region "Updating"

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        dtlGeneralInfo.UpdateItem(True)
        dtlSpecialRate.UpdateItem(True)
    End Sub

    Private Sub dtlGeneralInfo_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles dtlGeneralInfo.ItemUpdating
        ' get the template field values
        Dim description As TextBox = dtlGeneralInfo.FindControl("txtDescription")
        If description IsNot Nothing Then
            e.NewValues.Add("Description", description.Text)
        End If
    End Sub

    Private Sub linqSourceGeneral_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqSourceGeneral.Updating
        Try
            If (e.Exception IsNot Nothing) Then
                For Each innerException As KeyValuePair(Of String, Exception) _
                       In e.Exception.InnerExceptions
                    Me.UpdateMessage.Text &= innerException.Key & ": " & _
                        innerException.Value.Message & "<br />"
                Next
                e.ExceptionHandled = True

            Else
                With e.NewObject
                    GeneralController.UpdateBaseRateDetails(.BaseRateId, .Title, .Description)
                End With
            End If
        Catch ex As Exception
            UpdateMessage.Text = "Update failed: " + ex.Message
        Finally
            e.Cancel = True ' cancel the original LINQ source transaction
        End Try
    End Sub

    Private Sub linqSourceSpecial_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqSourceSpecial.Updating
        Try
            If (e.Exception IsNot Nothing) Then
                For Each innerException As KeyValuePair(Of String, Exception) _
                       In e.Exception.InnerExceptions
                    Me.UpdateMessage.Text &= innerException.Key & ": " & _
                        innerException.Value.Message & "<br />"
                Next
                e.ExceptionHandled = True
            Else
                Dim specialRate As DataModel.SpecialRate = TryCast(e.NewObject, DataModel.SpecialRate)
                With specialRate
                    If GeneralController.UpdateSpecialRateDetails(_specialRateId, .NumOfInsertions, .MaximumWords, _
                                                                  .SetPrice, .Discount, .LineAdBoldHeader, .LineAdImage) Then
                        UpdateMessage.Text = "Update Successful. "
                        UpdateMessage.ForeColor = Drawing.Color.Green
                    Else
                        UpdateMessage.Text = "Update Failed: Contact Administrator if problem persists.."
                    End If
                End With
            End If
        Catch ex As Exception
            UpdateMessage.Text = "Update Failed: " + ex.Message
        Finally
            e.Cancel = True ' cancel the original LINQ source transaction
        End Try
    End Sub

#End Region

End Class