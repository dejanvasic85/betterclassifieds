Imports BetterclassifiedsCore
Imports BetterClassified.UI.WebPage

Partial Public Class Step4
    Inherits BaseOnlineBookingPage

    Private Const _selectableDates As String = "vsDates"
    Private Const _adTypeState As String = "vsAdType"
    Private _maxInsertSetting As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' load variable maximum insertions from App Settings
        _maxInsertSetting = AppKeyReader(Of Integer).ReadFromStore(AppKey.MaximumInsertions, 10)

        If Not Page.IsPostBack Then

            Dim adType As String = BookingController.AdBookCart.MainAdType().Code.Trim
            Dim action As String = Request.QueryString("action")

            calStartDate.SelectedDate = Today.Date ' select todays date
            trEndDate.Visible = True
            EndDate = BookingController.GetEndDate(calStartDate.SelectedDate)
            lblEndDate.Text = EndDate.ToLongDateString
            lblStartDate.Text = Today.ToLongDateString


            ' check if the user is returning to this step to change something
            If action = "back" Then
                calStartDate.SelectedDate = BookingController.AdBookCart.StartDate


                trEndDate.Visible = True
                EndDate = BookingController.AdBookCart.EndDate
                lblEndDate.Text = EndDate.ToLongDateString


            End If

            Me.DataBind()
        End If
    End Sub

    Private Sub calStartDate_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calStartDate.DayRender

        If e.Day.Date < Today.Date Then
            e.Day.IsSelectable = False
        End If

    End Sub

    Private Sub ucxNavButtons_NextStep(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucxNavButtons.NextStep
        Dim errorList As New List(Of String)

        If (ValidatePage(errorList)) Then


            ' for online ads, we sould already have the end date, so just pass it in
            'BookingController.SetSchedulingDetails(calStartDate.SelectedDate, EndDate)
            BookingController.SetBookEntriesAndPrice(1, calStartDate.SelectedDate, _
                                                     BookingController.AdBookCart.PublicationList, _
                                                     BookingController.AdBookCart.MainCategoryId)

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

        Return errorList.Count = 0
    End Function

    
    Private Property EndDate() As DateTime
        Get
            Return ViewState("endDate")
        End Get
        Set(ByVal value As DateTime)
            ViewState("endDate") = value
        End Set
    End Property


End Class