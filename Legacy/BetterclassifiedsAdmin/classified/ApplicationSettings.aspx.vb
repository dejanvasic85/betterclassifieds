Imports BetterclassifiedsCore
Imports BetterclassifiedAdmin.Configuration

Partial Public Class ApplicationSettings
    Inherits System.Web.UI.Page

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' bind the drop down controls
            ddlModules.DataSource = GeneralController.GetAppSettingModules()
            ddlModules.DataBind()

            ' bind key names
            If ddlModules.Items.Count > 0 Then
                ddlSetting.DataSource = GeneralController.GetAppSettingNames(ddlModules.Items(0).Value)
                ddlSetting.DataBind()
            End If
        End If

        lblSettingMsg.Text = ""
    End Sub

#End Region

#Region "Databinding"

    Private Sub ddlModules_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlModules.SelectedIndexChanged
        ddlSetting.DataSource = GeneralController.GetAppSettingNames(ddlModules.SelectedValue)
        ddlSetting.DataBind()
    End Sub

    Private Sub btnView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnView.Click
        ' we need to trigger the selecting event for the linqDataSource
        ' clear the current datasource and reattach for the details view
        dtlSetting.DataSourceID = linqSourceSetting.ID
    End Sub

    Private Sub linqSourceSetting_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqSourceSetting.Selecting
        ' execute our own select method
        e.Result = GeneralController.GetApplicationSetting(ddlModules.SelectedValue, ddlSetting.SelectedValue)
    End Sub

#End Region

#Region "Updating"

    Private Sub linqSourceSetting_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqSourceSetting.Updating
        Dim setting As DataModel.AppSetting = TryCast(e.NewObject, DataModel.AppSetting)
        ' send the setting to be updated
        Try
            ' validate the object before trying to update - these attempts would throw an exception
            If setting.DataType.Trim = "int" Then
                'try to cast to integer value
                Dim value As Integer = Convert.ToInt32(setting.SettingValue)
            ElseIf setting.DataType.Trim = "bit" Then
                Dim value As Boolean = Convert.ToBoolean(setting.SettingValue)
            ElseIf setting.DataType.Trim = "decimal" Then
                Dim value As Decimal = Convert.ToDecimal(setting.SettingValue)
            End If

            ' if we reach this code then validation is completed ok.
            If GeneralController.UpdateApplicationSetting(setting.Module, setting.AppKey, setting.SettingValue) Then
                ' print msg
                lblSettingMsg.Text = "Save Successful"
                lblSettingMsg.ForeColor = Drawing.Color.Green
            End If

        Catch fexc As FormatException
            lblSettingMsg.Text = "Save Failed: Please provide a number value."
        Catch ex As Exception
            lblSettingMsg.Text = "Save Failed: " + ex.Message
        Finally
            e.Cancel = True ' catch the original linq update since we do it ..
        End Try
    End Sub

#End Region

End Class