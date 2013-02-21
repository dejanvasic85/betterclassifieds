Imports BetterclassifiedsCore

Partial Public Class Edit_Location
    Inherits System.Web.UI.Page

    Private _id As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _id = Request.QueryString("id")
            lblMsg.Text = String.Empty
        Catch ex As Exception
            lblMsg.Text = ex.ToString
        End Try
    End Sub

    Private Sub linqSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqSource.Selecting
        e.Result = GeneralController.GetLocationById(_id)
    End Sub

#Region "Updating"

    Private Sub linqSource_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqSource.Updating
        Try
            Dim location As DataModel.Location = TryCast(e.NewObject, DataModel.Location)
            If location IsNot Nothing Then
                GeneralController.UpdateLocation(location.LocationId, location.Title, location.Description)
                lblMsg.Text = "Update Successful"
                lblMsg.ForeColor = Drawing.Color.Green
            ElseIf e.Exception IsNot Nothing Then
                lblMsg.Text = "Update Failed: " + e.Exception.Message
            End If
        Catch ex As Exception
            lblMsg.Text = "Unable to update location details. Error: " + ex.Message
        Finally
            e.Cancel = True
        End Try
    End Sub

    Private Sub dtlLocation_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles dtlLocation.ItemUpdating
        ' get the template field values into the new values collection of the control
        Try
            Dim desc As TextBox = dtlLocation.FindControl("txtDescription")
            If desc IsNot Nothing Then
                e.NewValues.Add("Description", desc.Text)
            End If
        Catch ex As Exception
            lblMsg.Text = "Update Failed: " + ex.Message
        End Try
        
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        dtlLocation.UpdateItem(True)
    End Sub

#End Region


End Class