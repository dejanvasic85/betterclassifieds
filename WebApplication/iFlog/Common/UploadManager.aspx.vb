Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.Dsl
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.DSL.UIController.ViewObjects

Partial Public Class UploadManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SetupOnlineUpload()
            SetupPrintUpload()

            'If UploadParameter.IsOnlineAdUpload = False And UploadParameter.IsPrintAdUpload = False Then
            '    lblUserMessage.Text = "Uploading images is not available for the package selected."
            'End If

        End If
    End Sub

    Private Sub SetupOnlineUpload()
        ' Toggle visibility for the upload online panel
        pnlOnline.Visible = UploadParameter.IsOnlineAdUpload

        If UploadParameter.IsOnlineAdUpload Then

            ' Fetch the data we need to work with
            Dim onlineAd = AdController.GetOnlineAd(UploadParameter.AdDesignId)
            Dim graphics = AdController.GetAdGraphicDocuments(UploadParameter.AdDesignId)

            paramountFileUpload.DocumentCategory = DslDocumentCategoryTypeView.OnlineAd
            paramountFileUpload.ReferenceData = UploadParameter.BookingReference
            paramountFileUpload.ImageList = graphics
            paramountFileUpload.MaxFiles = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, Utilities.Constants.CONST_KEY_Maximum_Image_Files)

        End If
    End Sub

    Private Sub SetupPrintUpload()
        pnlPrint.Visible = UploadParameter.IsPrintAdUpload
        If UploadParameter.IsPrintAdUpload Then
            Dim documentId = String.Empty
            Dim imageList = AdController.GetAdGraphicDocuments(UploadParameter.AdDesignId)
            If imageList.Count > 0 Then
                ' We only use the first document
                documentId = imageList(0)
            End If
            pnlPrint.Visible = True
            paramountWebImageMaker.ImageHeight = Convert.ToInt32(BetterclassifiedSetting.LineAdImageHeight * BetterclassifiedSetting.LineAdImageDPI)
            paramountWebImageMaker.ImageWidth = Convert.ToInt32(BetterclassifiedSetting.LineAdImageWidth * BetterclassifiedSetting.LineAdImageDPI)
            paramountWebImageMaker.ReferenceData = UploadParameter.BookingReference
            paramountWebImageMaker.ImageResolution = BetterclassifiedSetting.LineAdImageDPI
            paramountWebImageMaker.DocumentID = documentId
        End If
    End Sub

    Private Sub paramountFileUpload_RemoveDocument(ByVal sender As Object, ByVal e As SelectedDocumentArgs) Handles paramountFileUpload.RemoveDocument
        AdController.DeleteAdGraphic(UploadParameter.AdDesignId, e.DocumentID)
    End Sub

    Private Sub paramountFileUpload_UploadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles paramountFileUpload.UploadComplete
        ' Call the controller to save all the images into the DB
        AdController.CreateAdGraphics(UploadParameter.AdDesignId, paramountFileUpload.ImageList)
    End Sub

    Private Sub paramountWebImageMaker_RemoveComplete(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountWebImageMaker.RemoveComplete
        AdController.DeleteAdGraphic(UploadParameter.AdDesignId, e.DocumentID)
    End Sub

    Private Sub paramountWebImageMaker_UploadComplete(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountWebImageMaker.UploadComplete
        ' Set up parameter to save ad graphic
        Dim documents As New List(Of String)
        documents.Add(e.DocumentID)
        AdController.CreateAdGraphics(UploadParameter.AdDesignId, documents)
    End Sub

End Class