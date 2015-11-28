Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.DSL.UIController

Partial Public Class Create_Publication
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnCreate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreate.Click
        dtlPublication.InsertItem(True)
    End Sub

    Private Sub dtlPublication_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlPublication.DataBound
        DatabindPublicationTypes()
    End Sub

    Private Sub dtlPublication_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DetailsViewInsertEventArgs) Handles dtlPublication.ItemInserting
        ' get values out of the details view that we need from template fields
        Try
            ' get the template field values
            Dim description As TextBox = dtlPublication.FindControl("txtDescription")
            If description IsNot Nothing Then
                e.Values.Add("Description", description.Text)
            End If

            Dim type As DropDownList = dtlPublication.FindControl("ddlPublicationType")
            If type IsNot Nothing Then
                e.Values.Add("PublicationTypeId", type.SelectedValue)
            End If

            e.Values.Add("FrequencyType", ddlFrequencyType.SelectedValue)
            If ddlFrequencyType.SelectedValue = "Weekly" Then
                e.Values.Add("FrequencyValue", ddlWeekDay.SelectedValue)
            End If

            ' add the image to the DSL service if a file was provided
            Dim upl As FileUpload = TryCast(Me.dtlPublication.FindControl("fileUploadImage"), FileUpload)
            If upl IsNot Nothing Then
                If upl.HasFile Then
                    Dim documentId = DslController.UploadDslDocument(Paramount.Common.DataTransferObjects.DSL.DslDocumentCategoryType.Logos, _
                                                             upl.PostedFile.InputStream, _
                                                             upl.PostedFile.ContentLength, _
                                                             upl.PostedFile.FileName, _
                                                             upl.PostedFile.ContentType, _
                                                             Membership.GetUser.UserName, _
                                                             "PublicationLogo", _
                                                             Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ApplicationName, _
                                                             Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                    e.Values.Add("ImageUrl", documentId)
                End If
            End If

        Catch ex As Exception
            msgInsert.Text = "Creating new Publication Failed: " + ex.Message
            e.Cancel = True
        End Try
    End Sub

    Private Sub dtlPublication_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles dtlPublication.Load
        If Not Page.IsPostBack Then
            DatabindPublicationTypes()
        End If
    End Sub

    Private Sub DatabindPublicationTypes()

        Dim publicationType As DropDownList = TryCast(dtlPublication.FindControl("ddlPublicationType"), DropDownList)

        If publicationType IsNot Nothing Then
            Dim onlinePaper As DataModel.Publication = PublicationController.GetOnlinePublication()
            If onlinePaper IsNot Nothing Then
                ' do not allow them to select online paper if there
                publicationType.DataSource = PublicationController.GetPublicationTypes(False)
            Else
                publicationType.DataSource = PublicationController.GetPublicationTypes(True)
            End If
            publicationType.DataBind()
        End If
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
                Dim item As DataModel.Publication = TryCast(e.NewObject, DataModel.Publication)
                If item IsNot Nothing Then
                    ' save to the database!

                    Dim adTypeId As Integer
                    Select Case PublicationController.GetPublicationTypeById(item.PublicationTypeId).Code
                        Case "ONLINE"
                            ' create publication Ad type record for Online Ads
                            adTypeId = AdController.GetAdTypeByCode(SystemAdType.ONLINE).AdTypeId
                        Case Else
                            ' create publication Ad type record for Line Ads
                            adTypeId = AdController.GetAdTypeByCode(SystemAdType.LINE).AdTypeId
                    End Select

                    With item
                        Dim newId = PublicationController.CreatePublication(.Title, .Description, .PublicationTypeId, .ImageUrl, _
                                                                            .FrequencyType, .FrequencyValue, .Active, adTypeId, .SortOrder)
                        msgInsert.Text = "Successfully created new publication."
                        msgInsert.ForeColor = Drawing.Color.Green
                    End With
                End If
            End If
        Catch ex As Exception
            msgInsert.Text = "Creating new Publication Failed: " + ex.Message
        Finally
            e.Cancel = True ' cancel the automatic insert generated by the data source since we handled this
        End Try
    End Sub

    Private Sub ddlFrequencyType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFrequencyType.SelectedIndexChanged
        trWeekly.Visible = (ddlFrequencyType.SelectedValue = "Weekly")
    End Sub
End Class