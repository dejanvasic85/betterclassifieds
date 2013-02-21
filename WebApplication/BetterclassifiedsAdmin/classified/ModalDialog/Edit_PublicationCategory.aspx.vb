Imports BetterclassifiedsCore

Partial Public Class Edit_PublicationCategory
    Inherits System.Web.UI.Page

    Private _id As Integer
    Private _isParent As Boolean

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _id = Request.QueryString("id")
        If _id > 0 Then
            _isParent = Request.QueryString("isParent")
            pnlRateDetails.Visible = Not _isParent

            If Not Page.IsPostBack Then
                DatabindRateDetails()
            End If

        Else
            pnlControls.Visible = False
            lblCategoryMsg.Text = "Please select a category to edit."
        End If
    End Sub

#End Region

#Region "DataBinding"

    Private Sub DatabindRateDetails()
        ' binds ratees and special rates
        ddlRatecard.DataSource = GeneralController.GetRatecards()
        ddlRatecard.DataBind()
        ddlSpecialRate.DataSource = GeneralController.GetSpecialRates()
        ddlSpecialRate.DataBind()

        ' add the - none option for special rate
        ddlSpecialRate.Items.Insert(0, "-- none -- ")

        Dim rate = GeneralController.GetPublicationRateByCategory(_id)
        If rate IsNot Nothing Then
            ' databind the assigned rates
            ddlRatecard.SelectedValue = rate.RatecardId
        End If

        Dim publicationSpecial = GeneralController.GetPublicationSpecialRateByCategory(_id)
        If publicationSpecial IsNot Nothing Then
            ddlSpecialRate.SelectedValue = publicationSpecial.SpecialRateId
        End If
    End Sub

    Private Sub lnqCategorySource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles lnqCategorySource.Selecting
        If _id > 0 Then
            e.Result = PublicationController.GetPublicationCategoryById(_id)
        End If
    End Sub
#End Region

#Region "Updating"

    Private Sub lnqCategorySource_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles lnqCategorySource.Updating
        Try
            If (e.Exception IsNot Nothing) Then
                For Each innerException As KeyValuePair(Of String, Exception) _
                       In e.Exception.InnerExceptions
                    Me.lblCategoryMsg.Text &= innerException.Key & ": " & _
                        innerException.Value.Message & "<br />"
                Next
                e.ExceptionHandled = True
            Else
                ' update the object back to the database
                Dim pubCategory As DataModel.PublicationCategory = TryCast(e.NewObject, DataModel.PublicationCategory)
                With pubCategory
                    PublicationController.UpdatePublicationCategory(_id, .Title, .Description, .ImageUrl, _
                                                                       .ParentId, .MainCategoryId, .PublicationId)
                End With
            End If
        Catch ex As Exception
            lblCategoryMsg.Text = "An error occurred while saving record. Error: " + ex.Message
        Finally
            e.Cancel = True ' cancel the original operation done by LINQ data source
        End Try
    End Sub

    Private Sub btnUpdateCategory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateCategory.Click
        Try
            dtlPublicationCategory.UpdateItem(True) 'updates the details in the view

            ' update the rates and specials assigned
            PublicationController.AssignRatecard(_id, ddlRatecard.SelectedValue)
            If ddlSpecialRate.SelectedIndex > 0 Then
                PublicationController.AssignSpecialRate(_id, ddlSpecialRate.SelectedValue)
            Else
                PublicationController.RemoveSpecialRates(_id)
            End If

            ' print message
            lblCategoryMsg.Text = "Successfully Updated Category Details."
            lblCategoryMsg.ForeColor = Drawing.Color.Green
        Catch ex As Exception
            lblCategoryMsg.Text = "An error occurred while saving record. Error: " + ex.Message
        End Try
    End Sub

    Private Sub dtlPublicationCategory_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles dtlPublicationCategory.ItemUpdating
        '' description
        Dim description As TextBox = dtlPublicationCategory.FindControl("txtDescription")
        If description IsNot Nothing Then
            e.NewValues.Add("Description", description.Text)
        End If
    End Sub

#End Region
End Class