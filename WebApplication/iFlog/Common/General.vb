Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports Paramount.Modules.Logging.UIController
Imports System.Runtime.CompilerServices
Imports BetterClassified.Models

Module General

    Public Function SecurityCheckUserAdBooking(ByVal userId As String, ByVal bookingId As Integer) As Boolean
        If GeneralRoutine.CheckUserAdBooking(userId, bookingId) Then
            Return True
        Else
            Dim sb As New StringBuilder()
            sb.AppendLine("Security Infringement!")
            sb.AppendLine(String.Format("UserID = {0}", userId))
            sb.AppendLine(String.Format("BookingID = {0}", bookingId))
            sb.AppendLine("Action: User attempted to view a booking that does not belong to them.")

            Try
                Throw New Exception(sb.ToString)
            Catch ex As Exception
                ExceptionLogController(Of Exception).AuditException(ex)
            End Try
            HttpContext.Current.Response.Redirect(PageUrl.ErrorAccessDenied)
        End If
    End Function

    Public Function SecurityCheckUserAdBooking(ByVal userId As String, ByVal bookingId As Integer, ByVal adDesignId As Integer) As Boolean
        If GeneralRoutine.CheckUserAdBooking(userId, bookingId, adDesignId) Then
            Return True
        Else
            Dim sb As New StringBuilder()
            sb.AppendLine("Security Infringement;")
            sb.AppendLine("User attempted to view a booking that does not belong to them;")
            sb.AppendLine(String.Format("UserID = {0};", userId))
            sb.AppendLine(String.Format("BookingID = {0};", bookingId))

            Try
                Throw New Exception(sb.ToString)
            Catch ex As Exception
                ExceptionLogController(Of Exception).AuditException(ex)
            End Try

            HttpContext.Current.Response.Redirect(PageUrl.ErrorAccessDenied)
        End If
    End Function

    Public Sub ClearCurrentBookings()
        BookingController.ClearAdBooking()
        BundleController.ClearBundleBooking()
    End Sub

    Public Sub StartBookingStep2(ByVal type As SystemAdType)
        ' clear any current bookings
        BookingController.ClearAdBooking()
        BundleController.ClearBundleBooking()
        ' start the new booking now with no user defined
        If Membership.GetUser Is Nothing Then
            BookingController.StartAdBooking("")
        Else
            BookingController.StartAdBooking(Membership.GetUser().UserName)
        End If
        BookingController.BookingType = Booking.BookingAction.NormalBooking
        ' set the ad type to be Print
        BookingController.SetAdType(AdController.GetAdTypeByCode(type).AdTypeId)
        ' specify that the user is coming from the home page which means they may need to store the user again
        HttpContext.Current.Response.Redirect(PageUrl.BookingStep_2 + "?action=home")
    End Sub

    Public Sub StartBundleBookingStep2()
        BookingController.ClearAdBooking()
        BundleController.ClearBundleBooking()
        ' start a new booking
        If Membership.GetUser Is Nothing Then
            BundleController.StartNewBundleBooking("")
        Else
            BundleController.StartNewBundleBooking(Membership.GetUser().UserName)
        End If
        BookingController.BookingType = Booking.BookingAction.BundledBooking
        HttpContext.Current.Response.Redirect(PageUrl.BookingBundle_2 + "?action=home")
    End Sub

    Public Sub StartBundleBookingStep2(ByVal categoryId As Integer, ByVal subCategoryId As Integer, ByVal publications As List(Of Integer), _
                                       ByVal isOnlinePublicationIncluded As Boolean, ByVal printAdText As String, _
                                       ByVal headingText As String, ByVal lineAdGraphicId As String)
        Dim redirectionPage = String.Empty
        Dim query = "?action=home"

        BookingController.ClearAdBooking()
        BundleController.ClearBundleBooking()
        ' start a new booking
        If Membership.GetUser Is Nothing Then
            BundleController.StartNewBundleBooking("")
        Else
            BundleController.StartNewBundleBooking(Membership.GetUser().UserName)
        End If
        BookingController.BookingType = Booking.BookingAction.BundledBooking

        ' Set selections already made
        Dim bundle As New BundleController()
        bundle.SetCategory(categoryId, subCategoryId)
        bundle.SetPublication(publications, isOnlinePublicationIncluded)

        ' todo - LineAdNewDesign (set the properties for upgrading to premium)
        bundle.SetLineAdDetails(headingText, printAdText, False)
        If Not String.IsNullOrEmpty(lineAdGraphicId) Then
            bundle.SetLineAdGraphic(lineAdGraphicId)
        End If

        If publications.Count > 0 Then
            redirectionPage = PageUrl.BookingBundle_3 + query
        Else
            redirectionPage = PageUrl.BookingBundle_2 + query
        End If
        HttpContext.Current.Response.Redirect(redirectionPage)
    End Sub

    Public Sub StartBundleBookingStep2(ByVal adBookingId As Integer)
        Dim designId = AdController.GetOnlineAdByAdBooking(adBookingId).AdDesignId
        StartBundleBookingStep2(designId, SystemAdType.ONLINE)
    End Sub

    Public Sub StartBundleBookingStep2(ByVal AdDesignId As Integer, ByVal type As SystemAdType)
        ' starts a new bundle booking with existing ad details
        BookingController.ClearAdBooking()
        BundleController.ClearBundleBooking()
        ' start a new booking
        If Membership.GetUser Is Nothing Then
            BundleController.StartNewBundleBooking("")
        Else
            BundleController.StartNewBundleBooking(Membership.GetUser().UserName)
        End If
        BookingController.BookingType = Booking.BookingAction.BundledBooking

        ' set up the ad details passed through by the design id
        Dim lineAd As DataModel.LineAd
        Dim lineAdGraphicId As String = String.Empty
        Dim onlineAd As DataModel.OnlineAd
        Dim onlineGraphics As New List(Of String)

        If type = SystemAdType.LINE Then
            ' get the line ad 
            lineAd = AdController.GetLineAd(AdDesignId)
            If lineAd.UsePhoto Then
                lineAdGraphicId = AdController.GetLineAdGraphic(lineAd.AdDesignId).DocumentID
            End If
            ' get online details if they exist.. 
            onlineAd = AdController.GetOnlineAd(lineAd.AdDesignId)
            If onlineAd Is Nothing Then
                Dim anyLocationId = GeneralController.GetAnyLocationId()
                Dim anyAreaId = GeneralController.GetAnyLocationAreaId
                ' otherwise, we map the heading and online ad text to give a guide
                onlineAd = New DataModel.OnlineAd With {.Heading = lineAd.AdHeader, .Description = lineAd.AdText, _
                                                        .HtmlText = lineAd.AdText, .LocationId = anyLocationId, _
                                                        .LocationAreaId = anyAreaId, .Price = 0, .ContactName = String.Empty, _
                                                        .ContactType = "Email", .ContactValue = String.Empty}
            End If
        ElseIf type = SystemAdType.ONLINE Then
            ' get the online ad
            onlineAd = AdController.GetOnlineAd(AdDesignId)
            ' get the online ad graphics
            For Each gr In AdController.GetAdGraphics(onlineAd.AdDesignId)
                onlineGraphics.Add(gr.DocumentID)
            Next
            ' get the line ad (if exists)
            lineAd = AdController.GetLineAd(AdDesignId)
            If lineAd Is Nothing Then
                ' otherwise, map the line to online as a guide for details
                lineAd = New DataModel.LineAd With {.AdHeader = onlineAd.Heading, .AdText = onlineAd.Description, _
                                                    .UsePhoto = False, .UseBoldHeader = False}
            End If
        End If

        Dim controller As New BundleController
        ' set online details
        With onlineAd
            controller.SetOnlineAdDetails(.Heading, .Description, .HtmlText, .Price, .LocationId, .LocationAreaId, .ContactName, .ContactType, .ContactValue)
            controller.SetOnlineAdGraphics(onlineGraphics)
        End With
        ' set line ad details
        ' todo - LineAdNewDesign (set the properties for upgrading to premium)
        With lineAd
            controller.SetLineAdDetails(.AdHeader, .AdText, .UseBoldHeader)
            If (lineAd.UsePhoto) Then
                controller.SetLineAdGraphic(lineAdGraphicId)
            End If
        End With

        HttpContext.Current.Response.Redirect(PageUrl.BookingBundle_2 + "?action=home")
    End Sub

    ' Mapper extension
    <Extension()> _
    Public Function ToTutorAdModel(ByVal tutorAd As DataModel.TutorAd)
        Return New TutorAdModel With {.TutorAdId = tutorAd.TutorAdId, _
                                      .OnlineAdId = tutorAd.OnlineAdId, _
                                      .AgeGroupMax = tutorAd.AgeGroupMax, _
                                      .AgeGroupMin = tutorAd.AgeGroupMin, _
                                      .ExpertiseLevel = tutorAd.ExpertiseLevel, _
                                      .Objective = tutorAd.Objective, _
                                      .PricingOption = tutorAd.PricingOption, _
                                      .Subjects = tutorAd.Subjects, _
                                      .TravelOption = tutorAd.TravelOption, _
                                      .WhatToBring = tutorAd.WhatToBring}
    End Function
End Module
