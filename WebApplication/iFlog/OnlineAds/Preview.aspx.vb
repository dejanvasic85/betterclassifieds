Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BusinessEntities
Imports BetterClassified

Imports Paramount
Imports Paramount.Betterclassifieds.Business.Repository

Partial Public Class Preview
    Inherits System.Web.UI.Page
    Private ReadOnly _adRepository As IAdRepository

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Fix issue with ie 8, where page is being cached.
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim onlineAd As BusinessEntities.OnlineAdEntity = Nothing

        Select Case Request.QueryString("viewType").ToString.ToLower
            Case "db"
                ' then load the ad details from the database by the ad design
                onlineAd = AdController.GetOnlineAdEntityByDesign(Request.QueryString("id"), True)
                ' bind the online ad to the user control
                ucxOnlineAd.BindOnlineAd(onlineAd)

                If (onlineAd.OnlineAdTag.HasValue) Then
                    ' Find the control
                    Dim adTypeControl = pnlAdTypes.FindControl(Of OnlineAdViewControl)("ucx" + onlineAd.OnlineAdTag)
                    adTypeControl.Visible = True
                    adTypeControl.DatabindAd(_adRepository.GetTutorAd(onlineAd.OnlineAdId))
                End If

            Case "session"
                If BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                    ' grab the session values from the booking cart
                    With BundleBooking.BundleController.BundleCart
                        Dim imageList As New List(Of String)
                        For Each g In .OnlineAdGraphics
                            imageList.Add(g.DocumentID)
                        Next
                        Dim locationValue = GeneralController.GetLocationById(.OnlineAd.LocationId).Title
                        Dim areaValue = GeneralController.GetLocationAreaById(.OnlineAd.LocationAreaId).Title
                        onlineAd = .OnlineAd.ToOnlineAdEntity(imageList, .MainParentCategory, .MainSubCategory, DateTime.Today, .BookReference, locationValue, areaValue)
                    End With

                    ' Bind the online ad to the user control
                    ucxOnlineAd.BindOnlineAd(onlineAd)

                    ' Display the ad details 
                    If BundleBooking.BundleController.BundleCart.TutorAd IsNot Nothing Then
                        Dim adControl = pnlAdTypes.FindControl(Of OnlineAdViewControl)("ucxTutors")
                        adControl.Visible = True
                        adControl.DatabindAd(BundleBooking.BundleController.BundleCart.TutorAd.ToTutorAdModel)
                    End If

                ElseIf BookingController.AdBookCart IsNot Nothing Then
                    With BookingController.AdBookCart
                        Dim cartAd = .Ad.AdDesigns(0).OnlineAds(0)
                        Dim subCategory = CategoryController.GetMainCategoryById(.MainCategoryId)
                        Dim parentCategory = CategoryController.GetMainCategoryById(.ParentCategoryId)
                        Dim locationValue = GeneralController.GetLocationById(cartAd.LocationId).Title
                        Dim areaValue = GeneralController.GetLocationAreaById(cartAd.LocationAreaId).Title
                        onlineAd = cartAd.ToOnlineAdEntity(.ImageList, _
                                                              parentCategory, _
                                                              subCategory, _
                                                              DateTime.Today, _
                                                              .BookReference, _
                                                              locationValue, _
                                                              areaValue)
                        ucxOnlineAd.BindOnlineAd(onlineAd, BookingController.AdBookCart.ImageList, DateTime.Now, parentCategory.MainCategoryId, subCategory.MainCategoryId)

                        ' Display the ad details 
                        If BookingController.AdBookCart.TutorAd IsNot Nothing Then
                            Dim adControl = pnlAdTypes.FindControl(Of OnlineAdViewControl)("ucxTutors")
                            adControl.Visible = True
                            adControl.DatabindAd(BookingController.AdBookCart.TutorAd.ToTutorAdModel)
                        End If

                    End With
                End If
        End Select

    End Sub

End Class