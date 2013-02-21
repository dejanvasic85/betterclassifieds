Imports BetterclassifiedsCore
Imports Paramount.Modules.Logging.UIController

Partial Public Class AdTypeList
    Inherits System.Web.UI.UserControl

    Public Event AdTypeSelected As AdTypeSelectedHandler

    Public Delegate Sub AdTypeSelectedHandler(ByVal adTypeId As Integer)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim typeList As List(Of DataModel.AdType) = AdController.GetAllAdTypes()

            '' **************************
            '' **************************
            ' Add the Bundled Print and Online Option (HARD CODED) - DV

            Dim typeLineAndOnline As New DataModel.AdType With {.AdTypeId = 0, _
                                                                .Title = "Bundle Print and Online Ads", _
                                                                .ImageUrl = "example_print_online_hori.jpg"}
            ' make this also the first item in the list (by default)
            typeList.Insert(0, typeLineAndOnline)
            '' **************************
            '' **************************

            grdAdList.DataSource = typeList
            grdAdList.DataBind()

        End If

        ' check to see if there is an ad Type already selected.
        Dim adTypeId = ViewState("adTypeId")

        If (adTypeId <> Nothing) Then
            ' load the selected id
            For Each r As GridViewRow In grdAdList.Rows
                Dim hdnValue As HiddenField = r.FindControl("hdnId")
                If (hdnValue.Value = adTypeId) Then
                    r.RowState = DataControlRowState.Selected
                    ' get the radio button and select it
                    Dim rdo As RadioButton = TryCast(r.FindControl("rdoTypeSelect"), RadioButton)

                    If rdo IsNot Nothing Then
                        rdo.Checked = True
                    End If

                    Exit For
                End If
            Next
        End If
    End Sub

    Private Sub grdAdList_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdAdList.RowCommand
        If (e.CommandName = "Select") Then
            RaiseEvent AdTypeSelected(e.CommandArgument)
        End If
    End Sub

    Protected Sub rdoTypeSelect(ByVal sender As Object, ByVal e As System.EventArgs)

        ' selected radio button
        Dim rdoSelected As RadioButton = TryCast(sender, RadioButton) ' get the radio button that was selected

        ' selected gridview Row
        Dim selectedRow As GridViewRow = TryCast(rdoSelected.NamingContainer, GridViewRow)

        ' selected AdTypeId
        Dim adTypeId As Integer = TryCast(selectedRow.FindControl("hdnId"), HiddenField).Value

        ' loop through each other radio button and unselect it
        For Each row As GridViewRow In grdAdList.Rows
            If row IsNot selectedRow Then
                Dim rdoUnselect As RadioButton = TryCast(row.FindControl("rdoTypeSelect"), RadioButton)
                ' perform unselect
                rdoUnselect.Checked = False
            End If
        Next

        grdAdList.SelectedIndex = selectedRow.RowIndex
        IsTypeSelected = True

        ' raise the event for the containing page
        RaiseEvent AdTypeSelected(adTypeId)

    End Sub

    Public Sub LoadCurrentData(ByVal adTypeId As Integer)
        ' store the ID in the viewstate so that on page load it will select it
        ViewState("adTypeId") = adTypeId
    End Sub

    Public Property Enabled() As Boolean
        Get
            Return grdAdList.Enabled
        End Get
        Set(ByVal value As Boolean)
            grdAdList.Enabled = value
        End Set
    End Property

    Public Property IsTypeSelected() As Boolean
        Get
            Return ViewState("isTypeSelected")
        End Get
        Set(ByVal value As Boolean)
            ViewState("isTypeSelected") = value
        End Set
    End Property

End Class