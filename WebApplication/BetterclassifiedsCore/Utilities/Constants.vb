Namespace Utilities

    Public Structure Environment
        Public Const Live As String = "LIVE"
        Public Const DEV As String = "DEV"
        Private None As String
    End Structure
    Public Class Constants

        Public Enum CustomerSearchType
            FirstName = 0
            LastName = 1
            Phone = 3
            BusinessName = 4
            Username = 5
        End Enum

        Public Const CONST_SYSTEM_MaxRequest_Length As Integer = 4194304       ' 4MB maximum file accepted = 4194304 bytes for a request.
        Public Const CONST_SYSTEM_adPreviewLength As Integer = 50              ' Online Ad Preview length in the system
        Public Const CONST_REGEX_Email As String = "\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b"

#Region "GST"
        Public Const GST As Decimal = 0.1
#End Region

#Region "Line Ad Image Constants"

        ' *************
        ' Constants for calculating the number of pixels required for converting line ad image dimensions and quality
        ' *************

        Public Const CONST_LineAd_Height_Inches As Double = 1.10236         ' 28 mm = 1.1811 Inches
        Public Const CONST_LineAd_Width_Inches As Double = 1.1811           ' 30 mm = 1.10236 Inches
        Public Const CONST_LineAd_Image_dpi As Integer = 300                ' 200 DPI accepted

#End Region

#Region "Error Handling"

        ' *************
        ' Constants for handling errors in Betterclassifieds Web Application
        ' *************
        Public Const CONST_ERROR_DEFAULT_URL As String = "~/Error/Default.aspx"
        Public Const CONST_ERROR_CONNECTION As String = "conn"
        Public Const CONST_ERROR_SETTINGS As String = "setting"
        Public Const CONST_ERROR_REQUEST_SIZE As String = "size"

#End Region

#Region "Application Module Names"

        ' *************
        ' Constants stored in the DB under App Settings table. Used for retrieving the Module Information
        ' *************
        Public Const CONST_MODULE_SYSTEM As String = "System"
        Public Const CONST_MODULE_LINE_ADS As String = "LineAds"
        Public Const CONST_MODULE_ONLINE_ADS As String = "OnlineAds"
        Public Const CONST_MODULE_ADBOOKING As String = "AdBooking"

#End Region

#Region "Application Settings"

        ' *************
        ' Constants stored in the DB under App Settings table. Helps retrieve each Application Key Name
        ' *************
        Public Const CONST_KEY_BoldHeadingLimit As String = "BoldHeadingLimit"
        Public Const CONST_KEY_AdTitleLength As String = "AdTitleLength"
        Public Const CONST_KEY_Image_Store_Path As String = "ImageStoragePath"
        Public Const CONST_KEY_Maximum_Insertions As String = "MaximumInsertions"
        Public Const CONST_KEY_Maximum_Image_Files As String = "MaximumImageFiles"
        Public Const CONST_KEY_Online_AdDurationDays As String = "AdDurationDays"
        Public Const CONST_KEY_LineAd_Word_Separators As String = "WordSeparators"
        Public Const CONST_KEY_LineAd_Word_MaxLength As String = "WordMaxLength"
        Public Const CONST_KEY_Online_BundleWithPrint As String = "BundleWithPrint"
        Public Const CONST_KEY_System_AdminEmails As String = "AdminNotificationAccounts"
        Public Const CONST_KEY_System_SupportEmails As String = "SupportNotificationAccounts"
        Public Const CONST_KEY_System_ExpiredHistoryMonths As String = "ExpiredHistoryMonths"
        Public Const CONST_KEY_System_LastBackupTime As String = "LastBackupTime"
        Public Const CONST_KEY_System_EnableFAQPage As String = "EnableFAQPage"

#End Region


    End Class

End Namespace