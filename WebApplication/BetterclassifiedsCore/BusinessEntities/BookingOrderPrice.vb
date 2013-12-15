Imports System
Imports System.Linq

Namespace BusinessEntities
    <Serializable()>
    Public Class BookingOrderPrice

        Public Sub New()
            PublicationPriceList = New List(Of BookingPublicationPriceEntity)
        End Sub

        Public Property PublicationPriceList As List(Of BookingPublicationPriceEntity)
        Public Property MainCategoryId As Integer
        Public Property MainCategoryName As String

        Public Function CalculateSingleEditionPrice(ByVal bookingCart As IBookCartContext)
            ' Calculates price of all publications based on a single edition
            Dim price As Decimal = 0
            For Each p In PublicationPriceList
                If p.AdType = BookingAdType.LineAd Then
                    price += p.CalculateLineAdPrice(1, bookingCart.LineAdTextWordCount, bookingCart.LineAdIsNormalAdHeading, _
                                                 bookingCart.LineAdIsSuperBoldHeading, bookingCart.LineAdIsMainPhoto, _
                                                 bookingCart.LineAdIsSecondPhoto, bookingCart.LineAdIsColourHeading, _
                                                 bookingCart.LineAdIsColourBorder, bookingCart.LineAdIsColourBackground)
                ElseIf p.AdType = BookingAdType.OnlineAd Then
                    price += p.CalculateOnlineAdPrice()
                End If
            Next
            Return price
        End Function

        Public Function CalculateTotalPrice(ByVal bookingCart As IBookCartContext)
            Return CalculateSingleEditionPrice(bookingCart) * bookingCart.EditionCount
        End Function

        Public Function GetLineAdBoldHeaderPrice(ByVal isAverageRequired As Boolean) As Decimal
            Dim price As Decimal
            For Each p In PublicationPriceList
                If p.LineBoldHeader IsNot Nothing And p.AdType <> BookingAdType.OnlineAd Then
                    price += p.LineBoldHeader
                End If
            Next

            If isAverageRequired And lineAdPublicationCount > 0 Then
                price = price / lineAdPublicationCount
            End If

            Return price
        End Function

        Public Function GetLineAdSuperBoldHeaderPrice(ByVal isAverageRequired As Boolean) As Decimal
            Dim price As Decimal
            For Each p In PublicationPriceList
                If p.LineSuperBoldHeader IsNot Nothing And p.AdType <> BookingAdType.OnlineAd Then
                    price += p.LineSuperBoldHeader
                End If
            Next
            If isAverageRequired And lineAdPublicationCount > 0 Then
                price = price / lineAdPublicationCount
            End If
            Return price
        End Function

        Public Function GetLineAdColourHeaderPrice(ByVal isAverageRequired As Boolean) As Decimal
            Dim price As Decimal
            For Each p In PublicationPriceList
                If p.LineColourHeader IsNot Nothing And p.AdType <> BookingAdType.OnlineAd Then
                    price += p.LineColourHeader
                End If
            Next
            If isAverageRequired And lineAdPublicationCount > 0 Then
                price = price / lineAdPublicationCount
            End If
            Return price
        End Function

        Public Function GetLineAdColourBorderPrice(ByVal isAverageRequired As Boolean) As Decimal
            Dim price As Decimal
            For Each p In PublicationPriceList
                If p.LineColourBorder IsNot Nothing And p.AdType <> BookingAdType.OnlineAd Then
                    price += p.LineColourBorder
                End If
            Next
            If isAverageRequired And lineAdPublicationCount > 0 Then
                price = price / lineAdPublicationCount
            End If
            Return price
        End Function

        Public Function GetLineAdColourBackgroundPrice(ByVal isAverageRequired As Boolean) As Decimal
            Dim price As Decimal
            For Each p In PublicationPriceList
                If p.LineColourBackground IsNot Nothing And p.AdType <> BookingAdType.OnlineAd Then
                    price += p.LineColourBackground
                End If
            Next
            If isAverageRequired And lineAdPublicationCount > 0 Then
                price = price / lineAdPublicationCount
            End If
            Return price
        End Function

        Public Function GetLineAdMainPhotoPrice() As Decimal
            Return GetLineAdMainPhotoPrice(Paramount.Betterclassified.Utilities.Configuration.BetterclassifiedSetting.IsAveragePriceUsedForLineAd)
        End Function

        Public Function GetLineAdMainPhotoPrice(ByVal isAverageRequired As Boolean) As Decimal
            Dim price As Decimal
            For Each p In PublicationPriceList
                If p.LineMainPhoto IsNot Nothing And p.AdType <> BookingAdType.OnlineAd Then
                    price += p.LineMainPhoto
                End If
            Next
            If isAverageRequired And lineAdPublicationCount > 0 Then
                price = price / lineAdPublicationCount
            End If
            Return price
        End Function

        Private ReadOnly Property lineAdPublicationCount As Integer
            Get
                Return PublicationPriceList.LongCount(Function(bpp) bpp.AdType = BookingAdType.LineAd)
            End Get
        End Property

    End Class
End Namespace
