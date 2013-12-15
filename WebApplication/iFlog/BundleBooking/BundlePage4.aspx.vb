Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking

Imports BetterClassified.UI.WebPage

Partial Public Class BundlePage4
    Inherits BaseBookingPage

    Private _bundleController As BundleController
    Private ReadOnly _daysPriorPrint As Integer = -7
    Private ReadOnly _daysAfterPrint As Integer = 6

#Region "Page Load / Pre render"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' check if the bundle booking cart is expired
        If BundleController.BundleCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If

        'make sure ad has not been saved in temp booking
        If (AdController.TempRecordExist(BundleController.BundleCart.BookReference)) Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If

        ' initiate the global variables
        _bundleController = New BundleController()

        If Not Page.IsPostBack Then
            ' set the global properties - first time only (not on each postback so we call DB only once)
            Me.MaximumEditions = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ADBOOKING, Utilities.Constants.CONST_KEY_Maximum_Insertions)
            Me.ScheduleOnlineWithPrint = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, Utilities.Constants.CONST_KEY_Online_BundleWithPrint)
            ' get the selectable dates for all publications and store them into the session (for first load only)
            Me.PublicationEditions = Me.GetPublicationEditions(BundleController.BundleCart.PublicationList, Me.MaximumEditions)

            ' Databind the upcoming editions combo
            Me.ddlUpcomingEditions.DataSource = Me.PublicationEditions.Select(Function(d) New With {.EditionDate = d.Value, .EditionDateFormatted = d.GetValueOrDefault().ToString("dd-MMM-yyyy")})
            Me.ddlUpcomingEditions.DataBind()

            ' databind the Combo Box for Insertion selection
            Me.DataBindEditionCombo(Me.MaximumEditions, Me.PublicationEditions, BundleController.BundleCart.PublicationList)
            ' databind the publication deadlines
            Me.DataBindPublicationDeadlines(BundleController.BundleCart.PublicationList)

            ' call the method to bind any existing data in the session
            LoadCurrentSessionData()
        End If

    End Sub

#End Region

#Region "DataBinding"

    Private Sub DataBindEditionCombo(ByVal maxEditions As Integer, ByVal editionDates As List(Of Nullable(Of Date)), ByVal publicationList As List(Of DataModel.Publication))
        ' we can only show the number of insertions based on the publications selected and editions set up
        Dim editionRatio As Integer = editionDates.Count / publicationList.Count

        ' declare amount of editions to show and first set to max editions
        Dim listItemsCount As Integer = maxEditions

        ' however, if edition ratio is less than max editions, then we use edition ratio instead
        If editionRatio < maxEditions Then
            listItemsCount = editionRatio
        End If

        ' clear the items from the current UI list
        Me.ddlInserts.Items.Clear()

        ' insert the items into the list
        For i As Integer = 1 To listItemsCount
            ddlInserts.Items.Add(i)
        Next
        ' insert an extra item to force validation
        ddlInserts.Items.Insert(0, New ListItem("-- Select --", 0))
    End Sub

    'Private Sub calStartDate_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calStartDate.DayRender

    '    ' this event will fire for each day that will appear in the calendar
    '    ' we can get the date for each day from the e parameter

    '    If (Me.PublicationEditions.Contains(e.Day.Date)) Then
    '        ' if this day is an edition we allow to be selectable
    '        e.Cell.Style.Add("background-color", "#CCFFDD")
    '    Else
    '        ' otherwise we set the background colour
    '        e.Day.IsSelectable = False
    '    End If
    'End Sub

    Private Sub DataBindPublicationDeadlines(ByVal publicationList As List(Of DataModel.Publication))
        ' databind the publications but ensure that we pass in the publication id's only
        Dim pubIdList As List(Of Integer) = publicationList.Select(Function(i) i.PublicationId).ToList
        ' call the publication controller method to do the work
        listPublicationDeadlines.DataSource = PublicationController.PublicationDeadlines(pubIdList)
        listPublicationDeadlines.DataBind()
    End Sub

    Private Sub LoadCurrentSessionData()
        ' ensure that the bundle cart is not null
        If BundleController.BundleCart IsNot Nothing Then
            ' ensure their existing selections are valid
            Dim minDate As DateTime = DateTime.Today.AddDays(_daysPriorPrint)
            If BundleController.BundleCart.StartDate >= minDate And BundleController.BundleCart.Insertions > 0 Then
                ' simply call the method that will set these details
                UpdateEditionDetails(BundleController.BundleCart.FirstEdition, BundleController.BundleCart.StartDate, BundleController.BundleCart.Insertions)
                ' set the UI's
                SetEditionValue(BundleController.BundleCart.Insertions)
                SetStartDateValue(BundleController.BundleCart.FirstEdition)
            End If
        End If
    End Sub

#End Region

#Region "Properties"

    Private Property PublicationEditions() As List(Of Nullable(Of Date))
        Get
            Return Session("editionDates")
        End Get
        Set(ByVal value As List(Of Nullable(Of Date)))
            Session("editionDates") = value ' store into session (may be large value)
        End Set
    End Property

    Private Property MaximumEditions() As Integer
        Get
            Return ViewState("maximumEditions")
        End Get
        Set(ByVal value As Integer)
            ViewState("maximumEditions") = value ' store into viewstate (small value)
        End Set
    End Property

    Private Property ScheduleOnlineWithPrint() As Boolean
        Get
            Return ViewState("bundleOnline")
        End Get
        Set(ByVal value As Boolean)
            ViewState("bundleOnline") = value ' store into viewstate (small value)
        End Set
    End Property

#End Region

#Region "Helper Methods"

    Private Function GetPublicationEditions(ByVal publicationList As List(Of DataModel.Publication), ByVal maxEditions As Integer) As List(Of Nullable(Of Date))
        ' create the object collection we need to use to retrieve a list of editions
        Dim publicationIdList As List(Of Integer) = publicationList.Select(Function(i) i.PublicationId).ToList

        ' make a call to Publication Controller to do the work for us here
        Dim dates = PublicationController.PublicationEditions(publicationIdList, maxEditions)

        ' simply return the dates
        Return dates
    End Function

    Private Sub SetEditionValue(ByVal editionCount As Integer)
        ' sets the dropdown box value for the number of editions
        ddlInserts.SelectedValue = editionCount
    End Sub

    Private Sub SetStartDateValue(ByVal startDate As DateTime)
        ' sets the UI calendar to a specific date
        ' calStartDate.SelectedDate = startDate
        ddlUpcomingEditions.SelectedValue = startDate
    End Sub

    Private Sub UpdateEditionDetails(ByVal selectedDate As DateTime, ByVal onlineStartDate As DateTime, ByVal editionCount As Integer)
        ' handles the procedure when a user/session sets the start date for the booking

        Dim minDate As DateTime = Today.Date.AddDays(_daysPriorPrint)
        ' check if the user selected more than one edition/insertions and today or later for start date
        If editionCount > 0 And onlineStartDate >= minDate Then
            ' create the list of editions used for binding and calculation
            Dim pubEditions As List(Of Booking.EditionList) = _bundleController.GetPublicationEditions(BundleController.BundleCart.PublicationList, editionCount, selectedDate)
            ucxEditionDates.BindPaperEditions(pubEditions)

            ' display the start and end date for the user based on the App Setting
            lblStartDate.Text = onlineStartDate.ToLongDateString
            ' take a variable for the end date and set
            Dim endDate As DateTime = Today
            ' set the first edition to be what the user selected
            _bundleController.SetFirstEdition(selectedDate)
            _bundleController.SetStartDate(onlineStartDate)

            ' set and display the END DATE!
            If Me.ScheduleOnlineWithPrint Then
                ' the end date needs to be what the user has selected (last publication edition)
                endDate = GetLastEditionDate(pubEditions).AddDays(_daysAfterPrint)
                ' ensure that the date is at least 6 days longer than the start
                Dim ts As TimeSpan = endDate.Subtract(onlineStartDate)
                If ts.Days < _daysAfterPrint Then
                    ' simply set the end date to be a week after the start date
                    endDate = onlineStartDate.AddDays(_daysAfterPrint)
                End If
            Else
                ' get the normal running online ad date
                Dim numberOfDays As Integer = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, Utilities.Constants.CONST_KEY_Online_AdDurationDays)
                ' set the end date in Variable and sessioon
                endDate = onlineStartDate.AddDays(numberOfDays)
            End If
            ' set the date in the session
            _bundleController.SetEndDate(endDate)
            ' set the UI end date once it's sorted
            Me.lblEndDate.Text = endDate.ToLongDateString
        Else
            ' display the error message when user wants to check the editions
            ucxEditionDates.RemoveBindings()
        End If
    End Sub

    Private Function GetLastEditionDate(ByVal editionList As List(Of Booking.EditionList)) As DateTime
        ' loops through all the edition dates in the list and returns the largest one
        Dim largestDate As DateTime = Today
        For Each pubEdition In editionList
            For Each ed In pubEdition.EditionList
                If ed > largestDate Then
                    largestDate = ed
                End If
            Next
        Next
        Return largestDate
    End Function

    Private Function GetStartDate() As Date
        'Dim startDate = calStartDate.SelectedDate.Date.AddDays(_daysPriorPrint)
        Dim startDate = Me.SelectedDate.AddDays(_daysPriorPrint)
        If startDate < DateTime.Now Then
            startDate = DateTime.Today
        End If
        Return startDate
    End Function

#End Region

    Private Property SelectedDate As DateTime
        Get
            Return DateTime.Parse(ddlUpcomingEditions.SelectedValue)
        End Get
        Set(value As DateTime)
            ddlUpcomingEditions.SelectedValue = value
        End Set
    End Property


#Region "Navigation Buttons"

    Protected Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        ' call method to check all the user input within the page and display the errors
        If ValidatePage() Then
            _bundleController.SetTotalBookingPrice()
            _bundleController.SetBookEntries()

            ' redirect to confirmation page once all done
            Response.Redirect(PageUrl.BookingBundle_5)
        End If
    End Sub

    Protected Sub btnPrevious_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrevious.Click
        ' go back to step 3
        Response.Redirect(PageUrl.BookingBundle_3)
    End Sub

#End Region

#Region "Validation"

    Private Function ValidatePage() As Boolean
        Dim errorList As New List(Of String)

        ' start date needs to be selected
        If Me.SelectedDate < Today.Date Then
            errorList.Add("Start Date has not been selected.")
        End If

        If (ddlInserts.SelectedIndex = 0) Then
            errorList.Add("Please specify the number of insertions.")
        End If

        ' display the errors
        PageErrors1.ShowErrors(errorList)

        ' return true (validation ok) if the error list contains 0 elements
        Return errorList.Count = 0
    End Function

#End Region

#Region "Event Handling"

    'Private Sub calStartDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calStartDate.SelectionChanged
    '    Dim startDate = GetStartDate(calStartDate.SelectedDate)
    '    ' handle the procedure when a selection is made on the UI
    '    UpdateEditionDetails(calStartDate.SelectedDate, startDate, Me.ddlInserts.SelectedValue)
    'End Sub

    Private Sub ddlUpcomingEditions_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUpcomingEditions.SelectedIndexChanged
        UpdateEditionDetails(Me.SelectedDate, GetStartDate(), Me.ddlInserts.SelectedValue)
    End Sub

    Private Sub ddlInserts_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlInserts.SelectedIndexChanged
        ' set the number of inserts into the session
        _bundleController.SetEditionCount(ddlInserts.SelectedValue)
        If Me.SelectedDate > DateTime.MinValue Then
            ' handle the procedure when a selection is made 
            Dim onlineStartDate = GetStartDate()
            UpdateEditionDetails(Me.SelectedDate, onlineStartDate, Me.ddlInserts.SelectedValue)
        End If
    End Sub

#End Region

    
End Class