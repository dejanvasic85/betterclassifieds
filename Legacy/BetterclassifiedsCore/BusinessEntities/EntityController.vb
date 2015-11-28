Namespace BusinessEntities

    Public Class EntityController

#Region "AdBookingEntity"

        Public Shared Function GetAdBookingByUser(ByVal UserId As String) As List(Of BusinessEntities.AdBookingEntity)
            Using db = DataModel.BetterclassifiedsDataContext.NewContext
                Dim query = (From adBooking In db.AdBookings Where _
                            adBooking.UserId.Contains(UserId) _
                            Select adBooking).ToList

                Return Utility.ToAdBookingEntityList(query)
            End Using
        End Function

        Public Shared Function GetAdBookingByReference(ByVal BookingReference As String) As List(Of BusinessEntities.AdBookingEntity)
            Using db = DataModel.BetterclassifiedsDataContext.NewContext
                Dim query = From adBooking In db.AdBookings Where _
                            adBooking.BookReference = BookingReference _
                            Select adBooking

                Return Utility.ToAdBookingEntityList(query.ToList)
            End Using
        End Function

#End Region

#Region "Publication"

        ''' <summary>
        ''' Creates a publication into the database and returns true if successful.
        ''' </summary>
        ''' <param name="publication"><see cref="PublicationEntity">PublicationEntity</see></param>
        ''' <returns>True if save successful.</returns>
        Public Shared Function CreatePublication(ByVal publication As PublicationEntity) As Boolean
            Using db = DataModel.BetterclassifiedsDataContext.NewContext
                Dim p As New DataModel.Publication
                With p
                    .PublicationId = publication.PublicationId
                    .Title = publication.Title
                    .Description = publication.Description
                    .PublicationTypeId = publication.PublicationTypeId
                    .ImageUrl = publication.ImageUrl
                    .FrequencyType = publication.FrequencyType
                    .FrequencyValue = publication.FrequencyValue
                    .Active = publication.Active
                End With

                db.Publications.InsertOnSubmit(p)
                db.SubmitChanges()

            End Using
        End Function

#End Region

    End Class

End Namespace
