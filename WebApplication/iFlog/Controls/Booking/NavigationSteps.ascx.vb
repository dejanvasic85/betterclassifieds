Imports BetterclassifiedsCore
Imports BetterclassifiedsCore.Booking

Partial Public Class NavigationSteps
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then SetCurrentStep()
    End Sub

    Public Property Width() As Unit
        Get
            Return pnlSteps.Width
        End Get
        Set(ByVal value As Unit)
            pnlSteps.Width = value
        End Set
    End Property

    Public Property Instruction() As String
        Get
            Return litTitle.Text
        End Get
        Set(ByVal value As String)
            litTitle.Text = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return litDescription.Text
        End Get
        Set(ByVal value As String)
            litDescription.Text = value
        End Set
    End Property

    Private currentStep As String

    Public Property StepNumber() As Integer
        Get
            Return currentStep
        End Get
        Set(ByVal value As Integer)
            currentStep = value
            FormatSteps(value)
            litStepNumber.Text = "Step " + StepNumber.ToString + "."
        End Set
    End Property

    Private Const _actionBack As String = "?action=back"

    Private Sub FormatSteps(ByVal stepNumber As Integer)
        Select Case stepNumber
            Case 1
                pnlStep1.CssClass = "stepLarge"
                pnlStep2.CssClass = "stepSmallRed"
                pnlStep3.CssClass = "stepSmallRed"
                pnlStep4.CssClass = "stepSmallRed"
                pnlStep5.CssClass = "stepSmallRed"

                lnkStep1.NavigateUrl = ""
                lnkStep2.NavigateUrl = ""
                lnkStep3.NavigateUrl = ""
                lnkStep4.NavigateUrl = ""
                lnkStep5.NavigateUrl = ""

            Case 2
                pnlStep1.CssClass = "stepSmall"
                pnlStep2.CssClass = "stepLarge"
                pnlStep3.CssClass = "stepSmallRed"
                pnlStep4.CssClass = "stepSmallRed"
                pnlStep5.CssClass = "stepSmallRed"

                lnkStep2.NavigateUrl = ""
                lnkStep3.NavigateUrl = ""
                lnkStep4.NavigateUrl = ""
                lnkStep5.NavigateUrl = ""

            Case 3
                pnlStep1.CssClass = "stepSmall"
                pnlStep2.CssClass = "stepSmall"
                pnlStep3.CssClass = "stepLarge"
                pnlStep4.CssClass = "stepSmallRed"
                pnlStep5.CssClass = "stepSmallRed"

                ' set the second step because of the bundle

                If BookingController.BookingType = BookingAction.NormalBooking Then
                    lnkStep2.NavigateUrl = PageUrl.BookingStep_2 + _actionBack
                ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                    lnkStep2.NavigateUrl = PageUrl.BookingBundle_2
                End If

                lnkStep3.NavigateUrl = ""
                lnkStep4.NavigateUrl = ""
                lnkStep5.NavigateUrl = ""

            Case 4
                pnlStep1.CssClass = "stepSmall"
                pnlStep2.CssClass = "stepSmall"
                pnlStep3.CssClass = "stepSmall"
                pnlStep4.CssClass = "stepLarge"
                pnlStep5.CssClass = "stepSmallRed"

                ' set the 2nd and 3rd steps based on the type of booking
                If BookingController.BookingType = BookingAction.NormalBooking Then
                    lnkStep2.NavigateUrl = PageUrl.BookingStep_2 + _actionBack
                    lnkStep3.NavigateUrl = PageUrl.BookingStep_3 + _actionBack
                ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                    lnkStep2.NavigateUrl = PageUrl.BookingBundle_2
                    lnkStep3.NavigateUrl = PageUrl.BookingBundle_3
                End If

                lnkStep4.NavigateUrl = ""
                lnkStep5.NavigateUrl = ""

            Case 5
                pnlStep1.CssClass = "stepSmall"
                pnlStep2.CssClass = "stepSmall"
                pnlStep3.CssClass = "stepSmall"
                pnlStep4.CssClass = "stepSmall"
                pnlStep5.CssClass = "stepLarge"

                ' set the 2nd 3rd & 4th steps based on the type of booking
                If BookingController.BookingType = BookingAction.NormalBooking Then
                    lnkStep2.NavigateUrl = PageUrl.BookingStep_2 + _actionBack
                    lnkStep3.NavigateUrl = PageUrl.BookingStep_3 + _actionBack
                    lnkStep4.NavigateUrl = PageUrl.BookingStep_4 + _actionBack
                ElseIf BookingController.BookingType = Booking.BookingAction.BundledBooking Then
                    lnkStep2.NavigateUrl = PageUrl.BookingBundle_2
                    lnkStep3.NavigateUrl = PageUrl.BookingBundle_3
                    lnkStep4.NavigateUrl = PageUrl.BookingBundle_4
                End If

                lnkStep5.NavigateUrl = ""
        End Select
    End Sub

    Public Sub SetCurrentStep()

    End Sub

End Class