Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.WebAdvertising
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Partial Public Class AdSpaceConfiguration
    Inherits System.Web.UI.UserControl

#Region "Properties"
    Public Property SettingId() As Integer
        Get
            If (ViewState("settingId") IsNot Nothing) Then
                Return ViewState("settingId")
            Else : Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState("settingId") = value
        End Set
    End Property

#End Region

    Private _adLimit As Integer

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Using ct As New AdSpaceController
            Dim adSpaces = ct.GetAdSpaces(Me.SettingId)
            grdAds.DataSource = adSpaces
            grdAds.DataBind()
        End Using
    End Sub

    Public Sub DataBindControls(ByVal setting As Integer)
        ' set the private property
        Me.SettingId = setting
        ' retrieve the data
        Using Controller As New AdSpaceController
            Dim settingItem As DataModel.WebAdSpaceSetting = Controller.GetAdSpaceSettingById(SettingId)
        End Using
    End Sub

    Private Sub grdAds_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdAds.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.DataItem.SpaceType = AdSpaceType.Image Then
                Dim documentId As String = e.Row.DataItem.ImageUrl
                Dim query As New DslQueryParam(Request.QueryString)
                query.DocumentId = documentId
                query.Height = BetterclassifiedSetting.DslThumbHeight
                query.Width = BetterclassifiedSetting.DslThumbWidth
                query.Resolution = BetterclassifiedSetting.DslDefaultResolution
                query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                Dim img As New Image
                img.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
                e.Row.Cells(2).Controls.Add(img)
            ElseIf e.Row.DataItem.SpaceType = AdSpaceType.Text Then
                e.Row.Cells(2).Text = e.Row.DataItem.DisplayText
            End If
        End If
    End Sub
End Class