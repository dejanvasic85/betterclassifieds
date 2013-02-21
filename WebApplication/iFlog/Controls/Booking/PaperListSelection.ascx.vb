Imports BetterclassifiedsCore
Imports Paramount.Betterclassified.Utilities.Configuration
Imports Paramount.Utility.Dsl
Imports Paramount.Utility

''' <summary>
''' User control that displays a list of Papers from the Database in a grid view.
''' </summary>
''' <remarks>This user control is based on the grid view. It has the ability
''' to allow multiple or single paper selection. Depending on the way this
''' property is set, it will either render a radio button or a check box
''' inside the grid view. 
''' However, it's up to the developer to call the right method to return 
''' the values they need from this control.</remarks>
Partial Public Class PaperListSelection
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Dim idList As List(Of Integer) = ViewState("pubIdList")
            If Not idList Is Nothing Then
                SelecPublications(idList)
            End If
        End If

    End Sub

    Public Sub LoadCurrentData(ByVal pubIdList As List(Of Integer))
        ViewState("pubIdList") = pubIdList
    End Sub

    Public Sub SelecPublications(ByVal publicationList As List(Of Integer))
        ' select the papers
        For Each row As GridViewRow In grdPapers.Rows
            Dim hdnPubId As HiddenField = row.FindControl("hdnPaperId")
            If (publicationList.Contains(hdnPubId.Value)) Then
                Dim checkBox As CheckBox = row.FindControl("chkPaper")
                checkBox.Checked = True
            End If
        Next
    End Sub

    Public Sub LoadPapers(ByVal publicationList As List(Of BetterclassifiedsCore.DataModel.Publication))
        grdPapers.DataSource = publicationList
        Me.DataBind()
    End Sub

    Public Property AllowMultiplePapers() As Boolean
        Get
            Return CBool(ViewState("allowMultiplePapers"))
        End Get
        Set(ByVal value As Boolean)
            ViewState("allowMultiplePapers") = value
        End Set
    End Property

    ''' <summary>
    ''' Returns a list of selected PublicationId from the list. Only call this method
    ''' when AllowMultiplePapers property is set to true.
    ''' </summary>
    ''' <returns>
    ''' This method should only be called when the AllowMultiplePapers property is set to true.
    ''' This public function loops through all the data items in the list and checks 
    ''' each column if a paper is selected. If so, it places the paper id into a list
    ''' of </returns>
    ''' <remarks></remarks>
    Public Function GetSelectedPapers(ByVal adTypeCode As SystemAdType) As List(Of Integer)

        'declare a return value
        Dim IDlist As New List(Of Integer)

        If AllowMultiplePapers Then

            ' If adType is ONLINE - this control should not be shown.
            ' There should be only one online paper to select and that's what we return
            If (adTypeCode = SystemAdType.ONLINE) Then
                Dim fieldOnline As HiddenField = grdPapers.Rows(0).Cells(0).FindControl("hdnPaperId")
                IDlist.Add(fieldOnline.Value)
            Else
                'loop through all the items in the grid
                For Each row As GridViewRow In grdPapers.Rows

                    Dim checkBox As CheckBox = row.Cells(0).FindControl("chkPaper")

                    If Not checkBox Is Nothing Then
                        If checkBox.Checked Then

                            ' the id is hidden inside the value of a hidden field
                            Dim field As HiddenField = row.Cells(0).FindControl("hdnPaperId")

                            ' store the value into the return list
                            IDlist.Add(field.Value)

                        End If
                    End If

                Next
            End If
        End If

        Return IDlist

    End Function

    ''' <summary>
    ''' Returns the selected PublicationId from the list. Only call this method
    ''' when AllowMultiplePapers property is set to false.
    ''' </summary>
    ''' <returns><see cref="Integer"/></returns>
    ''' <remarks>This method returns the PublicationId that corresponds to
    ''' value that was selected in the grid view from the radio boxes.
    ''' It should only be called if the AllowMultiplePapers property is set
    ''' to false.</remarks>
    Public Function GetSelectedPaper() As Integer

        If AllowMultiplePapers <> True Then
            Dim value = Request.Form("rdoPaperId")
            Return CInt(value)
        Else
            Return Nothing
        End If

    End Function

    Private Sub grdPapers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdPapers.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim paperImg As Image = TryCast(e.Row.FindControl("imgPaper"), Image)
            If paperImg IsNot Nothing Then
                Dim query As New DslQueryParam(Request.QueryString)
                query.Entity = CryptoHelper.Encrypt(Paramount.ApplicationBlock.Configuration.ConfigSettingReader.ClientCode)
                query.DocumentId = e.Row.DataItem.ImageUrl
                paperImg.ImageUrl = query.GenerateUrl(BetterclassifiedSetting.DslImageUrlHandler)
            End If
        End If
    End Sub
End Class