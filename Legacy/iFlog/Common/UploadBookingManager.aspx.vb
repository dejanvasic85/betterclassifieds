Imports BetterclassifiedsCore.ParameterAccess
Imports Paramount.Betterclassified.Utilities.Configuration
Imports BetterclassifiedsCore

Partial Public Class UploadBookingManager
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            SetupOnlineUpload()
            SetupPrintUpload()

            If UploadParameter.IsOnlineAdUpload = False And UploadParameter.IsPrintAdUpload = False Then
                lblUserMessage.Text = "Uploading images is not available for the package selected."
            End If

        End If
    End Sub

    Private Sub SetupOnlineUpload()
        ' Toggle visibility for the upload online panel
        pnlOnline.Visible = UploadParameter.IsOnlineAdUpload
        If UploadParameter.IsOnlineAdUpload Then
            ' Set the required properties on the controls
            paramountFileUpload.ReferenceData = UploadParameter.BookingReference
            paramountFileUpload.ImageList = GetImageGraphicsFromBookingSession()
            'paramountFileUpload.MaxFiles = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, Utilities.Constants.CONST_KEY_Maximum_Image_Files)
            paramountFileUpload.MaxFiles = AppKeyReader(Of String).ReadFromStore("MaxOnlineImages")

        End If
    End Sub

    Private Sub SetupPrintUpload()
        pnlPrint.Visible = UploadParameter.IsPrintAdUpload
        If UploadParameter.IsPrintAdUpload Then
            paramountWebImageMaker.ImageHeight = Convert.ToInt32(BetterclassifiedSetting.LineAdImageHeight * BetterclassifiedSetting.LineAdImageDPI)
            paramountWebImageMaker.ImageWidth = Convert.ToInt32(BetterclassifiedSetting.LineAdImageWidth * BetterclassifiedSetting.LineAdImageDPI)
            paramountWebImageMaker.ReferenceData = UploadParameter.BookingReference
            paramountWebImageMaker.ImageResolution = BetterclassifiedSetting.LineAdImageDPI
            paramountWebImageMaker.DocumentID = GetPrintGraphicFromBookingSession()

            ' Display the price
            Dim bookingCart = BetterClassified.UIController.Booking.BookCartController.GetCurrentBookCart()
            Dim isAveragePrice = Paramount.Betterclassified.Utilities.Configuration.BetterclassifiedSetting.IsAveragePriceUsedForLineAd
            If isAveragePrice Then
                lblPrice.Text = String.Format("{0:C} average per publication", bookingCart.BookingOrderPrice.GetLineAdMainPhotoPrice())
            Else
                lblPrice.Text = String.Format("{0:C} per publication", bookingCart.BookingOrderPrice.GetLineAdMainPhotoPrice())
            End If

        End If
    End Sub

    Private Sub paramountFileUpload_RemoveDocument(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountFileUpload.RemoveDocument
        RemoveImageFromBookingSession(e.DocumentID)
    End Sub

    Private Sub paramountFileUpload_UploadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles paramountFileUpload.UploadComplete
        SetImageListToBookingSession(paramountFileUpload.ImageList)
    End Sub

    Private Sub paramountWebImageMaker_RemoveComplete(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountWebImageMaker.RemoveComplete
        RemovePrintImageFromBookingSession(e.DocumentID)
    End Sub

    Private Sub paramountWebImageMaker_UploadComplete(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountWebImageMaker.UploadComplete
        SetPrintImageToBookingSession(e.DocumentID)
    End Sub

    Private Function GetImageGraphicsFromBookingSession() As List(Of String)
        Dim list As New List(Of String)
        Select Case BookingController.BookingType
            Case Booking.BookingAction.NormalBooking
                list = BookingController.AdBookCart.ImageList
            Case Booking.BookingAction.BundledBooking
                Dim controller As New BundleBooking.BundleController
                list = controller.GetOnlineGraphicsIdList
        End Select
        Return list
    End Function

    Private Function GetPrintGraphicFromBookingSession() As String
        Dim docId As String = String.Empty
        Select Case BookingController.BookingType
            Case Booking.BookingAction.NormalBooking
                If BookingController.AdBookCart.LineAdGraphic IsNot Nothing Then
                    docId = BookingController.AdBookCart.LineAdGraphic.DocumentID
                End If
            Case Booking.BookingAction.BundledBooking
                If BundleBooking.BundleController.BundleCart.LineAdGraphic IsNot Nothing Then
                    docId = BundleBooking.BundleController.BundleCart.LineAdGraphic.DocumentID
                End If
        End Select
        Return docId
    End Function

    Private Sub RemoveImageFromBookingSession(ByVal imageGuid As String)
        Select Case BookingController.BookingType
            Case Booking.BookingAction.NormalBooking
                BookingController.AdBookCart.ImageList.Remove(imageGuid)
            Case Booking.BookingAction.BundledBooking
                Dim controller As New BundleBooking.BundleController
                controller.RemoveOnlineAdGraphic(imageGuid)
        End Select
    End Sub

    Private Sub RemovePrintImageFromBookingSession(ByVal docId As String)
        Select Case BookingController.BookingType
            Case Booking.BookingAction.NormalBooking
                BookingController.AdBookCart.LineAdGraphic = Nothing
            Case Booking.BookingAction.BundledBooking
                BundleBooking.BundleController.BundleCart.LineAdGraphic = Nothing
                BundleBooking.BundleController.BundleCart.LineAdIsMainPhoto = False
        End Select
    End Sub

    Private Sub SetImageListToBookingSession(ByVal list As List(Of String))
        Select Case BookingController.BookingType
            Case Booking.BookingAction.NormalBooking
                BookingController.AdBookCart.ImageList = list
            Case Booking.BookingAction.BundledBooking
                Dim controller As New BundleBooking.BundleController
                controller.SetOnlineAdGraphics(list)
        End Select
    End Sub

    Private Sub SetPrintImageToBookingSession(ByVal docId As String)
        Select Case BookingController.BookingType
            Case Booking.BookingAction.NormalBooking
                BookingController.SetLineAdGraphic(docId)
            Case Booking.BookingAction.BundledBooking
                Dim controller As New BundleBooking.BundleController
                controller.SetLineAdGraphic(docId)
        End Select
    End Sub

End Class