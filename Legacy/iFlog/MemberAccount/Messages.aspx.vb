Imports BetterclassifiedsCore.Controller
Imports BetterClassified.UI
Imports BetterClassified.UI.Messaging
Imports Telerik.Web.UI

Partial Public Class Messages
    Inherits System.Web.UI.Page
    Protected Sub OnNeedDataSource(ByVal sender As Object, ByVal e As GridNeedDataSourceEventArgs)
        Me.messageGrid.DataSource = OnlineAdEnquiryController.GetOnlineAdEnquiryListByUser(User.Identity.Name)
    End Sub
    Protected Overrides Sub OnInit(ByVal e As System.EventArgs)
        MyBase.OnInit(e)
        Me.messageGrid.MasterTableView.Columns.AddAt(0, GetActionTemplate)
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        AddHandler messageGrid.NeedDataSource, AddressOf OnNeedDataSource
        If Not IsPostBack Then
            Me.messageGrid.DataSource = OnlineAdEnquiryController.GetOnlineAdEnquiryListByUser(User.Identity.Name)
            messageGrid.DataBind()
        End If
    End Sub

    Private Function GetActionTemplate() As GridColumn
         Dim item As New ActionButtonTemplate
        AddHandler item.ActionClick, AddressOf ActionClick
        Return New GridTemplateColumn With {.ItemTemplate = item}
    End Function

    Protected Sub ActionClick(ByVal sender As Object, ByVal e As ActionEventArgs)
        If e.ButtonType = ActionButtonType.Delete Then
            OnlineAdEnquiryController.MarkAsInactive(e.Key)
        End If

        If e.ButtonType = ActionButtonType.Mark Then
            OnlineAdEnquiryController.MarkAsRead(e.Key)
        End If
        Response.Redirect(Request.Url.ToString)
    End Sub

End Class