Imports BetterclassifiedsCore

Partial Public Class LocationManagement
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblLocation.Text = String.Empty
        lblLocationArea.Text = String.Empty

        If Not Page.IsPostBack Then
            DatabindControls()
        End If
    End Sub

#Region "Databinding"

    Private Sub DatabindControls()
        lstLocations.DataSource = GeneralController.GetLocations()
        lstLocations.DataBind()

        If lstLocations.Items.Count > 0 Then
            ' select the area
            lstLocations.SelectedIndex = 0
            DataBindAreas(lstLocations.Items(0).Value)
            EditLocationLink(lstLocations.SelectedValue)
        End If
    End Sub

    Private Sub DataBindAreas(ByVal locationId)
        lstAreas.DataSource = GeneralController.GetLocationAreas(locationId, False)
        lstAreas.DataBind()

        If lstAreas.Items.Count > 0 Then
            lstAreas.SelectedIndex = 0
            EditAreaLink(lstAreas.SelectedValue)
        End If
    End Sub

#End Region

#Region "Create"

    Private Sub btnAddLocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddLocation.Click
        Try
            ' add new location to db
            If GeneralController.LocationExists(txtLocation.Text) Then
                ' print msg that location already exists
                lblLocation.Text = "Location already exists in the system."
            Else
                ' add the new location
                GeneralController.CreateLocation(txtLocation.Text)
                lblLocation.Text = "Successfully created location '" + txtLocation.Text + "'"
                DatabindControls()
            End If
        Catch ex As Exception
            lblLocation.Text = ex.ToString ' print the error msg
            lblLocation.ForeColor = Drawing.Color.Red
        End Try

    End Sub

    Private Sub btnAddArea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddArea.Click
        Try
            If GeneralController.LocationAreaExists(txtLocationArea.Text, lstLocations.SelectedValue) Then
                lblLocationArea.Text = "Area already exists in the system."
            Else
                GeneralController.CreateLocationArea(txtLocationArea.Text, lstLocations.SelectedValue)
                lblLocationArea.Text = "Successfully created area '" + txtLocationArea.Text + "'"
                DataBindAreas(lstLocations.SelectedValue)
            End If
        Catch ex As Exception
            lblLocationArea.Text = ex.ToString ' print the error msg
            lblLocationArea.ForeColor = Drawing.Color.Red
        End Try
    End Sub

#End Region

#Region "Delete"

    Private Sub btnDeleteArea_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteArea.Click
        Try
            Dim name As String = lstAreas.SelectedItem.Text
            GeneralController.DeleteArea(lstAreas.SelectedValue)
            DataBindAreas(lstLocations.SelectedValue)
            lblLocationArea.Text = "Successfully deleted '" + name + "'"
        Catch ex As Exception
            lblLocationArea.Text = ex.Message
            lblLocationArea.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub btnDeleteLocation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteLocation.Click
        ' deletes selected location
        Try
            If lstLocations.SelectedIndex > -1 Then
                GeneralController.DeleteLocation(lstLocations.SelectedValue)
                lblLocation.Text = "Successfully deleted Location"
                DatabindControls()
            End If
        Catch ex As Exception
            lblLocation.Text = ex.ToString
        End Try
    End Sub

#End Region

    Private Sub lstLocations_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstLocations.SelectedIndexChanged
        ' databind the areas
        DataBindAreas(lstLocations.SelectedValue)
        EditLocationLink(lstLocations.SelectedValue)
    End Sub

    Private Sub EditLocationLink(ByVal id As Integer)
        lnkEditLocation.NavigateUrl = "~/classified/ModalDialog/Edit_Location.aspx?id=" + id.ToString
    End Sub

    Private Sub EditAreaLink(ByVal id As Integer)
        lnkEditArea.NavigateUrl = "~/classified/ModalDialog/Edit_LocationArea.aspx?id=" + id.ToString
    End Sub

    Private Sub lstAreas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstAreas.SelectedIndexChanged
        EditAreaLink(lstAreas.SelectedValue)
    End Sub

    Private Sub lnkRefreshAreas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshAreas.Click
        DataBindAreas(lstLocations.SelectedValue)
    End Sub

    Private Sub lnkRefreshLocations_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshLocations.Click
        DatabindControls()
    End Sub

End Class