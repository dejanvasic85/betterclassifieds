Imports System.Web
Imports BetterclassifiedsCore.DataModel
Imports System.Data.SqlClient
Imports BetterclassifiedsCore.Controller
Imports BetterclassifiedsCore.BusinessEntities
Imports BetterclassifiedsCore.Booking

Namespace BundleBooking

    ''' <summary>
    ''' Controller object used to track a bundle booking containing a print and line ad details
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BundleController
        Implements IDisposable

        Private Const _sessionBundleName As String = "sessionBundleBooking"

        Private Shared _context As BetterclassifiedsDataContext

        Private Property Context() As BetterclassifiedsDataContext
            Get
                Return _context
            End Get
            Set(ByVal value As BetterclassifiedsDataContext)
                _context = value
            End Set
        End Property

#Region "Constructor"

        Public Sub New()
            _context = BetterclassifiedsDataContext.NewContext
        End Sub

        Public Sub New(ByVal connectionString As String)
            _context.Connection.ConnectionString = connectionString
        End Sub

#End Region

#Region "BundleCart and Shared Properties"

        Public Shared ReadOnly Property BundleCart() As BundleCart
            Get
                Return HttpContext.Current.Session(_sessionBundleName)
            End Get
        End Property

        Public Shared ReadOnly Property IsActive As Boolean
            Get
                Return BundleCart IsNot Nothing
            End Get
        End Property

        Public Shared Sub StartNewBundleBooking(ByVal username As String)
            ' first cancel anything in the session when starting a new booking
            ClearBundleBooking()
            '' check if there's nothing in the session first
            If BundleCart Is Nothing Then
                HttpContext.Current.Session(_sessionBundleName) = New BundleCart(username)
                BookingController.BookingType = BookingAction.BundledBooking
            End If
        End Sub

        Public Shared Sub ClearBundleBooking()
            '' check if there's a bundle booking in the session
            If BundleCart IsNot Nothing Then
                ' cancel what ever is in the session
                HttpContext.Current.Session(_sessionBundleName) = Nothing
            End If
        End Sub

#End Region

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
                _context = Nothing
            End If
            Me.disposedValue = True
        End Sub

#Region "Booking Get Methods"

        Public Function GetPublicationEditions(ByVal publicationList As List(Of DataModel.Publication), ByVal takeAmount As Integer, ByVal startDate As DateTime) As List(Of Booking.EditionList)
            ' create the return value
            Dim list As New List(Of Booking.EditionList)
            ' loop through each paper
            For Each pub In publicationList
                ' check that it's not an online publication
                Dim pub1 = pub
                If pub.PublicationType.Code.Trim <> "ONLINE" Then

                    Dim nonPublicationDates = _context.NonPublicationDates.Where(Function(n) n.PublicationId = pub.PublicationId And n.EditionDate >= startDate).ToList()

                    ' create a new edition
                    Dim editions As New Booking.EditionList
                    With editions
                        .Title = pub.Title
                        .EditionList = _context.spPublicationEditions(startDate, pub.PublicationId) _
                            .Select(Function(sp) sp.EditionDate) _
                            .Take(takeAmount) _
                            .OrderBy(Function(sp) sp) _
                            .ToList()

                        '.EditionList = (From ed In _context.Editions _
                        '                Let npd = From _context.NonPublicationDates  _
                        '                Where ed.PublicationId = pub1.PublicationId _
                        '                And ed.EditionDate >= startDate _
                        '                Order By ed.EditionDate _
                        '                Select ed.EditionDate _
                        '                Take takeAmount) _
                        '            .ToList
                    End With
                    ' add the editionList
                    list.Add(editions)

                End If
            Next
            ' return the list
            Return list
        End Function

        Public Function GetPublicationEditions(ByVal publicationId As Integer, ByVal insertions As Integer, ByVal startDate As DateTime) As List(Of Nullable(Of Date))
            ' create the return value
            Dim dateList = (From ed In _context.Editions _
                    Where ed.PublicationId = publicationId _
                    And ed.EditionDate >= startDate _
                    Order By ed.EditionDate _
                    Select ed.EditionDate Take insertions).ToList

            Dim nonPublicationDates = _context.NonPublicationDates.Where(Function(n) n.PublicationId = publicationId And n.EditionDate >= dateList.First() And n.EditionDate <= dateList.Last()).ToList()
            For Each npd In nonPublicationDates
                dateList.Remove(npd.EditionDate)
            Next

            Return dateList
        End Function

        Public Function CheckAllPublicationsExist(ByVal existingPublications As List(Of DataModel.Publication), ByVal newPubIdList As List(Of Integer), ByVal includeOnline As Boolean) As Boolean
            ' loop through each id and ensure that its on the list (mark the flag)
            Dim exists As Boolean = True
            ' check if we need to remove the online paper
            If includeOnline = False Then
                ' remove the online paper from the existing list (we do not count that here)
                existingPublications.Remove(existingPublications.Where(Function(i) i.PublicationType.Code.Trim = "ONLINE").FirstOrDefault)
            End If

            ' loop through each already selected publication
            For Each publication In existingPublications
                ' ensure that the new papers selected contains an already selected paper
                exists = newPubIdList.Contains(publication.PublicationId)
                If exists = False Then
                    ' it does not exist - return false
                    Exit For
                End If
            Next
            Return exists
        End Function

        Public Sub RemoveSchedulingDetailsFromSession()
            ' simply mark all the properties related to scheduling to null or 0
            BundleCart.StartDate = Nothing
            BundleCart.EndDate = Nothing
            BundleCart.Insertions = 0
        End Sub

        Public Function GetBindableBookCart(ByVal bundle As BundleCart) As List(Of BundleCart)
            ' create a new list to return
            Dim list As New List(Of BundleCart)
            ' add the bundle into the list
            list.Add(bundle)
            Return list
        End Function

        Public Function GetPublicationAdTypes(ByVal publicationId As Integer) As List(Of PublicationAdType)
            Return _context.PublicationAdTypes.Where(Function(pat) pat.PublicationId = publicationId).ToList()
        End Function

        Public Function GetOnlineAdTypeTagForBooking() As String
            If BundleCart IsNot Nothing And BundleCart.MainSubCategory IsNot Nothing Then
                Return IIf(Not String.IsNullOrEmpty(BundleCart.MainSubCategory.OnlineAdTag), BundleCart.MainSubCategory.OnlineAdTag, BundleCart.MainParentCategory.OnlineAdTag)
            End If
            Return String.Empty
        End Function

#End Region

#Region "Booking Set Methods"

        Public Sub SetPublication(ByVal publicationIdList As List(Of Integer), ByVal includeOnline As Boolean)
            If BundleCart IsNot Nothing Then
                ' check if there's no publications in the list
                If BundleCart.PublicationList Is Nothing Then
                    BundleCart.PublicationList = New List(Of DataModel.Publication) ' create a new list
                Else
                    ' otherwise, we will check if they selected the same publications as last time 
                    If CheckAllPublicationsExist(BundleCart.PublicationList, publicationIdList, False) = False Then
                        ' then we remove any scheduling details because the user changed their pubication selections
                        RemoveSchedulingDetailsFromSession()
                    End If
                End If

                ' clear anything that's in the current list
                BundleCart.PublicationList.Clear()

                ' add each publication object into the list after getting all data from database
                For Each id As Integer In publicationIdList
                    Dim pubId = id
                    BundleCart.PublicationList.Add(_context.Publications.Where(Function(i) i.PublicationId = pubId).FirstOrDefault)
                Next

                ' check if we also need to add the online paper
                If includeOnline Then
                    BundleCart.PublicationList.Add(_context.Publications.Where(Function(i) i.PublicationType.Code.Trim = "ONLINE").FirstOrDefault)
                End If
            End If
        End Sub

        Public Sub SetCategory(ByVal parentCategoryId As Integer, ByVal subCategoryId As Integer)
            If BundleCart IsNot Nothing Then
                ' set the main category object into session
                BundleCart.MainParentCategory = _context.MainCategories.Where((Function(i) i.MainCategoryId = parentCategoryId)).FirstOrDefault
                ' set the main sub category object into session 
                BundleCart.MainSubCategory = _context.MainCategories.Where((Function(i) i.MainCategoryId = subCategoryId)).FirstOrDefault
            End If
        End Sub

        Public Sub SetBookingReference(ByVal subCategoryId As Integer)
            If BundleCart IsNot Nothing Then
                If BundleCart.BookReference = String.Empty Then
                    ' call the general routine method to set a new reference
                    BundleCart.BookReference = GeneralRoutine.GetBookingReference(subCategoryId, True)
                End If
            End If
        End Sub

        'Public Sub SetBookingSinglePrice(ByVal singlePrice As Decimal)
        '    If BundleCart IsNot Nothing Then
        '        ' simply set the price property in the session object
        '        BundleCart.SingleEditionPrice = singlePrice
        '    End If
        'End Sub

        Public Sub SetLineAdDetails(ByVal adHeader As String, ByVal adText As String, ByVal useBoldHeading As Boolean)
            SetLineAdDetails(adHeader, adText, useBoldHeading, False, False, Nothing, False, Nothing, False, Nothing)
        End Sub

        Public Sub SetLineAdDetails(ByVal adHeader As String, ByVal adText As String, ByVal useBoldHeading As Boolean, _
                                    ByVal isSuperBoldHeading As Boolean, _
                                    ByVal isColourHeading As Boolean, ByVal headerColourCode As String, _
                                    ByVal isColourBorder As Boolean, ByVal borderColourCode As String, _
                                    ByVal isColourBackground As Boolean, ByVal backgroundColourCode As String)

            If BundleCart IsNot Nothing Then
                ' store the required ad details into the session
                Dim lineAd As New DataModel.LineAd With {.AdText = adText, _
                                                         .AdHeader = adHeader, _
                                                         .NumOfWords = GeneralRoutine.LineAdWordCount(adText), _
                                                         .UseBoldHeader = useBoldHeading, _
                                                         .IsSuperBoldHeading = isSuperBoldHeading, _
                                                         .IsColourBoldHeading = isColourHeading, _
                                                         .BoldHeadingColourCode = headerColourCode, _
                                                         .IsColourBorder = isColourBorder, _
                                                         .BorderColourCode = borderColourCode, _
                                                         .IsColourBackground = isColourBackground, _
                                                         .BackgroundColourCode = backgroundColourCode}
                lineAd.UsePhoto = BundleCart.LineAdGraphic IsNot Nothing
                BundleCart.LineAd = lineAd
            End If
        End Sub

        Public Sub SetLineAdGraphic(ByVal docId As String)
            Dim graphic As New DataModel.AdGraphic With {.DocumentID = docId}
            ' save the ad graphic for the line ad in the session
            BundleCart.LineAdGraphic = graphic
            BundleCart.LineAdIsMainPhoto = True
        End Sub

        Public Sub SetOnlineAdDetails(ByVal heading As String, ByVal description As String, ByVal html As String, ByVal price As Decimal, ByVal locationId As Integer, ByVal locationAreaId As Integer, _
                                      ByVal contactName As String, ByVal contactPhone As String, ByVal contactEmail As String)
            ' ensure that the session object is not null
            If BundleCart IsNot Nothing Then
                Dim onlineAd As New DataModel.OnlineAd With {.Heading = heading, _
                                                             .Description = description, _
                                                             .HtmlText = html, _
                                                             .Price = price, _
                                                             .LocationId = locationId, _
                                                             .LocationAreaId = locationAreaId, _
                                                             .ContactName = contactName, _
                                                             .ContactEmail = contactEmail, _
                                                             .ContactPhone = contactPhone, _
                                                             .OnlineAdTag = GetOnlineAdTypeTagForBooking()}
                ' save the ad in the session
                BundleCart.OnlineAd = onlineAd
            End If
        End Sub

        Public Sub SetTutorAdDetails(ByVal ageGroupMax As Nullable(Of Integer), ByVal ageGroupMin As Nullable(Of Integer), _
                                     ByVal level As String, ByVal objective As String, ByVal pricingOption As String, _
                                     ByVal subjects As String, ByVal travelOption As String, ByVal whatToBring As String)
            If BundleCart IsNot Nothing Then
                BundleCart.TutorAd = New DataModel.TutorAd With { _
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

        Public Sub SetOnlineAdGraphics(ByVal imageList As List(Of String))
            Dim onlineGraphics As New List(Of DataModel.AdGraphic)
            For Each imageGuid In imageList
                onlineGraphics.Add(New DataModel.AdGraphic With {.DocumentID = imageGuid})
            Next
            BundleCart.OnlineAdGraphics = onlineGraphics
        End Sub

        Public Sub RemoveOnlineAdGraphic(ByVal imageGuid As String)
            Dim graphic = BundleCart.OnlineAdGraphics.Where(Function(i) i.DocumentID = imageGuid).FirstOrDefault
            If graphic IsNot Nothing Then
                BundleCart.OnlineAdGraphics.Remove(graphic)
            End If
        End Sub

        Public Function GetOnlineGraphicsIdList() As List(Of String)
            Dim list As New List(Of String)
            For Each g In BundleCart.OnlineAdGraphics
                list.Add(g.DocumentID)
            Next
            Return list
        End Function

        Public Sub SetStartDate(ByVal startDate As DateTime)
            ' ensure that the session object is not null
            If BundleCart IsNot Nothing Then
                ' simply set the start date
                BundleCart.StartDate = startDate
            End If
        End Sub

        Public Sub SetFirstEdition(ByVal firstEdition As DateTime)
            If BundleCart IsNot Nothing Then
                BundleCart.FirstEdition = firstEdition
            End If
        End Sub

        Public Sub SetInsertions(ByVal Insertions As Integer)
            ' ensure that the session object is not null
            If BundleCart IsNot Nothing Then
                ' simply set the start date
                BundleCart.Insertions = Insertions
            End If
        End Sub

        Public Sub SetEndDate(ByVal endDate As DateTime)
            ' ensure that the session object is not null
            If BundleCart IsNot Nothing Then
                ' simply set
                BundleCart.EndDate = endDate
            End If
        End Sub

        Public Sub SetEditionCount(ByVal editionCount As Integer)
            ' ensure that the session object is not null
            If BundleCart IsNot Nothing Then
                ' simply set the value
                BundleCart.Insertions = editionCount
            End If
        End Sub

        Public Sub SetTotalBookingPrice()
            ' ensure that the session object is not null
            If BundleCart IsNot Nothing Then
                With BundleCart
                    ' simply set the value
                    .TotalPrice = .BookingOrderPrice.CalculateTotalPrice(BundleCart)
                End With
            End If
        End Sub

        Public Sub SetBookEntries()

            ' make sure there's something in the session before we go on
            If BundleCart IsNot Nothing Then

                With BundleCart
                    If .BookEntries Is Nothing Then
                        .BookEntries = New List(Of BookEntry)
                    End If
                    .BookEntries.Clear()
                    Dim selectedDate = .FirstEdition
                    Dim startDate = .StartDate
                    For Each publicationRate In .BookingOrderPrice.PublicationPriceList
                        If publicationRate.AdType = BookingAdType.LineAd Then
                            'calculate the single edition ad price
                            Dim singleEditionAdPrice = publicationRate.CalculateLineAdPrice(1, .LineAdTextWordCount, .LineAdIsNormalAdHeading, _
                                                                                .LineAdIsSuperBoldHeading, .LineAdIsMainPhoto, _
                                                                                .LineAdIsSecondPhoto, .LineAdIsColourHeading, _
                                                                                .LineAdIsColourBorder, .LineAdIsColourBackground)
                            Dim publicationBookingPrice = singleEditionAdPrice * .EditionCount
                            Dim editions = GetPublicationEditions(publicationRate.PublicationId, .EditionCount, selectedDate)
                            For Each ed In editions
                                BundleCart.BookEntries.Add(New BookEntry With {.BaseRateId = publicationRate.RateCardId, _
                                                                               .EditionDate = ed, _
                                                                               .PublicationId = publicationRate.PublicationId, _
                                                                               .EditionAdPrice = singleEditionAdPrice, _
                                                                               .PublicationPrice = publicationBookingPrice, _
                                                                               .RateType = BookingController.GetRateTypeString})
                            Next
                        ElseIf publicationRate.AdType = BookingAdType.OnlineAd Then
                            ' create a single book entry for the online ad every time since scheduling doesn't apply here
                            BundleCart.BookEntries.Add(New BookEntry With {.BaseRateId = publicationRate.RateCardId, _
                                                                              .PublicationId = publicationRate.PublicationId, _
                                                                              .EditionDate = startDate, _
                                                                              .EditionAdPrice = publicationRate.CalculateOnlineAdPrice, _
                                                                              .PublicationPrice = publicationRate.CalculateOnlineAdPrice, _
                                                                              .RateType = BookingController.GetRateTypeString})
                        End If
                    Next
                End With
            End If
        End Sub

        ' Call this method when Publications and Category are selected
        Public Sub SetBookingOrderPrices(ByVal mainCategoryId As Integer, ByVal bookingPublications As List(Of BookingPublicationEntity))

            ' Fetch all the required ratecards for the publications selected and category
            If BundleCart IsNot Nothing Then

                ' Fetch the name of the selected main category
                Dim mainCategoryName = _context.MainCategories.Where(Function(m) m.MainCategoryId = mainCategoryId).Single().Title
                BundleCart.BookingOrderPrice = New BookingOrderPrice With {.MainCategoryId = mainCategoryId, _
                                                                           .MainCategoryName = mainCategoryName}

                For Each bk In bookingPublications

                    ' Set up all the publication costs
                    ' Call the stored procedure to fetch data via stored proc
                    Dim publicationRate = _context.usp_PublicationRateCard__Select(bk.PublicationId, mainCategoryId, bk.AdType).FirstOrDefault

                    Dim bookingPublicationPriceEntity As New BookingPublicationPriceEntity
                    With bookingPublicationPriceEntity
                        .RateCardId = publicationRate.RatecardId
                        .PublicationId = publicationRate.PublicationId
                        .AdType = bk.AdType
                        .PublicationName = publicationRate.PublicationName
                        .PublicationCategoryName = publicationRate.PublicationCategoryName
                        .PublicationCategoryId = publicationRate.PublicationCategoryId
                        .MinimumCharge = publicationRate.MinCharge
                        .MaximumCharge = publicationRate.MaxCharge
                        .LineAdFreeWordCount = publicationRate.MeasureUnitLimit
                        .LineBoldHeader = publicationRate.BoldHeading
                        .LineColourBackground = publicationRate.LineAdColourBackground
                        .LineColourBorder = publicationRate.LineAdColourBorder
                        .LineColourHeader = publicationRate.LineAdColourHeading
                        .LineMainPhoto = publicationRate.PhotoCharge
                        .LineRatePerWord = publicationRate.RatePerMeasureUnit
                        .LineSecondaryPhoto = publicationRate.LineAdExtraImage
                        .LineSuperBoldHeader = publicationRate.LineAdSuperBoldHeading
                    End With


                    ' Add the publication Price information in to the session
                    BundleCart.BookingOrderPrice.PublicationPriceList.Add(bookingPublicationPriceEntity)
                Next
            End If
        End Sub
#End Region

#Region "Save Temp Booking"

        Public Shared Sub SaveTempAdBundleRecord(ByVal Id As String, ByVal totalPrice As Decimal, ByVal sessionId As String, ByVal UserId As String, ByVal recordValue As BundleCart, ByVal transType As TransactionType)
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
            GeneralRoutine.PlaceBundledAd(recordValue, transType, BookingStatus.UNPAID)
        End Sub

#End Region

#Region " IDisposable Support "
        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace
