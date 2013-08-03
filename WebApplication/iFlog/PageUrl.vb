Public Class PageUrl

    ' item view
    Public Shared Function AdViewItem(ByVal id As String, Optional ByVal preview As Boolean = False, Optional ByVal typeOfId As String = "bkId") As String
        Return String.Format("~/OnlineAds/AdView.aspx?id={0}&preview={1}&type={2}", id, preview, typeOfId)
    End Function

    ' Account
    Public Shared Function ExtendBooking(ByVal adBookingId As Integer) As String
        Return String.Format("~/MemberAccount/ExtendBooking.aspx?AdBookingId={0}", adBookingId)
    End Function

    Public Shared Function ViewInvoice(ByVal reference As String) As String
        Return String.Format("~/MemberAccount/ViewTransaction.aspx?ref={0}", reference)
    End Function

    Public Shared Function EditLineAd(ByVal adBookingId As Integer) As String
        Return String.Format("~/MemberAccount/EditLineAd.aspx?bkId={0}", adBookingId)
    End Function

    Public Shared Function EditOnlineAd(ByVal adBookingId As Integer) As String
        Return String.Format("~/MemberAccount/EditOnlineAd.aspx?bkId={0}", adBookingId)
    End Function

    Public Shared Function MemberBookings(Optional ByVal view As BetterClassified.UI.Models.UserBookingViewType = BetterClassified.UI.Models.UserBookingViewType.Current, _
                                          Optional ByVal extensionComplete As Boolean = False) As String
        Return String.Format("~/MemberAccount/Bookings.aspx?view={0}&extension={1}", view, extensionComplete)
    End Function

    Public Shared Function MemberDetails() As String
        Return "~/MemberAccount/MemberDetails.aspx"
    End Function

    Public Const CreditCardPaymentPage As String = "~/payment/cc.aspx"
    Public Const PaypalPaymentPage As String = "~/payment/pp.aspx"

    ' search results
    Public Const SearchCategoryResults As String = "~/OnlineAds/Default.aspx"
    Public Const SearchSubCategoryResults As String = "~/OnlineAds/Default.aspx"

    ' booking steps
    Public Const BookingStep_1 As String = "~/Booking/Step1.aspx"
    Public Const BookingStep_2 As String = "~/Booking/Step2.aspx"
    Public Const BookingStep_3 As String = "~/Booking/Step3.aspx"
    Public Const BookingStep_4 As String = "~/Booking/Step4.aspx"
    Public Const BookingStep_5 As String = "~/Booking/Step5.aspx"

    ' bundle booking steps
    Public Const BookingBundle_2 As String = "~/BundleBooking/BundlePage2.aspx"
    Public Const BookingBundle_3 As String = "~/BundleBooking/BundlePage3.aspx"
    Public Const BookingBundle_4 As String = "~/BundleBooking/BundlePage4.aspx"
    Public Const BookingBundle_5 As String = "~/BundleBooking/BundlePage5.aspx"

    ' specials
    Public Const BookSpecialCategories As String = "~/Booking/BookSpecialCategories.aspx"
    Public Const BookSpecialMain As String = "~/Booking/BookSpecial.aspx"

    ' login page
    Public Const Login As String = "~/Login.aspx"

    ' online ad search result
    Public Const OnlineAdSearch As String = "~/OnlineAds/Default.aspx"

    ' Document Service Library
    Public Const DSL As String = "~/dsl/document.ashx?id="
    Public Const DSLThumb As String = "~/dsl/document.ashx?thumb=y&id="
    Public Const DSLAdSpace As String = "~/dsl/document.ashx?docType=3&id="
    Public Const DSLPaperImage As String = "~/dsl/document.ashx?docType=4&id="
    Public Const DSLPaperThumb As String = "~/dsl/document.ashx?docType=5&id="

    ' error Pages
    Public Const ErrorAccessDenied As String = "~/Error/AccessDenied.aspx"

    ' Help Pages
    Public Const HelpContact As String = "~/Help/Contact.aspx"
    Public Const RatesPage As String = "~/Rates.aspx"



End Class
