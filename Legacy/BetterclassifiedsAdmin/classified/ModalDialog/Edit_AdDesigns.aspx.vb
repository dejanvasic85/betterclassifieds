Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Telerik.Web.UI

Partial Public Class Edit_AdDesigns
    Inherits System.Web.UI.Page

    Private _adBookingId As Integer
    Private _booking As DataModel.AdBooking

    Private Const _lineAdTabIndex As Integer = 0
    Private Const _onlineAdTabIndex As Integer = 1
    Private Const _adBookingParameter As String = "adBookingId"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _adBookingId = Request.QueryString(_adBookingParameter)
        If _adBookingId > 0 Then
            _booking = BookingController.GetAdBookingById(_adBookingId)
            lblAdId.Text = "Ad ID: " + _adBookingId.ToString
        End If

        If Not Page.IsPostBack Then
            ' enable/disable line ad
            IsLineAdEnabled = EnableLineAd()
            ' enable disable online ad
            IsOnlineAdEnabled = EnableOnlineAd()

            SetupOnlineUpload()
            SetupPrintUpload()

        End If
    End Sub

#Region "Selecting"

    Private Sub linqSourceOnlineAd_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqSourceOnlineAd.Selecting
        If IsOnlineAdEnabled Then
            e.Result = AdController.OnlineAdByBookingId(_adBookingId)
        End If
    End Sub
#End Region

#Region "Update Online Ad"

    Private Sub btnUpdateOnline_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateOnline.Click
        Try ' update online ad details
            dtlOnlineAd.UpdateItem(True)
            lblOnlineMessage.Text = "Successfully Updated Online Ad Details"
            lblOnlineMessage.CssClass = "message-success"
        Catch ex As Exception
            lblOnlineMessage.Text = ex.Message
            lblOnlineMessage.CssClass = "message-fail"
        End Try
    End Sub

    Private Sub dtlOnlineAd_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlOnlineAd.DataBound
        If IsOnlineAdEnabled Then
            If ddlLocation.Items.Count = 0 Then
                DataBindLocations()
            End If
            ddlLocation.SelectedValue = dtlOnlineAd.DataItem.LocationId
            DataBindLocationAreas(dtlOnlineAd.DataItem.LocationId)
            ddlLocationArea.SelectedValue = dtlOnlineAd.DataItem.LocationAreaId
            Dim r As RadEditor = DirectCast(dtlOnlineAd.FindControl("radEditor"), RadEditor)
            r.Content = dtlOnlineAd.DataItem.HtmlText
        End If
    End Sub

    Private Sub dtlOnlineAd_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles dtlOnlineAd.ItemUpdating
        e.NewValues.Add("LocationId", ddlLocation.SelectedValue)
        e.NewValues.Add("LocationAreaId", ddlLocationArea.SelectedValue)
        e.NewValues.Add("HtmlText", DirectCast(dtlOnlineAd.FindControl("radEditor"), RadEditor).Content)
        e.NewValues.Add("Description", DirectCast(dtlOnlineAd.FindControl("radEditor"), RadEditor).Text)
    End Sub

    Private Sub linqSourceOnlineAd_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqSourceOnlineAd.Updating
        ' update online ad details (and preserve some old data)
        Using db = DataModel.BetterclassifiedsDataContext.NewContext
            Dim onlineAdId As Integer = CType(e.OriginalObject.OnlineAdId, Integer)
            Dim ad = db.OnlineAds.Where(Function(i) i.OnlineAdId = onlineAdId).SingleOrDefault
            ' set the properties of the object
            With ad
                .AdDesignId = e.OriginalObject.AdDesignId
                .ContactName = e.NewObject.ContactName
                .ContactPhone = e.NewObject.ContactPhone
                .ContactEmail = e.NewObject.ContactEmail
                .Heading = e.NewObject.Heading
                .Description = e.NewObject.Description
                .HtmlText = e.NewObject.HtmlText
                .LocationAreaId = e.NewObject.LocationAreaId
                .LocationId = e.NewObject.LocationId
                .NumOfViews = e.OriginalObject.NumOfViews
                .Price = e.NewObject.Price
            End With
            db.SubmitChanges() ' update changes to the database
        End Using
        e.Cancel = True
    End Sub

#End Region

#Region "Databinding"

    Private Sub DataBindLocations()
        ddlLocation.DataSource = GeneralController.GetLocations
        ddlLocation.DataBind()
        If ddlLocation.Items.Count > 0 Then
            DataBindLocationAreas(ddlLocation.SelectedValue)
        End If
    End Sub

    Private Sub DataBindLocationAreas(ByVal locationId As Integer)
        ddlLocationArea.DataSource = GeneralController.GetLocationAreas(locationId)
        ddlLocationArea.DataBind()
    End Sub

    Private Sub ddlLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        DataBindLocationAreas(ddlLocation.SelectedValue)
    End Sub

#End Region

#Region "Helper Methods"

    Private Function EnableLineAd() As Boolean
        Dim lineAd = AdController.LineAdByBookingId(_adBookingId)
        Dim isEnabled As Boolean = (lineAd Is Nothing = False)
        radPageLineAd.Visible = isEnabled
        radTabStrip.Tabs(_lineAdTabIndex).Enabled = isEnabled
        If Not isEnabled Then
            radTabStrip.SelectedIndex = _onlineAdTabIndex
            radMultiPage.SelectedIndex = _onlineAdTabIndex
        Else
            radTabStrip.SelectedIndex = _lineAdTabIndex
            radMultiPage.SelectedIndex = _lineAdTabIndex
        End If
        Return isEnabled
    End Function

    Private Function EnableOnlineAd() As Boolean
        Dim ad = AdController.OnlineAdByBookingId(_adBookingId)
        Dim isEnabled As Boolean = (ad Is Nothing = False)
        radPageOnlineAd.Visible = isEnabled
        radTabStrip.Tabs(_onlineAdTabIndex).Enabled = isEnabled

        If isEnabled Then
            DataBindLocations()
        End If

        Return isEnabled
    End Function

#End Region

#Region "Properties"

    Private Property IsLineAdEnabled() As Boolean
        Get
            Return ViewState("isLineAdEnabled")
        End Get
        Set(ByVal value As Boolean)
            ViewState("isLineAdEnabled") = value
        End Set
    End Property

    Private Property IsOnlineAdEnabled() As Boolean
        Get
            Return ViewState("isOnlineAdEnabled")
        End Get
        Set(ByVal value As Boolean)
            ViewState("isOnlineAdEnabled") = value
        End Set
    End Property

#End Region

#Region "File Upload"

    Private Sub SetupOnlineUpload()
        ' Toggle visibility for the upload online panel
        pnlOnline.Visible = IsOnlineAdEnabled

        If IsOnlineAdEnabled Then
            ' Fetch the data we need to work with
            Dim onlineAd = AdController.GetOnlineAdByAdBooking(_adBookingId)
            Dim graphics = AdController.GetAdGraphicDocuments(onlineAd.AdDesignId)

            OnlineAdDesignId = onlineAd.AdDesignId
            paramountFileUpload.DocumentCategory = Paramount.Common.DataTransferObjects.DSL.DslDocumentCategoryType.OnlineAd
            paramountFileUpload.ReferenceData = _booking.BookReference
            paramountFileUpload.ImageList = graphics
            paramountFileUpload.MaxFiles = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, Utilities.Constants.CONST_KEY_Maximum_Image_Files)
        End If
    End Sub

    Private Sub SetupPrintUpload()
        pnlPrint.Visible = IsLineAdEnabled
        If IsLineAdEnabled Then
            Dim documentId = String.Empty
            Dim lineAd = AdController.GetLineAdByBookingId(_adBookingId)
            Dim imageList = AdController.GetAdGraphicDocuments(lineAd.AdDesignId)
            If imageList.Count > 0 Then
                ' We only use the first document
                documentId = imageList(0)
            End If
            pnlPrint.Visible = True
            PrintAdDesignId = lineAd.AdDesignId
            paramountWebImageMaker.ImageHeight = Convert.ToInt32(BetterclassifiedSetting.LineAdImageHeight * BetterclassifiedSetting.LineAdImageDPI)
            paramountWebImageMaker.ImageWidth = Convert.ToInt32(BetterclassifiedSetting.LineAdImageWidth * BetterclassifiedSetting.LineAdImageDPI)
            paramountWebImageMaker.ReferenceData = _booking.BookReference
            paramountWebImageMaker.ImageResolution = BetterclassifiedSetting.LineAdImageDPI
            paramountWebImageMaker.DocumentID = documentId
        End If
    End Sub

    Private Sub paramountFileUpload_RemoveDocument(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountFileUpload.RemoveDocument
        AdController.DeleteAdGraphic(OnlineAdDesignId, e.DocumentID)
    End Sub

    Private Sub paramountFileUpload_UploadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles paramountFileUpload.UploadComplete
        ' Call the controller to save all the images into the DB
        AdController.CreateAdGraphics(OnlineAdDesignId, paramountFileUpload.ImageList)
    End Sub

    Private Sub paramountWebImageMaker_RemoveComplete(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountWebImageMaker.RemoveComplete
        AdController.DeleteAdGraphic(PrintAdDesignId, e.DocumentID)
    End Sub

    Private Sub paramountWebImageMaker_UploadComplete(ByVal sender As Object, ByVal e As BetterClassified.UI.Dsl.SelectedDocumentArgs) Handles paramountWebImageMaker.UploadComplete
        ' Set up parameter to save ad graphic
        Dim documents As New List(Of String)
        documents.Add(e.DocumentID)
        AdController.CreateAdGraphics(PrintAdDesignId, documents)
    End Sub

    Private Property PrintAdDesignId() As Integer
        Get
            Return ViewState("PrintAdDesignId")
        End Get
        Set(ByVal value As Integer)
            ViewState("PrintAdDesignId") = value
        End Set
    End Property

    Private Property OnlineAdDesignId() As Integer
        Get
            Return ViewState("OnlineAdDesignId")
        End Get
        Set(ByVal value As Integer)
            ViewState("OnlineAdDesignId") = value
        End Set
    End Property

#End Region


    Private Sub lineAdDetails_SubmitEvent(sender As Object, e As EventArgs) Handles lineAdDetails.Submit
        lblLineMessage.Text = "Line Ad details updated successfully..."
        lblLineMessage.CssClass = "message-success"
    End Sub
End Class