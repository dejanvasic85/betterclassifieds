Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.BundleBooking
Imports BetterClassified.UI.WebPage

Partial Public Class AdType
    Inherits BaseBookingPage

    Private _action As String

    Private Enum PackageSelection
        Free
        Premium
        OnlineOnly
        NoSelection
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' first check if we are coming back to this page.

        Response.RedirectPermanent("~/Booking/Step/1")

        _action = Request.QueryString("action")

        Me.lblSessionExpired.Visible = (_action = "expired")
        If Not Page.IsPostBack Then
            ' this is a new booking so start clear the session and start again
            BookingController.ClearAdBooking()
            BundleController.ClearBundleBooking() ' clear any bundled booking
        End If
    End Sub

    Private Sub ucxNavButton_NextStep(ByVal sender As Object, ByVal e As System.EventArgs) Handles ucxNavButtons.NextStep
        Select Case GetSelectedPackage()
            Case PackageSelection.Premium
                ' Start a normal Bundle Booking
                BundleController.StartNewBundleBooking(Membership.GetUser().UserName)
                BookingController.BookingType = Booking.BookingAction.BundledBooking
                Response.Redirect(PageUrl.BookingBundle_2)
            Case PackageSelection.Free
                ' Redirect simply to the rates page for them to select the category
                Response.Redirect(PageUrl.BookSpecialCategories)
            Case PackageSelection.OnlineOnly
                ' Start a normal Online Booking only
                BookingController.StartAdBooking(Membership.GetUser().UserName)
                BookingController.BookingType = Booking.BookingAction.NormalBooking
                BookingController.SetAdType(SystemAdType.ONLINE)
                Response.Redirect(PageUrl.BookingStep_2)
            Case PackageSelection.NoSelection
                ' Display an error that nothing was selected
                Dim pageErrors As List(Of String) = New List(Of String)
                pageErrors.Add("No package was selected.")
                ucxPageErrors.ShowErrors(pageErrors)
        End Select
    End Sub

    Private Function GetSelectedPackage() As PackageSelection
        'If rdoFree.Checked Then
        '    Return PackageSelection.Free
        'Else
        If rdoOnlineOnly.Checked Then
            Return PackageSelection.OnlineOnly
        ElseIf rdoPremium.Checked Then
            Return PackageSelection.Premium
        Else
            Return PackageSelection.NoSelection
        End If
    End Function

#Region "Redundant"

    'Private Sub ucxAdTypeList_AdTypeSelected(ByVal adTypeId As Integer) Handles ucxAdTypeList.AdTypeSelected
    '    If adTypeId > 0 Then
    '        BundleSelected = False
    '    Else
    '        BundleSelected = True
    '    End If
    '    Me.AdTypeId = adTypeId
    'End Sub

#End Region

End Class