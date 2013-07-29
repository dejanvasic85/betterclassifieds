Imports BetterclassifiedsCore

Partial Public Class Obsolete_UpComing
    Inherits System.Web.UI.Page

    Private _userId As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' set the global variables
        _userId = Membership.GetUser.UserName

        If Not Page.IsPostBack Then
            Using controller As New BetterclassifiedsCore.CRM.UserClassController
                grdOnline.DataSource = controller.GetScheduledOnlineAds(_userId, BetterclassifiedsCore.Controller.BookingStatus.BOOKED)
                grdOnline.DataBind()

                grdPrintAds.DataSource = controller.GetScheduledLineAds(_userId, BetterclassifiedsCore.Controller.BookingStatus.BOOKED)
                grdPrintAds.DataBind()
            End Using
        End If
    End Sub

    Private Sub grdOnline_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdOnline.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim link As HyperLink = e.Row.FindControl("lnkStatus")
            If link IsNot Nothing Then

                If e.Row.DataItem.Status = "Cancelled" Then
                    ' encode the booking reference because it's a string
                    Dim bookReference As String = Server.UrlEncode(e.Row.DataItem.BookReference)

                    link.NavigateUrl = String.Format("~/MemberAccount/EditOnlineAd.aspx?des={0}&ref={1}&act=resub", _
                                                      e.Row.DataItem.AdDesignId, bookReference)

                    link.ToolTip = "Click here to edit and re-submit the ad."
                End If
                link.Text = e.Row.DataItem.Status
            End If
        End If

    End Sub

End Class