Imports BetterclassifiedsCore
Imports BetterclassifiedAdmin.Configuration

Partial Public Class Edit_AdBooking
    Inherits System.Web.UI.Page

    Private _adBookingId As Integer

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _adBookingId = Request.QueryString("adBookingId")
        lblMessage.Text = String.Empty
        If Not Page.IsPostBack Then
            DataBindParentCategories()
        End If
    End Sub

#End Region

#Region "Databinding"

    Private Sub linqSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqSource.Selecting
        Using db = DataModel.BetterclassifiedsDataContext.NewContext
            e.Result = db.AdBookings.Where(Function(i) i.AdBookingId = _adBookingId).Single
        End Using
    End Sub

    Private Sub dtlAdBooking_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlAdBooking.DataBound
        DirectCast(dtlAdBooking.FindControl("ddlStatus"), DropDownList).SelectedValue = dtlAdBooking.DataItem.BookingStatus
        Dim parentCategoryId = CategoryController.GetMainParentCategory(dtlAdBooking.DataItem.MainCategoryId).MainCategoryId
        ddlParentCategory.SelectedValue = CategoryController.GetMainParentCategory(dtlAdBooking.DataItem.MainCategoryId).MainCategoryId
        DataBindSubCategories(parentCategoryId)
        ddlSubCategory.SelectedValue = dtlAdBooking.DataItem.MainCategoryId
    End Sub

    Private Sub DataBindParentCategories()
        ddlParentCategory.DataSource = CategoryController.GetMainParentCategories
        ddlParentCategory.DataBind()
        If ddlParentCategory.Items.Count > 0 Then
            DataBindSubCategories(ddlParentCategory.Items(0).Value)
        End If
    End Sub

    Private Sub DataBindSubCategories(ByVal parentId As Integer)
        ddlSubCategory.DataSource = CategoryController.GetMainCategoriesByParent(parentId)
        ddlSubCategory.DataBind()
    End Sub

    Private Sub ddlParentCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlParentCategory.SelectedIndexChanged
        DataBindSubCategories(ddlParentCategory.SelectedValue)
    End Sub

#End Region

#Region "Check Username"

    Protected Sub CheckUser(ByVal sender As Object, ByVal e As System.EventArgs)
        CheckUserExists()
    End Sub

    Private Function CheckUserExists() As Boolean
        Dim label As Label = DirectCast(dtlAdBooking.FindControl("lblConfirm"), Label)
        Dim text As TextBox = DirectCast(dtlAdBooking.FindControl("txtUserId"), TextBox)
        Dim userExists = False
        Using db As New BetterclassifiedsCore.CRM.CrmController(ConfigManager.DBConnection)
            userExists = db.UsernameExists(text.Text)
            If userExists Then
                label.Text = "Ok"
                label.ForeColor = Drawing.Color.Green
            Else
                label.Text = "Fail"
                label.ForeColor = Drawing.Color.Red
            End If
        End Using
        Return userExists
    End Function

#End Region

#Region "Updating"

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If CheckUserExists() Then
                dtlAdBooking.UpdateItem(True)
                lblMessage.Text = "Ad Booking Saved Successfully"
            Else
                lblMessage.Text = "Selected user does not exist."
            End If
        Catch ex As Exception
            lblMessage.Text = ex.Message
        End Try
    End Sub

    Private Sub linqSource_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqSource.Updating
        Using db = BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext.NewContext
            Dim booking = db.AdBookings.Where(Function(i) i.AdBookingId = _adBookingId).SingleOrDefault
            With dtlAdBooking
                booking.AdBookingId = _adBookingId
                booking.AdId = e.OriginalObject.AdId
                booking.BookingDate = e.OriginalObject.BookingDate
                booking.StartDate = e.OriginalObject.StartDate
                booking.EndDate = e.OriginalObject.EndDate
                booking.TotalPrice = e.OriginalObject.TotalPrice
                booking.BookReference = e.OriginalObject.BookReference
                booking.Insertions = e.OriginalObject.Insertions
                booking.BookingType = e.OriginalObject.BookingType
                ' new values
                booking.BookingStatus = e.NewObject.BookingStatus
                booking.UserId = e.NewObject.UserId
                booking.MainCategoryId = e.NewObject.MainCategoryId
            End With
            db.SubmitChanges()
        End Using
        ' cancel the original update
        e.Cancel = True
    End Sub

    Private Sub dtlAdBooking_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles dtlAdBooking.ItemUpdating
        e.NewValues.Add("MainCategoryId", ddlSubCategory.SelectedValue)
        e.NewValues.Add("BookingStatus", DirectCast(dtlAdBooking.FindControl("ddlStatus"), DropDownList).SelectedValue)
        e.NewValues.Add("UserId", DirectCast(dtlAdBooking.FindControl("txtUserId"), TextBox).Text)
    End Sub

#End Region

End Class