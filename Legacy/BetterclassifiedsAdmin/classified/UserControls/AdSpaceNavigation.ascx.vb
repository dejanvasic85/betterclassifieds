Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.WebAdvertising

Partial Public Class AdSpaceNavigation
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' make new controller object on post backs - currently use the default connection string
        If Not Page.IsPostBack Then
            Using _adSpaceController As New AdSpaceController
                rptAdSettingLinks.DataSource = _adSpaceController.GetAdSpaceSettings()
                rptAdSettingLinks.DataBind()
            End Using
        End If

    End Sub

End Class