﻿Namespace Utilities

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