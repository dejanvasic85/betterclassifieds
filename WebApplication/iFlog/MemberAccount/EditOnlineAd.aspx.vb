Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified
Imports BetterClassified.Models
Imports BetterClassified.Repository
Imports Microsoft.Practices.Unity

Partial Public Class EditOnlineAd
    Inherits System.Web.UI.Page

    Private _adBookingId As Integer
    Private _bookingReference As String
    Private _action As String
    Private _userId As String
    Private _list As String
    Private _adRepository As IAdRepository

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        _adBookingId = Request.QueryString("bkId")
        _action = Request.QueryString("act")
        _userId = Membership.GetUser().UserName
        _list = Request.QueryString("list")
        _adRepository = Unity.DefaultContainer.Resolve(Of IAdRepository)()
        lblUserMsg.Text = String.Empty

        If Not Page.IsPostBack Then
            Dim booking = BookingController.GetAdBookingById(_adBookingId)
            Dim onlineAd = AdController.OnlineAdByBookingId(_adBookingId)

            If General.SecurityCheckUserAdBooking(_userId, _adBookingId, onlineAd.AdDesignId) Then

                General.ClearCurrentBookings()

                ' get the required details for the online ad
                Dim images As List(Of String) = AdController.GetAdGraphicDocuments(onlineAd.AdDesignId)

                ' bind the online ad details.
                ucxOnlineAd.BookingReference = _bookingReference
                ucxOnlineAd.BindOnlineAd(onlineAd, images)

                ' Bind the ad specific details
                If (onlineAd.OnlineAdTag.HasValue) Then
                    Dim adTypeControl = pnlAdDetails.FindControl(Of OnlineAdViewControl)("ucx" + onlineAd.OnlineAdTag)
                    ' todo - use a factory here to determine which ad to fetch
                    DirectCast(adTypeControl, Control).Visible = True
                    adTypeControl.DatabindAd(_adRepository.GetTutorAd(onlineAd.OnlineAdId))
                End If

                ' Set up the online upload parameters
                UploadParameter.Clear()
                UploadParameter.AdDesignId = onlineAd.AdDesignId
                UploadParameter.IsOnlineAdUpload = True
                UploadParameter.BookingReference = booking.BookReference
                radWindowImages.OpenerElementID = lnkOnlineImages.ClientID

                ' Set up the Preview Window
                radWindowPreview.NavigateUrl = String.Format("../OnlineAds/Preview.aspx?viewType=db&id={0}", onlineAd.AdDesignId)
                radWindowPreview.OpenerElementID = lnkPreview.ClientID

                ' Set the iFlog ID for user to see
                'lblAdDesignId.Text = _adBookingId.ToString
                Me.Title = String.Format("Edit Ad {0}", _adBookingId)

            End If

        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        If ValidatePage() Then
            With ucxOnlineAd
                ' Update the ad details - without images
                Dim onlineAd = AdController.OnlineAdByBookingId(_adBookingId)
                AdController.UpdateOnlineAd(onlineAd.AdDesignId, .Heading, .Description, .HtmlText, .Price, .LocationId, .LocationArea, .ContactName, .ContactType, .ContactValue)

                If (ucxTutors.Visible) Then
                    Dim tutorAdModel = ucxTutors.GetTutorAd()
                    _adRepository.UpdateTutor(tutorAdModel)
                End If

                lblUserMsg.Text = "Online Ad Details have been updated successfully."
                pnlSuccess.Visible = True
            End With
        End If
    End Sub

    Private Function ValidatePage() As Boolean
        Dim errorList As New List(Of String)

        ' Validate the online ad control
        If (ucxOnlineAd.Heading = String.Empty) Then
            errorList.Add("Please provide the heading for the Online Ad")
        End If

        If (ucxOnlineAd.Description = String.Empty) Then
            errorList.Add("Please provide the description for the Online Ad")
        End If
        ucxPageError.ShowErrors(errorList)
        pnlSuccess.Visible = False
        Return errorList.Count = 0 ' Returns true if there's no missing data
    End Function

End Class