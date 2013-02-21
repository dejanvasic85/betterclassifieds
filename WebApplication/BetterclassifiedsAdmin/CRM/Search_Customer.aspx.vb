Imports BetterclassifiedAdmin.Configuration.ConfigManager

Namespace CRM
    Partial Public Class Search_Customer
        Inherits System.Web.UI.Page

#Region "Hide delete button if no gridview results"

        Protected Sub Page_PreRenderComplete(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRenderComplete
            ' hide delete button if gridview returns nothing
            If UsersGridView.Rows.Count = 0 Then
                btnDeleteSelected.Visible = False
            End If

        End Sub

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            ' show delete button if gridview has rows
            If Not UsersGridView.Rows.Count = Nothing Then
                btnDeleteSelected.Visible = True
            End If
        End Sub

#End Region

#Region "Search and GridView display"

        ' declare global variable for getallusers that will be used later
        Dim pagesize As Integer
        Private allRegisteredUsers As MembershipUserCollection = CustomerMembershipProvider.GetAllUsers(0, 200, pagesize)

        ' bind all registered users to gridview
        Private Sub BindAllUsers(ByVal reloadAllUsers As Boolean)
            Dim allUsers As MembershipUserCollection = Nothing
            If reloadAllUsers Then
                allUsers = CustomerMembershipProvider.GetAllUsers(0, 200, pagesize)
            End If

            Dim searchText As String = ""
            If Not String.IsNullOrEmpty(UsersGridView.Attributes("SearchText")) Then
                searchText = UsersGridView.Attributes("SearchText")
            End If
            Dim searchByEmail As Boolean = False
            If Not String.IsNullOrEmpty(UsersGridView.Attributes("SearchByEmail")) Then
                searchByEmail = Boolean.Parse(UsersGridView.Attributes("SearchByEmail"))
            End If
            If searchText.Length > 0 Then
                If searchByEmail Then
                    allUsers = CustomerMembershipProvider.FindUsersByEmail(searchText, 0, 150, pagesize)
                Else
                    allUsers = CustomerMembershipProvider.FindUsersByName(searchText, 0, 150, pagesize)
                End If
            Else
                allUsers = allRegisteredUsers
            End If
            UsersGridView.DataSource = allUsers
            UsersGridView.DataBind()
        End Sub

        ' on search button click
        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim searchByEmail As Boolean = (ddlUserSearchTypes.SelectedValue = "E-mail")
            UsersGridView.Attributes.Add("SearchText", txtSearchText.Text + "%")
            UsersGridView.Attributes.Add("SearchByEmail", searchByEmail.ToString())
            BindAllUsers(False)
        End Sub

#End Region

#Region "Delete one user with trashbin image"

        ' before the row is deleted
        Protected Sub UserAccounts_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
            Dim UserName As String = UsersGridView.DataKeys(e.RowIndex).Value.ToString()
            ProfileManager.ApplicationName = CustomerProfileProvider.ApplicationName
            ProfileManager.DeleteProfile(UserName)
            CustomerMembershipProvider.DeleteUser(UserName, True)

            'Response.Redirect("Users.aspx");
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

#Region "Delete users selected by checkboxes"

        Protected Sub btnDeleteSelected_Click(ByVal sender As Object, ByVal e As EventArgs)
            For Each row As GridViewRow In UsersGridView.Rows
                Dim cb As CheckBox = DirectCast(row.FindControl("chkRows"), CheckBox)
                If cb IsNot Nothing AndAlso cb.Checked Then
                    Dim userName As String = UsersGridView.DataKeys(row.RowIndex).Value.ToString()
                    CustomerMembershipProvider.DeleteUser(userName, True)

                    'Response.Redirect("Users.aspx");
                    lblDeleteSuccess.Visible = True
                End If
            Next
        End Sub

#End Region

    End Class

End Namespace