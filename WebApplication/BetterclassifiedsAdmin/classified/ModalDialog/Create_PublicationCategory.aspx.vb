Imports BetterclassifiedsCore

Partial Public Class Create_PublicationCategory
    Inherits System.Web.UI.Page

    Private _publicationId As Integer
    Private _isParent As Boolean
    Private _publicationCategoryId As Nullable(Of Integer)

#Region "Page load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _publicationId = Request.QueryString("publicationId")
        If _publicationId > 0 Then
            _isParent = Request.QueryString("isParent")
            _publicationCategoryId = Request.QueryString("parentId")

            If Not Page.IsPostBack Then

                ' don't display rate details for parent categories
                pnlRateDetails.Visible = Not _isParent

                ' load the required special rate and ratecard details
                DatabindRateDetails()
            End If
        Else
            pnlDetails.Visible = False
            msgInsert.Text = "Please select a parent category before trying to create a sub category."
        End If
    End Sub

    Private Sub dtlPublicationCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlPublicationCategory.Load
        If _publicationId > 0 Then
            If Not Page.IsPostBack Then
                DatabindMainCategories()
            End If
        End If
    End Sub

#End Region

#Region "DataBinding"

    Public Sub DatabindRateDetails()
        ddlRatecard.DataSource = GeneralController.GetRatecards()
        ddlRatecard.DataBind()
        ddlSpecialRate.DataSource = GeneralController.GetSpecialRates()
        ddlSpecialRate.DataBind()

        ' add the - none option for special rate
        ddlSpecialRate.Items.Insert(0, "-- none -- ")
    End Sub

    Private Sub dtlPublicationCategory_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlPublicationCategory.DataBound
        DatabindMainCategories()
    End Sub

#End Region

#Region "Create"

    Private Sub dtlPublicationCategory_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertEventArgs) Handles dtlPublicationCategory.ItemInserting
        ' obtain template values out of the fields
        ' get the template field values
        Dim description As TextBox = dtlPublicationCategory.FindControl("txtDescription")
        Dim mainCategoryList As DropDownList = dtlPublicationCategory.FindControl("ddlMainCategory")
        e.Values.Add("Description", description.Text)
        e.Values.Add("MainCategoryId", mainCategoryList.SelectedValue)
        e.Values.Add("PublicationId", _publicationId)

        If _publicationCategoryId > 0 Then
            e.Values.Add("ParentId", _publicationCategoryId)
        Else
            e.Values.Add("ParentId", Nothing)
        End If

    End Sub

#End Region

    Private Sub DatabindMainCategories()
        Dim list As DropDownList = TryCast(dtlPublicationCategory.FindControl("ddlMainCategory"), DropDownList)
        If list IsNot Nothing Then
            ' load the main parent categories
            Dim unnassignedCategories = CategoryController.GetUnassignedMainCategories(_isParent, _publicationCategoryId, _publicationId)
            If unnassignedCategories.Count > 0 Then
                list.DataSource = unnassignedCategories
                list.DataBind()
            Else
                ' do not allow anything to be added because all main categories for this publication have been set.
                pnlDetails.Visible = False
                msgInsert.Text = "All main categories have been assigned to this publication."
            End If
        End If
    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        ' perform the creation for the category
        dtlPublicationCategory.InsertItem(True)
    End Sub

    Private Sub srcPublicationCategory_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceInsertEventArgs) Handles srcPublicationCategory.Inserting
        Try
            If (e.Exception IsNot Nothing) Then
                For Each innerException As KeyValuePair(Of String, Exception) _
                       In e.Exception.InnerExceptions
                    Me.msgInsert.Text &= innerException.Key & ": " & _
                        innerException.Value.Message & "<br />"
                Next
                e.ExceptionHandled = True
            Else
                Dim publicationCategry As DataModel.PublicationCategory = TryCast(e.NewObject, DataModel.PublicationCategory)
                With publicationCategry
                    Dim pubCatId = PublicationController.CreatePublicationCategory(.Title, .Description, .ImageUrl, _
                                                                    .PublicationId, .MainCategoryId, .ParentId)

                    If _isParent = False Then
                        If AssignRatecard(pubCatId) And AssignSpecialRate(pubCatId) Then
                            msgInsert.Text = "Successfully created Publication Category. Reload the lists to see new items."
                            msgInsert.ForeColor = Drawing.Color.Green
                        End If
                    End If

                End With
                DatabindMainCategories()

            End If
        Catch ex As Exception
            msgInsert.Text = "Creating Publication Category Failed: " + ex.Message
        Finally
            e.Cancel = True ' cancel the automatic insert generated by the data source since we handled this
            dtlPublicationCategory.DataBind()
        End Try
    End Sub

    Private Function AssignRatecard(ByVal publicationCategoryId As Integer) As Boolean
        Try
            Return PublicationController.AssignRatecard(publicationCategoryId, ddlRatecard.SelectedValue)
        Catch ex As Exception
            Return False
            msgInsert.Text = ex.Message
        End Try
    End Function

    Private Function AssignSpecialRate(ByVal publicationCategoryId As Integer) As Boolean
        Try
            If ddlSpecialRate.SelectedIndex > 0 Then
                ' user has chosen a special rate so we assign it
                Dim specialRateId As Integer = ddlSpecialRate.SelectedValue
                Return PublicationController.AssignSpecialRate(publicationCategoryId, specialRateId)
            End If
        Catch ex As Exception

        End Try
    End Function

End Class