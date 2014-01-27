Imports BetterclassifiedsCore

Partial Public Class DesignOnlineAd
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ucxDslUpload.MaxFileNumber = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_ONLINE_ADS, Utilities.Constants.CONST_KEY_Maximum_Image_Files)

        If Not Page.IsPostBack Then

            If ddlOnlineLocation.Items.Count = 0 Then
                ' bind the Locations / and areas
                BindLocations()
            End If
            
            ' load current data from the session
            If (Request.QueryString("action") = "back") Or BookingController.BookingType = Booking.BookingAction.Reschedule Then
                Dim onlineAdDesign As DataModel.AdDesign = BookingController.GetAdDesignDetails(SystemAdType.ONLINE)
                If Not onlineAdDesign Is Nothing Then
                    Dim newList As New List(Of String)
                    If (onlineAdDesign.AdGraphics.Count > 0) Then
                        ' pass the images down to be displayed also
                        For Each g In onlineAdDesign.AdGraphics
                            newList.Add(g.DocumentID)
                        Next
                        Images = newList
                    End If

                    BindOnlineAd(onlineAdDesign.OnlineAds(0), newList)
                End If

            End If
        End If

    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender

        ' limit the at title if we need to
        Dim titleLimit As Integer = AppKeyReader(Of Integer).ReadFromStore(AppKey.AdTitleLength, 160)
        If (titleLimit > 0) Then
            txtOnlineHeading.MaxLength = titleLimit
            lblHeadingLimit.Text = "Max Characters: " + titleLimit.ToString
        End If

    End Sub

    Private Sub chkNamePrivate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkNamePrivate.CheckedChanged
        If (chkNamePrivate.Checked) Then
            txtSeller.Enabled = False
            txtSeller.Text = "Private"
        Else
            txtSeller.Enabled = True
            txtSeller.Text = ""
        End If
    End Sub

    Private Sub ddlLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOnlineLocation.SelectedIndexChanged
        BindLocationAreas(ddlOnlineLocation.SelectedValue)
    End Sub

    Private Sub BindLocations()
        ddlOnlineLocation.DataSource = GeneralController.GetLocations
        ddlOnlineLocation.DataBind()
        ' binding the locations would force the bind on the location areas also
        ' pass the first item in the list
        If ddlOnlineLocation.Items.Count > 0 Then
            BindLocationAreas(ddlOnlineLocation.Items(0).Value)
        End If
    End Sub

    Private Sub BindLocationAreas(ByVal locationId As Integer)
        ddlOnlineLocationArea.DataSource = GeneralController.GetLocationAreas(locationId)
        ddlOnlineLocationArea.DataBind()
    End Sub

    Private Sub BindLocationAreas()
        ddlOnlineLocationArea.DataSource = GeneralController.GetLocationAreas()
        ddlOnlineLocationArea.DataBind()
    End Sub

    Public Sub BindOnlineAd(ByVal onlineAd As DataModel.OnlineAd, ByVal images As List(Of String))
        With onlineAd
            Me.Heading = .Heading
            Me.HtmlText = .HtmlText
            If (.Price > 0) Then
                Me.Price = .Price
            End If

            BindLocations()
            Me.ddlOnlineLocation.SelectedValue = .LocationId
            BindLocationAreas(.LocationId)
            Me.ddlOnlineLocationArea.SelectedValue = .LocationAreaId

            Me.ContactName = .ContactName
            Me.ContactType = .ContactType
            Me.ContactValue = .ContactValue
            Me.Images = images

        End With
    End Sub

    Public Sub BindOnlineAd(ByVal onlineAd As DataModel.OnlineAd, ByVal imageList As List(Of DataModel.AdGraphic), ByVal enforceConstraints As Boolean)
        If (onlineAd IsNot Nothing) Then
            With onlineAd
                Me.Heading = .Heading
                Me.HtmlText = .HtmlText
                If (.Price > 0) Then
                    Me.Price = .Price
                End If

                BindLocations()
                Me.ddlOnlineLocation.SelectedValue = .LocationId
                BindLocationAreas(.LocationId)
                Me.ddlOnlineLocationArea.SelectedValue = .LocationAreaId

                Me.ContactName = .ContactName
                Me.ContactType = .ContactType
                Me.ContactValue = .ContactValue

                If Me.Images IsNot Nothing And imageList IsNot Nothing Then
                    ' check if there's no images in the control's session and there's images in bundled booking session
                    If Me.Images.Count = 0 And imageList.Count > 0 Then
                        For Each gr As DataModel.AdGraphic In imageList
                            Me.Images.Add(gr.DocumentID)
                        Next
                    End If
                End If
            End With
        End If
    End Sub

#Region "Control Properties"

    Public Property Heading() As String
        Get
            Return txtOnlineHeading.Text
        End Get
        Set(ByVal value As String)
            txtOnlineHeading.Text = value
        End Set
    End Property

    Public ReadOnly Property Description() As String
        Get
            Return radEditor.Text
        End Get
    End Property

    Public Property HtmlText() As String
        Get
            Return radEditor.Content
        End Get
        Set(ByVal value As String)
            radEditor.Content = value
        End Set
    End Property

    Public Property Price() As Decimal
        Get
            If txtPrice.Text = "" Then
                Return 0D
            Else
                Return CDec(txtPrice.Text)
            End If
        End Get
        Set(ByVal value As Decimal)
            txtPrice.Text = value.ToString
        End Set
    End Property

    Public Property LocationId() As Integer
        Get
            Return ddlOnlineLocation.SelectedValue
        End Get
        Set(ByVal value As Integer)
            Me.ddlOnlineLocation.SelectedValue = value
        End Set
    End Property

    Public Property LocationArea() As Integer
        Get
            Return ddlOnlineLocationArea.SelectedValue
        End Get
        Set(ByVal value As Integer)
            Me.ddlOnlineLocationArea.SelectedValue = value
        End Set
    End Property

    Public Property ContactName() As String
        Get
            Return HttpUtility.HtmlEncode(txtSeller.Text)
        End Get
        Set(ByVal value As String)
            txtSeller.Text = HttpUtility.HtmlEncode(value)
        End Set
    End Property

    Public Property ContactType() As String
        Get
            Return ddlContactType.SelectedValue
        End Get
        Set(ByVal value As String)
            ddlContactType.SelectedValue = value
        End Set
    End Property

    Public Property ContactValue() As String
        Get
            Return HttpUtility.HtmlEncode(txtContactValue.Text)
        End Get
        Set(ByVal value As String)
            txtContactValue.Text = HttpUtility.HtmlEncode(value)
        End Set
    End Property

    Public Property Images() As List(Of String)
        Get
            Dim list As New List(Of String)
            'For Each i In ucxDslUpload.Files
            '    list.Add(i.DocumentId)
            'Next
            Return list
        End Get
        Set(ByVal value As List(Of String))
            Dim c As New Collection
            For Each i As String In value
                c.Add(New With {.DocumentId = i}, i)
            Next
            'ucxDslUpload.Files = c
        End Set
    End Property

    Public Property BookingReference() As String
        Get
            'Return ucxDslUpload.Reference
            Return String.Empty
        End Get
        Set(ByVal value As String)
            'ucxDslUpload.Reference = value
        End Set
    End Property

#End Region

End Class