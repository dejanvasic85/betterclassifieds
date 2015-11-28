Imports Telerik.Web.UI

Partial Public Class InvoiceGrid
    Inherits System.Web.UI.UserControl

    Private ReadOnly Property User() As String
        Get
            Return HttpContext.Current.User.Identity.Name
        End Get
       
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    
    End Sub

    Protected Sub ObjectDataSource1_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs) Handles ObjectDataSource1.Selecting
        e.InputParameters.Clear()
        e.InputParameters.Add("userId", User)
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
    '    Me.radgrid1.MasterTableView.ExportToPdf()
    'End Sub

End Class