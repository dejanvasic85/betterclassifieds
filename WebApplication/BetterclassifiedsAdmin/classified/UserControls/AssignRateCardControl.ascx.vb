Imports Telerik.Web.UI
Imports BetterClassified.UIController

Public Class AssignRateCardControl
    Inherits System.Web.UI.UserControl

    Private _publicationController As PublicationController
    Private _categoryController As CategoryController

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _publicationController = New PublicationController
        _categoryController = New CategoryController

        If Not Page.IsPostBack Then
            dataBindPublications()
            dataBindCategories()

            If Me.RateCardId <> 0 Then
                DataBindRateCard(RateCardId)
            End If
        End If
    End Sub

    Private Sub dataBindPublications()
        chkListPublications.DataSource = _publicationController.GetPublications
        chkListPublications.DataBind()
    End Sub

    Private Sub dataBindCategories()
        trCategories.DataSource = _categoryController.GetMainCategories()
        trCategories.DataBind()
    End Sub

    Private Sub trCategories_NodeChecked(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadTreeNodeEventArgs) Handles trCategories.NodeCheck
        For Each n As RadTreeNode In e.Node.Nodes
            n.Checked = e.Node.Checked
        Next
    End Sub

    Public Sub AssignRates(ByVal ratecardId As Integer)
        ' Get the selected Publications
        Dim publications As New List(Of Integer)
        For Each pubItem As ListItem In Me.chkListPublications.Items
            If pubItem.Selected Then
                publications.Add(Integer.Parse(pubItem.Value))
            End If
        Next

        Dim categories As New List(Of Integer)
        For Each n As RadTreeNode In Me.trCategories.CheckedNodes
            If n.Level = 1 And n.Checked Then
                categories.Add(n.Value)
            End If
        Next

        _publicationController.AddPublicationRate(publications, categories, ratecardId)
    End Sub

    Public Property RateCardId As Integer
        Get
            If ViewState("RateCardId") IsNot Nothing Then
                Return Integer.Parse(ViewState("RateCardId"))
            Else
                Return 0
            End If
        End Get
        Set(ByVal value As Integer)
            ViewState("RateCardId") = value
        End Set
    End Property

    Public Sub DataBindRateCard(ByVal rateCardId As Integer)
        ' Get Selected Publications
        Dim publicationList = _publicationController.GetPublicationsForRatecard(rateCardId)
        dataBindPublications(publicationList)

        ' Get Selected Categories
        Dim categoryList = _categoryController.GetMainCategoriesForRatecard(rateCardId)
        dataBindCategories(categoryList)
    End Sub

    Private Sub dataBindPublications(ByVal publicationList As List(Of Integer))
        For Each p In publicationList
            Dim listItem = chkListPublications.Items.FindByValue(p)
            If listItem IsNot Nothing Then
                listItem.Selected = True
            End If
        Next
    End Sub

    Private Sub dataBindCategories(ByVal categoryList As List(Of Nullable(Of Integer)))
        For Each c In categoryList
            Dim chkItem = trCategories.FindNodeByValue(c)
            If chkItem IsNot Nothing Then
                chkItem.Checked = True
                If chkItem.ParentNode.Checked = False Then
                    chkItem.ParentNode.Checked = True
                End If
            End If
        Next
    End Sub
End Class