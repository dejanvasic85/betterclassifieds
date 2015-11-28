Imports BetterclassifiedsCore.DataModel
Imports BetterclassifiedsCore.BusinessEntities
Imports BetterclassifiedsCore.Controller
Imports Paramount.ApplicationBlock.Configuration

Public Class GeneralController

    ''' <summary>
    ''' Uses the current date and time and calculates 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CurrentOnlineAdStartDate() As DateTime
        ' get the ad duration app setting for online ads
        Dim adDuration As Integer = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, _
                                                                 Utilities.Constants.CONST_KEY_Online_AdDurationDays)
        ' calculate the start date we need to get the currently running online ads
        Return DateTime.Today.AddDays(-adDuration)
    End Function

#Region "Rates"

    ''' <summary>
    ''' Returns a Ratecard object with all its properties by the Id
    ''' </summary>
    ''' <param name="id">Id for the ratecard to retrieve.</param>
    Public Shared Function GetRateDetailsById(ByVal id As Integer) As Ratecard
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.Ratecards.Where(Function(i) i.RatecardId = id).Single
        End Using
    End Function

    ''' <summary>
    ''' Returns a base rate object based on the ratecard Id
    ''' </summary>
    ''' <param name="rateId">Id for the Ratecard to be queried</param>
    Public Shared Function GetBaseRateDetailsByRatecard(ByVal rateId As Integer) As BaseRate
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From r In db.Ratecards Where r.RatecardId = rateId _
                        Join br In db.BaseRates On br.BaseRateId Equals r.BaseRateId _
                        Select br).Single
            Return query
        End Using
    End Function

    Public Shared Function GetRatecardsByPublicationCategory(ByVal publicationId As Integer, ByVal publicationCategoryId As Integer, ByVal adTypeId As Integer) As Ratecard
        Using db = BetterclassifiedsDataContext.NewContext
            ' perform query to get the ratecard
            Dim rateCard = From pubRate In db.PublicationRates _
                           Join pubAdType In db.PublicationAdTypes On pubRate.PublicationAdTypeId Equals pubAdType.PublicationAdTypeId _
                           Join rate In db.Ratecards On pubRate.RatecardId Equals rate.RatecardId _
                           Where pubAdType.PublicationId = publicationId _
                           And pubAdType.AdTypeId = adTypeId _
                           And pubRate.PublicationCategoryId = publicationCategoryId _
                           Select rate
            Return rateCard.FirstOrDefault
        End Using
    End Function

    Public Shared Function GetBaseRateDetailsBySpecial(ByVal specialId As Integer) As DataModel.BaseRate
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From sr In db.SpecialRates _
                         Join br In db.BaseRates On br.BaseRateId Equals sr.BaseRateId _
                         Where sr.SpecialRateId = specialId _
                         Select br).Single
            Return query
        End Using
    End Function

    Public Shared Function GetPublicationRateByCategory(ByVal pubCategoryId As Integer) As DataModel.PublicationRate
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.PublicationRates.Where(Function(i) i.PublicationCategoryId = pubCategoryId).FirstOrDefault
        End Using
    End Function

    Public Shared Function GetPublicationSpecialRateByCategory(ByVal pubCategoryId As Integer) As DataModel.PublicationSpecialRate
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.PublicationSpecialRates.Where(Function(i) i.PublicationCategoryId = pubCategoryId).FirstOrDefault
        End Using
    End Function

    Public Shared Function GetCurrentSpecials(ByVal categoryId As Integer) As List(Of spSpecialRatesByCategoryResult)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spSpecialRatesByCategory(categoryId).ToList
        End Using
    End Function

    Public Shared Function RatecardsByCategoryId(ByVal categoryId As Integer) As List(Of spRatecardsByCategoryResult)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spRatecardsByCategory(categoryId).ToList
        End Using
    End Function

    ''' <summary>
    ''' Returns a List of all New objects containing Ratecard Id, Title and Description from Base Rate ordered by ratecard id.
    ''' </summary>
    Public Shared Function GetRatecards() As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = From ra In db.Ratecards _
                        Join br In db.BaseRates On ra.BaseRateId Equals br.BaseRateId _
                        Order By br.Title _
                        Select New With {.RatecardId = ra.RatecardId, _
                                         .Title = br.Title, _
                                         .Description = br.Description.Substring(0, 35) + "...", _
                                         .MinimumCharge = ra.MinCharge}
            Return query.ToList
        End Using
    End Function

    Public Shared Function GetRatecard(ByVal publicationId As Integer, ByVal subCategoryId As Integer, ByVal adTypeId As Integer)
        ' first get the publication category Id
        Dim publicationCategory As DataModel.PublicationCategory = CategoryController.GetPublicationCategory(subCategoryId, publicationId)
        ' call method to return the ratecard that we need
        Return GetRatecardsByPublicationCategory(publicationId, publicationCategory.PublicationCategoryId, adTypeId)
    End Function

    ''' <summary>
    ''' Updates the ratecard and baserate details back into the database.
    ''' </summary>
    ''' <returns>True if save was successful.</returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateRatecardDetails(ByVal ratecardId As Integer, ByVal minCharge As Decimal, ByVal maxCharge As Decimal, ByVal ratePerUnit As Decimal, ByVal measureUnitLimit As Integer, _
                                                 ByVal photoCharge As Decimal, ByVal boldHeading As Decimal, ByVal onlineEditionBundle As Nullable(Of Decimal)) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim rate = (From r In db.Ratecards Where r.RatecardId = ratecardId Select r).Single
            With rate
                .MinCharge = minCharge
                .MaxCharge = maxCharge
                .PhotoCharge = photoCharge
                .RatePerMeasureUnit = ratePerUnit
                .MeasureUnitLimit = measureUnitLimit
                .PhotoCharge = photoCharge
                .BoldHeading = boldHeading
                .OnlineEditionBundle = onlineEditionBundle
            End With
            db.SubmitChanges()
            Return True
        End Using
    End Function

    ''' <summary>
    ''' Updates the BaseRate details back into the database
    ''' </summary>
    ''' <returns>True if the transaction was successful.</returns>
    Public Shared Function UpdateBaseRateDetails(ByVal baseRateId As Integer, ByVal title As String, ByVal descriptions As String) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim br = (From r In db.BaseRates Where r.BaseRateId = baseRateId Select r).Single
            With br
                .Title = title
                .Description = descriptions
            End With
            db.SubmitChanges()
        End Using
    End Function

    ''' <summary>
    ''' Creates a new Ratecard record in the database and return the new ID
    ''' </summary>
    ''' <returns>Returns the New ID of the record added.</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateBaseRaseRate(ByVal title As String, ByVal description As String) As Integer
        Using db = BetterclassifiedsDataContext.NewContext
            Dim newItem As New BaseRate With {.Title = title, .Description = description}
            db.BaseRates.InsertOnSubmit(newItem)
            db.SubmitChanges()
            Return newItem.BaseRateId ' return the new value after it's been inserted
        End Using
    End Function

    ''' <summary>
    ''' Creates a new Ratecard record in the database and returns the new ID
    ''' </summary>
    ''' <returns>Returns the New ID of the record added.</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateRatecard(ByVal baseRateId As Integer, ByVal minCharge As Nullable(Of Decimal), ByVal maxCharge As Nullable(Of Decimal), _
                                          ByVal ratePerUnit As Nullable(Of Decimal), ByVal unitLimit As Nullable(Of Integer), _
                                          ByVal photoCharge As Nullable(Of Decimal), ByVal boldHeading As Nullable(Of Decimal), _
                                          ByVal onlineEditionBundle As Nullable(Of Decimal)) As Integer
        Using db = BetterclassifiedsDataContext.NewContext
            Dim newRate As New Ratecard With {.BaseRateId = baseRateId, .MinCharge = minCharge, .MaxCharge = maxCharge, _
                                              .RatePerMeasureUnit = ratePerUnit, .MeasureUnitLimit = unitLimit, _
                                              .PhotoCharge = photoCharge, .BoldHeading = boldHeading, .OnlineEditionBundle = onlineEditionBundle}
            db.Ratecards.InsertOnSubmit(newRate)
            db.SubmitChanges()
            Return newRate.RatecardId ' return the new value
        End Using
    End Function

    ''' <summary>
    ''' Attempts to remove the record from the database completely by the objects ID.
    ''' </summary>
    ''' <returns>True if the deletion is successful.</returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteBaseRate(ByVal baseRateId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            db.BaseRates.DeleteOnSubmit(db.BaseRates.Where(Function(i) i.BaseRateId = baseRateId).Single)
            db.SubmitChanges()
            Return True
        End Using
    End Function

    ''' <summary>
    ''' Attempts to remove BaseRate and it's children Ratecards by the ratecard ID.
    ''' </summary>
    ''' <param name="ratecardId">ID of the ratecard to delete both BaseRate and Ratecard records.</param>
    ''' <returns>true if the deletion is successful.</returns>
    ''' <remarks></remarks>
    Public Shared Function DeleteBaseRateByRateId(ByVal ratecardId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim rate = (From r In db.Ratecards Where r.RatecardId = ratecardId).Single
            db.Ratecards.DeleteOnSubmit(rate)
            db.BaseRates.DeleteOnSubmit(rate.BaseRate)
            db.SubmitChanges()
            Return True
        End Using
    End Function

#End Region

#Region "Special Rates"

    Public Shared Function GetSpecialRates() As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = From sr In db.SpecialRates _
                       Join br In db.BaseRates On sr.BaseRateId Equals br.BaseRateId _
                       Order By br.Title _
                       Select New With {.SpecialRateId = sr.SpecialRateId, _
                                        .BaseRateId = br.BaseRateId, _
                                        .Title = br.Title, _
                                        .Description = br.Description.Substring(0, 35) + "...", _
                                        .SetPrice = sr.SetPrice}
            Return query.ToList
        End Using
    End Function

    Public Shared Function GetSpecialRateById(ByVal specialId As Integer) As DataModel.SpecialRate
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.SpecialRates.Where(Function(i) i.SpecialRateId = specialId).Single
        End Using
    End Function

    ''' <summary>
    ''' Creates a new SpecialRate in the database and returns the new ID
    ''' </summary>
    ''' <returns>Returns the New ID of the record added.</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateSpecialRate(ByVal baseRateId As Integer, ByVal numOfInsertions As Nullable(Of Integer), ByVal maximumWords As Nullable(Of Integer), _
                                             ByVal setPrice As Nullable(Of Decimal), ByVal discount As Nullable(Of Decimal), ByVal boldHeading As Boolean, ByVal image As Boolean) As Integer
        Using db = BetterclassifiedsDataContext.NewContext
            Dim newRate As New SpecialRate With {.BaseRateId = baseRateId, .NumOfInsertions = numOfInsertions, .MaximumWords = maximumWords, _
                                                 .SetPrice = setPrice, .Discount = discount, .LineAdBoldHeader = boldHeading, .LineAdImage = image}
            db.SpecialRates.InsertOnSubmit(newRate)
            db.SubmitChanges()
            Return newRate.SpecialRateId ' return the new value
        End Using
    End Function

    ''' <summary>
    ''' Updates the special rate details back into the database.
    ''' </summary>
    ''' <returns>True if save was successful.</returns>
    ''' <remarks></remarks>
    Public Shared Function UpdateSpecialRateDetails(ByVal specialRateId As Integer, ByVal numOfInsertions As Nullable(Of Integer), ByVal maximumWords As Nullable(Of Integer), _
                                             ByVal setPrice As Nullable(Of Decimal), ByVal discount As Nullable(Of Decimal), ByVal boldHeading As Boolean, ByVal image As Boolean) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim special = (From r In db.SpecialRates Where r.SpecialRateId = specialRateId Select r).Single
            With special
                .NumOfInsertions = numOfInsertions
                .MaximumWords = maximumWords
                .SetPrice = setPrice
                .Discount = discount
                .LineAdBoldHeader = boldHeading
                .LineAdImage = image
            End With
            db.SubmitChanges()
            Return True
        End Using
    End Function

    Public Shared Sub DeleteSpecialRateById(ByVal specialRateId As Integer, ByVal groupLogId As Guid)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim isCascadeDelete = True
            db.spSpecialRateDelete(specialRateId, isCascadeDelete)
        End Using
    End Sub

#End Region

#Region "Location and Location Area"

    Public Shared Sub CreateLocation(ByVal title As String)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim loc As New Location With {.Title = title}
            db.Locations.InsertOnSubmit(loc)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub CreateLocationArea(ByVal title As String, ByVal locId As Integer)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim area As New LocationArea With {.Title = title, .LocationId = locId}
            db.LocationAreas.InsertOnSubmit(area)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Function GetLocations() As List(Of DataModel.Location)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.Locations.OrderBy(Function(i) i.Title).ToList
        End Using
    End Function

    Public Shared Function GetLocationById(ByVal id As Integer) As DataModel.Location
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.Locations.Where(Function(i) i.LocationId = id).Single
        End Using
    End Function

    Public Shared Function GetLocationAreaById(ByVal id As Integer) As DataModel.LocationArea
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.LocationAreas.Where(Function(i) i.LocationAreaId = id).Single
        End Using
    End Function

    Public Shared Function GetLocationAreas() As List(Of DataModel.LocationArea)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.LocationAreas.OrderBy(Function(i) i.Title).ToList
        End Using
    End Function

    Public Shared Function GetLocationAreas(ByVal locationId As Integer) As List(Of LocationArea)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From loc In db.LocationAreas Where loc.LocationId = locationId _
                        Or loc.Title.Contains("Any Area") Select loc Order By loc.Title).ToList
            Return query
        End Using
    End Function

    Public Shared Function GetAnyLocationId() As Integer
        Using db = DataModel.BetterclassifiedsDataContext.NewContext
            Return db.Locations.Where(Function(i) i.Title.Contains("Any Location")).SingleOrDefault.LocationId
        End Using
    End Function

    Public Shared Function GetAnyLocationAreaId() As Integer
        Using db = DataModel.BetterclassifiedsDataContext.NewContext
            Return db.LocationAreas.Where(Function(i) i.Title.Contains("Any Area")).SingleOrDefault.LocationAreaId
        End Using
    End Function

    Public Shared Function GetLocationAreas(ByVal locationId As Integer, ByVal withAnyArea As Boolean) As List(Of LocationArea)
        Using db = BetterclassifiedsDataContext.NewContext
            If withAnyArea Then
                Return (From loc In db.LocationAreas Where loc.LocationId = locationId Or loc.Title.Contains("Any Area") Select loc Order By loc.Title).ToList
            Else
                Return db.LocationAreas.Where(Function(i) i.LocationId = locationId).OrderBy(Function(t) t.Title).ToList
            End If
        End Using
    End Function

    Public Shared Function LocationExists(ByVal title As String) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.Locations.Where(Function(i) i.Title = title).Count > 0
        End Using
    End Function

    Public Shared Function LocationAreaExists(ByVal title As String, ByVal locId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim areas = From a In db.LocationAreas Where a.Title = title And a.LocationId = locId Select a
            Return areas.Count > 0
        End Using
    End Function

    Public Shared Sub DeleteLocation(ByVal id As Integer)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim loc = (From l In db.Locations Where l.LocationId = id Select l).Single
            Dim areas = (From a In db.LocationAreas Where a.LocationId = id Select a).ToList
            db.LocationAreas.DeleteAllOnSubmit(areas)
            db.Locations.DeleteOnSubmit(loc)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub DeleteArea(ByVal id As Integer)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim area = (From a In db.LocationAreas Where a.LocationAreaId = id Select a).Single
            db.LocationAreas.DeleteOnSubmit(area)
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub UpdateLocation(ByVal locationId As Integer, ByVal title As String, ByVal description As String)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim loc = From l In db.Locations Where l.LocationId = locationId Select l
            If loc.Count > 0 Then
                With loc.FirstOrDefault()
                    .Title = title
                    .Description = description
                End With
            End If
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Sub UpdateLocationArea(ByVal areaId As Integer, ByVal title As String, ByVal description As String)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim area = From a In db.LocationAreas Where a.LocationAreaId = areaId Select a
            If area.Count > 0 Then
                With area.FirstOrDefault
                    .Title = title
                    .Description = description
                End With
            End If
            db.SubmitChanges()
        End Using
    End Sub

#End Region

#Region "Home Page Listings"

    Public Shared Function GetRecentlyAddedAds(ByVal bookStatus As BookingStatus) As List(Of spOnlineAdSelectRecentlyAddedResult)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spOnlineAdSelectRecentlyAdded(bookStatus).ToList
        End Using
    End Function

#End Region

#Region "Online Ad Search"

    Public Shared Function SearchOnlineAdsByCategory(ByVal categoryId As Integer, ByVal bookingStatus As BookingStatus) As List(Of DataModel.spOnlineAdSelectByCategoryResult)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spOnlineAdSelectByCategory(categoryId, bookingStatus).ToList
        End Using
    End Function

    Public Shared Function SearchOnlineAds(ByVal category As Nullable(Of Integer), ByVal subCategoryId As Nullable(Of Integer), _
                                           ByVal locationId As Nullable(Of Integer), ByVal areaId As Nullable(Of Integer), _
                                           ByVal keyword As String, ByVal bookStatus As BookingStatus) As List(Of spOnlineAdSelectResult)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spOnlineAdSelect(category, subCategoryId, locationId, areaId, keyword, bookStatus).ToList
        End Using
    End Function

#End Region

#Region "App Settings"

    ''' <summary>
    ''' Returns an IList containing all the distinct Module Names in the Application Settings
    ''' </summary>
    Public Shared Function GetAppSettingModules() As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From a In db.AppSettings Select a.Module Distinct).ToList
            Return query
        End Using
    End Function

    ''' <summary>
    ''' Returns an IList of all the Application Setting Names that belongs to the Required Module
    ''' </summary>
    ''' <param name="moduleName">Module Name in Application Settings.</param>
    Public Shared Function GetAppSettingNames(ByVal moduleName As String) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From a In db.AppSettings Where a.Module = moduleName Select a.AppKey Distinct).ToList
            Return query
        End Using
    End Function

    ''' <summary>
    ''' Returns an IList of AppSetting objects by the Module Name and Key name. The result should return one object for databinding.
    ''' </summary>
    ''' <param name="moduleName"></param>
    ''' <param name="key"></param>
    Public Shared Function GetApplicationSetting(ByVal moduleName As String, ByVal key As String) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.AppSettings.Where(Function(i) i.Module = moduleName And i.AppKey = key).ToList
        End Using
    End Function

    ''' <summary>
    ''' Updates the required application setting.
    ''' </summary>
    Public Shared Function UpdateApplicationSetting(ByVal moduleName As String, ByVal key As String, ByVal setting As String) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            Dim q = (From ap In db.AppSettings Where ap.Module = moduleName And key = ap.AppKey Select ap).Single
            ' update the new setting value
            q.SettingValue = setting
            'update the database
            db.SubmitChanges()
            Return True
        End Using
    End Function

#End Region

#Region "Support Enquiry"

    Public Shared Sub CreateSupportEnquiry(ByVal enquiryTypeName As String, ByVal fullName As String, ByVal email As String, ByVal phone As String, _
                                    ByVal subject As String, ByVal enquiryText As String)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim enquiry As New SupportEnquiry With {.EnquiryTypeName = enquiryTypeName, _
                                                    .FullName = fullName, _
                                                    .Email = email, _
                                                    .Phone = phone, _
                                                    .Subject = subject, _
                                                    .EnquiryText = enquiryText, _
                                                    .CreatedDate = DateTime.Now}
            db.SupportEnquiries.InsertOnSubmit(enquiry)
            db.SubmitChanges()
        End Using
    End Sub

#End Region

#Region "Web Content"

    Public Shared Sub UpdateWebContent(ByVal pageId As String, ByVal webContent As String, ByVal adminId As String, ByVal title As String)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim content = db.WebContents.Where(Function(i) i.PageId = pageId).SingleOrDefault

            Dim isNew As Boolean
            If content Is Nothing Then
                content = New WebContent With {.PageId = pageId, .Title = title}
                isNew = True
            End If

            content.WebContent = webContent
            content.LastModifiedDate = DateTime.Now
            content.LastModifiedUser = adminId

            If isNew Then
                ' Insert new record
                db.WebContents.InsertOnSubmit(content)
            End If

            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Function GetWebContent(ByVal pageId As String) As String
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.WebContents.Where(Function(i) i.PageId = pageId).Single.WebContent
        End Using
    End Function

    Public Shared Function GetWebContentDetail(ByVal pageId As String) As DataModel.WebContent
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.WebContents.Where(Function(i) i.PageId = pageId).SingleOrDefault
        End Using
    End Function

#End Region

End Class
