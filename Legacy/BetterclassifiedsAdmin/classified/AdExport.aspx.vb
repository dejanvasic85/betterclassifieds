Imports BetterclassifiedsCore

Partial Public Class AdExport
    Inherits System.Web.UI.Page

    Private Const _daysInAdvance As Integer = 40
    Private _imageTotal As Integer
    Private _headingTotal As Integer
    Private _editionDate As DateTime

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            DataBindPublications()
        End If
    End Sub

#Region "DataBinding"
    Private Sub DataBindPublications()
        ' perform databinding
        radComboPublication.DataSource = PublicationController.GetPublications()
        radComboPublication.DataBind()

        If radComboPublication.Items.Count > 0 Then
            DataBindEditions(radComboPublication.Items(0).Value)
        End If
    End Sub

    Private Sub DataBindEditions(ByVal publicationId As Integer)
        radComboEdition.DataSource = PublicationController.PublicationEditions(publicationId, _daysInAdvance)
        radComboEdition.DataBind()
    End Sub

    Private Sub radComboPublication_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radComboPublication.SelectedIndexChanged
        DataBindEditions(e.Value)
        grdPrintAds.Rebind()
    End Sub

    Private Sub grdPrintAds_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles grdPrintAds.ItemCommand
        If e.CommandName.Equals("delete", StringComparison.OrdinalIgnoreCase) Then
            Dim bookEntryId = Integer.Parse(e.CommandArgument)
            ' Perform delete on the book entry
            AdController.DeleteBookEntry(bookEntryId)
        End If
    End Sub

    Private Sub grdPrintAds_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles grdPrintAds.ItemDataBound
        ' Print the totals in the footer
        If e.Item.ItemType = Telerik.Web.UI.GridItemType.Footer Then
            e.Item.Cells(6).Text = String.Format("Total: {0}", _imageTotal)
            e.Item.Cells(7).Text = String.Format("Total: {0}", _headingTotal)
        End If
    End Sub

    Private Sub grdPrintAds_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles grdPrintAds.NeedDataSource
        ' Fetch the required date from the UI
        Dim publicationId As Integer = radComboPublication.SelectedValue
        Dim editionDate As DateTime = radComboEdition.SelectedValue

        ' Allow the date picker to take priority
        If Not _editionDate = Date.MinValue Then
            editionDate = _editionDate
        End If

        Dim data As List(Of DataModel.spLineAdExportListResult) = AdController.GetExportItems(publicationId, editionDate)
        _imageTotal = data.Where(Function(i) i.UsePhoto).Count
        _headingTotal = data.Where(Function(i) i.UseBoldHeader).Count
        grdPrintAds.DataSource = data
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        grdPrintAds.Rebind()
    End Sub

#End Region

    Private Sub radComboEdition_SelectedIndexChanged(ByVal o As Object, ByVal e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles radComboEdition.SelectedIndexChanged
        If radDatePicker.IsEmpty Then
            _editionDate = radComboEdition.SelectedValue
            grdPrintAds.Rebind()
        End If
    End Sub

    Private Sub radDatePicker_SelectedDateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs) Handles radDatePicker.SelectedDateChanged
        If Not radDatePicker.IsEmpty Then
            _editionDate = radDatePicker.SelectedDate
            grdPrintAds.Rebind()
        End If
    End Sub

    Private Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Dim publicationId = radComboPublication.SelectedValue
        Dim editionDate = radDatePicker.SelectedDate
        Dim adStorePath = txtOutputPath.Text.Trim

        If radDatePicker.IsEmpty Then
            editionDate = DateTime.Parse(radComboEdition.SelectedValue)
        End If

        Dim xmlDoc = GeneralRoutine.ExportBookings(publicationId, editionDate, adStorePath)
        Dim fileName = String.Format("{0} - {1:yyyy-MM-dd}.xml", radComboPublication.SelectedItem.Text, editionDate)

        '' Force download of XML file to browser
        Response.Clear()
        Response.ContentType = "application/octet-stream"
        Response.AddHeader("Content-type", "application/octet-stream")
        Response.AddHeader("Content-Disposition", "attachment;filename=""" + fileName + """")
        Response.Write(xmlDoc.ToString)
        Response.End()

    End Sub

    Private Sub btnDownloadReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDownloadReport.Click
        grdPrintAds.MasterTableView.ExportToExcel()
    End Sub
End Class