Imports System.Web
Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore.Controller
Imports System.Data.SqlClient
Imports System.Text
Imports BetterclassifiedsCore.Booking
Imports Paramount.DSL.UIController
Imports System.Configuration
Imports System.Collections.Specialized
Imports Paramount.Utility
Imports Paramount.ApplicationBlock.Configuration

Public Enum TransactionType
    CREDIT = 1
    PAYPAL = 2
    FREEAD = 3
End Enum

Public Enum AdDesignStatus
    Pending = 1
    Approved = 2
    Cancelled = 3
End Enum

Public Class BookingController

#Region "Ad Booking Procedure"

    Private Const bookingSessionName As String = "_bookingSession"
    Private Const bookingTypeAction As String = "_bookingTypeAction"

    Public Shared Property BookingType() As BookingAction
        Get
            If Not (HttpContext.Current.Session(bookingTypeAction)) Is Nothing Then
                Return HttpContext.Current.Session(bookingTypeAction)
            End If
        End Get
        Set(ByVal value As BookingAction)
            HttpContext.Current.Session(bookingTypeAction) = value
        End Set
    End Property

    Public Shared Function IsBooked(ByVal paymentRef As String)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim book = From b In db.AdBookings _
                       Join t In db.TempBookingRecords On b.BookReference Equals t.AdReferenceId _
                       Where t.BookingRecordId.ToString = paymentRef Select b
            If book.Count = 1 Then
                If book.Single.BookingStatus.Value = BookingStatus.BOOKED Then
                    Return True
                End If
            End If
        End Using
        Return False
    End Function

    Public Shared ReadOnly Property AdBookCart() As BookCart
        Get
            Return HttpContext.Current.Session(bookingSessionName)
        End Get
    End Property

    Public Shared ReadOnly Property IsZeroValueTransaction()
        Get
            Return AdBookCart.TotalPrice = 0
        End Get
    End Property

    ''' <summary>
    ''' Returns the BookCart from session that can be binded to a control.
    ''' </summary>
    Public Shared ReadOnly Property GetBookSummary() As List(Of BookCart)
        Get
            Dim list As New List(Of BookCart)
            list.Add(AdBookCart)
            Return list
        End Get
    End Property

    ''' <summary>
    ''' Checks to see if there is no existing <see cref="BookCart">
    ''' BookCart</see> in the session, and creates a new object.
    ''' </summary>
    ''' <remarks>Call this method during within the application when a 
    ''' new booking is being made.</remarks>
    Public Shared Sub StartAdBooking(ByVal userId As String)

        If HttpContext.Current.Session(bookingSessionName) Is Nothing Then
            HttpContext.Current.Session(bookingSessionName) = New BookCart

            ' set the user 
            AdBookCart.UserId = userId

        End If

    End Sub

    ''' <summary>
    ''' Clears the current <see cref="BookCart">BookCart</see> object
    ''' from the session.
    ''' </summary>
    ''' <remarks>Call this method when you want to kill the current
    ''' BookCart object from the session. This may occur if you want to
    ''' clear the object </remarks>
    Public Shared Sub ClearAdBooking()

        If Not HttpContext.Current.Session(bookingSessionName) Is Nothing Then
            'clear the session value
            HttpContext.Current.Session(bookingSessionName) = Nothing
            HttpContext.Current.Session(bookingTypeAction) = Nothing
        End If

    End Sub

    ''' <summary>
    ''' Commits the Ad Book Cart from the session into the database
    ''' </summary>
    ''' <returns>Returns True if the Booking Succeeded</returns>
    ''' <remarks></remarks>
    ''' <param name="transactionType"></param>
    Public Shared Function PlaceAd(ByVal transactionType As TransactionType) As Boolean
        Return GeneralRoutine.PlaceAd(AdBookCart, transactionType)
    End Function

    ''' <summary>
    ''' Retrieves the Ad Booking by Id, and updates the Booking Status column to BookingStatus.CANCELLED enum value.
    ''' </summary>
    ''' <param name="bookingId">Id for the Booking held in the database</param>
    ''' <returns>True if the setting has been changed successfully.</returns>
    ''' <remarks></remarks>
    Public Shared Function CancelExistingBooking(ByVal bookingId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim booking = (From b In db.AdBookings Where b.AdBookingId = bookingId Select b).Single
            booking.BookingStatus = BookingStatus.CANCELLED
            db.SubmitChanges()
            Return True
        End Using
    End Function

    Public Shared Function ExpireExistingBooking(ByVal bookingId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim booking = (From b In db.AdBookings Where b.AdBookingId = bookingId Select b).Single
            booking.EndDate = DateTime.Today
            booking.BookingStatus = BookingStatus.CANCELLED
            db.SubmitChanges()
            Return True
        End Using
    End Function

    ''' <summary>
    ''' Updates the required Ad Booking status to BOOKED in the database.
    ''' </summary>
    ''' <param name="bookingId">The ID for the Booking to be adjusted.</param>
    ''' <returns>True if the booking status has been changed successfully.</returns>
    ''' <remarks></remarks>
    Public Shared Function ActiveExistingBooking(ByVal bookingId As Integer) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim booking = (From b In db.AdBookings Where b.AdBookingId = bookingId Select b).Single
            booking.BookingStatus = BookingStatus.BOOKED
            db.SubmitChanges()
            Return True
        End Using

    End Function

    Public Shared Function ReBook(ByVal adBookingId As Integer, ByVal startDate As DateTime, ByVal endDate As DateTime, _
                                  ByVal bookReference As String, ByVal totalPrice As Decimal) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            '' need to retrieve all the required object to submit the booking again

            ' start with ad and designs.
            Dim booking = (From b In db.AdBookings Where b.AdBookingId = adBookingId Select b).Single
        End Using

    End Function

    Public Shared Function BookTempAdRecord(ByVal Id As String, ByVal sessionId As String, ByVal totalCost As Decimal) As Boolean
        Dim list = New List(Of AdBooking)
        Using db = BetterclassifiedsDataContext.NewContext
            list = (From item In db.AdBookings _
                    Join item2 In db.TempBookingRecords On item2.AdReferenceId Equals item.BookReference _
                    Where item2.SessionID = sessionId _
                    AndAlso item2.BookingRecordId.ToString = Id _
                    AndAlso item2.TotalCost = totalCost Select item).ToList

            If list.Count = 1 Then
                Dim item = list(0)
                item.BookingStatus = BookingStatus.BOOKED
                db.SubmitChanges()
                Return True
            End If
        End Using
        Return False
    End Function

    Public Shared Sub SaveTempAdRecord(ByVal Id As String, ByVal totalPrice As Decimal, ByVal sessionId As String, ByVal UserId As String, ByVal recordValue As BookCart, ByVal transType As TransactionType)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim check = From a In db.TempBookingRecords Where a.BookingRecordId = New Guid(Id)
            If check.Count > 0 Then
                Return
            End If
        End Using


        Using db = BetterclassifiedsDataContext.NewContext
            Dim record As New TempBookingRecord
            record.BookingRecordId = New Guid(Id)
            record.DateTime = DateTime.Now
            record.SessionID = sessionId
            record.TotalCost = totalPrice
            record.UserId = UserId
            record.AdReferenceId = recordValue.BookReference
            db.TempBookingRecords.InsertOnSubmit(record)
            db.SubmitChanges()
        End Using
        recordValue.BookingStatus = BookingStatus.UNPAID
        GeneralRoutine.PlaceAd(recordValue, transType)
    End Sub
#End Region

#Region "Set Methods"

    ''' <summary>
    ''' Sets the Ad Type into the session for the current ad booking.
    ''' </summary>
    ''' <param name="adTypeId">AdType ID value that corresponds to the ID from the database.</param>
    ''' <remarks>
    ''' Creates a new AdDesign with the AdType passed as parameter. Then it associates the Ad Design to a new Ad in the AdBookCart.</remarks>
    Public Shared Sub SetAdType(ByVal adTypeId As Integer)

        Dim cart As BookCart = AdBookCart
        If Not (cart) Is Nothing Then

            ' create a new ad design for this ad with the ad type the user chosen
            Dim design As New AdDesign
            design.AdTypeId = adTypeId

            ' create new ad in the adbooking with the design we also created.
            cart.Ad = New Ad
            cart.Ad.AdDesigns.Add(design)

            ' set the main ad type as reference in the BookCart object also
            Using db = BetterclassifiedsDataContext.NewContext
                cart.MainAdType = (From t In db.AdTypes _
                                  Where t.AdTypeId = adTypeId _
                                  Select t).Single
            End Using

        Else
            ' throw an exception. 
            Throw New Exception("The booking has not been started. Should not be able to access this part of the system.")
        End If
    End Sub

    ''' <summary>
    ''' Creates a Booking Reference if one doesn't exist and sets the value into the session.
    ''' </summary>
    ''' <param name="categoryId"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetBookingReference(ByVal categoryId As Integer)
        If Not AdBookCart Is Nothing Then
            If AdBookCart.BookReference = "" Then
                AdBookCart.BookReference = GeneralRoutine.GetBookingReference(categoryId, True)
            Else
                ' user already passed through this so don't send to increment the value
                AdBookCart.BookReference = GeneralRoutine.GetBookingReference(categoryId, False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sets the List of Publications into the session for the current ad booking.
    ''' </summary>
    ''' <param name="paperIds">List of integer values representing the Publication ID's from the Database.</param>
    Public Shared Sub SetPublications(ByVal paperIds As List(Of Integer))
        If Not AdBookCart Is Nothing Then
            AdBookCart.PublicationList = paperIds
        End If
    End Sub

    ''' <summary>
    ''' Sets the general Ad Details for the Ad Booking.
    ''' </summary>
    ''' <param name="title">Title of the Ad. This is the header in a Line Ad.</param>
    ''' <param name="comments">Comments related to the booking.</param>
    ''' <param name="useTemplate">States if this Ad can be used as an Ad Template.</param>
    Public Shared Sub SetAdDetails(ByVal title As String, ByVal comments As String, ByVal useTemplate As Boolean)
        If Not AdBookCart Is Nothing Then
            ' the Ad should be created in the SetAdType Method already

            With AdBookCart.Ad
                .Title = title
                .Comments = comments
                .UseAsTemplate = useTemplate
            End With
        End If
    End Sub

    ''' <summary>
    ''' Sets the Main Category for the current ad booking in the session.
    ''' </summary>
    ''' <param name="categoryId">ID Value of the selected category from the database.</param>
    Public Shared Sub SetMainCategory(ByVal categoryId As Integer)
        AdBookCart.MainCategoryId = categoryId
    End Sub

    Public Shared Function GetOnlineAdTypeTagForBooking() As String
        Using db = BetterclassifiedsDataContext.NewContext
            Dim category = db.MainCategories.FirstOrDefault(Function(c) c.MainCategoryId = AdBookCart.MainCategoryId)
            Dim parentCategory = db.MainCategories.FirstOrDefault(Function(c) c.MainCategoryId = AdBookCart.ParentCategoryId)
            Return IIf(Not String.IsNullOrEmpty(category.OnlineAdTag), category.OnlineAdTag, parentCategory.OnlineAdTag)
        End Using
    End Function

    ''' <summary>
    ''' Sets the Parent Category for the main category selected during booking.
    ''' </summary>
    ''' <param name="parentCategoryId"></param>
    Public Shared Sub SetParentCategory(ByVal parentCategoryId As Integer)
        AdBookCart.ParentCategoryId = parentCategoryId
    End Sub

    ''' <summary>
    ''' Creates a line ad object and places into the booking cart.
    ''' </summary>
    ''' <param name="header">Line Ad Header is the first bold text in a line ad.</param>
    ''' <param name="adText">Line Ad Text is the main text in a classified ad.</param>
    ''' <remarks>Note that we should always only contain one ad design at this point in the booking</remarks>
    Public Shared Sub SetLineAd(ByVal header As String, ByVal adText As String)
        If Not AdBookCart Is Nothing Then

            ' first remove any current line ad in the session 
            AdBookCart.Ad.AdDesigns(0).LineAds.Clear()

            Dim a As New LineAd With {.AdText = adText, .AdHeader = header, .NumOfWords = GeneralRoutine.LineAdWordCount(adText)}

            If (header <> String.Empty) Then
                a.UseBoldHeader = True
            Else
                a.UseBoldHeader = False
            End If

            ' now we add the line ad into the booking cart object which contains the relationship
            AdBookCart.Ad.AdDesigns(0).LineAds.Add(a)

        End If
    End Sub

    Public Shared Sub SetLineAdGraphic(ByVal docId As String)
        If AdBookCart IsNot Nothing Then
            AdBookCart.LineAdGraphic = New AdGraphic With {.DocumentID = docId}
        End If
    End Sub

    ''' <summary>
    ''' Creates an Online Ad Object and places into the booking session object.
    ''' </summary>
    Public Shared Sub SetOnlineAd(ByVal heading As String, ByVal description As String, ByVal html As String, ByVal price As Decimal, ByVal locationId As Integer, ByVal locationAreaId As Integer, ByVal contactName As String, _
                                  ByVal contactType As String, ByVal contactValue As String)

        If Not AdBookCart Is Nothing Then
            ' first clear any data in the session before we begin
            AdBookCart.Ad.AdDesigns(0).OnlineAds.Clear()
            AdBookCart.Ad.AdDesigns(0).AdGraphics.Clear()

            ' create an Online Ad
            Dim a As New DataModel.OnlineAd With {.Heading = heading, .Description = description, .HtmlText = html, .Price = price, _
                                         .ContactName = contactName, .ContactType = contactType, _
                                         .ContactValue = contactValue}
            If (locationId > 0) Then
                a.LocationId = locationId
            End If
            If (locationAreaId > 0) Then
                a.LocationAreaId = locationAreaId
            End If
            AdBookCart.Ad.AdDesigns(0).OnlineAds.Add(a)
        End If
    End Sub

    ''' <summary>
    ''' Creates an Online Ad Object and places into the booking session object.
    ''' </summary>
    Public Shared Sub SetOnlineAd(ByVal heading As String, ByVal description As String, ByVal html As String, ByVal price As Decimal, ByVal locationId As Integer, ByVal locationAreaId As Integer, ByVal contactName As String, _
                                  ByVal contactType As String, ByVal contactValue As String, ByVal imageList As List(Of String))

        If Not AdBookCart Is Nothing Then
            ' first clear any data in the session before we begin
            AdBookCart.Ad.AdDesigns(0).OnlineAds.Clear()
            AdBookCart.Ad.AdDesigns(0).AdGraphics.Clear()

            ' create an Online Ad
            Dim onlineAd As New DataModel.OnlineAd With {.Heading = heading, .Description = description, .HtmlText = html, .Price = price, _
                                         .ContactName = contactName, .ContactType = contactType, _
                                         .ContactValue = contactValue, .OnlineAdTag = GetOnlineAdTypeTagForBooking()}
            If (locationId > 0) Then
                onlineAd.LocationId = locationId
            End If
            If (locationAreaId > 0) Then
                onlineAd.LocationAreaId = locationAreaId
            End If
            AdBookCart.Ad.AdDesigns(0).OnlineAds.Add(onlineAd)
            AdBookCart.ImageList = imageList
        End If
    End Sub

    Public Shared Sub SetTutorAdDetails(ByVal ageGroupMax As Nullable(Of Integer), ByVal ageGroupMin As Nullable(Of Integer), _
                                         ByVal level As String, ByVal objective As String, ByVal pricingOption As String, _
                                         ByVal subjects As String, ByVal travelOption As String, ByVal whatToBring As String)
        If AdBookCart IsNot Nothing Then
            AdBookCart.TutorAd = New DataModel.TutorAd With { _
                .AgeGroupMax = ageGroupMax, _
                .AgeGroupMin = ageGroupMin, _
                .ExpertiseLevel = level, _
                .Objective = objective, _
                .PricingOption = pricingOption, _
                .Subjects = subjects, _
                .TravelOption = travelOption, _
                .WhatToBring = whatToBring}
        End If
    End Sub

    ''' <summary>
    ''' Retrieves the Ratecards from the databases associated with the selected Publications and Category
    ''' </summary>
    ''' <param name="publicationIds">List of PublicationId's selected in the session</param>
    ''' <param name="mainCategory">Main Category ID chosen that's also in the session</param>
    Public Shared Function SetRatecards(ByVal publicationIds As List(Of Integer), ByVal mainCategory As Integer) As Boolean


        ' instatiate the object we need to set
        Dim rateList As New Dictionary(Of Integer, Ratecard)
        Dim adType As AdType = BookingController.AdBookCart.MainAdType

        Dim db = BetterclassifiedsDataContext.NewContext

        ' loop through each publication ID chosen and obtain the right ratecard we need to use
        For Each pubId In publicationIds
            Dim id As Integer = pubId
            ' retrieve the publication category we need to use
            Dim pubCategory As PublicationCategory = CategoryController.GetPublicationCategory(mainCategory, pubId)

            If pubCategory Is Nothing Then
                Return False
                Exit Function
            End If

            ' perform query to get the ratecard
            Dim rateCard = From pubRate In db.PublicationRates _
                           Join pubAdType In db.PublicationAdTypes On pubRate.PublicationAdTypeId Equals pubAdType.PublicationAdTypeId _
                           Join rate In db.Ratecards On pubRate.RatecardId Equals rate.RatecardId _
                           Where pubAdType.PublicationId = id _
                           And pubAdType.AdTypeId = adType.AdTypeId _
                           And pubRate.PublicationCategoryId = pubCategory.PublicationCategoryId _
                           Select rate

            If rateCard.Count > 0 Then
                rateList.Add(pubId, rateCard.Single)
            Else
                Return False
                Exit Function
            End If

        Next

        ' store the rate card list in the session object
        If Not AdBookCart Is Nothing Then
            AdBookCart.RatecardList = rateList
        End If

        db = Nothing ' clean up
        rateList = Nothing

        Return True

    End Function

    Public Shared Function GetBookingRefByTempRec(ByVal tempRef As String) As String
        Dim contentBuilder = New StringBuilder

        Using db = BetterclassifiedsDataContext.NewContext
            Dim bk = From b In db.TempBookingRecords Where b.BookingRecordId.ToString = tempRef Select b.AdReferenceId
            If bk.Count = 1 Then
                Return bk.Single
            End If
        End Using
        Return 0

    End Function

    Public Shared Function GetBookingStringContentByRef(ByVal bookRef As String) As String
        Dim contentBuilder = New StringBuilder

        Using db = BetterclassifiedsDataContext.NewContext
            Dim bk = From b In db.AdBookings Where b.BookReference = bookRef

            If bk.Count = 1 Then
                Dim adBooking = bk.SingleOrDefault

                contentBuilder.AppendLine(String.Format("Ad ID: {0} <br/>", adBooking.AdBookingId))
                contentBuilder.AppendLine(String.Format("Booking Reference: {0} <br/>", adBooking.BookReference))
                contentBuilder.AppendLine(String.Format("User Id: {0} <br/>", adBooking.UserId))
                contentBuilder.AppendLine(String.Format("Total Price: {0:C} <br/>", adBooking.TotalPrice))
                contentBuilder.AppendLine(String.Format("Main Category: {0} <br/>", adBooking.MainCategory.Title))

                If adBooking.Ad.AdDesigns.Count > 0 Then
                    For Each item In adBooking.Ad.AdDesigns
                        For Each item2 In item.LineAds
                            contentBuilder.AppendLine("<p>**********--- Line Ad----********** </p>")
                            contentBuilder.AppendLine(String.Format("Header: {0}<br/><br/>", item2.AdHeader))
                            contentBuilder.AppendLine("------ Text ------<br/>")
                            contentBuilder.AppendLine(item2.AdText)
                            contentBuilder.AppendLine("<br/><br/>")
                        Next


                        For Each item3 In item.OnlineAds
                            contentBuilder.AppendLine("<p>**********--- Online Ad----**********</p>")
                            contentBuilder.AppendLine(String.Format("Header: {0}<br/>", item3.Heading))
                            contentBuilder.AppendLine("------ Text ------<br/>")
                            contentBuilder.AppendLine(item3.Description + "<br/>")
                            contentBuilder.AppendLine("------ HTML ------<br/>")
                            contentBuilder.AppendLine(item3.HtmlText + "<br/>")
                            contentBuilder.AppendLine("<br/><br/>")
                        Next

                        contentBuilder.AppendLine("<p>------ Images ------</p>")
                        For Each item4 In item.AdGraphics
                            Dim collection As New NameValueCollection()
                            Dim dslQuery As New DslQueryParam(collection)
                            dslQuery.DocumentId = item4.DocumentID
                            dslQuery.Entity = CryptoHelper.Encrypt(ConfigurationManager.AppSettings.Get("ClientCode"))
                            dslQuery.Height = 200
                            dslQuery.Width = 200
                            dslQuery.Resolution = 90
                            Dim baseUrl = New Uri(ConfigurationManager.AppSettings.Get("BaseUrl"))
                            Dim imageUrl = New Uri(baseUrl, ConfigSettingReader.DslImageHandler.TrimStart("~", "/"))
                            Dim imageTag As String = String.Format("<img id=""{0}"" src=""{1}?{2}"" />", item4.DocumentID, imageUrl, dslQuery.GenerateUrl)
                            contentBuilder.AppendLine(imageTag)
                        Next

                    Next
                End If

                contentBuilder.AppendLine("<p>----- Publications & Date(s) ---- </p>")
                Dim pub = From i In adBooking.BookEntries Group i By i.Publication.Title Into Group
                For Each item In pub
                    contentBuilder.AppendLine("<br/>")
                    contentBuilder.AppendLine(String.Format("----- {0}:<br/>", item.Title))
                    For Each item2 In item.Group
                        contentBuilder.AppendLine(item2.EditionDate + "<br/>")
                    Next
                Next
                Return contentBuilder.ToString
            Else
                Return String.Empty
            End If
        End Using
    End Function

    ''' <summary>
    ''' Sets the total price for the booking in the session object.
    ''' </summary>
    ''' <param name="price">The Total Price already calculated.</param>
    ''' <remarks></remarks>
    Public Shared Sub SetTotalPrice(ByVal price As Decimal)
        If Not AdBookCart Is Nothing Then
            AdBookCart.TotalPrice = price
        End If
    End Sub

    ''' <summary>
    ''' Sets the price for a single Ad. This method needs to be called during the Design Step.
    ''' </summary>
    ''' <param name="price"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetSingleAdPrice(ByVal price As Decimal)
        If Not AdBookCart Is Nothing Then
            AdBookCart.SingleAdPrice = price
        End If
    End Sub

    ''' <summary>
    ''' Creates the required Book Entry objects and relates them to the ad booking in the session. Then sets the total price of the ad.
    ''' </summary>
    ''' <param name="startDate"></param>
    ''' <param name="insertions"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetSchedulingDetails(ByVal startDate As DateTime, ByVal insertions As Integer, ByVal pubIdList As List(Of Integer))

        ' get the selected papers first from the session
        Dim endDate As Date = Today.Date

        ' first we clear any current book entries associated with this ad booking
        If Not AdBookCart Is Nothing Then

            AdBookCart.BookEntries.Clear()

            For Each pubId In pubIdList
                Dim id As Integer = pubId
                ' get the paper details from the DB

                ' loop through each edition and create a book entry 
                For Each d In PublicationController.PublicationEditions(pubId, insertions, startDate)

                    'Dim entry As New BookEntry With {.EditionDate = d, .PublicationId = paper, .AdBooking = AdBookCart}
                    Dim entry As New BookEntry With {.EditionDate = d, .PublicationId = pubId, .AdBooking = AdBookCart}
                    ' add the book entries into the session
                    AdBookCart.BookEntries.Add(entry)

                    ' also test the end date
                    If d > endDate Then
                        endDate = d
                    End If
                Next

            Next

            ' need to store the start and end dates.
            AdBookCart.StartDate = startDate
            AdBookCart.EndDate = endDate
            AdBookCart.Insertions = insertions

            ' calculate the price again by multiplying the current price to the number of insertions.
            ' also overwrite the existing price in the session.
            AdBookCart.TotalPrice = AdBookCart.SingleAdPrice * insertions
        End If
    End Sub

    ''' <summary>
    ''' Sets the start and end date for an online ad booking.
    ''' </summary>
    ''' <param name="startDate">Start Date for the online ad.</param>
    ''' <param name="endDate">End Date is calculated during the selection and passed as parameter.</param>
    Public Shared Sub SetSchedulingDetails(ByVal startDate As DateTime, ByVal endDate As DateTime)
        If Not AdBookCart Is Nothing Then
            AdBookCart.StartDate = startDate
            AdBookCart.EndDate = endDate

            'create the book entry for the online ad also
            If AdBookCart.PublicationList.Count = 1 Then
                Dim id As Integer = AdBookCart.PublicationList(0)

                Dim entry As New BookEntry With {.EditionDate = startDate, .PublicationId = id, .AdBooking = AdBookCart}

                AdBookCart.BookEntries.Add(entry)
            End If

            ' set the price as well - no need for insertions (simply copy the single ad price)
            AdBookCart.TotalPrice = AdBookCart.SingleAdPrice
        End If
    End Sub

    Public Shared Sub SetBookEntriesAndPrice(ByVal insertions As Integer, ByVal startDate As DateTime, _
                                  ByVal publicationList As List(Of Integer), ByVal mainCategoryId As Integer)

        Dim endDate As Date = Today.Date

        ' make sure there's something in the session before we go on
        If AdBookCart IsNot Nothing Then

            ' ensure that the object list is ready for book entries
            If AdBookCart.BookEntries Is Nothing Then
                AdBookCart.BookEntries = New System.Data.Linq.EntitySet(Of BookEntry)
            End If

            AdBookCart.BookEntries.Clear() ' clear any current book entries in the session
            AdBookCart.TotalPrice = 0 ' reset the total price

            ' also make sure that proper selection was made for publication and categories
            If publicationList.Count > 0 And mainCategoryId > 0 Then

                ' loop through each publication to get the price and calculate the single edition price
                For Each pubId In publicationList

                    Dim pub = PublicationController.GetPublicationById(pubId)
                    Dim adType As DataModel.AdType = AdController.GetAdTypeByPublication(pubId)

                    ' get the publication category details from the database
                    Dim publicationCategory As DataModel.PublicationCategory = CategoryController.GetPublicationCategory(mainCategoryId, pub.PublicationId)

                    ' ********************************
                    ' get the ratecard we need to use 
                    ' ********************************
                    Dim rateCard As DataModel.Ratecard = GeneralController.GetRatecardsByPublicationCategory(pub.PublicationId, publicationCategory.PublicationCategoryId, adType.AdTypeId)


                    ' *******
                    ' LINE AD
                    ' *******
                    If adType.Code.Trim = SystemAdType.LINE.ToString Then

                        Dim lineAd = AdBookCart.Ad.AdDesigns(0).LineAds(0)

                        'calculate the single edition ad price
                        Dim singleEditionAdPrice As Decimal = GeneralRoutine.LineAdRatePrice(rateCard, insertions, GeneralRoutine.LineAdWordCount(lineAd.AdText), lineAd.UsePhoto, lineAd.UseBoldHeader)

                        ' now get the editions to publish in
                        Dim editions = PublicationController.PublicationEditions(pub.PublicationId, insertions, startDate)

                        ' to get the price for the entire publication (include all edition) we simply multiply the price by the insertions
                        Dim publicationBookingPrice As Decimal = singleEditionAdPrice * insertions

                        ' loop through each edition and create the required book entry with all the data
                        For Each ed In editions
                            AdBookCart.BookEntries.Add(New BookEntry With {.BaseRateId = rateCard.BaseRateId, _
                                                                           .EditionDate = ed, _
                                                                           .PublicationId = pub.PublicationId, _
                                                                           .EditionAdPrice = singleEditionAdPrice, _
                                                                           .PublicationPrice = publicationBookingPrice, _
                                                                           .RateType = BookingController.GetRateTypeString})
                            ' set the end date 
                            If ed > endDate Then
                                endDate = ed
                            End If
                        Next

                        ' set the total price
                        AdBookCart.TotalPrice += singleEditionAdPrice * insertions
                    Else
                        ' **********
                        ' ONLINE AD
                        ' **********
                        Dim onlineSingleAdPrice = rateCard.MinCharge
                        Dim onlineDurationDays As Integer = AppKeyReader(Of Integer).ReadFromStore(AppKey.AdDurationDays, defaultIfNotExists:=30)

                        endDate = startDate.AddDays(onlineDurationDays)

                        ' create a single book entry for the online ad every time since scheduling doesn't apply here
                        AdBookCart.BookEntries.Add(New BookEntry With {.BaseRateId = rateCard.BaseRateId, _
                                                                          .PublicationId = pub.PublicationId, _
                                                                          .EditionDate = startDate, _
                                                                          .EditionAdPrice = onlineSingleAdPrice, _
                                                                          .PublicationPrice = onlineSingleAdPrice, _
                                                                          .RateType = BookingController.GetRateTypeString})
                        AdBookCart.TotalPrice = onlineSingleAdPrice
                    End If
                Next
            End If

            ' set the other details
            AdBookCart.StartDate = startDate
            AdBookCart.EndDate = endDate
            AdBookCart.Insertions = insertions
        End If

    End Sub

#End Region

#Region "Retrieve"

    Public Shared Function GetAdBookingById(ByVal id As Integer) As DataModel.AdBooking
        Using db = BetterclassifiedsDataContext.NewContext
            Dim bk = From b In db.AdBookings Where b.AdBookingId = id Select b
            If bk.Count > 0 Then
                Return bk.FirstOrDefault
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Shared Function GetAdBookingByendDate(ByVal selectedDate As Date) As AdBookingSet
        Dim dt As New DataTable
        Using proxy = BetterclassifiedsDataContext.NewContext
            Using cmd As New SqlCommand With {.Connection = proxy.Connection, .CommandType = CommandType.StoredProcedure, .CommandText = Proc.GetAdBookingByEndDate.Name}
                cmd.Parameters.Add(New SqlParameter(Proc.GetAdBookingByEndDate.Params.EndDate, selectedDate))

                Using adp As New SqlDataAdapter(cmd)
                    cmd.Connection.Open()
                    Try
                        adp.Fill(dt)
                    Catch ex As Exception
                    Finally
                        cmd.Connection.Close()
                    End Try
                End Using
            End Using
        End Using
        Return New AdBookingSet(dt)
    End Function
    ''' <summary>
    ''' Checks the Publication ID's that were chosen in session and returns a list of Publication Objects.
    ''' </summary>
    ''' <returns>List Of Publication Objects</returns>
    Public Shared Function GetSelectedPublications() As List(Of Publication)
        Dim list As New List(Of Publication)
        If Not AdBookCart Is Nothing Then
            Using db = BetterclassifiedsDataContext.NewContext
                For Each id In AdBookCart.PublicationList
                    Dim pId As Integer = id
                    list.Add((From p In db.Publications Where p.PublicationId = pId Select p).Single)
                Next
            End Using
        End If
        Return list
    End Function

    ''' <summary>
    ''' Returns a list of Price Summary objects that contains all the pricing information for a Line Ad. Bindable to a data control.
    ''' </summary>
    Public Shared Function GetPriceSummary(ByVal rateList As Dictionary(Of Integer, Ratecard), ByVal mainCategory As Integer, ByVal numOfInserts As Integer, ByVal adText As String, _
                                       ByVal usePhoto As Boolean, ByVal boldHeader As Boolean) As List(Of PriceSummary)
        ' create the return value
        Dim listSummaries As New List(Of PriceSummary)
        ' call method to calculate the words for the ad
        Dim numOfWords As Integer = GeneralRoutine.LineAdWordCount(adText)

        For Each r In rateList

            ' get the associated publication for this ratecard
            Dim publication = PublicationController.GetPublicationById(r.Key)
            Dim category = CategoryController.GetPublicationCategory(mainCategory, publication.PublicationId)

            Dim summary As New PriceSummary With _
            {.PaperName = publication.Title, _
             .CategoryName = category.Title, _
             .PaperPrice = GeneralRoutine.LineAdRatePrice(r.Value, numOfInserts, numOfWords, usePhoto, boldHeader), _
             .NumOfWords = numOfWords, _
             .MinimumCharge = r.Value.MinCharge, _
             .RegularWordCount = r.Value.MeasureUnitLimit, _
             .PhotoCharge = r.Value.PhotoCharge, _
             .BoldHeading = r.Value.BoldHeading, _
             .RatePerUnit = r.Value.RatePerMeasureUnit, _
             .MaximumCharge = r.Value.MaxCharge}

            listSummaries.Add(summary)
        Next

        Return listSummaries
    End Function

    ''' <summary>
    ''' Method to return a 
    ''' </summary>
    Public Shared Function GetPriceSummary(ByVal rateList As Dictionary(Of Integer, DataModel.Ratecard), ByVal mainCategory As Integer, ByVal adText As String) As List(Of Booking.PriceSummary)
        'create return value
        Dim listSummary As New List(Of PriceSummary)

        For Each r In rateList
            Dim publication As DataModel.Publication = PublicationController.GetPublicationById(r.Key)
            Dim category As DataModel.PublicationCategory = CategoryController.GetPublicationCategory(mainCategory, publication.PublicationId)

            Dim summary As New PriceSummary With _
            { _
                .PaperName = publication.Title, _
                .CategoryName = category.Title, _
                .PaperPrice = GeneralRoutine.OnlineAdRatePrice(r.Value) _
            }
            listSummary.Add(summary)
        Next
        Return listSummary
    End Function

    ''' <summary>
    ''' Checks session object and returns the Ad Design according to the parameter of Ad Type.
    ''' </summary>
    ''' <returns>Returns a list of LineAd objects.</returns>
    Public Shared Function GetAdDesignDetails(ByVal adType As SystemAdType) As AdDesign
        If Not AdBookCart Is Nothing Then
            If Not AdBookCart.Ad Is Nothing Then
                ' loop through all the ad designs in the session
                ' there shouldn't be more than one of each ad types in there.
                For Each design As AdDesign In AdBookCart.Ad.AdDesigns
                    If (AdController.GetAdType(design.AdTypeId).Code = adType.ToString) Then
                        Return design
                        Exit For
                    End If
                Next
            End If
        End If

        Return Nothing
    End Function

    ''' <summary>
    ''' Calculates the end date for an Online Ad by adding an App Setting value for number of days to the start date.
    ''' </summary>
    ''' <param name="startDate">Date when the Ad starts running.</param>
    Public Shared Function GetEndDate(ByVal startDate As DateTime) As DateTime
        Dim numOfDays As Integer = AppKeyReader(Of Integer).ReadFromStore(AppKey.AdDurationDays, defaultIfNotExists:=30)
        Return startDate.AddDays(numOfDays)
    End Function

    ''' <summary>
    ''' Calculates the end date depending if Line Ad Or Online ad are used or both.
    ''' </summary>
    ''' <remarks>Business rules indicate that we only specify the start and end date once for each
    ''' Ad Booking. However if we have more than one design, it may contain a Line Ad and online Ads which
    ''' could have different start and end dates. Requirements specify that if an Online Ad is booked along side
    ''' a Line ad, then it will make use of the Line Ad schedule rules which means that Line Ad Classified supersedes online.</remarks>
    Public Shared Function GetEndDate(ByVal startDate As DateTime, ByVal insertions As Integer, ByVal isLineAd As Boolean, ByVal isOnline As Boolean, ByVal publicationIdList As List(Of Integer)) As DateTime
        Dim onlineDays As Integer = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, _
                                                                 Utilities.Constants.CONST_KEY_Online_AdDurationDays)
        Dim endDate As DateTime = Today.Date

        ' check if the special rates includes line ads.
        If isLineAd Then

            ' check if the online ad is included in the rate. If so, then we simply multiple insertions to 7 (days)
            If isOnline Then
                endDate = startDate.AddDays(insertions * 7)
            Else
                Dim editionList As List(Of Nullable(Of DateTime)) = PublicationController.PublicationEditions(publicationIdList, insertions, startDate)
                For Each d In editionList
                    If d > endDate Then
                        endDate = d
                    End If
                Next
            End If

        ElseIf isOnline Then
            endDate = startDate.AddDays(onlineDays)        ' simply return the start days plus number of days in app setting
        End If
        Return endDate
    End Function

    ''' <summary>
    ''' Returns a list of Booking Entries by the AdBooking Id
    ''' </summary>
    ''' <param name="adBookingId">Ad Booking Id to query the Book Entries</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetBookEntries(ByVal adBookingId As Integer) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = From en In db.BookEntries Where en.AdBookingId = adBookingId _
                        Order By en.Publication.Title _
                        Select New With {.BookEntryId = en.BookEntryId, _
                                         .EditionDate = en.EditionDate, _
                                         .AdBookingId = en.AdBookingId, _
                                         .PublicationId = en.PublicationId, _
                                         .PublicationTitle = en.Publication.Title}
            Return query.ToList
        End Using

    End Function

    ''' <summary>
    ''' Queries the Ad Booking database table and returns true if the requested booking reference already exists.
    ''' </summary>
    ''' <param name="bookingReference"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function Exists(ByVal bookingReference As String) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = From b In db.AdBookings Where b.BookReference = bookingReference Select b
            Return query.Count > 0
        End Using
    End Function

    ''' <summary>
    ''' Returns a list of invoices/transactions for a particular user after a specified date.
    ''' </summary>
    ''' <param name="userId">The user ID for the transactions to return.</param>
    ''' <param name="startDate">The date from which to retrieve the data. Ignores the transaction dates before this date.</param>
    ''' <returns>List of invoices</returns>
    ''' <remarks></remarks>
    Public Shared Function GetInvoice(ByVal userId As String, ByVal startDate As DateTime) As List(Of Invoice)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = From i In db.Invoices Where i.UserId = userId And i.TransactionDate >= startDate Select i
            Return query.ToList
        End Using
    End Function

    Public Shared Function GetTransaction(ByVal userId As String, ByVal startDate As DateTime) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spTransactionsByUser(userId, startDate).ToList
        End Using
    End Function

    Public Shared Function GetTransaction(ByVal transactionTitle As String, ByVal userId As String) As BusinessEntities.TransactionEntity
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From t In db.Transactions Where t.Title = transactionTitle And t.UserId = userId Select t)

            If query.Count > 0 Then
                Dim tran = query.FirstOrDefault
                Return New BusinessEntities.TransactionEntity With {.Amount = tran.Amount, _
                                                                    .Description = tran.Description, _
                                                                    .Title = tran.Title, _
                                                                    .TransactionDate = tran.TransactionDate, _
                                                                    .TransactionId = tran.TransactionId, _
                                                                    .TransactionType = tran.TransactionType, _
                                                                    .UserId = tran.UserId}
            Else
                GetTransaction = Nothing
            End If
        End Using
    End Function

    Public Shared Function GetTransactionItems(ByVal transactionTitle As String, ByVal userId As String) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Dim items = (From bk In db.AdBookings _
                         Join ca In db.MainCategories On ca.MainCategoryId Equals bk.MainCategoryId _
                         Join ds In db.AdDesigns On ds.AdId Equals bk.AdId _
                         Join ty In db.AdTypes On ty.AdTypeId Equals ds.AdTypeId _
                         Where bk.BookReference = transactionTitle And bk.UserId = userId _
                         Select New With {.AdDesignId = ds.AdDesignId, _
                                          .AdType = ty.Code, _
                                          .Photos = ds.AdGraphics.Count, _
                                          .Category = ca.Title})
            Return items.ToList
        End Using
    End Function

    Public Shared Function GetBookingTypeString() As String
        Select Case BookingController.BookingType
            Case BookingAction.BundledBooking
                Return "Bundled"
            Case BookingAction.Empty
                Return "Empty"
            Case BookingAction.NormalBooking
                Return "Regular"
            Case BookingAction.Reschedule
                Return "Rescheduled"
        End Select
        Return Nothing
    End Function

    Public Shared Function GetRateTypeString() As String
        Select Case BookingController.BookingType
            Case BookingAction.BundledBooking
                Return "Ratecard"
            Case BookingAction.Empty
                Return "Ratecard"
            Case BookingAction.NormalBooking
                Return "Ratecard"
            Case BookingAction.Reschedule
                Return "Ratecard"
        End Select
        Return Nothing
    End Function

#End Region

End Class
