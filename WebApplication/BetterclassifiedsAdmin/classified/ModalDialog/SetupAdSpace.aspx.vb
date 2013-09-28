Imports BetterclassifiedsCore.WebAdvertising
Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility
Imports Paramount.DSL.UIController

Partial Public Class SetupAdSpace
    Inherits System.Web.UI.Page

    Private _spaceId As Integer
    Private _settingID As Integer
    Private _mode As Mode

    Private Enum Mode
        create = 1
        edit = 2
    End Enum

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _spaceId = Request.QueryString("spaceId")
        _settingID = Request.QueryString("settingId")
        _mode = Request.QueryString("mode")
        Select Case _mode
            Case Mode.create
                dtlAdSpace.DefaultMode = DetailsViewMode.Insert
            Case Mode.edit
                dtlAdSpace.DefaultMode = DetailsViewMode.Edit
        End Select
    End Sub

#End Region

#Region "DataBinding"

    Private Sub lnqSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles lnqSource.Selecting
        If _mode = Mode.edit Then
            Using db As New AdSpaceController
                Dim item = db.GetAdSpaceById(_spaceId)
                e.Result = item
            End Using
        End If
    End Sub

    Private Sub dtlAdSpace_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlAdSpace.DataBound
        If _mode = Mode.edit Then
            ' Target
            Dim ddlTarget As DropDownList = TryCast(dtlAdSpace.FindControl("ddlTarget"), DropDownList)
            ddlTarget.SelectedValue = dtlAdSpace.DataItem.AdTarget
            'Type
            Dim ddlType As DropDownList = TryCast(dtlAdSpace.FindControl("ddlType"), DropDownList)
            ddlType.SelectedValue = dtlAdSpace.DataItem.SpaceType
            ' Sort order
            Dim ddlSort As DropDownList = TryCast(dtlAdSpace.FindControl("ddlSortOrder"), DropDownList)
            ddlSort.SelectedValue = dtlAdSpace.DataItem.SortOrder
            ' Image display
            If dtlAdSpace.DataItem.ImageUrl <> String.Empty Then
                divImage.Visible = True
                ' Use Query Param to render the image
                Dim documentId As String = dtlAdSpace.DataItem.ImageUrl
                Dim query As New Paramount.Utility.Dsl.DslQueryParam(Request.QueryString)
                query.DocumentId = documentId
                query.Height = BetterclassifiedSetting.DslThumbHeight
                query.Width = BetterclassifiedSetting.DslThumbWidth
                query.Resolution = BetterclassifiedSetting.DslDefaultResolution
                query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                imgDisplay.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
            Else : divImage.Visible = False
            End If
        End If
    End Sub

#End Region

#Region "Update"

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If _mode = Mode.edit Then
            dtlAdSpace.UpdateItem(True)
        ElseIf _mode = Mode.create Then
            dtlAdSpace.InsertItem(True)
        End If
    End Sub

    Private Sub dtlAdSpace_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles dtlAdSpace.ItemUpdating
        e.NewValues.Add("AdTarget", DirectCast(dtlAdSpace.FindControl("ddlTarget"), DropDownList).SelectedValue)
        e.NewValues.Add("SpaceType", DirectCast(dtlAdSpace.FindControl("ddlType"), DropDownList).SelectedValue)
        e.NewValues.Add("SortOrder", DirectCast(dtlAdSpace.FindControl("ddlSortOrder"), DropDownList).SelectedValue)
        ' add the image to the DSL service if a file was provided
        Dim upl As FileUpload = TryCast(dtlAdSpace.FindControl("fileUpload"), FileUpload)
        If upl IsNot Nothing Then
            If upl.HasFile Then
                Dim documentId = DslController.UploadDslDocument(Paramount.Common.DataTransferObjects.DSL.DslDocumentCategoryType.BannerAd, _
                                                                 upl.PostedFile.InputStream, _
                                                                 upl.PostedFile.ContentLength, _
                                                                 upl.PostedFile.FileName, _
                                                                 upl.PostedFile.ContentType, _
                                                                 Membership.GetUser.UserName, _
                                                                 "Banner-Ad", _
                                                                 Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ApplicationName, _
                                                                 Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                If Not String.IsNullOrEmpty(documentId) Then
                    e.NewValues.Add("ImageUrl", documentId)
                End If
            End If
        End If
    End Sub

    Private Sub lnqSource_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles lnqSource.Updating
        Using db As New AdSpaceController
            Try
                Dim obj As DataModel.WebAdSpace = DirectCast(e.NewObject, DataModel.WebAdSpace)
                db.UpdateAdSpace(obj)
                lblMsh.Text = "Updated details successfully. Click Refresh button on grid to view latest changes."
            Catch ex As Exception
                lblMsh.Text = ex.Message
            End Try
        End Using
        e.Cancel = True ' cancel the original transaction
    End Sub

#End Region

#Region "Inserting"

    Private Sub dtlAdSpace_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertEventArgs) Handles dtlAdSpace.ItemInserting
        e.Values.Add("SettingID", _settingID)
        e.Values.Add("AdTarget", DirectCast(dtlAdSpace.FindControl("ddlTarget"), DropDownList).SelectedValue)
        e.Values.Add("SpaceType", DirectCast(dtlAdSpace.FindControl("ddlType"), DropDownList).SelectedValue)
        e.Values.Add("SortOrder", DirectCast(dtlAdSpace.FindControl("ddlSortOrder"), DropDownList).SelectedValue)
        ' add the image to the DSL service if a file was provided
        Dim upl As FileUpload = TryCast(dtlAdSpace.FindControl("fileUpload"), FileUpload)
        If upl IsNot Nothing Then
            If upl.HasFile Then
                Dim documentId = DslController.UploadDslDocument(Paramount.Common.DataTransferObjects.DSL.DslDocumentCategoryType.BannerAd, _
                                                                 upl.PostedFile.InputStream, _
                                                                 upl.PostedFile.ContentLength, _
                                                                 upl.PostedFile.FileName, _
                                                                 upl.PostedFile.ContentType, _
                                                                 Membership.GetUser.UserName, _
                                                                 "Banner-Ad", _
                                                                 Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ApplicationName, _
                                                                 Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                If Not String.IsNullOrEmpty(documentId) Then
                    e.Values.Add("ImageUrl", documentId)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Remove Image"

    Private Sub lnkRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRemove.Click
        ' removes the image from the DSL Library completely
        dtlAdSpace.DataBind()
        Dim id As String = dtlAdSpace.DataItem.ImageUrl
        If id <> String.Empty Then
            DslController.DeleteDocument(id)
            Using db As New AdSpaceController
                db.DeleteImageUrl(_spaceId)
                dtlAdSpace.DataBind()
            End Using
        End If
    End Sub

#End Region


End Class