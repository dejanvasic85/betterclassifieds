Imports BetterclassifiedAdmin.Configuration.ConfigManager
Namespace CRM
    Partial Public Class Customers
        Inherits System.Web.UI.Page

#Region "Gridview and a-z navigation"

        Private Sub UpdateGrid()
            Dim totalRecords As Integer
            UsersGridView.DataSource = CustomerMembershipProvider.FindUsersByName(Me.UsernameToMatch + "%", Me.PageIndex, Me.PageSize, totalRecords)
            UsersGridView.DataBind()
            UpdatePanel1.Update()
        End Sub



        ' create a-z navigation links
        Private Sub BindAtoZNavigation()
            Dim AtoZfilterOptions As String() = {"All", "A", "B", "C", "D", "E", _
             "F", "G", "H", "I", "J", "K", _
             "L", "M", "N", "O", "P", "Q", _
             "R", "S", "T", "U", "V", "W", _
             "X", "Y", "Z"}
            AtoZRepeater.DataSource = AtoZfilterOptions
            AtoZRepeater.DataBind()
        End Sub

        ' create datasource for user gridview
        Private Sub BindUserAccounts()
            Dim totalRecords As Integer
            UsersGridView.DataSource = CustomerMembershipProvider.FindUsersByName(Me.UsernameToMatch + "%", Me.PageIndex, Me.PageSize, totalRecords)
            UsersGridView.DataBind()

            ' Enable/disable the pager buttons based on which page we're on
            Dim visitingFirstPage As Boolean = (Me.PageIndex = 0)
            lnkFirst.Enabled = Not visitingFirstPage
            lnkPrev.Enabled = Not visitingFirstPage

            Dim lastPageIndex As Integer = (totalRecords - 1) / Me.PageSize
            Dim visitingLastPage As Boolean = (Me.PageIndex >= lastPageIndex)
            lnkNext.Enabled = Not visitingLastPage
            lnkLast.Enabled = Not visitingLastPage
        End Sub

        ' when the "all" button is clicked within the repeater
        Protected Sub AtoZRepeater_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
            If e.CommandName = "All" Then
                Me.UsernameToMatch = String.Empty
            Else
                Me.UsernameToMatch = e.CommandName
            End If

            BindUserAccounts()
        End Sub

#End Region

#Region "Paging Interface Click Event Handlers"

        ' first pager link
        Protected Sub lnkFirst_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.PageIndex = 0
            BindUserAccounts()
        End Sub

        ' previous pager link
        Protected Sub lnkPrev_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.PageIndex -= 1
            BindUserAccounts()
        End Sub

        ' next pager link
        Protected Sub lnkNext_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.PageIndex += 1
            BindUserAccounts()
        End Sub

        ' last pager link
        Protected Sub lnkLast_Click(ByVal sender As Object, ByVal e As EventArgs)
            ' Determine the total number of records
            Dim totalRecords As Integer
            CustomerMembershipProvider.FindUsersByName(Me.UsernameToMatch + "%", Me.PageIndex, Me.PageSize, totalRecords)

            ' Navigate to the last page index
            Me.PageIndex = (totalRecords - 1) / Me.PageSize
            BindUserAccounts()
        End Sub

#End Region

#Region "Properties"

        ' put the clicked letter into viewstate
        Private Property UsernameToMatch() As String
            Get
                Dim o As Object = ViewState("UsernameToMatch")
                If o Is Nothing Then
                    Return String.Empty
                Else
                    Return DirectCast(o, String)
                End If
            End Get
            Set(ByVal value As String)
                ViewState("UsernameToMatch") = value
            End Set
        End Property

        ' put the page index into viewstate
        Private Property PageIndex() As Integer
            Get
                Dim o As Object = ViewState("PageIndex")
                If o Is Nothing Then
                    Return 0
                Else
                    Return CInt(o)
                End If
            End Get
            Set(ByVal value As Integer)
                ViewState("PageIndex") = value
            End Set
        End Property

        ' set the page size for the gridview
        Private ReadOnly Property PageSize() As Integer
            Get
                Return 10
            End Get
        End Property

#End Region

#Region "Member delete one user click event handlers"

        ' before the row is deleted
        Protected Sub UserAccounts_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
            Dim userName As String = UsersGridView.DataKeys(e.RowIndex).Value.ToString()
            'CustomerProfileProvider.DeleteProfiles(New String() {userName})
            CustomerMembershipProvider.DeleteUser(userName, True)
            UpdateGrid()
            lblDeleteSuccess.Visible = True
        End Sub

        ' when the rows are being rendered alert user with delete confirmation
        Protected Sub UserAccounts_RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim btn As ImageButton = TryCast(e.Row.Cells(10).Controls(0), ImageButton)
                btn.OnClientClick = "if (confirm('Are you sure you want to delete this user?') == false) return false;"
            End If
        End Sub

#End Region

#Region "Delete all users and related data click even handler"

        Protected Sub deleteAllUsers_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim matchedUsers As Integer
            For Each usr As MembershipUser In CustomerMembershipProvider.GetAllUsers(0, 200, matchedUsers)
                CustomerMembershipProvider.DeleteUser(usr.UserName, True)
            Next
            UpdateGrid()
            deleteAllUsers.Enabled = False
            deleteAllUsers.Text = "Delete complete.."
        End Sub

#End Region

#Region "Delete users selected by checkbox"

        Protected Sub btnDeleteSelected_Click(ByVal sender As Object, ByVal e As EventArgs)
            For Each row As GridViewRow In UsersGridView.Rows
                Dim cb As CheckBox = DirectCast(row.FindControl("chkRows"), CheckBox)
                If cb IsNot Nothing AndAlso cb.Checked Then
                    Dim userName As String = UsersGridView.DataKeys(row.RowIndex).Value.ToString()
                    'ProfileManager.DeleteProfile(userName)
                    CustomerMembershipProvider.DeleteUser(userName, True)

                    'Response.Redirect("Users.aspx");
                    lblDeleteSuccess.Visible = True
                End If
            Next
            UpdateGrid()
        End Sub

#End Region


        Private Sub Users_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then
                BindUserAccounts()
                BindAtoZNavigation()
            End If
        End Sub

        Private Sub Users_PreRenderComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRenderComplete
            ' hide delete button if gridview returns nothing
            btnDeleteSelected.Visible = UsersGridView.Rows.Count > 0
        End Sub
    End Class
End Namespace
