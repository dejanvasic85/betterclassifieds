Imports BetterclassifiedsCore

Imports BetterClassified.UI.WebPage

Partial Public Class Step4
    Inherits BaseBookingPage

    Private Const _selectableDates As String = "vsDates"
    Private Const _adTypeState As String = "vsAdType"
    Private _maxInsertSetting As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If BookingController.AdBookCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If

        If (AdController.TempRecordExist(BookingController.AdBookCart.BookReference)) Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If

        ' load variable maximum insertions from App Settings
        _maxInsertSetting = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ADBOOKING, Utilities.Constants.CONST_KEY_Maximum_Insertions)

        If Not Page.IsPostBack Then

            Dim adType As String = BookingController.AdBookCart.MainAdType().Code.Trim
            Dim action As String = Request.QueryString("action")

            ' perform data bind to show the deadlines
            listPublicationDeadlines.DataSource = PublicationController.PublicationDeadlines(BookingController.AdBookCart.PublicationList)
            listPublicationDeadlines.DataBind()

            If (adType = SystemAdType.LINE.ToString) Then

                ' get the available dates for each Publication
                Dim dates = PublicationController.PublicationEditions(BookingController.AdBookCart.PublicationList, _maxInsertSetting)

                ' check if we have editions set up in the database
                If dates.Count > 0 Then

                    ' store the selectable dates in the viewstate for the calendar to render
                    ViewState(_selectableDates) = dates


                    ' divide the number of Editions by the Number of Publications
                    Dim ratio As Integer = dates.Count / BookingController.AdBookCart.PublicationList.Count

                    ' need to work out the number of Insertions the user is able to select!
                    Dim items As Integer
                    If ratio < _maxInsertSetting Then
                        items = ratio
                    Else
                        items = _maxInsertSetting
                    End If

                    ' call method to databind the drop down box (select the first item)
                    DataBindInsertions(items, 0)

                Else
                    'EventLogManager.Log(New EventLog("Publication editions have not been set up for selected publications: " + BookingController.AdBookCart.PublicationList.ToString))
                    Response.Redirect(Utilities.Constants.CONST_ERROR_DEFAULT_URL + "?type=" + _
                                      Utilities.Constants.CONST_ERROR_SETTINGS)
                End If

                Me.AdType = SystemAdType.LINE

            ElseIf (adType = SystemAdType.ONLINE.ToString) Then
                Me.AdType = SystemAdType.ONLINE
                calStartDate.SelectedDate = Today.Date ' select todays date
                trEndDate.Visible = True
                EndDate = BookingController.GetEndDate(calStartDate.SelectedDate)
                lblEndDate.Text = EndDate.ToLongDateString
                lblStartDate.Text = Today.ToLongDateString
            End If

            ' check if the user is returning to this step to change something
            If action = "back" Then
                calStartDate.SelectedDate = BookingController.AdBookCart.StartDate

                If Me.AdType = SystemAdType.LINE Then
                    ' for line ads we should the insertions
                    ddlInserts.SelectedValue = BookingController.AdBookCart.Insertions

                ElseIf Me.AdType = SystemAdType.ONLINE Then
                    trEndDate.Visible = True
                    EndDate = BookingController.AdBookCart.EndDate
                    lblEndDate.Text = EndDate.ToLongDateString
                End If

            End If

            Me.DataBind()
        End If
    End Sub

    Private Sub calStartDate_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calStartDate.DayRender

        If AdType = SystemAdType.LINE Then
            ' obtain the list of dates stored in the viewstate.
            Dim list As List(Of Nullable(Of DateTime)) = ViewState(_selectableDates)

            ' check if this date is an edition,
            If Not (list.Contains(e.Day.Date)) Then
                ' don't allow to be a selectable date if not an edition
                e.Day.IsSelectable = False
            Else
                'e.Cell.BackColor = Drawing.Color.Wheat
                e.Cell.Style.Add("background-color", "#CCFFDD")
            End If
        ElseIf AdType = SystemAdType.ONLINE Then
            If e.Day.Date < Today.Date Then
                e.Day.IsSelectable = False
            End If
        End If


    End Sub

    Private Sub UpdateEditionCheck(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInserts.SelectedIndexChanged, calStartDate.SelectionChanged

        If (AdType = SystemAdType.LINE) Then

            If ddlInserts.SelectedIndex > 0 And calStartDate.SelectedDate >= Today Then
                ' get the dates selected by user

                Dim selectedDates = PublicationController.PublicationEditionList(BookingController.AdBookCart.PublicationList, ddlInserts.SelectedValue, calStartDate.SelectedDate)
                ucxEditionDates.BindPaperEditions(selectedDates)
                ' display the start date to the user
                lblLineStartDate.Text = calStartDate.SelectedDate.ToLongDateString
            Else
                ' remove the bindings from the control
                ucxEditionDates.RemoveBindings()
            End If

        ElseIf (AdType = SystemAdType.ONLINE) Then

            EndDate = BookingController.GetEndDate(calStartDate.SelectedDate)
            lblEndDate.Text = EndDate.ToLongDateString
            lblStartDate.Text = calStartDate.SelectedDate.ToLongDateString
        End If

    End Sub

    Private Sub ucxNavButtons_NextStep(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucxNavButtons.NextStep
        Dim errorList As New List(Of String)

        If (ValidatePage(errorList)) Then

            If AdType = SystemAdType.LINE Then
                ' get the details we need from our control in UI
                ' call method to store the scheduling data into the booking session
                Dim startDate = calStartDate.SelectedDate + DateTime.Today.TimeOfDay
                'BookingController.SetSchedulingDetails(calStartDate.SelectedDate, ddlInserts.SelectedValue, BookingController.AdBookCart.PublicationList)
                BookingController.SetBookEntriesAndPrice(ddlInserts.SelectedValue, calStartDate.SelectedDate, _
                                                         BookingController.AdBookCart.PublicationList, _
                                                         BookingController.AdBookCart.MainCategoryId)
            ElseIf AdType = SystemAdType.ONLINE Then
                ' for online ads, we sould already have the end date, so just pass it in
                'BookingController.SetSchedulingDetails(calStartDate.SelectedDate, EndDate)
                BookingController.SetBookEntriesAndPrice(1, calStartDate.SelectedDate, _
                                                         BookingController.AdBookCart.PublicationList, _
                                                         BookingController.AdBookCart.MainCategoryId)
            End If

            ' redirect to the 5th (Confirmation step)
            Response.Redirect("Step5.aspx")
        Else
            ucxPageErrors.ShowErrors(errorList)
        End If
    End Sub

    Private Function ValidatePage(ByRef errorList As List(Of String)) As Boolean

        ' we only validate the calendar date.
        If (calStartDate.SelectedDate < Today) Then
            errorList.Add("Start Date has not been selected.")
        End If

        If AdType = SystemAdType.LINE Then
            If (ddlInserts.SelectedIndex = 0) Then
                errorList.Add("Please specify the number of insertions.")
            End If
        End If

        Return errorList.Count = 0
    End Function

    Private Sub DataBindInsertions(ByVal maxInsertions As Integer, ByVal selectedValue As Integer)
        ' load the combo box that allows the number of insertions to be selected

        ddlInserts.Items.Clear()

        For i As Integer = 1 To maxInsertions
            Me.ddlInserts.Items.Add(i.ToString)
        Next

        Dim extraItem As New ListItem("-- Select --", 0)
        ddlInserts.Items.Insert(0, extraItem)

        Me.ddlInserts.SelectedValue = selectedValue
    End Sub

#Region "Properties"

    Public Property AdType() As SystemAdType
        Get
            Return ViewState(_adTypeState)
        End Get
        Set(ByVal value As SystemAdType)
            ViewState(_adTypeState) = value
        End Set
    End Property

    Private Property EndDate() As DateTime
        Get
            Return ViewState("endDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("endDate") = value
        End Set
    End Property
#End Region

    Private Sub ucxEditionDates_GridPageIndexChanged() Handles ucxEditionDates.GridPageIndexChanged
        Dim selectedDates = PublicationController.PublicationEditionList(BookingController.AdBookCart.PublicationList, ddlInserts.SelectedValue, calStartDate.SelectedDate)
        ucxEditionDates.BindPaperEditions(selectedDates)
    End Sub
End Class