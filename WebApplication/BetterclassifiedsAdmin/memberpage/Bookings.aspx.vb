Public Partial Class Bookings
    Inherits System.Web.UI.Page

 
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.DataBind()
    End Sub

    Private Sub CustomerSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles BookingSource.Selecting
        'e.InputParameters("key") = Me.txtSearchKey.Text
    End Sub
End Class