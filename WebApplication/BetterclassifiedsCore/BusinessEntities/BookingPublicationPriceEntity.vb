Namespace BusinessEntities
    Public Class BookingPublicationPriceEntity

        ' Publication Information
        Public Property RateCardId As Integer
        Public Property PublicationId As String
        Public Property PublicationName As String
        Public Property PublicationCategoryId As Integer
        Public Property PublicationCategoryName As String
        Public Property AdType As BookingAdType

        ' Regular Ratecard Charges
        Public Property MinimumCharge As Nullable(Of Decimal)
        Public Property MaximumCharge As Nullable(Of Decimal)

        Public Property LineBoldHeader As Nullable(Of Decimal)
        Public Property LineSuperBoldHeader As Nullable(Of Decimal)
        Public Property LineAdFreeWordCount As Nullable(Of Decimal)
        Public Property LineRatePerWord As Nullable(Of Decimal)
        Public Property LineMainPhoto As Nullable(Of Decimal)
        Public Property LineSecondaryPhoto As Nullable(Of Decimal)
        Public Property LineColourHeader As Nullable(Of Decimal)
        Public Property LineColourBorder As Nullable(Of Decimal)
        Public Property LineColourBackground As Nullable(Of Decimal)


        Public Function CalculateLineAdPrice(ByVal editionCount As Decimal, ByVal lineAdWordCount As Integer, ByVal isBoldHeader As Boolean, _
                                             ByVal isSuperBoldHeader As Boolean, ByVal isMainPhoto As Boolean, ByVal isSecondaryPhoto As Boolean, _
                                             ByVal isColourHeader As Boolean, ByVal isColourBorder As Boolean, ByVal isColourBackground As Boolean) As Decimal

            Dim totalPrice As Decimal = 0

            If lineAdWordCount > Me.LineAdFreeWordCount Then
                totalPrice += (lineAdWordCount - Me.LineAdFreeWordCount) * Me.LineRatePerWord
            End If

            If isBoldHeader And Me.LineBoldHeader IsNot Nothing Then
                totalPrice += Me.LineBoldHeader
            End If
            If isSuperBoldHeader And Me.LineSuperBoldHeader IsNot Nothing Then
                totalPrice += Me.LineSuperBoldHeader
            End If
            If isMainPhoto And Me.LineMainPhoto IsNot Nothing Then
                totalPrice += Me.LineMainPhoto
            End If
            If isSecondaryPhoto And Me.LineSecondaryPhoto IsNot Nothing Then
                totalPrice += Me.LineSecondaryPhoto
            End If
            If isColourHeader And Me.LineColourHeader IsNot Nothing Then
                totalPrice += Me.LineColourHeader
            End If
            If isColourBorder And Me.LineColourBorder IsNot Nothing Then
                totalPrice += Me.LineColourBorder
            End If
            If isColourBackground And Me.LineColourBackground IsNot Nothing Then
                totalPrice += Me.LineColourBackground
            End If

            If totalPrice < MinimumCharge And Me.MinimumCharge IsNot Nothing Then
                totalPrice = MinimumCharge
            End If

            If totalPrice > MaximumCharge And Me.MaximumCharge IsNot Nothing Then
                totalPrice = MaximumCharge
            End If

            ' Multiply by the number of editions
            totalPrice = totalPrice * editionCount

            Return totalPrice
        End Function

        Public Function CalculateOnlineAdPrice() As Decimal
            Return Me.MinimumCharge
        End Function

        Public Function CalculatePrice(ByVal bookCartContext As IBookCartContext) As Decimal
            Dim publicationPrice As Decimal = 0
            Select Case Me.AdType
                Case BookingAdType.LineAd
                    Dim editionCount As Integer = 1
                    If bookCartContext.EditionCount > 1 Then
                        editionCount = bookCartContext.EditionCount
                    End If
                    Return CalculateLineAdPrice(editionCount, _
                                                bookCartContext.LineAdTextWordCount, _
                                                bookCartContext.LineAdIsNormalAdHeading, _
                                                bookCartContext.LineAdIsSuperBoldHeading, _
                                                bookCartContext.LineAdIsMainPhoto, _
                                                bookCartContext.LineAdIsSecondPhoto, _
                                                bookCartContext.LineAdIsColourHeading, _
                                                bookCartContext.LineAdIsColourBorder, _
                                                bookCartContext.LineAdIsColourBackground)
                Case BookingAdType.OnlineAd
                    Return CalculateOnlineAdPrice()
                Case BookingAdType.BlockAd
                    Throw New NotImplementedException
            End Select
            Return publicationPrice
        End Function

    End Class
End Namespace
