Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.DataModel

Partial Public Class DesignLineAd
    Inherits System.Web.UI.UserControl

    Const TextNotAvailable As String = "Not available for Free Ads. Click above to Upgrade"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' load current data from the session if there is anything

            If BookingController.BookingType = Booking.BookingAction.SpecialBooking Or _
                    BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                Exit Sub
            End If

            If Request.QueryString("action") = "back" Or BookingController.BookingType = Booking.BookingAction.Reschedule Then
                Dim lineAd As AdDesign = BookingController.GetAdDesignDetails(SystemAdType.LINE)
                If Not lineAd Is Nothing Then
                    With lineAd
                        lineAdDescriptionBox.Text = .LineAds(0).AdText
                        txtHeader.Text = .LineAds(0).AdHeader
                        CountWords()
                    End With
                End If
            End If

            CountWords()
        End If
        Me.DataBind()
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        ' check out page property if the image has been uploaded
        Dim headingLimit As Integer = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_LINE_ADS, Utilities.Constants.CONST_KEY_BoldHeadingLimit)
        If (headingLimit > 0) Then
            txtHeader.MaxLength = headingLimit
            lblHeadingLimit.Text = "Max Characters: " + headingLimit.ToString
        End If
    End Sub

    Private Sub btnGetWords_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetWords.Click
        CountWords()
    End Sub

    Public Sub CountWords()
        lblWords.Text = GeneralRoutine.LineAdWordCount(lineAdDescriptionBox.Text).ToString
    End Sub

    Public Sub BindLineAd(ByVal lineAd As DataModel.LineAd)
        If Not lineAd Is Nothing Then
            With lineAd
                Me.AdHeader = .AdHeader
                Me.AdText = .AdText
                Me.WordLimit = .NumOfWords
            End With
        End If
    End Sub

    Public Sub BindLineAd(ByVal lineAd As DataModel.LineAd, ByVal enforceLimits As Boolean)
        If Not lineAd Is Nothing Then
            With lineAd
                Me.AdHeader = .AdHeader
                Me.AdText = .AdText
                If enforceLimits Then
                    Me.WordLimit = .NumOfWords
                    Me.pnlBoldHeading.Visible = lineAd.UseBoldHeader
                End If
            End With
        End If
    End Sub

    Protected Sub btnPreview_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPreview.Click
        lineAdDescriptionBox.Text = lineAdDescriptionBox.Text
    End Sub

#Region "Properties"

    Public Property AdHeader() As String
        Get
            Dim r = New Regex("\s+")
            If Not txtHeader.Text.Equals(TextNotAvailable) Then
                Return r.Replace(txtHeader.Text, " ")
            End If
            Return String.Empty
        End Get
        Set(ByVal value As String)
            txtHeader.Text = value
        End Set
    End Property

    Public Property AdText() As String
        Get
            Return lineAdDescriptionBox.Text
        End Get
        Set(ByVal value As String)
            lineAdDescriptionBox.Text = value
        End Set
    End Property

    Public Property UseBoldHeading() As Boolean
        Get
            If txtHeader.Text <> String.Empty Then
                ViewState("useBoldHeading") = True
            Else
                ViewState("useBoldHeading") = False
            End If
            Return ViewState("useBoldHeading")
        End Get
        Set(ByVal value As Boolean)
            ViewState("useBoldHeading") = value
        End Set
    End Property

    Public Property ShowWordCount() As Boolean
        Get
            Return ViewState("showWordCount")
        End Get
        Set(ByVal value As Boolean)
            ViewState("showWordCount") = value
            'pnlWordsUpdate.Visible = value
        End Set
    End Property

    Public Property BookingReference() As String
        Get
            Return ViewState("bookingReference")
        End Get
        Set(ByVal value As String)
            ViewState("bookingReference") = value
        End Set
    End Property

    Public Property WordLimit() As Integer
        Get
            Return ViewState("wordLimit")
        End Get
        Set(ByVal value As Integer)
            ViewState("wordLimit") = value
            If value > 0 Then
                lblWordLimit.Text = "Currently you have a limit of " + value.ToString + " words for this ad."
            End If
        End Set
    End Property

    Public Property IsBoldHeadingEnabled() As Boolean
        Get
            Return ViewState("IsBoldHeadingEnabled")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsBoldHeadingEnabled") = value
            txtHeader.Enabled = value
        End Set
    End Property

#End Region

#Region "Upgrade"

    Public Event UpgradeSelected As EventHandler

    Private Sub btnUpgrade_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpgrade.Click, btnUpgradeText.Click
        ' Raise the event to handle in the page.
        RaiseEvent UpgradeSelected(sender, e)
    End Sub

    Public Property IsUpgradable() As Boolean
        Get
            Return ViewState("IsUpgradable")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsUpgradable") = value
            If value Then
                btnUpgrade.Visible = True
                txtHeader.Text = TextNotAvailable
                txtHeader.ToolTip = TextNotAvailable
            Else
                txtHeader.Enabled = True
            End If
        End Set
    End Property

    Public Property IsUpgradableText() As Boolean
        Get
            Return ViewState("IsUpgradableText")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsUpgradableText") = value
            btnUpgradeText.Visible = True
        End Set
    End Property
#End Region
    
End Class