Imports BetterclassifiedsCore
Imports BetterclassifiedsWeb.Controls.Booking.ExpiredAds

Partial Public Class Expired
    Inherits System.Web.UI.Page

    Private _userId As String
    Private _months As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' set the global variables
        _userId = Membership.GetUser.UserName

        If Not Page.IsPostBack Then
            Using controller As New BetterclassifiedsCore.CRM.UserClassController

                _months = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_System_ExpiredHistoryMonths)

                grdOnline.DataSource = controller.GetExpiredOnlineAds(_userId, DateTime.Today.AddMonths(-_months))
                grdOnline.DataBind()

                grdPrintAds.DataSource = controller.GetExpiredLineAds(_userId, BetterclassifiedsCore.Controller.BookingStatus.BOOKED, DateTime.Today.AddMonths(-3))
                grdPrintAds.DataBind()

                ' show the label of months to the user
                lblMonths.Text = _months.ToString
            End Using
        End If
    End Sub


    Private Function GetBookFieldColumn() As TemplateColumn
        Dim template = New TemplateColumn With {.ItemTemplate = New LinkItemTemplate()}
        'Dim c As New datacontr
        Return template
    End Function

    Protected Sub grdPrintAds_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdPrintAds.RowCommand
        If e.CommandName = "bookagain" Then
            General.StartBundleBookingStep2(e.CommandArgument, SystemAdType.LINE)
        End If
    End Sub

    Protected Sub grdOnline_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdOnline.RowCommand
        If e.CommandName = "bookagain" Then
            General.StartBundleBookingStep2(e.CommandArgument, SystemAdType.ONLINE)
        End If
    End Sub
End Class