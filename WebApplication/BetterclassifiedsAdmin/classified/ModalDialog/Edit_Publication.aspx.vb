Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility
Imports Paramount.DSL.UIController

Partial Public Class Edit_Publication
    Inherits System.Web.UI.Page

#Region "Global Variables"
    Private _publicationId As Integer
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _publicationId = Request.QueryString("publicationId")
    End Sub

#Region "Databinding"

    Private Sub viewPublications_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles viewPublications.DataBound
        If viewPublications.DataItem.ImageUrl <> String.Empty Then
            divImage.Visible = True
            Dim documentId As String = viewPublications.DataItem.ImageUrl
            Dim query As New Paramount.Utility.Dsl.DslQueryParam(Request.QueryString)
            query.DocumentId = documentId
            query.Height = BetterclassifiedSetting.DslThumbHeight
            query.Width = BetterclassifiedSetting.DslThumbWidth
            query.Resolution = BetterclassifiedSetting.DslDefaultResolution
            query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
            imgDisplay.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
        Else : divImage.Visible = False
        End If
    End Sub

    Private Sub viewPublications_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles viewPublications.Load
        Dim pubLabel As Label = TryCast(viewPublications.FindControl("lblPublicationType"), Label)

        Dim publication As DataModel.Publication = TryCast(viewPublications.DataItem, DataModel.Publication)
        If publication IsNot Nothing Then
            pubLabel.Text = PublicationController.GetPublicationTypeById(publication.PublicationTypeId).Title
        End If

    End Sub

    Private Sub linqPublicationUpdate_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceStatusEventArgs) Handles linqPublicationUpdate.Selected
        Dim pub As DataModel.Publication = TryCast(e.Result(0), DataModel.Publication)
        lblFrequencyType.Text = pub.FrequencyType
        trWeeklyDetails.Visible = (pub.FrequencyType = "Weekly")
        If pub.FrequencyType = "Weekly" Then
            ddlWeekDay.SelectedValue = pub.FrequencyValue
        End If
    End Sub

    Private Sub linqPublicationUpdate_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceSelectEventArgs) Handles linqPublicationUpdate.Selecting
        e.Result = PublicationController.GetPublicationById(_publicationId)
    End Sub

#End Region

#Region "Updating Existing Publication Procedure"

    Private Sub btnUpdatePublication_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdatePublication.Click
        viewPublications.UpdateItem(True)
    End Sub

    Private Sub viewPublications_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewUpdateEventArgs) Handles viewPublications.ItemUpdating

        Dim view As DetailsView = TryCast(sender, DetailsView)

        ' for some reason the template field is not getting saved into viewstate.
        ' need to find what the values are and apply them in our linq data source to be updated back to the database

        ' description 
        Dim desc As TextBox = view.FindControl("txtDescription")
        If desc IsNot Nothing Then
            e.NewValues.Add("Description", desc.Text)
        End If

        ' image url
        Dim img As TextBox = view.FindControl("txtImageUrl")
        If img IsNot Nothing Then
            e.NewValues.Add("ImageUrl", img.Text)
        End If

        ' frequency
        If lblFrequencyType.Text = "Weekly" Then
            e.NewValues.Add("FrequencyValue", ddlWeekDay.SelectedValue)
        End If

        ' add the image to the DSL service if a file was provided
        Dim upl As FileUpload = TryCast(viewPublications.FindControl("fileUploadImage"), FileUpload)
        If upl.HasFile Then
            Dim documentId = DslController.UploadDslDocument(ViewObjects.DslDocumentCategoryTypeView.Logos, _
                                                             upl.PostedFile.InputStream, _
                                                             upl.PostedFile.ContentLength, _
                                                             upl.PostedFile.FileName, _
                                                             upl.PostedFile.ContentType, _
                                                             Membership.GetUser.UserName, _
                                                             "PublicationLogo", _
                                                             Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ApplicationName, _
                                                             Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
            If Not String.IsNullOrEmpty(documentId) Then
                e.NewValues.Add("ImageUrl", documentId)
            End If
        End If
    End Sub

    Private Sub linqPublicationUpdate_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.LinqDataSourceUpdateEventArgs) Handles linqPublicationUpdate.Updating
        ' perform some validation
        Try
            If (e.Exception IsNot Nothing) Then
                For Each innerException As KeyValuePair(Of String, Exception) _
                       In e.Exception.InnerExceptions
                    Me.UpdateMessage.Text &= innerException.Key & ": " & _
                        innerException.Value.Message & "<br />"
                Next
                e.ExceptionHandled = True
            Else

                Dim newPub As DataModel.Publication = TryCast(e.NewObject, DataModel.Publication)

                ' update the publication
                With newPub
                    If PublicationController.UpdatePublication(_publicationId, .Title, .Description, .PublicationTypeId, _
                                                               .ImageUrl, .FrequencyType, .FrequencyValue, .Active, .SortOrder) Then
                        UpdateMessage.Text = "Successfully updated publication details."
                        UpdateMessage.ForeColor = Drawing.Color.Green
                    End If
                End With

            End If
        Catch ex As Exception
            UpdateMessage.Text = "Update Failed: " + ex.Message
        Finally
            e.Cancel = True ' Cancel the original transaction by the LINQ data source (we used our own method)
        End Try
    End Sub

#End Region

#Region "Remove Image"

    Private Sub lnkRemove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRemove.Click

        Try
            viewPublications.DataBind()
            ' todo DSL Delete
            ' redundant
            ' call the method to remove the physical image from the database
            DslController.DeleteDocument(viewPublications.DataItem.ImageUrl)
            ' call publication controller to delete the image
            PublicationController.DeletePublicationImage(viewPublications.DataItem.PublicationId)
            ' update the message if all is successful
            UpdateMessage.Text = "Successfully removed image from the system."
            UpdateMessage.ForeColor = Drawing.Color.Green
            ' databind the new details.
            viewPublications.DataBind()
        Catch ex As Exception
            UpdateMessage.Text = "Unable to remove the image. Error: <b>" + ex.Message + "</b>"
        End Try
    End Sub

#End Region

End Class