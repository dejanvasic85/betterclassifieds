Imports BetterclassifiedsCore

Partial Public Class Transactions
    Inherits System.Web.UI.Page

    Private _months As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            _months = GeneralRoutine.GetAppSetting(Utilities.Constants.CONST_MODULE_SYSTEM, Utilities.Constants.CONST_KEY_System_ExpiredHistoryMonths)

            grdTransactions.DataSource = BookingController.GetTransaction(Membership.GetUser.UserName, DateTime.Now.AddMonths(-_months))
            grdTransactions.DataBind()

            lblMonths.Text = _months.ToString
        End If
    End Sub

    Private Sub grdTransactions_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdTransactions.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim typeLiteral As Literal = TryCast(e.Row.FindControl("lblType"), Literal)
            If typeLiteral IsNot Nothing Then
                Select Case e.Row.DataItem.TransactionType
                    Case TransactionType.CREDIT
                        typeLiteral.Text = "Credit"
                    Case TransactionType.PAYPAL
                        typeLiteral.Text = "PayPal"
                    Case TransactionType.FREEAD
                        typeLiteral.Text = "Free"
                End Select
            End If
        End If
    End Sub
End Class