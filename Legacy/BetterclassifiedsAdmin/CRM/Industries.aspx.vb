Imports BetterclassifiedsCore.CRM
Imports BetterclassifiedAdmin.Configuration

Partial Public Class Industries
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            DataBindIndustres()
        End If

        ' clear any text and reset to red color
        lblCategory.Text = String.Empty
        lblCategory.ForeColor = Drawing.Color.Red
        lblIndustry.Text = String.Empty
        lblIndustry.ForeColor = Drawing.Color.Red
    End Sub

    Private Sub lstIndustry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstIndustry.SelectedIndexChanged
        Try
            DataBindCategories(lstIndustry.SelectedValue)
            EditIndustryLinks(lstIndustry.SelectedValue)
        Catch ex As Exception
            lblIndustry.Text = ex.ToString
        End Try
    End Sub

    Private Sub lstCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstCategory.SelectedIndexChanged
        Try
            EditCategoryLinks(lstCategory.SelectedValue)
        Catch ex As Exception
            lblCategory.Text = ex.ToString
        End Try
    End Sub

#Region "Databinding"

    Public Sub DataBindIndustres()
        Try
            Using db As New BetterclassifiedsCore.CRM.CrmController(ConfigManager.DBConnection)
                Dim list = db.GetIndustries()
                lstIndustry.DataSource = list
                lstIndustry.DataBind()

                If lstIndustry.Items.Count > 0 Then
                    lstIndustry.SelectedIndex = 0
                    DataBindCategories(lstIndustry.Items(0).Value)
                    EditIndustryLinks(lstIndustry.Items(0).Value)
                End If
            End Using
        Catch ex As Exception
            lblIndustry.Text = ex.ToString
        End Try
    End Sub

    Public Sub DataBindCategories(ByVal industryId As Integer)
        Try
            Using db As New BetterclassifiedsCore.CRM.CrmController(ConfigManager.DBConnection)
                lstCategory.DataSource = db.GetBusinessCategoryByIndustry(industryId)
                lstCategory.DataBind()
                If lstCategory.Items.Count > 0 Then
                    lstCategory.SelectedIndex = 0
                    EditCategoryLinks(lstCategory.Items(0).Value)
                End If
            End Using
        Catch ex As Exception
            lblCategory.Text = ex.ToString
        End Try
    End Sub

    Private Sub EditIndustryLinks(ByVal industryId As Integer)
        lnkEditIndustry.NavigateUrl = "~/crm/ModalDialog/Edit_Industry.aspx?id=" + industryId.ToString
    End Sub

    Private Sub EditCategoryLinks(ByVal id As Integer)
        Me.lnkEditCategory.NavigateUrl = "~/crm/ModalDialog/Edit_BusinessCategory.aspx?id=" + id.ToString
    End Sub

    Private Sub lnkRefreshIndustry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshIndustry.Click
        DataBindIndustres()
    End Sub

    Private Sub lnkRefreshCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRefreshCategory.Click
        DataBindCategories(lstIndustry.SelectedValue)
    End Sub

#End Region

#Region " Adding "

    Private Sub btnAddIndustry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddIndustry.Click
        Try
            Using db As New BetterclassifiedsCore.CRM.CrmController(ConfigManager.DBConnection)
                If db.IndustryExists(txtIndustry.Text) Then
                    lblIndustry.Text = "Industry already exists with the name '" + txtIndustry.Text + "'."
                Else
                    ' add the new record into the database
                    If db.CreateIndustry(txtIndustry.Text) > 0 Then
                        lblIndustry.Text = "Successfully created Industry '" + txtIndustry.Text + "'."
                        lblIndustry.ForeColor = Drawing.Color.Green
                        DataBindIndustres()
                    End If
                End If
            End Using
        Catch ex As Exception
            lblIndustry.Text = ex.ToString
        End Try
    End Sub

    Private Sub btnAddCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCategory.Click
        Try
            Using db As New BetterclassifiedsCore.CRM.CrmController(ConfigManager.DBConnection)
                If db.BusinessCategoryExists(txtCategory.Text, lstIndustry.SelectedValue) Then
                    lblCategory.Text = "Category already exists with the name '" + txtCategory.Text + "'."
                Else
                    If db.CreateBusinessCategory(txtCategory.Text, lstIndustry.SelectedValue) Then
                        lblCategory.Text = "Successfully created category '" + txtCategory.Text + "'."
                        lblCategory.ForeColor = Drawing.Color.Green
                        DataBindCategories(lstIndustry.SelectedValue)
                    End If
                End If
            End Using
        Catch ex As Exception
            lblCategory.Text = ex.ToString
        End Try
    End Sub

#End Region

#Region "Deleting"

    Private Sub btnDeleteIndustry_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteIndustry.Click
        Try
            Using db As New BetterclassifiedsCore.CRM.CrmController(ConfigManager.DBConnection)
                Dim str As String = lstIndustry.SelectedItem.Text
                db.DeleteIndustry(lstIndustry.SelectedValue)
                lblIndustry.Text = "Successfully deleted industry '" + str + "'."
                lblIndustry.ForeColor = Drawing.Color.Green
                DataBindIndustres()
            End Using
        Catch ex As Exception
            lblIndustry.Text = "Unable to delete industry due to conflict. <BR>Details:<br>" + ex.Message
        End Try
    End Sub

    Private Sub btnDeleteCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteCategory.Click
        Try
            Using db As New BetterclassifiedsCore.CRM.CrmController(ConfigManager.DBConnection)
                Dim str As String = lstCategory.SelectedItem.Text
                db.DeleteBusinessCategory(lstCategory.SelectedValue)
                lblCategory.Text = "Successfully deleted category '" + str + "'."
                lblCategory.ForeColor = Drawing.Color.Green
                DataBindCategories(lstIndustry.SelectedValue)
            End Using
        Catch ex As Exception
            lblCategory.Text = "Unable to delete industry due to conflict. <BR>Details:<br>" + ex.Message
        End Try
    End Sub

#End Region

End Class