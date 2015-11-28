Imports BetterclassifiedsCore.Controller

Partial Public Class TransactionReport
    Inherits System.Web.UI.Page

    Dim _reportController As ReportController

#Region "Page Load / Pre Render"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _reportController = New ReportController
        If Not Page.IsPostBack Then
            dtpStart.SelectedDate = DateTime.Today.AddMonths(-1)
            dtpEndDate.SelectedDate = DateTime.Today
        End If
    End Sub

#End Region

#Region "Databinding"

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        RadGrid1.Rebind()
    End Sub

    Private Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        ' query the database and store in list
        Dim list = _reportController.GetTransactionReport(dtpStart.SelectedDate, dtpEndDate.SelectedDate)

        ' get the totals and display them
        RadGrid1.MasterTableView.Columns(7).FooterText = String.Format("{0:c}", list.Sum(Function(i) i.TotalPrice))
        RadGrid1.MasterTableView.Columns(6).FooterText = String.Format("{0:c}", list.Sum(Function(i) i.GST))
        RadGrid1.MasterTableView.Columns(5).FooterText = String.Format("{0:c}", list.Sum(Function(i) i.PriceExGST))
        
        ' databind the list
        RadGrid1.DataSource = list
    End Sub

#End Region


#Region "Export"

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        RadGrid1.MasterTableView.ExportToExcel()
    End Sub

#End Region

End Class