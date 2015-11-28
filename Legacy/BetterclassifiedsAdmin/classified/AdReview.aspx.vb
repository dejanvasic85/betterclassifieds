Imports BetterclassifiedsCore

Partial Public Class AdReview
    Inherits System.Web.UI.Page

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DatabindPendingList()
        End If
    End Sub

#End Region

#Region "Databinding"
    Private Sub DatabindPendingList()
        Dim query = AdController.GetPendingOnlineAds
        lblRecords.Text = "Found " + query.Count.ToString + " items."
        grdSearchResults.DataSource = query
        grdSearchResults.DataBind()
    End Sub

    Private Sub grdSearchResults_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdSearchResults.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.DataItem.Heading.Length > 20 Then
                Dim lblHeading As Label = e.Row.FindControl("lblHeading")
                lblHeading.Text = e.Row.DataItem.Heading.Substring(0, 20) + "..."
            End If

            If e.Row.DataItem.Description.Length > 50 Then
                Dim lblDescription As Label = e.Row.FindControl("lblDescription")
                lblDescription.Text = e.Row.DataItem.Description.Substring(0, 50) + "..."
            End If
        End If
    End Sub

    Private Sub btnRefreshList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefreshList.Click
        DatabindPendingList()
    End Sub

#End Region

#Region "Approval"

    Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        If MassChangeStatus(AdDesignStatus.Approved) Then
            lblMessage.Text = "Selected Online Ads have been approved successfully."
            lblMessage.ForeColor = Drawing.Color.Green
        Else
            lblMessage.Text = "Failed to Approve selected Online Ads."
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If MassChangeStatus(AdDesignStatus.Cancelled) Then
            lblMessage.Text = "Selected Online Ads have been cancelled successfully."
            lblMessage.ForeColor = Drawing.Color.Green
        Else
            lblMessage.Text = "Failed to Cancel selected Online Ads."
        End If
    End Sub

    Private Function MassChangeStatus(ByVal adStatus As AdDesignStatus) As Boolean
        Try
            For Each row As GridViewRow In grdSearchResults.Rows
                Dim checkBox As CheckBox = row.FindControl("chkRows")
                If checkBox IsNot Nothing Then
                    If checkBox.Checked Then
                        Dim onlineAdId As Integer = DirectCast(row.FindControl("hdnOnlineId"), HiddenField).Value
                        AdController.UpdateOnlineAdStatus(onlineAdId, adStatus)
                    End If
                End If
            Next
            DatabindPendingList()
            Return True
        Catch ex As Exception
            lblMessage.Text = "Error occurred when changing status. Detail: " + ex.Message
            Return False
        End Try
    End Function

#End Region

End Class