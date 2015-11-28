Imports System.Web.UI.WebControls
Imports BetterclassifiedsCore.DataModel
Imports System.Web.UI

Namespace UI
    <ToolboxData("<{0}:ParamountGrid runat=server></{0}:GridView>")> _
    Public Class ParamountGrid
        Inherits System.Web.UI.WebControls.GridView

        Public Sub New()
            MyBase.New()
            Me.AllowPaging = True
            Me.AllowSorting = True
            Me.PagerSettings.Mode = PagerButtons.NextPreviousFirstLast
        End Sub

        Protected Overloads Overrides Sub InitializePager(ByVal row As GridViewRow, ByVal columnSpan As Integer, ByVal pagedDataSource As PagedDataSource)
            ' This method is called to initialise the pager on the grid. We intercepted this and override
            ' the values of pagedDataSource to achieve the custom paging using the default pager supplied
            If CustomPaging Then
                pagedDataSource.AllowCustomPaging = True
                pagedDataSource.VirtualCount = VirtualItemCount
                pagedDataSource.CurrentPageIndex = CurrentPageIndex
            End If
            MyBase.InitializePager(row, columnSpan, pagedDataSource)
        End Sub

        Public Property VirtualItemCount() As Integer
            Get
                If ViewState("pgv_vitemcount") Is Nothing Then
                    ViewState("pgv_vitemcount") = -1
                End If
                Return Convert.ToInt32(ViewState("pgv_vitemcount"))
            End Get
            Set(ByVal value As Integer)
                ViewState("pgv_vitemcount") = value
            End Set
        End Property

        Private Property CurrentPageIndex() As Integer
            Get
                If ViewState("pgv_pageindex") Is Nothing Then
                    ViewState("pgv_pageindex") = 0
                End If
                Return Convert.ToInt32(ViewState("pgv_pageindex"))
            End Get
            Set(ByVal value As Integer)
                ViewState("pgv_pageindex") = value
            End Set
        End Property

        Public Shadows WriteOnly Property DataSource() As DataSource
            Set(ByVal value As DataSource)
                MyBase.DataSource = value.Data
                VirtualItemCount = value.TotalPopulation
                ' we store the page index here so we dont lost it in databind
                CurrentPageIndex = PageIndex
            End Set
        End Property


        Private ReadOnly Property CustomPaging() As Boolean
            Get
                Return (VirtualItemCount <> -1)
            End Get
        End Property
    End Class
End Namespace