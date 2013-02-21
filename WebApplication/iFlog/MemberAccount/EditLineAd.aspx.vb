Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess

Partial Public Class EditLineAd
    Inherits BetterclassifiedsCore.UI.BasePage

    Private _userId As String
    Private _adBookingId As Integer
    Private _adDesignId As Integer
    Private _listType As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Set up the required variables for the form
        _userId = Membership.GetUser().UserName
        _adBookingId = Request.QueryString("bkId")
        _listType = Request.QueryString("list")
        Dim lineAd = AdController.GetLineAdByBookingId(_adBookingId)
        AdController.GetLineAdGraphic(lineAd.AdDesignId)

        _adDesignId = lineAd.AdDesignId
        Dim isPrintAd As Boolean = True
        Dim isPrintAdEditEnforceLimits As Boolean = True

        If Not Page.IsPostBack Then
            ' Perform security check for naughty people trying to edit someone else's ad.
            If General.SecurityCheckUserAdBooking(_userId, _adBookingId, _adDesignId) Then
                General.ClearCurrentBookings()

                Dim booking = BookingController.GetAdBookingById(_adBookingId)

                ' Set up the image upload parameter
                UploadParameter.Clear()
                UploadParameter.AdDesignId = _adDesignId
                UploadParameter.IsPrintAdUpload = isPrintAd
                UploadParameter.BookingReference = booking.BookReference
                radWindowImages.OpenerElementID = lnkPrintImages.ClientID

                ' Set up the Preview Window
                radWindowPreview.NavigateUrl = String.Format("../LineAds/PreviewLineAd.aspx?viewType=db&id={0}", _adDesignId)
                radWindowPreview.OpenerElementID = lnkPreview.ClientID

                ' Toggle visibility on the Image Upload based on original line ad.
                lnkPrintImages.Visible = lineAd.UsePhoto

                ' Set the iflog id
                lblAdDesignId.Text = _adDesignId
                Me.Title = String.Format("Edit iFlog {0}", _adDesignId)
            End If
        End If

    End Sub

    Private Sub lineAdDetails_CancelEvent(ByVal sender As Object, ByVal e As System.EventArgs) Handles lineAdDetails.CancelEvent
        If String.Compare(_listType, "scheduled", True) = 0 Then
            Response.Redirect(PageUrl.MemberScheduledAds)
        ElseIf String.Compare(_listType, "current", True) = 0 Then
            Response.Redirect(PageUrl.MemberCurrentAds)
        End If

    End Sub
End Class