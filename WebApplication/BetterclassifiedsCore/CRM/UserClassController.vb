Imports BetterclassifiedsCore.DataModel


Namespace CRM

    Public Class UserClassController
        Implements IDisposable

        Private _classContext As BetterclassifiedsDataContext
        Private disposedValue As Boolean = False        ' To detect redundant calls

#Region "Contructor"

        Public Sub New(ByVal connection As String)
            _classContext = BetterclassifiedsDataContext.NewContext
        End Sub

        Public Sub New()
            _classContext = BetterclassifiedsDataContext.NewContext
        End Sub

#End Region

#Region "Properties"

        Public ReadOnly Property ClassifiedContext() As BetterclassifiedsDataContext
            Get
                Return _classContext
            End Get
        End Property

#End Region

#Region "Retrieve"

        Public Function GetCurrentAdBookings(ByVal userId As String, ByVal bookStatus As Controller.BookingStatus) As IList
            Return _classContext.spAdBookingSelectUserActive(userId, bookStatus).ToList
        End Function

        Public Function GetCurrentOnlineAds(ByVal userId As String, ByVal bookStatus As Controller.BookingStatus) As IList
            Return _classContext.spOnlineAdSelectUserCurrent(userId, bookStatus).ToList
        End Function

        Public Function GetExpiredOnlineAds(ByVal userId As String, ByVal endDate As DateTime) As IList
            Return _classContext.spOnlineAdSelectUserExpired(userId, endDate).ToList
        End Function

        Public Function GetScheduledOnlineAds(ByVal userId As String, ByVal bookStatus As Controller.BookingStatus) As IList
            Return _classContext.spOnlineAdSelectUserScheduled(userId, bookStatus).ToList
        End Function

        Public Function GetCurrentLineAds(ByVal userId As String, ByVal bookStatus As Controller.BookingStatus) As IList
            Dim list = _classContext.spLineAdSelectUserCurrent(userId, bookStatus).ToList
            ' use the online iFlog ID instead of Line Ad if the online version exists
            For Each item In list
                Dim onlineDesignId = _classContext.spOnlineAdSelectByLineAdDesign(item.AdDesignId).FirstOrDefault
                If onlineDesignId IsNot Nothing Then
                    item.AdDesignId = onlineDesignId.AdDesignId
                End If
            Next
            Return list
        End Function

        Public Function GetExpiredLineAds(ByVal userId As String, ByVal bookStatus As Controller.BookingStatus, ByVal endDate As DateTime) As IList
            Dim list = _classContext.spLineAdSelectUserExpired(userId, endDate).ToList
            ' use the online iFlog ID instead of Line Ad if the online version exists
            For Each item In list
                Dim onlineDesignId = _classContext.spOnlineAdSelectByLineAdDesign(item.AdDesignId).FirstOrDefault
                If onlineDesignId IsNot Nothing Then
                    item.AdDesignId = onlineDesignId.AdDesignId
                End If
            Next
            Return list
        End Function

        Public Function GetScheduledLineAds(ByVal userId As String, ByVal bookStatus As Controller.BookingStatus) As IList
            Dim list = _classContext.spLineAdSelectUserScheduled(userId, bookStatus).ToList
            For Each item In list
                Dim onlineDesignId = _classContext.spOnlineAdSelectByLineAdDesign(item.AdDesignId).FirstOrDefault
                If onlineDesignId IsNot Nothing Then
                    item.AdDesignId = onlineDesignId.AdDesignId
                End If
            Next
            Return list
        End Function

#End Region

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
