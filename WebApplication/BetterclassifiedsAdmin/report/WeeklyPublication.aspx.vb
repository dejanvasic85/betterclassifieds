Imports BetterclassifiedsCore.Controller
Imports BetterclassifiedsCore
Imports Telerik.Web

Partial Public Class WeeklyPublication
    Inherits System.Web.UI.Page

    Dim _reportController As ReportController

#Region "Page Load / Pre Render"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _reportController = New ReportController
        If Not Page.IsPostBack Then
            DataBindPublications()
            dtpEdition.SelectedDate = DateTime.Today
        End If
    End Sub

#End Region

#Region "Databinding"

    Protected Sub RadGrid1_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource

        ' query the database and store in list
        Dim list = _reportController.GetWeeklySalesReport(ddlPublication.SelectedValue, _
                                                          dtpEdition.SelectedDate, _
                                                          ddlStatus.SelectedValue, Nothing)
        ' get the totals and display them
        RadGrid1.MasterTableView.Columns(12).FooterText = String.Format("{0:c}", list.Sum(Function(i) i.TotalPrice))
        RadGrid1.MasterTableView.Columns(11).FooterText = String.Format("{0:c}", list.Sum(Function(i) i.GST))
        RadGrid1.MasterTableView.Columns(10).FooterText = String.Format("{0:c}", list.Sum(Function(i) i.PriceExGST))
        RadGrid1.MasterTableView.Columns(9).FooterText = list.Sum(Function(i) i.Photos).ToString
        RadGrid1.MasterTableView.Columns(8).FooterText = list.Sum(Function(i) i.BoldHeadings).ToString
        RadGrid1.MasterTableView.Columns(7).FooterText = list.Sum(Function(i) i.NumOfWords).ToString

        ' databind the list
        RadGrid1.DataSource = list
    End Sub

    Private Sub DataBindPublications()
        ddlPublication.DataSource = PublicationController.GetPublications()
        ddlPublication.DataBind()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        RadGrid1.Rebind()
    End Sub

#End Region

#Region "Exporting Data"
    Protected Sub ExportToExcel(ByVal source As Object, ByVal e As EventArgs)
        RadGrid1.MasterTableView.ExportToExcel()
    End Sub
#End Region

End Class