Imports BetterclassifiedsCore

Partial Public Class SpecialRates
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        msgRates.Text = ""

        If Not Page.IsPostBack Then
            DatabindSpecialRates()
        End If
    End Sub

    Private Sub DatabindSpecialRates()
        grdSpecialRates.DataSource = GeneralController.GetSpecialRates()
        grdSpecialRates.DataBind()
    End Sub

#Region "Deleting Methods"

    Private Sub grdSpecialRates_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdSpecialRates.RowDeleting
        ' handles the deleting action
        Dim groupId = Guid.NewGuid
        Dim hdnField As HiddenField = grdSpecialRates.Rows(e.RowIndex).FindControl("hdnSpecialRateId")
        If hdnField IsNot Nothing Then
            Try
                GeneralController.DeleteSpecialRateById(hdnField.Value, groupId)
                DatabindSpecialRates()
            Catch ex As Exception
                msgRates.Text = "An error occurred when trying to delete. Error: " + ex.Message
            End Try
        End If
    End Sub

    Private Sub btnDeleteSelected_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteSelected.Click
        Try
            Dim groupID = Guid.NewGuid
            For Each row As GridViewRow In grdSpecialRates.Rows
                Dim chkBox As CheckBox = row.FindControl("chkRows")
                If chkBox IsNot Nothing Then
                    If chkBox.Checked Then
                        ' delete the items
                        Dim hdnField As HiddenField = row.FindControl("hdnSpecialRateId")
                        If hdnField IsNot Nothing Then
                            GeneralController.DeleteSpecialRateById(hdnField.Value, groupID)
                        End If
                    End If
                End If
            Next
            ' print fail msg
            msgRates.Text = "Successfully deleted selected specials."
            msgRates.ForeColor = Drawing.Color.Green
            DatabindSpecialRates()
        Catch ex As Exception
            msgRates.Text = "Error occurred when trying to delete records. Error: " + ex.Message
        End Try
    End Sub

#End Region

    Private Sub btnRefreshList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshList.Click
        DatabindSpecialRates()
    End Sub
End Class