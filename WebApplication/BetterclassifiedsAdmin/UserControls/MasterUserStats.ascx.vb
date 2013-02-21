Public Partial Class MasterUserStats
    Inherits System.Web.UI.UserControl


#Region "count total users and users online"

    ' get number of registred users and assign it to global variable
    Private allRegisteredUsers As MembershipUserCollection = Membership.GetAllUsers()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        ' get the number of registered and online users and bind them to the labels on the page
        ' For counting users online, global.asax is used
        If Not Page.IsPostBack Then
            'lblOnlineUsers.Text = Membership.GetNumberOfUsersOnline().ToString();
            lblOnlineUsers.Text = Application("OnlineUsers").ToString()
            lblTotalUsers.Text = allRegisteredUsers.Count.ToString()
        End If
    End Sub

#End Region

End Class