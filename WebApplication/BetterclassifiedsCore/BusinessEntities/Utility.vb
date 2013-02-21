Imports System.Reflection

Namespace BusinessEntities

    Public Module Utility

#Region "AdBookingEntity"
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToAdBookingEntity(ByVal adBooking As DataModel.AdBooking) As AdBookingEntity
            Return New AdBookingEntity With {.AdBookingId = adBooking.AdBookingId, _
                                       .StartDate = adBooking.StartDate, _
                                       .EndDate = adBooking.EndDate, _
                                       .TotalPrice = adBooking.TotalPrice, _
                                       .BookReference = adBooking.BookReference, _
                                       .AdId = adBooking.AdId, _
                                       .UserId = adBooking.UserId, _
                                       .BookingStatus = adBooking.BookingStatus, _
                                       .MainCategoryId = adBooking.MainCategoryId}
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToAdBookingEntityList(ByVal adBookingList As List(Of DataModel.AdBooking)) As List(Of AdBookingEntity)
            Dim list As New List(Of AdBookingEntity)

            For Each book As DataModel.AdBooking In adBookingList
                list.Add(book.ToAdBookingEntity)
            Next
            Return list
        End Function

#End Region

#Region "Publication"
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToPublicationEntity(ByVal publication As DataModel.Publication) As PublicationEntity
            Return New PublicationEntity With {.PublicationId = publication.PublicationId, _
                                               .Title = publication.Title, _
                                               .Description = publication.Description, _
                                               .PublicationTypeId = publication.PublicationTypeId, _
                                               .ImageUrl = publication.ImageUrl, _
                                               .FrequencyType = publication.FrequencyType, _
                                               .FrequencyValue = publication.FrequencyValue, _
                                               .Active = publication.Active}
        End Function

        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToPublicationList(ByVal queryList As List(Of DataModel.spSpecialRatePublicationsResult)) As List(Of DataModel.Publication)
            Dim list As New List(Of DataModel.Publication)
            For Each item In queryList
                list.Add(New DataModel.Publication With {.PublicationId = item.PublicationId, _
                                                         .Title = item.Publication, _
                                                         .ImageUrl = item.ImageUrl})

            Next
            Return list
        End Function

#End Region

#Region "Ratecards"

#End Region

#Region "OnlineAd"
        <System.Runtime.CompilerServices.Extension()> _
        Public Function ToOnlineAdEntity(ByVal onlineAd As DataModel.OnlineAd, _
                                         ByVal imageList As List(Of String), _
                                         ByVal parentCategory As DataModel.MainCategory, _
                                         ByVal subCategory As DataModel.MainCategory, _
                                         ByVal datePosted As DateTime, _
                                         ByVal bookingReference As String, _
                                         ByVal location As String, _
                                         ByVal area As String) As OnlineAdEntity
            Dim entity As New OnlineAdEntity
            With entity
                .OnlineAdId = onlineAd.OnlineAdId
                .AdDesignId = onlineAd.AdDesignId
                .Heading = onlineAd.Heading
                .Description = onlineAd.Description
                .HtmlText = onlineAd.HtmlText
                .Price = onlineAd.Price
                .LocationId = onlineAd.LocationId
                .LocationAreaId = onlineAd.LocationAreaId
                .ContactName = onlineAd.ContactName
                .ContactType = onlineAd.ContactType
                .ContactValue = onlineAd.ContactValue
                .NumOfViews = onlineAd.NumOfViews
                .ImageList = imageList
                .DatePosted = datePosted
                .BookingReference = bookingReference
                .ParentCategory = parentCategory
                .SubCategory = subCategory
                .LocationValue = location
                .AreaValue = area
            End With
            Return entity
        End Function

      
#End Region

    End Module

End Namespace