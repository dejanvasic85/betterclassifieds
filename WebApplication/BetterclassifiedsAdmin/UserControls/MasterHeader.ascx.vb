Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.ApplicationBlock.Configuration

Partial Public Class MasterHeader
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim environment = ConfigSettingReader.GetConfigurationContext()
            If Not environment = "LIVE" Then
                lnkTitle.Text = String.Format("{0} - Version : {1}", lnkTitle.Text, BetterclassifiedSetting.Version)
            End If
        End If
    End Sub

End Class