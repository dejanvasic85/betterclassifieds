Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.Controller

Partial Public Class UserList
    Inherits System.Web.UI.Page

    Private type As String      ' accepted values: current/scheduled/expired
    Private userId As String    ' obtained from Membership object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        type = Request.QueryString("type")
        userId = Membership.GetUser().UserName

        If Not Page.IsPostBack Then
            If Not type Is Nothing Then
                Select Case type.ToLower
                    Case "current"
                        DatabindCurrentAds()
                    Case "scheduled"
                        DatabindScheduledList()
                        If Request.QueryString("edited") = "true" Then
                            lblEditInformation.Text = "Your Ad was successfully updated."
                        End If
                    Case "expired"
                        DatabindExpiredList()
                End Select
            End If
        End If
    End Sub

    Private Sub grdCurrentList_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdCurrentList.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim link As HyperLink = e.Row.FindControl("lnkStatus")
            If link IsNot Nothing Then

                If e.Row.DataItem.Status = "Cancelled" Then
                    ' encode the booking reference because it's a string
                    Dim bookReference As String = Server.UrlEncode(e.Row.DataItem.BookReference)

                    Dim url As String = String.Format("~/MemberAccount/EditOnlineAd.aspx?des={0}&ref={1}&act=resub", _
                                                      e.Row.DataItem.AdDesignId, bookReference)

                    link.NavigateUrl = url
                    link.ToolTip = "Click here to edit and re-submit the ad."
                End If
                link.Text = e.Row.DataItem.Status
            End If
        End If
    End Sub

    Private Sub grdCurrentLineAds_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdCurrentLineAds.RowCommand
        If e.CommandName.ToLower = "view" Then
            Dim btn As LinkButton = TryCast(e.CommandSource, LinkButton)
            Dim row = btn.Parent
            Dim extender As AjaxControlToolkit.ModalPopupExtender = TryCast(row.FindControl("extLinePopup"), AjaxControlToolkit.ModalPopupExtender)
            If extender IsNot Nothing Then

                ' show the Line Ad Preview here.
                Dim ucxLine As LineAdPreview = TryCast(row.FindControl("ucxLinePreview"), LineAdPreview)

                If ucxLine IsNot Nothing And e.CommandArgument > 0 Then
                    Dim lineAd As DataModel.LineAd = AdController.GetLineAd(e.CommandArgument)
                    Dim graphics As List(Of DataModel.AdGraphic) = AdController.GetAdGraphics(e.CommandArgument)
                    Dim image As String = String.Empty
                    If graphics.Count > 0 Then
                        image = graphics(0).DocumentID
                    End If
                    If lineAd IsNot Nothing Then
                        ucxLine.BindLineAd(lineAd, image)
                    End If
                End If
                extender.Show()
            End If
        End If
    End Sub

    Private Sub grdCurrentList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCurrentList.SelectedIndexChanged, grdScheduledList.SelectedIndexChanged, grdCurrentLineAds.SelectedIndexChanged

        Dim grd As GridView = DirectCast(sender, GridView)
        Dim row As GridViewRow = grd.SelectedRow

        If row Is Nothing Then
            Return
        End If

        Dim hdnControl As HiddenField = row.FindControl("hdnBookingId")

        ' set the required properties
        BookingReference = row.Cells(0).Text

        Dim id As Integer = hdnControl.Value
        If id > 0 Then
            SelectedId = id
        End If

        Dim extender As AjaxControlToolkit.ModalPopupExtender = TryCast(row.FindControl("extProject"), AjaxControlToolkit.ModalPopupExtender)

        If extender IsNot Nothing Then
            extender.Show()
        End If
    End Sub

    Private Sub DatabindExpiredList()
        ' get the number of days user wants to go into history
        Try
            lblHeader.Text = "Expired Designs"

            ' set the current limit to 3 months
            Dim endDate As DateTime = DateTime.Today.AddMonths(-3)

            'grdExpiredAds.DataSource = AdController.GetUserListExpiredDesigns(userId, BookingStatus.BOOKED, endDate)
            'grdExpiredAds.DataBind()

            pnlExpiredList.Visible = True
            Me.Title = "Expired Designs"
        Catch
            lblEditInformation.Text = "An error occurred while processing your request."
            lblEditInformation.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub DatabindCurrentAds()
        Try
            lblHeader.Text = "Current Running Ads"

            'grdCurrentList.DataSource = AdController.GetRunningAdsByUser(userId, SystemAdType.ONLINE)
            'grdCurrentList.DataBind()

            'grdCurrentLineAds.DataSource = AdController.GetRunningAdsByUser(userId, SystemAdType.LINE)
            'grdCurrentLineAds.DataBind()

            Me.Title = "Current Running Ads"
            pnlCurrentList.Visible = True

        Catch ex As Exception
            lblEditInformation.Text = "An error occurred while processing your request."
            lblEditInformation.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Sub DatabindScheduledList()
        Try
            lblHeader.Text = "Scheduled Bookings"

            pnlSchedule.Visible = True
            'grdScheduledList.DataSource = AdController.GetScheduledBookings(userId)
            'grdScheduledList.DataBind()

            Me.Title = "Scheduled Bookings"
            grdScheduledList.Visible = True
        Catch
            lblEditInformation.Text = "An error occurred while processing your request."
            lblEditInformation.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Protected Sub CancelBooking(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If SelectedId > 0 Then
                If BookingController.CancelExistingBooking(SelectedId) Then
                    lblEditInformation.Text = "Booking has been cancelled for booking reference " + BookingReference

                    ' perform the databind again
                    Select Case type.ToLower
                        Case "scheduled"
                            DatabindScheduledList()
                        Case "current"
                            DatabindCurrentAds()
                        Case "expired"
                            DatabindExpiredList()
                    End Select
                End If
            End If
        Catch ex As Exception
            lblEditInformation.Text = "An error occurred while processing your request."
            lblEditInformation.ForeColor = Drawing.Color.Red
        End Try
    End Sub

    Private Property SelectedId() As Integer
        Get
            Return ViewState("bookingId")
        End Get
        Set(ByVal value As Integer)
            ViewState("bookingId") = value
        End Set
    End Property

    Private Property BookingReference() As String
        Get
            Return ViewState("bookingReference")
        End Get
        Set(ByVal value As String)
            ViewState("bookingReference") = value
        End Set
    End Property

End Class