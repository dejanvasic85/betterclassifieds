Imports BetterclassifiedsCore.DataModel
Imports Paramount.ApplicationBlock.Logging.EventLogging
Imports Paramount.ApplicationBlock.Logging.AuditLogging
Imports Paramount.ApplicationBlock.Configuration

Public Enum PublicationFrequency
    Daily
    Weekly
    Online
End Enum


''' <summary>
''' Provides the general data layer methods required to communicate with the backend Database.
''' </summary>
''' <remarks></remarks>
Public Class PublicationController

#Region "Create"

    Public Shared Function CreatePublication(ByVal title As String, ByVal description As String, ByVal publicationTypeId As Nullable(Of Integer), _
                                             ByVal imageUrl As String, ByVal freqType As String, ByVal freqValue As String, ByVal active As Boolean, _
                                             ByVal adTypeId As Nullable(Of Integer), ByVal sortOrder As Integer) As Integer
        Using db = BetterclassifiedsDataContext.NewContext
            Dim publication As New DataModel.Publication With {.Title = title, .Description = description, _
                                                               .PublicationTypeId = publicationTypeId, .ImageUrl = imageUrl, _
                                                               .FrequencyType = freqType, .FrequencyValue = freqValue, .Active = active, _
                                                               .SortOrder = sortOrder}
            If adTypeId IsNot Nothing Then
                'create the publication ad types for this publication as well
                Dim pubAdType As New PublicationAdType With {.Publication = publication, .AdTypeId = adTypeId}
            End If

            db.Publications.InsertOnSubmit(publication)
            db.SubmitChanges()
            Return publication.PublicationId
        End Using
    End Function

    Public Shared Function CreatePublicationAdType(ByVal publicationId As Integer, ByVal adTypeId As Integer) As Integer
        Using db = BetterclassifiedsDataContext.NewContext
            Dim pubAdType As New PublicationAdType With {.PublicationId = publicationId, .AdTypeId = adTypeId}
            db.PublicationAdTypes.InsertOnSubmit(pubAdType)
            db.SubmitChanges()
            Return pubAdType.PublicationAdTypeId
        End Using
    End Function

    Public Shared Function CreatedPublicationEditions(ByVal publicationId As Integer, ByVal startDate As DateTime, ByVal insertions As Integer, _
                                                      ByVal daysBeforeEdition As Integer, ByVal deadlineHour As Integer, ByVal deadlineMins As Integer) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim pub As Publication = (From p In db.Publications Where p.PublicationId = publicationId Select p).Single
            Dim dates As List(Of Date) = GeneralRoutine.GetEditionDates(pub, startDate, insertions)

            For Each d In dates
                Dim d1 = d
                Dim deadline As DateTime = d.AddDays(-daysBeforeEdition).AddHours(deadlineHour).AddMinutes(deadlineMins)
                ' check if edition exists already
                If db.Editions.Where(Function(i) i.EditionDate = d1 And i.PublicationId = publicationId).Count = 0 Then
                    ' create the new edition and add into the datasbe.
                    Dim ed As New Edition With {.EditionDate = d, .PublicationId = publicationId, .Deadline = deadline, .Active = True}
                    db.Editions.InsertOnSubmit(ed)
                End If
            Next

            db.SubmitChanges()
            Return True
        End Using
    End Function

    Public Shared Function CreatePublicationCategory(ByVal title As String, ByVal description As String, ByVal imageUrl As String, ByVal publicationId As Integer, ByVal mainCategoryId As Integer, ByVal parentId As System.Nullable(Of Integer)) As Integer
        Using db = BetterclassifiedsDataContext.NewContext
            Dim cat As New PublicationCategory With {.Title = title, _
                                                     .Description = description, _
                                                     .ImageUrl = imageUrl, _
                                                     .PublicationId = publicationId, _
                                                     .MainCategoryId = mainCategoryId, _
                                                     .ParentId = parentId}

            db.PublicationCategories.InsertOnSubmit(cat)
            db.SubmitChanges()
            Return cat.PublicationCategoryId
        End Using
    End Function

    Public Shared Sub AssignSpecialRate(ByVal publicationList As List(Of Integer), ByVal categoryList As List(Of Integer), ByVal specialRateId As Integer, ByVal isClearCurrentRates As Boolean)
        Using db = BetterclassifiedsDataContext.NewContext
            For Each publicationId As Integer In publicationList
                For Each categoryId As Integer In categoryList
                    db.spPublicationSpecialRateAdd(publicationId, categoryId, specialRateId, isClearCurrentRates)
                Next
            Next
        End Using
    End Sub


    Public Shared Function AssignRatecard(ByVal publicationCatId As Integer, ByVal ratecardId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            RemoveAssignedRatecards(publicationCatId)

            ' assign the following ratecard to the publication Category Id
            Dim pubCat = (From pc In db.PublicationCategories Where pc.PublicationCategoryId = publicationCatId _
                         Select pc).Single

            For Each t As PublicationAdType In pubCat.Publication.PublicationAdTypes
                Dim newPubRate As New PublicationRate With {.PublicationCategoryId = publicationCatId, _
                                                            .RatecardId = ratecardId, _
                                                            .PublicationAdTypeId = t.PublicationAdTypeId}
                db.PublicationRates.InsertOnSubmit(newPubRate)
            Next

            db.SubmitChanges()
            Return True
        End Using
    End Function

    Public Shared Function AssignSpecialRate(ByVal publicationCatId As Integer, ByVal specialRateId As Integer) As Boolean
        Using db = BetterclassifiedsDataContext.NewContext
            RemoveSpecialRates(publicationCatId)

            ' assign the new special rate to this publication category
            Dim pubCategory = (From pc In db.PublicationCategories Where pc.PublicationCategoryId = publicationCatId Select pc).Single

            For Each pubType As PublicationAdType In pubCategory.Publication.PublicationAdTypes
                Dim specialPubRate As New PublicationSpecialRate With {.PublicationCategoryId = publicationCatId, _
                                                                       .SpecialRateId = specialRateId, _
                                                                       .PublicationAdTypeId = pubType.PublicationAdTypeId}
                db.PublicationSpecialRates.InsertOnSubmit(specialPubRate)
            Next

            ' submit changes back to the database
            db.SubmitChanges()
            Return True
        End Using

    End Function

#End Region

#Region "Retrieve"

    ''' <summary>
    ''' Returns all the Papers and their details from the database.
    ''' </summary>
    ''' <returns><see cref="List(Of Paper)">List of Papers</see></returns>
    Public Shared Function GetAllPapers() As List(Of Publication)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.Publications.OrderBy(Function(i) i.SortOrder).ToList
        End Using
    End Function

    ''' <summary>
    ''' Gets all the publication objects that don't include Online Papers.
    ''' </summary>
    Public Shared Function GetPublications()
        Return GetPublications(False)
    End Function


    Public Shared Function GetPublications(ByVal activateOnly As Boolean) As List(Of Publication)
        Using db = BetterclassifiedsDataContext.NewContext
            Return (From p In db.Publications _
                        Join t In db.PublicationTypes On t.PublicationTypeId Equals p.PublicationTypeId _
                        Where t.Code <> "ONLINE" _
                        And ((activateOnly And p.Active = True) Or activateOnly = False)
                        Order By p.SortOrder _
                        Select p).ToList
        End Using
    End Function

    Public Shared Function GetAllPublicationByAdType(ByVal adTypeId As Integer, ByVal showOnline As Boolean) As List(Of Publication)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spGetPublicationsByAdType(adTypeId, showOnline).ToList
        End Using
    End Function

    Public Shared Function GetPublicationById(ByVal publicationId As Integer) As Publication
        Using db = BetterclassifiedsDataContext.NewContext
            Dim publication = From pub In db.Publications Where pub.PublicationId = publicationId Select pub
            Return publication.Single
        End Using
    End Function

    Public Shared Function GetPublicationByRatecard(ByVal rate As Ratecard) As Publication
        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From pubRates In db.PublicationRates _
                              Join pubAdTypes In db.PublicationAdTypes On pubRates.PublicationAdTypeId Equals pubAdTypes.PublicationAdTypeId _
                              Join publication In db.Publications On publication.PublicationId Equals pubAdTypes.PublicationId _
                              Where pubRates.RatecardId = rate.RatecardId _
                              Select publication).Single
            Return query
        End Using
    End Function

    Public Shared Function PublicationEditions(ByVal paperIdList As List(Of Integer), ByVal takeAmount As Integer) As List(Of Nullable(Of Date))
        Using db = BetterclassifiedsDataContext.NewContext
            Dim dateList As New List(Of Nullable(Of DateTime)) ' return value
            For Each paperId In paperIdList
                Dim id As Integer = paperId
                dateList.AddRange((From ed In db.Editions Where ed.EditionDate >= Today.Date _
                                   And ed.PublicationId = id And ed.Deadline > DateTime.Now _
                                   Order By ed.EditionDate _
                                   Select ed.EditionDate Take takeAmount).ToList)
            Next
            Return dateList
        End Using
    End Function

    Public Shared Function PublicationEditions(ByVal paperIdList As List(Of Integer), ByVal takeAmount As Integer, ByVal startDate As DateTime) As List(Of Nullable(Of DateTime))
        Using db = BetterclassifiedsDataContext.NewContext
            Dim dateList As New List(Of Nullable(Of DateTime))
            For Each paperId In paperIdList
                Dim id As Integer = paperId
                dateList.AddRange((From ed In db.Editions Where ed.EditionDate >= startDate _
                                   And ed.PublicationId = id _
                                   Order By ed.EditionDate _
                                   Select ed.EditionDate Take takeAmount).ToList)
            Next
            Return dateList
        End Using
    End Function

    ''' <summary>
    ''' Returns all upcoming editions for specified Paper and number of upcoming days from today.
    ''' </summary>
    ''' <param name="paperId">Id of the required publication</param>
    ''' <param name="days">Number of days from today.</param>
    Public Shared Function PublicationEditions(ByVal paperId As Integer, ByVal days As Integer) As List(Of Edition)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim endDate As DateTime = Today.Date.AddDays(days)
            Dim query = (From ed In db.Editions Where ed.PublicationId = paperId _
                       And ed.EditionDate > Today.Date And ed.EditionDate <= endDate _
                       Order By ed.EditionDate Select ed).ToList
            Return query
        End Using
    End Function

    Public Shared Function PublicationEditions(ByVal paperId As Integer, ByVal takeAmount As Integer, ByVal startDate As DateTime) As List(Of Nullable(Of DateTime))
        Using db = BetterclassifiedsDataContext.NewContext
            Return ((From ed In db.Editions Where ed.EditionDate >= startDate And ed.PublicationId = paperId _
                     Order By ed.EditionDate _
                     Select ed.EditionDate Take takeAmount).ToList)
        End Using
    End Function

    Public Shared Function PublicationEditionList(ByVal paperIdList As List(Of Integer), ByVal takeAmount As Integer, ByVal startDate As DateTime) As List(Of Booking.EditionList)
        Using db = BetterclassifiedsDataContext.NewContext

            Dim editionList As New List(Of Booking.EditionList)
            For Each p As Integer In paperIdList
                Dim id As Integer = p
                If PublicationController.GetPublicationType(id) <> SystemAdType.ONLINE.ToString Then
                    Dim edition As New Booking.EditionList
                    With edition
                        .Title = db.Publications.Where(Function(i) i.PublicationId = id).Single.Title
                        .EditionList = (From ed In db.Editions Where ed.PublicationId = id And ed.EditionDate >= startDate _
                                        Order By ed.EditionDate _
                                        Select ed.EditionDate Take takeAmount).ToList
                    End With
                    editionList.Add(edition)
                End If
            Next
            Return editionList
        End Using
    End Function

    Public Shared Function PublicationDeadlines(ByVal pubIdList As List(Of Integer)) As List(Of BusinessEntities.PublicationDeadline)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim list As New List(Of BusinessEntities.PublicationDeadline)
            For Each id In pubIdList
                Dim paperId = id
                If (db.Publications.Where(Function(i) i.PublicationId = paperId).FirstOrDefault).PublicationType.Code.Trim <> "ONLINE" Then
                    Dim query = db.spPublicationDeadlineSelect(paperId).Single
                    list.Add(New BusinessEntities.PublicationDeadline With {.Deadline = query.Deadline, _
                                                                            .Title = query.Publication})
                End If
            Next
            Return list
        End Using
    End Function

    Public Shared Function PublicationList(ByVal pubIdList As List(Of Integer), ByVal includeOnline As Boolean) As List(Of DataModel.Publication)
        Dim pubList As New List(Of Publication)
        Using db = BetterclassifiedsDataContext.NewContext
            For Each id As Integer In pubIdList
                Dim t As Integer = id
                pubList.Add(db.Publications.Where(Function(i) i.PublicationId = t).FirstOrDefault)
            Next

            If includeOnline = False Then
                Dim onlineId As Integer = (From typ In db.PublicationTypes Where typ.Code = "ONLINE" Select typ.PublicationTypeId).Single
                Return pubList.Where(Function(i) i.PublicationTypeId <> onlineId)
            Else
                Return pubList
            End If
        End Using
    End Function

    Public Shared Function EditionDeadlineList() As List(Of spPublicationEditionAndDeadlinesResult)
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spPublicationEditionAndDeadlines().ToList
        End Using
    End Function

    Public Shared Function SpecialRatePublications(ByVal specialRateId As Integer) As List(Of spSpecialRatePublicationsResult)

        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spSpecialRatePublications(specialRateId).ToList
        End Using
    End Function

    Public Shared Function GetPublicationType(ByVal publicationId As Integer) As String

        Using db = BetterclassifiedsDataContext.NewContext
            Dim query = (From pub In db.Publications Where pub.PublicationId = publicationId _
                        Join type In db.PublicationTypes On type.PublicationTypeId Equals pub.PublicationTypeId _
                        Select type).FirstOrDefault

            If query IsNot Nothing Then
                Return query.Code.Trim
            Else
                Return ""
            End If
        End Using

    End Function

    ''' <summary>
    ''' Returns a list of all the publication types from the database
    ''' </summary>
    ''' <param name="includeOnline">Indication whether to return the online publications as well.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetPublicationTypes(ByVal includeOnline As Boolean) As IList

        Using db = BetterclassifiedsDataContext.NewContext
            If includeOnline Then
                Return db.PublicationTypes.ToList
            Else
                Return db.PublicationTypes.Where(Function(i) i.Code <> SystemAdType.ONLINE.ToString()).ToList
            End If
        End Using

    End Function

    Public Shared Function GetPublicationTypeById(ByVal publicationTypeId As Integer) As PublicationType

        Using db = BetterclassifiedsDataContext.NewContext
            Return db.PublicationTypes.Where(Function(j) j.PublicationTypeId = publicationTypeId).Single
        End Using

    End Function

    Public Shared Function GetOnlinePublication() As DataModel.Publication

        Using db = BetterclassifiedsDataContext.NewContext
            Dim p = From pub In db.Publications _
                    Join typ In db.PublicationTypes On pub.PublicationTypeId Equals typ.PublicationTypeId _
                    Where typ.Code.Trim = "ONLINE" _
                    Select pub
            If p.Count = 1 Then
                Return p.Single
            Else
                Return Nothing
            End If
        End Using

    End Function

    ''' <summary>
    ''' Returns all the Publication Category objects as IList by the Publication, Main Category and Parent
    ''' </summary>
    Public Shared Function GetPublicationCategories(ByVal publicationId As Integer, ByVal parentId As System.Nullable(Of Integer), ByVal mainCategoryId As Integer) As IList

        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spPublicationCategories(publicationId, parentId, mainCategoryId).ToList
        End Using

    End Function

    ''' <summary>
    ''' Returns all the Publication category Objects as IList by Publication and Parent.
    ''' </summary>
    Public Shared Function GetPublicationCategories(ByVal publicationId As Integer, ByVal parentId As System.Nullable(Of Integer)) As IList
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.spPublicationCategoriesByPubId(publicationId, parentId).ToList
        End Using
    End Function

    Public Shared Function GetPublicationCategoryById(ByVal id As Integer) As DataModel.PublicationCategory
        Using db = BetterclassifiedsDataContext.NewContext
            Return db.PublicationCategories.Where(Function(i) i.PublicationCategoryId = id).Single
        End Using
    End Function

#End Region

#Region "Update"

    Public Shared Function UpdatePublication(ByVal publicationId As Integer, ByVal title As String, ByVal description As String, _
                                             ByVal publicationTypeId As Nullable(Of Integer), ByVal imageUrl As String, _
                                             ByVal frequencyType As String, ByVal frequencyValue As String, ByVal active As Boolean, _
                                             ByVal sortOrder As Integer) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim publication = (From p In db.Publications Where p.PublicationId = publicationId Select p).Single
            With publication
                .Title = title
                .Description = description
                .PublicationTypeId = publicationTypeId
                .ImageUrl = imageUrl
                .FrequencyType = frequencyType
                .FrequencyValue = frequencyValue
                .Active = active
                .SortOrder = sortOrder
            End With
            db.SubmitChanges()
            Return True
        End Using

    End Function

    ''' <summary>
    ''' Updates the publication category details back to the database
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function UpdatePublicationCategory(ByVal id As Integer, ByVal title As String, ByVal description As String, ByVal imageUrl As String, _
                                                     ByVal parentId As Nullable(Of Integer), ByVal mainCategoryId As Nullable(Of Integer), ByVal publicationId As Nullable(Of Integer)) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim pubCategory = (From pc In db.PublicationCategories Where pc.PublicationCategoryId = id Select pc).Single
            With pubCategory
                .Title = title
                .Description = description
                .ImageUrl = imageUrl
                .ParentId = parentId
                .MainCategoryId = mainCategoryId
                .PublicationId = publicationId
            End With
            db.SubmitChanges()
            Return True
        End Using

    End Function

#End Region

#Region "Delete"

    Public Shared Function DeletePublication(ByVal publicationId As Integer) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim pub = (From p In db.Publications Where p.PublicationId = publicationId Select p).Single
            Dim pubAdTypes = (From t In db.PublicationAdTypes Where t.PublicationId = publicationId Select t).ToList

            db.PublicationAdTypes.DeleteAllOnSubmit(pubAdTypes)
            db.Publications.DeleteOnSubmit(pub)

            db.SubmitChanges()
            Return True
        End Using

    End Function

    Public Shared Function DeletePaperEditions(ByVal editionList As List(Of Integer)) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim list As New List(Of Edition)
            For Each id In editionList
                Dim editionId = id
                list.Add(db.Editions.Where(Function(i) i.EditionId = editionId).Single)
            Next
            db.Editions.DeleteAllOnSubmit(list)
            db.SubmitChanges()
            Return True
        End Using

    End Function

    ''' <summary>
    ''' Deletes publication category and it's children by the Id.
    ''' </summary>
    ''' <param name="id">Id of publication category to delete.</param>
    Public Shared Function DeletePublicationCategory(ByVal id As Integer) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim obj = (From pc In db.PublicationCategories Where pc.PublicationCategoryId = id Select pc).Single
            If obj.ParentId IsNot Nothing Then
                ' remove the assigned publication rates and special rates for this publication category
                RemoveAssignedRatecards(id)
                RemoveSpecialRates(id)
            Else
                ' this is a parent category - delete all subcategories and delete their rates (including special rates)
                Dim subCategories = From pc In db.PublicationCategories Where pc.ParentId = id Select pc
                ' delete ratecards
                If subCategories.Count > 0 Then

                    For Each subCategory As PublicationCategory In subCategories.ToList
                        RemoveAssignedRatecards(subCategory.PublicationCategoryId)
                        RemoveSpecialRates(subCategory.PublicationCategoryId)
                    Next

                    ' delete all sub categories
                    db.PublicationCategories.DeleteAllOnSubmit(subCategories)

                End If
            End If
            db.PublicationCategories.DeleteOnSubmit(obj)
            db.SubmitChanges()
            Return True
        End Using

    End Function

    Public Shared Sub DeletePublicationImage(ByVal publicationId As Integer)
        ' only remove the reference to the image (does not remove the image from the DSL)
        Using db = BetterclassifiedsDataContext.NewContext
            Dim pub As DataModel.Publication = (From p In db.Publications Where p.PublicationId = publicationId Select p).Single
            pub.ImageUrl = Nothing
            db.SubmitChanges()
        End Using
    End Sub

    Public Shared Function RemoveAssignedRatecards(ByVal publicationCategoryId As Integer) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim pubRates = From pr In db.PublicationRates _
                           Where pr.PublicationCategoryId = publicationCategoryId _
                           Select pr
            db.PublicationRates.DeleteAllOnSubmit(pubRates)
            db.SubmitChanges()
        End Using

    End Function

    Public Shared Function RemoveSpecialRates(ByVal publicationCategoryId As Integer) As Boolean

        Using db = BetterclassifiedsDataContext.NewContext
            Dim publicationSpecials = From psr In db.PublicationSpecialRates _
                                      Where psr.PublicationCategoryId = publicationCategoryId _
                                      Select psr
            db.PublicationSpecialRates.DeleteAllOnSubmit(publicationSpecials)
            db.SubmitChanges()
        End Using

    End Function
#End Region

End Class