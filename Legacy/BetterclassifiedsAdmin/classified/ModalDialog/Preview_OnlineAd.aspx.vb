Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

Partial Public Class Preview_OnlineAd
    Inherits System.Web.UI.Page

    Private _onlineAdId As Integer
    Private _adDesignId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _onlineAdId = Request.QueryString("onlineAdId")
        _adDesignId = Request.QueryString("adDesignId")
        If Not Page.IsPostBack Then
            DatabindOnlineAdDetails()
        End If
    End Sub

    Private Sub DatabindOnlineAdDetails()
        dtlOnlineAd.DataSource = AdController.GetOnlineAdById(_onlineAdId)
        dtlOnlineAd.DataBind()

        Dim graphics As List(Of DataModel.AdGraphic) = AdController.GetAdGraphics(_adDesignId)
        tblImages.Visible = graphics.Count > 0
        For Each g In graphics
            Dim img As New Image
            Dim documentId As String = g.DocumentID
            Dim query As New DslQueryParam(Request.QueryString)
            query.DocumentId = documentId
            query.Height = BetterclassifiedSetting.DslThumbHeight
            query.Width = BetterclassifiedSetting.DslThumbWidth
            query.Resolution = BetterclassifiedSetting.DslDefaultResolution
            query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
            img.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)

            Dim newCell As New TableCell
            newCell.Controls.Add(img)
            rowImages.Cells.Add(newCell)
        Next

        ' set the status of the ad
        ddlStatus.SelectedValue = AdController.GetAdDesignById(_adDesignId).Status
    End Sub

    Private Sub btnUpdateStatus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateStatus.Click
        Try
            If AdController.UpdateOnlineAdStatus(_onlineAdId, Me.ddlStatus.SelectedValue) Then
                lblMessage.Text = "Updated status to " + ddlStatus.SelectedItem.Text + "<br> Please refresh the list of pending online ads to reflect the change."
                lblMessage.ForeColor = Drawing.Color.Green

                DatabindOnlineAdDetails()
            Else
                lblMessage.Text = "Error occurred when trying to change status. Contact administrator if problem persists."
            End If
        Catch ex As Exception
            lblMessage.Text = "Error occurred when changing status. Details: " + ex.Message
        End Try
    End Sub

End Class