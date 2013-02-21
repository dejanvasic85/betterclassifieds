Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore.Controller

Public Enum SystemAdType
    LINE = 1
    ONLINE = 2
End Enum

''' <summary>
''' Provides the general data layer methods required to communicate with the backend Database.
''' </summary>
''' <remarks>
''' 
''' </remarks>
Public Class AdController

#Region "Create"

    Public Shared Sub CreateAdGraphics(ByVal adDesignId As Integer, ByVal documentList As List(Of String))
        ' First Call method to remove the Graphics
        DeleteAdGraphics(adDesignId)

        ' Proceed to save the new image list
        Using db = BetterclassifiedsDataContext.NewContext
            For Each docId As String In documentList
                Dim newGraphic As New AdGraphic() With {.AdDesignId = adDesignId, .DocumentID = docId, .ModifiedDate = DateTime.Now}
                db.AdGraphics.InsertOnSubmit(newGraphic)
            Next

            Dim adTypeId = (From ds In db.AdDesigns Where ds.AdDesignId = adDesignId Select ds).Single.AdTypeId

            If adTypeId = SystemAdType.LINE Then
                ' Update the line ad so that it doesn't contain the Flag to contain an image
                Dim lineAd = (From ln In db.LineAds Where ln.AdDesignId = adDesignId Select ln).Single
                lineAd.UsePhoto = True
            End If

            db.SubmitChanges()
        End Using
    End Sub

#End Region

#Region "Retrieve"

    Public Shared Function TempRecordExist(ByVal bookingReference As String) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim tempRec = From t In db.TempBookingRecords Where t.AdReferenceId = bookingReference Select t
            If tempRec.Count <> 0 Then
                Return True
            End If
            Return False
        End Using
    End Function

    Public Shared Function GetAllAdTypes() As List(Of AdType)
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim types = From t In db.AdTypes Where t.Active = True Select t
                Return types.ToList
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAdType(ByVal adTypeId As Integer) As AdType
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From t In db.AdTypes Where t.AdTypeId = adTypeId Select t
                Return query.Single
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAdTypeByCode(ByVal typeCode As SystemAdType) As AdType
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.AdTypes.Where(Function(i) i.Code = typeCode.ToString()).FirstOrDefault
            End Using
        Catch ex As Exception
            Throw ex ' todo log exception
        End Try
    End Function

    Public Shared Function GetAdDesignById(ByVal adDesignId As Integer) As AdDesign
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return (From d In db.AdDesigns Where d.AdDesignId = adDesignId Select d).FirstOrDefault
            End Using
        Catch ex As Exception
            ' todo log exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAdDesignsByBooking(ByVal bookingId As Integer) As IList
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = (From bk In db.AdBookings _
                            Join de In db.AdDesigns On de.AdId Equals bk.AdId _
                            Where bk.AdBookingId = bookingId _
                            Select de)
                Return query.ToList
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetLineAd(ByVal adDesignId As Integer) As LineAd
        Using db = BetterclassifiedsDataContext.NewContext
            Dim lineAd As LineAd
            ' check if this ad design ID refers to the online version
            ' if so, then we get the ad, and find the Line Ad Version
            Dim design = db.AdDesigns.Where(Function(i) i.AdDesignId = adDesignId).FirstOrDefault
            Dim lineDesignId As Integer

            If design.AdType.Code.Trim = SystemAdType.ONLINE.ToString Then
                Dim query = db.AdDesigns.Where(Function(i) i.AdId = design.AdId _
                                                      And i.AdType.Code.Trim = SystemAdType.LINE.ToString).FirstOrDefault
                If query Is Nothing Then
                    Return Nothing
                End If
                lineDesignId = query.AdDesignId
            Else
                lineDesignId = adDesignId
            End If

            Return (From ln In db.LineAds Where ln.AdDesignId = lineDesignId Select ln).FirstOrDefault
        End Using
    End Function

    Public Shared Function GetLineAdById(ByVal lineAdId As Integer) As DataModel.LineAd
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.LineAds.Where(Function(i) i.LineAdId = lineAdId).FirstOrDefault
        End Using
    End Function

    Public Shared Function GetLineAdByBookingId(ByVal adBookingId As Integer) As LineAd
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From bk In db.AdBookings _
                        Where bk.AdBookingId = adBookingId _
                        Join ds In db.AdDesigns On ds.AdId Equals bk.AdId _
                        Join ln In db.LineAds On ln.AdDesignId Equals ds.AdDesignId _
                        Select ln).FirstOrDefault
            Return query
        End Using
    End Function

    Public Shared Function GetLineAds(ByVal publicationId As Integer, ByVal editionDate As DateTime) As IList
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From lineAd In db.LineAds _
                            Join design In db.AdDesigns On design.AdDesignId Equals lineAd.AdDesignId _
                            Join type In db.AdTypes On type.AdTypeId Equals design.AdTypeId _
                            Join book In db.AdBookings On book.AdId Equals design.AdId _
                            Join entry In db.BookEntries On entry.AdBookingId Equals book.AdBookingId _
                            Join cat In db.MainCategories On book.MainCategoryId Equals cat.MainCategoryId _
                            Where entry.PublicationId = publicationId And _
                            entry.EditionDate = editionDate And _
                            type.Code = SystemAdType.LINE.ToString And _
                            book.BookingStatus = BookingStatus.BOOKED _
                            Order By cat.Title _
                            Select New With {.AdDesignId = design.AdDesignId, _
                                             .LineAdId = lineAd.LineAdId, _
                                             .AdText = lineAd.AdText.Substring(0, 25) + "...", _
                                             .NumOfWords = lineAd.NumOfWords, _
                                             .UsePhoto = lineAd.UsePhoto, _
                                             .UseBoldHeader = lineAd.UseBoldHeader, _
                                             .Username = book.UserId, _
                                             .Category = cat.Title, _
                                             .CategoryId = cat.MainCategoryId}
                Return query.ToList
            End Using
        Catch ex As Exception
            Throw ex
            ' todo log exception
        End Try
    End Function

    Public Shared Function GetExportItems(ByVal publicationId As Integer, ByVal editionDate As DateTime) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spLineAdExportList(publicationId, editionDate).ToList
        End Using
    End Function

    ''' <summary>
    ''' Returns an OnlineAd Object by the Ad Design ID
    ''' </summary>
    Public Shared Function GetOnlineAd(ByVal adDesignId As Integer) As OnlineAd
        Using db = BetterclassifiedsDataContext.NewContext
            Return (From o In db.OnlineAds Where o.AdDesignId = adDesignId).FirstOrDefault
        End Using
    End Function

    Public Shared Function GetOnlineAdByAdBooking(ByVal adBookingId As Integer) As OnlineAd
        Using db = BetterclassifiedsDataContext.NewContext
            Dim onlineAd = (From o In db.OnlineAds _
                            Join d In db.AdDesigns On o.AdDesignId Equals d.AdDesignId _
                            Join b In db.AdBookings On b.AdId Equals d.AdId _
                            Where b.AdBookingId = adBookingId _
                            Select o).FirstOrDefault
            Return onlineAd
        End Using
    End Function

    Public Shared Function GetOnlineAdListByUser(ByVal userId As String) As List(Of OnlineAd)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = From bk In db.AdBookings _
                        Join ds In db.AdDesigns On ds.AdId Equals bk.AdId _
                        Join ad In db.OnlineAds On ad.AdDesignId Equals ds.AdDesignId _
                        Where bk.UserId = userId _
                        Select ad
            Return query.ToList
        End Using
    End Function

    Public Shared Function GetOnlineAdByLineAd(ByVal adDesignId As Integer) As DataModel.OnlineAd
        Using db = BetterclassifiedsDataContext.NewContext
            Dim onlineAd = db.spOnlineAdSelectByLineAdDesign(adDesignId).FirstOrDefault

            If onlineAd IsNot Nothing Then
                Return New DataModel.OnlineAd With {.AdDesignId = onlineAd.AdDesignId, _
                                                    .ContactName = onlineAd.ContactName, _
                                                    .ContactType = onlineAd.ContactType, _
                                                    .ContactValue = onlineAd.ContactValue, _
                                                    .Description = onlineAd.Description, _
                                                    .Heading = onlineAd.Description, _
                                                    .HtmlText = onlineAd.HtmlText, _
                                                    .LocationId = onlineAd.LocationId, _
                                                    .LocationAreaId = onlineAd.LocationAreaId, _
                                                    .NumOfViews = onlineAd.NumOfViews, _
                                                    .OnlineAdId = onlineAd.OnlineAdId, _
                                                    .Price = onlineAd.Price}
            End If
        End Using
        Return Nothing
    End Function

    Public Shared Function GetOnlineAdHtml(ByVal onlineAdId As Integer) As String
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.OnlineAds.Where(Function(i) i.OnlineAdId = onlineAdId).Single.HtmlText
        End Using
    End Function

    Public Shared Function GetOnlineAdById(ByVal onlineAdId As Integer) As IList
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From ad In db.OnlineAds _
                            Join location In db.Locations On location.LocationId Equals ad.LocationId _
                            Join locationArea In db.LocationAreas On locationArea.LocationAreaId Equals ad.LocationAreaId _
                            Where ad.OnlineAdId = onlineAdId _
                            Select New With {.OnlineAdID = ad.OnlineAdId, .AdDesignId = ad.AdDesignId, .Heading = ad.Heading, _
                                             .Description = ad.Description, .Price = ad.Price, .Location = location.Title, _
                                             .LocationArea = locationArea.Title, .ContactName = ad.ContactName, _
                                             .ContactType = ad.ContactType, .ContactValue = ad.ContactValue, _
                                             .NumOfViews = ad.NumOfViews}
                Return query.ToList
            End Using
        Catch ex As Exception
            Throw ex 'todo catch exception
        End Try
    End Function

    Public Shared Function GetOnlineAdEntityByDesign(ByVal adDesignId As Integer, ByVal isPreview As Boolean) As BusinessEntities.OnlineAdEntity
        Using db = BetterclassifiedsDataContext.NewContext
            Dim ad = (From o In db.OnlineAds _
                      Join ds In db.AdDesigns On ds.AdDesignId Equals o.AdDesignId _
                      Join bk In db.AdBookings On bk.AdId Equals ds.AdId _
                      Where o.AdDesignId = adDesignId _
                      And ((isPreview = False And bk.StartDate <= DateTime.Now And bk.EndDate > DateTime.Now _
                            And bk.BookingStatus = BookingStatus.BOOKED) _
                           Or (isPreview = True)) _
                      Select New BusinessEntities.OnlineAdEntity With {.OnlineAdId = o.OnlineAdId, _
                                                                       .AdDesignId = o.AdDesignId, _
                                                                       .Heading = o.Heading, _
                                                                       .Description = o.Description, _
                                                                       .HtmlText = o.HtmlText, _
                                                                       .Price = o.Price, _
                                                                       .LocationValue = o.Location.Title, _
                                                                       .AreaValue = o.LocationArea.Title, _
                                                                       .ContactName = o.ContactName, _
                                                                       .ContactType = o.ContactType, _
                                                                       .ContactValue = o.ContactValue, _
                                                                       .NumOfViews = o.NumOfViews, _
                                                                       .DatePosted = bk.StartDate, _
                                                                       .BookingReference = bk.BookReference, _
                                                                       .ImageList = GetAdGraphicDocuments(o.AdDesignId), _
                                                                       .SubCategory = bk.MainCategory, _
                                                                       .ParentCategory = (From mc In db.MainCategories Where mc.MainCategoryId = bk.MainCategory.ParentId Select mc).Single})

            If ad.Count > 0 Then
                ' increase the number of views for this ad if this is not a preview only.
                If Not isPreview Then
                    Dim onlineAdId = ad.FirstOrDefault.OnlineAdId
                    IncreaseOnlineAdViews(onlineAdId, db)
                End If
                Return ad.FirstOrDefault
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Shared Function GetOnlineAdEntityById(ByVal id As Integer, ByVal isPreview As Boolean) As BusinessEntities.OnlineAdEntity
        Using db = BetterclassifiedsDataContext.NewContext
            Dim ad = (From o In db.OnlineAds _
                      Join des In db.AdDesigns On des.AdDesignId Equals o.AdDesignId _
                      Join bk In db.AdBookings On bk.AdId Equals des.AdId _
                      Where o.OnlineAdId = id _
                      And ((isPreview = False And bk.StartDate <= DateTime.Now And bk.EndDate > DateTime.Now _
                            And bk.BookingStatus = BookingStatus.BOOKED) _
                       Or (isPreview = True)) _
                      Select New BusinessEntities.OnlineAdEntity With {.OnlineAdId = o.OnlineAdId, _
                                                                       .AdDesignId = o.AdDesignId, _
                                                                       .Heading = o.Heading, _
                                                                       .Description = o.Description, _
                                                                       .HtmlText = o.HtmlText, _
                                                                       .Price = o.Price, _
                                                                       .LocationValue = o.Location.Title, _
                                                                       .AreaValue = o.LocationArea.Title, _
                                                                       .ContactName = o.ContactName, _
                                                                       .ContactType = o.ContactType, _
                                                                       .ContactValue = o.ContactValue, _
                                                                       .NumOfViews = o.NumOfViews, _
                                                                       .DatePosted = bk.StartDate, _
                                                                       .BookingReference = bk.BookReference, _
                                                                       .ImageList = GetAdGraphicDocuments(o.AdDesignId), _
                                                                       .SubCategory = bk.MainCategory, _
                                                                       .ParentCategory = (From mc In db.MainCategories Where mc.MainCategoryId = bk.MainCategory.ParentId Select mc).Single})

            If ad.Count > 0 Then
                ' increase the number of views for this ad if this is not a preview only.
                If Not isPreview Then
                    Dim onlineAdId = ad.FirstOrDefault.OnlineAdId
                    IncreaseOnlineAdViews(onlineAdId, db)
                End If
                Return ad.FirstOrDefault
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Shared Function GetOnlineAdEntityByReference(ByVal bookRef As String, ByVal isPreview As Boolean) As BusinessEntities.OnlineAdEntity
        Using db = BetterclassifiedsDataContext.NewContext
            Dim ad = (From o In db.OnlineAds _
                      Join des In db.AdDesigns On des.AdDesignId Equals o.AdDesignId _
                      Join book In db.AdBookings On book.AdId Equals des.AdId _
                      Where book.BookReference = bookRef _
                      And ((isPreview = False _
                            And book.StartDate <= DateTime.Now _
                            And book.EndDate > DateTime.Now _
                            And book.BookingStatus = BookingStatus.BOOKED _
                       Or (isPreview = True))) _
                      Select New BusinessEntities.OnlineAdEntity With {.OnlineAdId = o.OnlineAdId, _
                                                                       .AdDesignId = o.AdDesignId, _
                                                                       .Heading = o.Heading, _
                                                                       .Description = o.Description, _
                                                                       .HtmlText = o.HtmlText, _
                                                                       .Price = o.Price, _
                                                                       .LocationValue = o.Location.Title, _
                                                                       .AreaValue = o.LocationArea.Title, _
                                                                       .ContactName = o.ContactName, _
                                                                       .ContactType = o.ContactType, _
                                                                       .ContactValue = o.ContactValue, _
                                                                       .NumOfViews = o.NumOfViews, _
                                                                       .DatePosted = book.StartDate, _
                                                                       .BookingReference = book.BookReference, _
                                                                       .ImageList = GetAdGraphicDocuments(o.AdDesignId), _
                                                                       .SubCategory = book.MainCategory, _
                                                                       .ParentCategory = (From mc In db.MainCategories Where mc.MainCategoryId = book.MainCategory.ParentId Select mc).Single})
            If ad.Count > 0 Then
                ' increase the number of views for this ad if this is not a preview only.
                If Not isPreview Then
                    Dim onlineAdId = ad.FirstOrDefault.OnlineAdId
                    IncreaseOnlineAdViews(onlineAdId, db)
                End If

                Return ad.FirstOrDefault
            Else
                Return Nothing
            End If
        End Using
    End Function

    Public Shared Function GetAdGraphics(ByVal adDesignId As Integer) As List(Of AdGraphic)
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return (From g In db.AdGraphics Where g.AdDesignId = adDesignId).ToList
            End Using
        Catch ex As Exception
            ' todo log exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAdGraphicDocuments(ByVal adDesignId As Integer) As List(Of String)
        Try
            Dim imageList As New List(Of String)
            Using db = BetterclassifiedsDataContext.NewContext
                Dim list = (From g In db.AdGraphics Where g.AdDesignId = adDesignId Select g).ToList
                For Each image In list
                    imageList.Add(image.DocumentID)
                Next
            End Using
            Return imageList
        Catch ex As Exception
            ' todo log exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Returns the first or default Ad Graphic object from the Database for the Ad Design Id
    ''' </summary>
    ''' <param name="adDesignId">ID for the Ad Design required to query the Ad Graphic table.</param>
    ''' <returns>DataModel.AdGraphic object</returns>
    ''' <remarks></remarks>
    Public Shared Function GetLineAdGraphic(ByVal adDesignId As Integer) As DataModel.AdGraphic
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.AdGraphics.Where(Function(i) i.AdDesignId = adDesignId).FirstOrDefault
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function LineAdByBookingId(ByVal adBookingId As Integer) As LineAd
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = (From line In db.LineAds _
                            Join design In db.AdDesigns On design.AdDesignId Equals line.AdDesignId _
                            Join book In db.AdBookings On book.AdId Equals design.AdId _
                            Where book.AdBookingId = adBookingId _
                            Select line)

                If query.Count > 0 Then
                    Return query.FirstOrDefault
                Else
                    Return Nothing
                End If

            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function LineAdGraphicDocumentId(ByVal lineAdId As Integer) As String
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From graphics In db.AdGraphics _
                            Join design In db.AdDesigns On design.AdDesignId Equals graphics.AdDesignId _
                            Join lineAd In db.LineAds On lineAd.AdDesignId Equals design.AdDesignId _
                            Where lineAd.LineAdId = lineAdId Select graphics

                If query.Count > 0 Then
                    Return query.FirstOrDefault.DocumentID
                Else
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function OnlineAdByBookingId(ByVal adBookingId As Integer) As OnlineAd
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = (From onl In db.OnlineAds _
                             Join design In db.AdDesigns On design.AdDesignId Equals onl.AdDesignId _
                             Join book In db.AdBookings On book.AdId Equals design.AdId _
                             Where book.AdBookingId = adBookingId _
                             Select onl)
                If query.Count > 0 Then
                    Return query.FirstOrDefault
                Else
                    Return Nothing
                End If
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function OnlineGraphicsByBookingId(ByVal adBookingId As Integer) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From gr In db.AdGraphics _
                         Join ds In db.AdDesigns On ds.AdDesignId Equals gr.AdDesignId _
                         Join bk In db.AdBookings On bk.AdId Equals ds.AdId _
                         Join at In db.AdTypes On at.AdTypeId Equals ds.AdTypeId _
                         Where bk.AdBookingId = adBookingId And at.Code.Trim = SystemAdType.ONLINE.ToString.ToUpper _
                         Select gr).ToList
            Return query
        End Using
    End Function

    ''' <summary>
    ''' Returns a list of Online Ad and Booking details for online designs that have a pending status.
    ''' </summary>
    Public Shared Function GetPendingOnlineAds() As IList
        Try
            Using db = BetterclassifiedsDataContext.NewContext

                Dim query = (From ad In db.OnlineAds _
                             Join design In db.AdDesigns On design.AdDesignId Equals ad.AdDesignId _
                             Join book In db.AdBookings On book.AdId Equals design.AdId _
                             Join cat In db.MainCategories On book.MainCategoryId Equals cat.MainCategoryId _
                             Where design.Status = AdDesignStatus.Pending _
                             And book.EndDate > DateTime.Today _
                             And book.BookingStatus = Controller.BookingStatus.BOOKED _
                             Select New With {.OnlineAdId = ad.OnlineAdId, _
                                              .AdDesignId = design.AdDesignId, _
                                              .Heading = ad.Heading, _
                                              .Description = ad.Description, _
                                              .NumOfViews = ad.NumOfViews, _
                                              .BookingReference = book.BookReference, _
                                              .UserId = book.UserId, _
                                              .MainCategory = cat.Title}).ToList
                Return query
            End Using
        Catch ex As Exception
            Throw ex ' todo log exception
        End Try
    End Function

    Public Shared Function GetAdTypeByPublication(ByVal publicationId As Integer)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim adType As DataModel.AdType = Nothing

            Dim pub = db.Publications.Where(Function(i) i.PublicationId = publicationId).Single
            If pub.PublicationType.Code.Trim.ToLower = "online" Then
                adType = (From a In db.AdTypes.Where(Function(i) i.Code = "ONLINE")).Single
            ElseIf pub.PublicationType.Code.Trim.ToLower = "mag" Or pub.PublicationType.Code.Trim.ToLower = "news" Then
                adType = (From a In db.AdTypes.Where(Function(i) i.Code = "LINE")).Single
            End If

            Return adType
        End Using
    End Function

    Public Shared Function GetAdBookingByDesignId(ByVal adDesignId As Integer) As DataModel.AdBooking
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From bk In db.AdBookings _
                         Join ds In db.AdDesigns On ds.AdId Equals bk.AdId _
                         Where ds.AdDesignId = adDesignId _
                         Select bk).FirstOrDefault
            Return query
        End Using
    End Function

#End Region

#Region "Update"

    Public Shared Function UpdateLineAdData(ByVal adDesignId As Integer, ByVal adHeader As String, ByVal adText As String) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim lineAd = (From l In db.LineAds Where l.AdDesignId = adDesignId Select l).Single

            With lineAd
                .AdHeader = adHeader
                .AdText = adText
            End With
            db.SubmitChanges()
        End Using
        Return True
    End Function

    Public Shared Function UpdateOnlineAd(ByVal adDesignId As Integer, ByVal header As String, ByVal description As String, ByVal html As String, ByVal price As Decimal, _
        ByVal locationId As Integer, ByVal locationAreaId As Integer, ByVal contactName As String, ByVal contactType As String, ByVal contactValue As String, ByVal images As List(Of String)) As Boolean

        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim onlineAd = (From o In db.OnlineAds Where o.AdDesignId = adDesignId Select o).Single

                With onlineAd
                    .Heading = header
                    .Description = description
                    .HtmlText = html
                    .Price = price
                    If locationId > 0 Then
                        .LocationId = locationId
                    End If
                    If locationAreaId > 0 Then
                        .LocationAreaId = locationAreaId
                    End If
                    .ContactName = contactName
                    .ContactType = contactType
                    .ContactValue = contactValue
                End With

                ' delete the graphics if we have any
                Dim graphics = (From g In db.AdGraphics Where g.AdDesignId = adDesignId Select g).ToList

                If (graphics.Count > 0) Then
                    For Each gr In graphics
                        db.AdGraphics.DeleteOnSubmit(gr)
                    Next
                End If

                ' now we add the graphics
                If images.Count > 0 Then
                    For Each id In images
                        Dim gr As New AdGraphic With {.DocumentID = id, .ModifiedDate = DateTime.Now}
                        onlineAd.AdDesign.AdGraphics.Add(gr)
                    Next
                End If

                db.SubmitChanges() ' submit all the changes
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function UpdateOnlineAd(ByVal adDesignId As Integer, ByVal header As String, ByVal description As String, ByVal html As String, ByVal price As Decimal, _
        ByVal locationId As Integer, ByVal locationAreaId As Integer, ByVal contactName As String, ByVal contactType As String, ByVal contactValue As String) As Boolean

        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim onlineAd = (From o In db.OnlineAds Where o.AdDesignId = adDesignId Select o).Single

                With onlineAd
                    .Heading = header
                    .Description = description
                    .HtmlText = html
                    .Price = price
                    If locationId > 0 Then
                        .LocationId = locationId
                    End If
                    If locationAreaId > 0 Then
                        .LocationAreaId = locationAreaId
                    End If
                    .ContactName = contactName
                    .ContactType = contactType
                    .ContactValue = contactValue
                End With

                db.SubmitChanges()
                Return True
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function IncreaseOnlineAdViews(ByVal adId As Integer) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                IncreaseOnlineAdViews(adId, db)
            End Using
        Catch ex As Exception
            'todo log exception
            Throw ex
        End Try
    End Function

    Public Shared Function IncreaseOnlineAdViews(ByVal adId As Integer, ByRef dataContext As BetterclassifiedsDataContext) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim ad = (From onl In db.OnlineAds Where onl.OnlineAdId = adId Select onl).Single

                If ad.NumOfViews Is Nothing Then
                    ad.NumOfViews = 1 ' this is the first time viewed.
                Else
                    ad.NumOfViews += 1 ' increase whatever the previous number was.
                End If

                ' udpate the database
                db.SubmitChanges()
            End Using
        Catch ex As Exception
            'todo log exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Updates the status of the required Online Ad.
    ''' </summary>
    ''' <param name="onlineAdId">ID for the online Ad.</param>
    ''' <param name="status">Type of AdDesignStatus enum to change the record to.</param>
    Public Shared Function UpdateOnlineAdStatus(ByVal onlineAdId As Integer, ByVal status As AdDesignStatus) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim onlineAd = (From ad In db.OnlineAds Where ad.OnlineAdId = onlineAdId Select ad).Single
                onlineAd.AdDesign.Status = status
                db.SubmitChanges()
                Return True
            End Using
        Catch ex As Exception
            Throw ex 'todo - log exception
        End Try
    End Function

    ''' <summary>
    ''' Updates the status of the required Online Ad by the Ad Design Id
    ''' </summary>
    ''' <param name="adDesignId">Ad Design ID that has a 1-1 relationship with Online Ad</param>
    ''' <param name="status">Type of AdDesignStatus to change to record to. </param>
    ''' <returns>True if the update is successful.</returns>
    Public Shared Function UpdateOnlineAdStatusByAdDesignId(ByVal adDesignId As Integer, ByVal status As AdDesignStatus) As Boolean
        Try
            Using db = BetterclassifiedsDataContext.NewContext
                Dim design = (From ad In db.AdDesigns Where ad.AdDesignId = adDesignId Select ad).Single
                design.Status = status
                db.SubmitChanges()
                Return True
            End Using
        Catch ex As Exception
            Throw ex 'todo - log exception
        End Try
    End Function

#End Region

#Region "Delete"

    Public Shared Sub DeleteAdGraphics(ByVal adDesignId As Integer)
        ' Remove all the ad graphic objects for the Required Ad Design
        Using db = BetterclassifiedsDataContext.NewContext
            Dim adGraphicList = From g In db.AdGraphics Where g.AdDesignId = adDesignId Select g
            db.AdGraphics.DeleteAllOnSubmit(adGraphicList)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub DeleteAdGraphic(ByVal adDesignId As Integer, ByVal documentId As String)
        DeleteAdGraphic(adDesignId, documentId, False)
    End Sub

    Public Shared Sub DeleteAdGraphic(ByVal adDesignId As Integer, ByVal documentId As String, ByVal removePhotoBooking As Boolean)
        ' Remove required document Id
        Using db = BetterclassifiedsDataContext.NewContext
            Dim adGraphic = (From g In db.AdGraphics Where g.AdDesignId = adDesignId And g.DocumentID = documentId Select g).Single
            Dim adTypeId = (From ds In db.AdDesigns Where ds.AdDesignId = adDesignId Select ds).Single.AdTypeId

            If adTypeId = SystemAdType.LINE And removePhotoBooking Then
                ' Update the line ad so that it doesn't contain the Flag to contain an image
                Dim lineAd = (From ln In db.LineAds Where ln.AdDesignId = adDesignId Select ln).Single
                lineAd.UsePhoto = False
            End If

            db.AdGraphics.DeleteOnSubmit(adGraphic)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub DeleteBookEntry(ByVal bookEntryId As Integer)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim be = db.BookEntries.Where(Function(i) i.BookEntryId = bookEntryId).FirstOrDefault
            If be IsNot Nothing Then
                db.BookEntries.DeleteOnSubmit(be)
                db.SubmitChanges()
            End If
        End Using
    End Sub

#End Region

End Class
