Imports BetterclassifiedsCore.DataModel
Imports System.Web
Imports System.Web.Security
Imports Paramount.Broadcast.Components

Namespace Controller
    Public Class OnlineAdEnquiryController
        Implements IDisposable

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free other state (managed objects).
                End If

                ' TODO: free your own state (unmanaged objects).
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        Public Shared Function GetEnquiryTypeList() As List(Of DataModel.EnquiryType)
            Using db = BetterclassifiedsDataContext.NewContext
                Return db.EnquiryTypes.ToList
            End Using
        End Function

        Public Shared Function GetOnlineAdEnquiryListByUser(ByVal userId As String) As List(Of OnlineAdEnquiry)
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From bk In db.AdBookings _
                                Join ds In db.AdDesigns On ds.AdId Equals bk.AdId _
                                Join ad In db.OnlineAds On ad.AdDesignId Equals ds.AdDesignId _
                                Join en In db.OnlineAdEnquiries On en.OnlineAdId Equals ad.OnlineAdId _
                                Where bk.UserId = userId And en.Active = True _
                                Select en
                Return query.ToList
            End Using
        End Function

        Public Shared Sub MarkAsRead(ByVal onlineAdEnquiryId As Integer)
            Using db = BetterclassifiedsDataContext.NewContext
                Dim result = (From a In db.OnlineAdEnquiries Where a.OnlineAdEnquiryId = onlineAdEnquiryId Select a).SingleOrDefault
                result.OpenDate = Date.Now
                db.SubmitChanges()
            End Using
        End Sub

        Public Shared Sub MarkAsInactive(ByVal onlineAdEnquiryId As Integer)
            Using db = BetterclassifiedsDataContext.NewContext
                Dim result = (From a In db.OnlineAdEnquiries Where a.OnlineAdEnquiryId = onlineAdEnquiryId Select a).SingleOrDefault
                result.Active = False
                db.SubmitChanges()
            End Using
        End Sub

        Public Shared Function GetUnreadMessageCount(ByVal userId As String) As Integer
            Using db = BetterclassifiedsDataContext.NewContext
                Dim query = From bk In db.AdBookings _
                                Join ds In db.AdDesigns On ds.AdId Equals bk.AdId _
                                Join ad In db.OnlineAds On ad.AdDesignId Equals ds.AdDesignId _
                                Join en In db.OnlineAdEnquiries On en.OnlineAdId Equals ad.OnlineAdId _
                                Where bk.UserId = userId And en.OpenDate Is Nothing And en.Active = True _
                                Select en
                Return query.Count
            End Using
        End Function

        Public Shared Sub CreateOnlineAdEnquiry(ByVal bookingId As Integer, ByVal enquiryTypeId As Integer, ByVal fullName As String, ByVal email As String, ByVal phone As String, ByVal enquiryText As String)
            Using db = BetterclassifiedsDataContext.NewContext

                Dim onlineAd = (From bk In db.AdBookings _
                                 Where bk.AdBookingId = bookingId _
                                 Join a In db.Ads On a.AdId Equals bk.AdId _
                                 Join d In db.AdDesigns On d.AdId Equals a.AdId _
                                 Join o In db.OnlineAds On o.AdDesignId Equals d.AdDesignId _
                                 Select New With {.AdDesignId = d.AdDesignId, .OnlineAdId = o.OnlineAdId}).Single

                Dim userData = GetUserEmailByAdDesignId(onlineAd.AdDesignId)
                ' SendEmail(bookingId, userData.Email, email, fullName, enquiryText, phone)


                Dim enquiry As New DataModel.OnlineAdEnquiry With {.OnlineAdId = onlineAd.OnlineAdId, _
                                                                   .EnquiryTypeId = enquiryTypeId, _
                                                                   .FullName = fullName, _
                                                                   .Email = email, _
                                                                   .Phone = phone, _
                                                                   .EnquiryText = enquiryText}
                ' set now as create and active to true
                enquiry.CreatedDate = DateTime.Now
                enquiry.Active = True
                db.OnlineAdEnquiries.InsertOnSubmit(enquiry)
                db.SubmitChanges()
            End Using
        End Sub

        Public Shared Function GetUserEmailByAdBookingId(ByVal adBookingId As Integer) As String
            Using db = BetterclassifiedsDataContext.NewContext
                Dim result = db.AdBookings.FirstOrDefault(Function(b) b.AdBookingId = adBookingId)

                If (result Is Nothing) Then
                    Throw New Exception("invalid email address")
                End If
                Return Membership.GetUser((result.UserId)).Email
            End Using
        End Function

        Public Shared Function GetUserEmailByAdDesignId(ByVal adDesignId As Integer)
            Using db = BetterclassifiedsDataContext.NewContext
                Dim result = From a In db.AdBookings _
                             Join b In db.AdDesigns On b.AdId Equals a.AdId _
                             Where b.AdDesignId = adDesignId _
                             Select a, b

                If (result.Count <> 1) Then
                    Throw New Exception("invalid email address")
                End If
                Dim dataItem = result.ToList(0)
                Return New With {.Email = Membership.GetUser((dataItem.a.UserId)).Email, .IflogId = dataItem.b.AdDesignId}
            End Using
        End Function

        'Friend Shared Sub SendEmail(ByVal adId As Integer, ByVal email As String, ByVal senderEmail As String, ByVal fullname As String, ByVal message As String, ByVal phone As String)
        '    Dim emailTemplate As New OnlineAdEnquiryNotification(email) With {.AdNumber = adId, .EmailAddress = senderEmail, .FullName = fullname, .Message = message, .Phone = phone}
        '    emailTemplate.Send()
        'End Sub

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
