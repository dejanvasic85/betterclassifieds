Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.DataModel
Imports System.Threading
Imports BetterclassifiedsCore.ParameterAccess
Imports BetterClassified.UI.WebPage

Partial Public Class Step3
    Inherits BaseBookingPage

    Private Const adTypeState As String = "vsAdType"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If BookingController.AdBookCart Is Nothing Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If

        If (AdController.TempRecordExist(BookingController.AdBookCart.BookReference)) Then
            Response.Redirect(PageUrl.BookingStep_1 + "?action=expired")
        End If

        If Not Page.IsPostBack Then

            Dim adType As String = BookingController.AdBookCart.MainAdType().Code.Trim

            ' store the type of Ad in Viewstate since it's used a bit in this page
            ViewState(adTypeState) = adType

            If Request.QueryString("action") = "back" Then

                If (adType = SystemAdType.LINE.ToString) Then
                    ' we retrieve the session data to calculate the price and display the price summary
                    Dim lineAd As AdDesign = BookingController.GetAdDesignDetails(SystemAdType.LINE)
                    ucxLineAdDesign.Visible = True
                    CalculatePrice(adType, lineAd.LineAds(0).AdText)
                Else
                    Dim onlineAd As AdDesign = BookingController.GetAdDesignDetails(SystemAdType.ONLINE)
                    ucxDesignOnlineAd.Visible = True
                    CalculatePrice(adType, onlineAd.OnlineAds(0).Description)
                End If

            Else

                ' display the required control on the page and calculate the price
                Select Case adType
                    Case SystemAdType.LINE.ToString
                        ucxLineAdDesign.Visible = True
                        CalculatePrice(adType, ucxLineAdDesign.AdText)
                    Case SystemAdType.ONLINE.ToString
                        ucxDesignOnlineAd.Visible = True
                        CalculatePrice(adType, ucxDesignOnlineAd.Description)
                End Select
            End If

            Dim ref = BookingController.AdBookCart.BookReference.Split("-")
            ucxLineAdDesign.BookingReference = ref(1)
            ucxDesignOnlineAd.BookingReference = ref(1)

            ' Set up the online upload parameters
            UploadParameter.Clear()
            UploadParameter.IsOnlineAdUpload = (adType = SystemAdType.ONLINE.ToString)
            UploadParameter.IsPrintAdUpload = (adType = SystemAdType.LINE.ToString)
            UploadParameter.BookingReference = BookingController.AdBookCart.BookReference
            radWindowImages.OpenerElementID = lnkUploadImages.ClientID

        End If

    End Sub

    'Private Sub btnUpdatePrice_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpdatePrice.Click
    '    ' calculate the price accordingly to the ad type
    '    If (ViewState(adTypeState) = SystemAdType.LINE.ToString) Then
    '        CalculatePrice(ViewState(adTypeState), ucxLineAdDesign.AdText)
    '    ElseIf (ViewState(adTypeState) = SystemAdType.ONLINE.ToString) Then
    '        CalculatePrice(ViewState(adTypeState), ucxDesignOnlineAd.Description)
    '    End If
    'End Sub

    ''' <summary>
    ''' Method to handle the Next Button Event
    ''' </summary>
    Private Sub ucxNavButtons_NextStep(ByVal sender As Object, ByVal e As EventArgs) Handles ucxNavButtons.NextStep
        Dim errorList As List(Of String) = New List(Of String)

        If (ValidatePage(errorList)) Then
            ' call method to send the Ad Details first - this is generic data regardless of the ad type
            BookingController.SetAdDetails("", "", False)

            Dim adTypeCode As String = ViewState(adTypeState)

            Dim totalPrice As Decimal

            If (adTypeCode = SystemAdType.LINE.ToString) Then

                BookingController.SetLineAd(ucxLineAdDesign.AdHeader, ucxLineAdDesign.AdText)

                totalPrice = CalculatePrice(adTypeCode, ucxLineAdDesign.AdText)

            ElseIf (adTypeCode = SystemAdType.ONLINE.ToString) Then

                BookingController.SetOnlineAd(ucxDesignOnlineAd.Heading, ucxDesignOnlineAd.Description, _
                                               ucxDesignOnlineAd.HtmlText, ucxDesignOnlineAd.Price, _
                                               ucxDesignOnlineAd.LocationId, ucxDesignOnlineAd.LocationArea, _
                                               ucxDesignOnlineAd.ContactName, ucxDesignOnlineAd.ContactType, _
                                               ucxDesignOnlineAd.ContactValue)

                totalPrice = CalculatePrice(adTypeCode, ucxDesignOnlineAd.Description)
            End If

            ' set the price in the Session for a single Edition (may be for multiple papers though)
            BookingController.SetSingleAdPrice(totalPrice)

            Response.Redirect("Step4.aspx")
        Else
            'ucxPageErrors.ShowErrors(errorList)
        End If
    End Sub

    Private Function ValidatePage(ByRef errorList As List(Of String)) As Boolean

        'If ucxAdDetails.AdTitle = String.Empty Then
        '    errorList.Add("Please provide a Title for the booking.")
        'End If

        If (ucxLineAdDesign.Visible = True) Then
            ' validate the line ad control
            If (ucxLineAdDesign.AdText = String.Empty) Then
                errorList.Add("Please provide the Classified Ad Text")
            End If
        End If

        If (ucxDesignOnlineAd.Visible = True) Then
            ' validate the online ad control
            If (ucxDesignOnlineAd.Heading = String.Empty) Then
                errorList.Add("Please provide the heading for the Online Ad")
            End If

            If (ucxDesignOnlineAd.Description = String.Empty) Then
                errorList.Add("Please provide the description for the Online Ad")
            End If
        End If

        Return errorList.Count = 0
    End Function

    ''' <summary>
    ''' Calculates the total price of the Ad Design, and displays the summary in the PriceSummary control.
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CalculatePrice(ByVal adType As String, ByVal adText As String) As Decimal

        '' first we grab the ratecards from the session
        Dim rates = BookingController.AdBookCart.RatecardList
        Dim mainCategory = BookingController.AdBookCart.MainCategoryId
        Dim totalPrice As Decimal

        If (adType = SystemAdType.LINE.ToString) Then

            Dim isGraphicUploaded As Boolean = (BookingController.AdBookCart.LineAdGraphic IsNot Nothing)
            Dim isHeaderUsed As Boolean = ucxLineAdDesign.UseBoldHeading

            Dim priceSummary = BookingController.GetPriceSummary(rates, mainCategory, 1, adText, isGraphicUploaded, isHeaderUsed)

            ' get the overall price for a single edition by adding price for each paper
            For Each price In priceSummary
                totalPrice += price.PaperPrice
            Next

            'ucxPriceSummary.BindPriceSummary(priceSummary, totalPrice, isGraphicUploaded, isHeaderUsed, SystemAdType.LINE)

        ElseIf (adType = SystemAdType.ONLINE.ToString) Then

            Dim _
                priceSummaryList = _
                    BookingController.GetPriceSummary(rates, mainCategory, ucxDesignOnlineAd.Description)

            ' there should be only one item in this collection for now
            totalPrice = priceSummaryList(0).PaperPrice

            'ucxPriceSummary.BindPriceSummary(priceSummaryList, totalPrice, False, False, SystemAdType.ONLINE)
        End If

        'lnkShowPrice.Text = String.Format("{0:C}", totalPrice)
        Return totalPrice
    End Function
End Class