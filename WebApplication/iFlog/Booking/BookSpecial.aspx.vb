Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BusinessEntities
Imports BetterclassifiedsCore.Context
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.WebPage

Partial Public Class BookSpecial
    Inherits BaseBookingPage

    Dim _mainCategoryId As Integer
    Dim _specialId As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' get the category and the special rate they have chosen
        _mainCategoryId = Request.QueryString("mainCategoryId")
        _specialId = Request.QueryString("specialId")

        If Not Page.IsPostBack Then

            ' prepare the object in the session
            BookingController.StartSpecialBooking(Membership.GetUser().UserName, _specialId, _mainCategoryId)

            ' get the publications the user will be able to select
            ' if there exists an online paper, we remove from selection and add automatically in booking option
            Dim publicationTypeList = PublicationController.SpecialRatePublications(_specialId)
            Dim onlineIndex As Integer

            For Each pubAdType In publicationTypeList
                ' check if online paper exists.
                If pubAdType.AdType = SystemAdType.ONLINE.ToString Then
                    'BookingController.SpecialBookCart.Publications.Add(pubAdType.PublicationId)
                    BookingController.SpecialBookCart.IsOnlineAd = True
                    onlineIndex = publicationTypeList.IndexOf(pubAdType)

                    ' set the online paper id in the viewstate so it gets stored into the session later
                    OnlinePublicationId = pubAdType.PublicationId
                End If
            Next

            ' remove the online paper from the selection
            If BookingController.SpecialBookCart.IsOnlineAd Then
                publicationTypeList.RemoveAt(onlineIndex)
                ucxOnlineAd.Visible = True
            End If

            If (publicationTypeList.Count > 0) Then
                ' display the control for user to edit the Line Ad Details
                ucxLineAd.Visible = True
                ' also display the properties specified by the special offer
                If BookingController.SpecialBookCart.MaximumWords > 0 Then
                    ucxLineAd.IsUpgradableText = True
                End If
                If Not BookingController.SpecialBookCart.AllowLineBoldHeader Then
                    ucxLineAd.IsUpgradable = True
                End If
                ucxLineAd.IsBoldHeadingEnabled = BookingController.SpecialBookCart.AllowLineBoldHeader
                ucxLineAd.WordLimit = BookingController.SpecialBookCart.MaximumWords
                ucxPublications.LoadPapers(publicationTypeList.ToPublicationList())
                fieldSetPublication.Visible = True
                BookingController.SpecialBookCart.IsLineAd = True
            ElseIf publicationTypeList.Count = 0 Then
                pnlEditions.Visible = False
                lnkCheckEditions.Visible = False
            End If

            ' Set up the online upload parameters for uploading images
            UploadParameter.Clear()
            UploadParameter.IsOnlineAdUpload = BookingController.SpecialBookCart.IsOnlineAd
            UploadParameter.IsPrintAdUpload = BookingController.SpecialBookCart.AllowLineImage
            UploadParameter.BookingReference = BookingController.SpecialBookCart.BookReference
            radWindowImages.OpenerElementID = lnkUploadImages.ClientID

            SetDateInformation()
            pnlPublicationSelect.Visible = BookingController.SpecialBookCart.IsLineAd
        End If
    End Sub

    Private Sub calStartDate_DayRender(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DayRenderEventArgs) Handles calStartDate.DayRender
        ' allow all dates to be displayed because both online and line ads are considered in this case.
        If (e.Day.Date >= Today.Date) Then
            e.Day.IsSelectable = True
        Else
            e.Day.IsSelectable = False
        End If
    End Sub

    Private Sub calStartDate_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calStartDate.SelectionChanged

        ' complete check if the session object exists
        If BookingController.SpecialBookCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1)
        End If

        ' first check if classified ads are placed into the
        If BookingController.SpecialBookCart.IsLineAd Then
            ' calculate the edition date
            Dim maxInserts As Integer = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ADBOOKING, _
                                                                     Utilities.Constants.CONST_KEY_Maximum_Insertions)

            ucxEditionDates.BindPaperEditions(PublicationController.PublicationEditionList(ucxPublications.GetSelectedPapers(SystemAdType.LINE), _
                                                                                           BookingController.SpecialBookCart.Insertions, _
                                                                                           calStartDate.SelectedDate))
        End If
    End Sub

    Private Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click

        ' complete check if the session object exists
        If BookingController.SpecialBookCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1)
        End If

        Dim errorList As New List(Of String)

        If (ValidatePage(errorList)) Then
            ' SET AD Details - Title and comments
            BookingController.SetSpecialAdDetails("", "", False) ' removing the ad details from the bookings (redundant data)

            ' SET LINE AD and PUBLICATIONS
            If BookingController.SpecialBookCart.IsLineAd Then
                BookingController.SetSpecialLineAd(BookingController.SpecialBookCart.MaximumWords, ucxLineAd.AdHeader, ucxLineAd.AdText)

                ' publications
                If (BookingController.SpecialBookCart.Publications.Count > 0) Then
                    BookingController.SpecialBookCart.Publications.Clear()
                End If
                BookingController.SpecialBookCart.Publications = ucxPublications.GetSelectedPapers(SystemAdType.LINE)

            End If

            ' SET ONLINE AD
            If BookingController.SpecialBookCart.IsOnlineAd Then
                BookingController.SetSpecialOnlineAd(ucxOnlineAd.Heading, ucxOnlineAd.Description, ucxOnlineAd.HtmlText, ucxOnlineAd.Price, ucxOnlineAd.LocationId, ucxOnlineAd.LocationArea, _
                                              ucxOnlineAd.ContactName, ucxOnlineAd.ContactType, ucxOnlineAd.ContactValue)

                BookingController.SpecialBookCart.Publications.Add(OnlinePublicationId)
            End If

            ' SET START AND END DATE!
            BookingController.SetSpecialSchedule(calStartDate.SelectedDate, BookingController.GetEndDate(calStartDate.SelectedDate, BookingController.SpecialBookCart.Insertions, _
                                                                                                         BookingController.SpecialBookCart.IsLineAd, BookingController.SpecialBookCart.IsOnlineAd, _
                                                                                                         ucxPublications.GetSelectedPapers(SystemAdType.LINE)))

            ' SET BOOK ENTRIES 
            BookingController.SetSpecialBookEntries(_specialId, calStartDate.SelectedDate, BookingController.SpecialBookCart.Publications, _
                                                    BookingController.SpecialBookCart.IsLineAd, BookingController.SpecialBookCart.IsOnlineAd)

            ' PRICE - Make sure we pass in only the line ad publication count and not the online publication
            Dim count As Integer = 0
            For Each pub In BookingController.SpecialBookCart.Publications
                If PublicationController.GetPublicationType(pub) <> "ONLINE" Then
                    count += 1
                End If
            Next
            BookingController.SetSpecialAdPrice(count, BookingController.SpecialBookCart.SetPrice)

            Response.Redirect("~/Booking/BookSpecialConfirm.aspx")
        Else
            ucxPageErrors.ShowErrors(errorList)
        End If
    End Sub

    Private Function ValidatePage(ByRef errorList As List(Of String)) As Boolean

        ' validate the page! 
        If (BookingController.SpecialBookCart.IsLineAd) Then
            ' paper selection - if includes line ad
            If (ucxPublications.GetSelectedPapers(SystemAdType.LINE).Count = 0) Then
                errorList.Add("No publications have been selected for the Classified.")
            End If
        End If

        ' date selection
        If (calStartDate.SelectedDate < Today) Then
            errorList.Add("Please select the start date for you Special Booking.")
        End If

        ' line ad text limit
        If (BookingController.SpecialBookCart.IsLineAd) Then
            If (GeneralRoutine.LineAdWordCount(ucxLineAd.AdText) > BookingController.SpecialBookCart.MaximumWords) Then
                errorList.Add("More than " + BookingController.SpecialBookCart.MaximumWords.ToString + " words have been counted in your Ad. Please correct.")
            End If

            If (ucxLineAd.AdText = String.Empty) Then
                errorList.Add("Please provide your classified Ad Text.")
            End If
        End If

        ' online ad text and heading
        If (BookingController.SpecialBookCart.IsOnlineAd) Then
            If (ucxOnlineAd.Description = String.Empty) Then
                errorList.Add("Please provide the Description for your Online Ad.")
            Else
                ' ensure that the text doesn't contain word more than 34 characters
                Dim arrayOfWords As String() = ucxOnlineAd.Description.Split(" ") ' only split by a space

                For Each item In arrayOfWords
                    If item.Length > Common.Constants.onlineAdTextCharsLimit Then
                        errorList.Add("Ad Text for Online Ad cannot have a word more than " + Common.Constants.onlineAdTextCharsLimit.ToString + " characters.")
                    End If
                Next
            End If
            If (ucxOnlineAd.Heading = String.Empty) Then
                errorList.Add("Please provide the Heading for you Online Ad.")
            Else

                ' ensure that the heading doesn't contain word more than 20 characters
                Dim arrayOfWords As String() = ucxOnlineAd.Heading.Split(" ") ' only split by a space

                For Each item In arrayOfWords
                    If item.Length > Common.Constants.onlineAdHeadingCharsLimit Then
                        errorList.Add("Heading for Online Ad cannot have a word more than " + Common.Constants.onlineAdHeadingCharsLimit.ToString + " characters.")
                    End If
                Next
            End If
        End If

        Return errorList.Count = 0
    End Function

    Private Property OnlinePublicationId() As Integer
        Get
            Return ViewState("onlinePaperId")
        End Get
        Set(ByVal value As Integer)
            ViewState("onlinePaperId") = value
        End Set
    End Property

    Private Sub SetDateInformation()
        If BookingController.SpecialBookCart.IsOnlineAd = True And BookingController.SpecialBookCart.IsLineAd = True Then
            lblDateInformation.Text = "The online ad will commence from the start date you specify. Click on check editions to view the print classified entries."
        ElseIf BookingController.SpecialBookCart.IsLineAd Then
            lblDateInformation.Text = "Ensure you chosen the publications then select a start date and click on check editions below to view the edition entries for each publication."
        ElseIf BookingController.SpecialBookCart.IsOnlineAd Then
            lblDateInformation.Text = "Select the start date for your online booking. The duration of the ad is 30 days."
        End If
    End Sub

    Private Sub ucxEditionDates_GridPageIndexChanged() Handles ucxEditionDates.GridPageIndexChanged
        ucxEditionDates.BindPaperEditions(PublicationController.PublicationEditionList(ucxPublications.GetSelectedPapers(SystemAdType.LINE), _
                                                                                           BookingController.SpecialBookCart.Insertions, _
                                                                                           calStartDate.SelectedDate))
    End Sub

    Private Sub ucxLineAd_UpgradeSelected(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucxLineAd.UpgradeSelected
        Dim parentCategoryId = CategoryController.GetMainCategoryById(_mainCategoryId).ParentId
        Dim publications As List(Of Integer) = ucxPublications.GetSelectedPapers(SystemAdType.LINE)
        Dim lineAdGraphicId = String.Empty
        If BookingController.SpecialBookCart.LineAdImage IsNot Nothing Then
            lineAdGraphicId = BookingController.SpecialBookCart.LineAdImage.DocumentID
        End If

        ' Start a new bundle booking by setting the required details and redirecting.
        General.StartBundleBookingStep2(parentCategoryId, _mainCategoryId, ucxPublications.GetSelectedPapers(SystemAdType.LINE), _
                                        BookingController.SpecialBookCart.IsOnlineAd, ucxLineAd.AdText, ucxLineAd.AdHeader, lineAdGraphicId)
    End Sub
End Class