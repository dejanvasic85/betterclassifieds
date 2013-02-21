Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess

Partial Public Class EditOnlineAd
    Inherits System.Web.UI.Page

    Private _adDesignId As Integer
    Private _adBookingId As Integer
    Private _bookingReference As String
    Private _action As String
    Private _userId As String
    Private _list As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _adDesignId = Request.QueryString("dsId")
        _adBookingId = Request.QueryString("bkId")
        _action = Request.QueryString("act")
        _userId = Membership.GetUser().UserName
        _list = Request.QueryString("list")
        lblUserMsg.Text = String.Empty

        If Not Page.IsPostBack Then
            If General.SecurityCheckUserAdBooking(_userId, _adBookingId, _adDesignId) Then

                General.ClearCurrentBookings()

                ' get the required details for the online ad
                Dim booking As DataModel.AdBooking = BookingController.GetAdBookingById(_adBookingId)
                Dim onlineAd As DataModel.OnlineAd = AdController.GetOnlineAd(_adDesignId)
                Dim images As List(Of String) = AdController.GetAdGraphicDocuments(_adDesignId)

                ' bind the online ad details.
                ucxOnlineAd.BookingReference = _bookingReference
                ucxOnlineAd.BindOnlineAd(onlineAd, images)

                ' Set up the online upload parameters
                UploadParameter.Clear()
                UploadParameter.AdDesignId = _adDesignId
                UploadParameter.IsOnlineAdUpload = True
                UploadParameter.BookingReference = booking.BookReference
                radWindowImages.OpenerElementID = lnkOnlineImages.ClientID

                ' Set up the Preview Window
                radWindowPreview.NavigateUrl = String.Format("../OnlineAds/Preview.aspx?viewType=db&id={0}", _adDesignId)
                radWindowPreview.OpenerElementID = lnkPreview.ClientID

                ' Set the iFlog ID for user to see
                lblAdDesignId.Text = _adDesignId.ToString
                Me.Title = String.Format("Edit iFlog {0}", _adDesignId)
            End If

        End If
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click

        If ValidatePage() Then
            With ucxOnlineAd
                ' Update the ad details - without images
                AdController.UpdateOnlineAd(_adDesignId, .Heading, .Description, .HtmlText, .Price, .LocationId, .LocationArea, .ContactName, .ContactType, .ContactValue)
                lblUserMsg.Text = "Online Ad Details have been updated successfully."
                pnlSuccess.Visible = True
            End With
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ' Redirect to the previous page
        Select Case _list
            Case "scheduled"
                Response.Redirect(PageUrl.MemberScheduledAds)
            Case "current"
                Response.Redirect(PageUrl.MemberCurrentAds)
        End Select
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