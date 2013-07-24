﻿Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.ParameterAccess
Imports Paramount.Modules.Logging.UIController

Namespace Controls
    Partial Public Class OnlineAdViewWithDiv
        Inherits System.Web.UI.UserControl

        Const MainCategoryViewState = "MainCategoryViewState"
        Const SubCategoryViewState = "SubCategoryViewState"

        Protected Sub LoadPage(ByVal id As String, ByVal type As String)
            If _preview = True Then
                mainHeaderItemPageIDHits.Visible = False
                divSitemap.Visible = False
            End If
            Try
                Select Case type.ToLower.Trim
                    Case "db"
                        divContent.InnerHtml = AdController.GetOnlineAdHtml(id)
                    Case "session"
                        If BookingController.BookingType = BetterclassifiedsCore.Booking.BookingAction.BundledBooking Then
                            divContent.InnerHtml = BetterclassifiedsCore.BundleBooking.BundleController.BundleCart.OnlineAd.HtmlText
                        ElseIf BookingController.BookingType = BetterclassifiedsCore.Booking.BookingAction.SpecialBooking Then
                            divContent.InnerHtml = BookingController.SpecialBookCart.OnlineAd.HtmlText
                        Else
                            divContent.InnerHtml = BookingController.AdBookCart.Ad.AdDesigns(0).OnlineAds(0).HtmlText
                        End If
                End Select
            Catch ex As Exception
                ' show an error 
                divContent.InnerHtml = "<b>Unable to display ad details at this time.</b>"
                ' write a log entry into the database
                ExceptionLogController(Of Exception).AuditException(ex)
            End Try
        End Sub

        Private _preview As Boolean
        Public Property PreviewOnly() As Boolean
            Get
                Return _preview
            End Get
            Set(ByVal value As Boolean)
                _preview = value
            End Set
        End Property

        Public Property MainCategoryId() As Integer
            Get
                Return CInt(ViewState(MainCategoryViewState))
            End Get
            Private Set(ByVal value As Integer)
                ViewState(MainCategoryViewState) = value
            End Set
        End Property

        Public Property SubCategoryId() As Integer
            Get
                Return CInt(ViewState(SubCategoryViewState))
            End Get
            Private Set(ByVal value As Integer)
                ViewState(SubCategoryViewState) = value
            End Set
        End Property

        Public Sub BindOnlineAd(ByVal onlineAd As BusinessEntities.OnlineAdEntity)
            With onlineAd
                ' html text
                If onlineAd Is Nothing Then
                    LoadPage(0, "session")
                End If

                If onlineAd.OnlineAdId > 0 Then
                    LoadPage(onlineAd.OnlineAdId, "db")
                Else
                    LoadPage(0, "session")
                End If

                lblHeading.Text = .Heading
                lblLocation.Text = .LocationValue
                lblArea.Text = .AreaValue
                lblNumOfViews.Text = .NumOfViews.ToString
                lblDatePosted.Text = String.Format("{0:D}", .DatePosted)
                If .AdBookingId.HasValue Then
                    lblIFlogID.Text = .AdBookingId
                    ' set up the sitemap for this ad
                    lblID.Text = .AdBookingId
                End If

                If .ContactName = "" Or .ContactName = "Private" Then
                    objContactName.Visible = False
                Else
                    lblContactName.Text = .ContactName
                End If

                If .ContactValue = "" Or .ContactName = "Private" Then
                    objContactDetail.Visible = False
                Else
                    If .ContactType.ToLower = "email" Then
                        Dim r As Regex = New Regex(Utilities.Constants.CONST_REGEX_Email, RegexOptions.IgnoreCase)
                        Dim m As Match = r.Match(.ContactValue)
                        If m.Success Then
                            litContactDetails.Text = String.Format("<a href=""mailto:{0}"">{1}</a>", .ContactValue, .ContactValue)
                        Else
                            litContactDetails.Text = .ContactValue
                        End If
                    Else
                        litContactDetails.Text = .ContactValue
                    End If
                End If

                objPrice.Visible = onlineAd.Price > 0
                lblPrice.Text = String.Format("{0:C}", .Price)

                MainCategoryId = .ParentCategory.MainCategoryId
                lnkCategory.Text = .ParentCategory.Title

                SubCategoryId = .SubCategory.MainCategoryId.ToString
                lnkSubCategory.Text = .SubCategory.Title

                paramountGallery.ImageList = onlineAd.ImageList
            End With
        End Sub

        Public Sub BindOnlineAd(ByVal onlineAd As DataModel.OnlineAd, ByVal imageList As List(Of String), ByVal datePosted As DateTime, ByVal parentCategoryID As Integer, ByVal subCategoryId As Integer)
            With onlineAd

                ' html text
                If onlineAd Is Nothing Then
                    LoadPage(0, "session")
                End If

                If onlineAd.OnlineAdId > 0 Then
                    LoadPage(onlineAd.OnlineAdId, "db")
                Else
                    LoadPage(0, "session")
                End If

                lblHeading.Text = .Heading
                lblLocation.Text = GeneralController.GetLocationById(.LocationId).Title
                lblArea.Text = GeneralController.GetLocationAreaById(.LocationAreaId).Title
                lblNumOfViews.Text = .NumOfViews.ToString
                lblDatePosted.Text = String.Format("{0:D}", datePosted)

                'If .AdDesignId > 0 Then
                '    lblIFlogID.Text = .AdDesignId
                '    lblID.Text = .AdDesignId
                'End If

                If .ContactName = "" Or .ContactName = "Private" Then
                    objContactName.Visible = False
                Else
                    lblContactName.Text = .ContactName
                End If

                If .ContactValue = "" Or .ContactName = "Private" Then
                    objContactDetail.Visible = False
                Else
                    If .ContactType.ToLower = "email" Then
                        litContactDetails.Text = String.Format("<a href='mailto:{0}'></a>", .ContactValue)
                    Else
                        litContactDetails.Text = .ContactValue
                    End If
                End If

                objPrice.Visible = onlineAd.Price > 0
                lblPrice.Text = String.Format("{0:C}", .Price)

                MainCategoryId = parentCategoryID
                lnkCategory.Text = CategoryController.GetMainCategoryById(parentCategoryID).Title

                subCategoryId = subCategoryId
                lnkSubCategory.Text = CategoryController.GetMainCategoryById(subCategoryId).Title

                paramountGallery.ImageList = imageList

            End With
        End Sub

        Private Sub lnkCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCategory.Click
            OnlineSearchParameter.Clear()
            OnlineSearchParameter.Category = Me.MainCategoryId
            Response.Redirect(PageUrl.OnlineAdSearch)
        End Sub

        Private Sub lnkSubCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSubCategory.Click
            OnlineSearchParameter.Clear()
            OnlineSearchParameter.Category = Me.MainCategoryId
            OnlineSearchParameter.SubCategory = Me.SubCategoryId
            Response.Redirect(PageUrl.OnlineAdSearch)
        End Sub


    End Class
End Namespace