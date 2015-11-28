<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_Category.aspx.vb" Inherits="BetterclassifiedAdmin.Edit_Category" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Category Details</title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 20px">
            <%-- ajax script manager --%>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

            <%-- ajax update panel start --%>
            <asp:UpdatePanel ID="pnlUpdateGeneralDetails" runat="server">
                <ContentTemplate>

                    <ajax:TabContainer ID="tcntUserInfo" runat="server" ActiveTabIndex="0"
                        Width="100%" Font-Size="10px">

                        <ajax:TabPanel ID="TabPanel2" runat="server" HeaderText="General Info">
                            <ContentTemplate>
                                <div style="font-size: 11px;">
                                    <br />

                                    <asp:DetailsView ID="viewCategory" runat="server" AutoGenerateRows="False"
                                        Style="width: 445px" HeaderText="Category Details"
                                        DefaultMode="Edit" DataKeyNames="MainCategoryId" DataSourceID="linqSourceCategory">
                                        <Fields>
                                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                        </Fields>
                                    </asp:DetailsView>
                                    
                                    <%--Online Ad tags (1 to 1 for now) --%>
                                    <table class="detailsviewMain" width="90%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td colspan="2" class="detailsviewheaderBG">Online Ad Mapping</td>
                                        </tr>
                                        <tr>
                                            <td>Type</td>
                                            <td><asp:DropDownList runat="server" ID="ddlOnlineTypes"/></td>
                                        </tr>
                                    </table>

                                    <asp:LinqDataSource ID="linqSourceCategory" runat="server" EnableUpdate="true" EnableObjectTracking="false"
                                        ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" TableName="MainCategories" />
                                </div>
                            </ContentTemplate>
                        </ajax:TabPanel>
                    </ajax:TabContainer>

                    <div class="controlDiv">
                        <asp:Button ID="btnUpdateCategory" runat="server" Text="Update" />
                    </div>

                    <br />

                    <div>
                        <asp:Label ID="lblCategoryMsg" runat="server" Text="" />
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="pnlUpdateGeneralDetails">
                <ProgressTemplate>
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/blue/Images/progress_circle.gif" Height="20" Width="20" /></td>
                            <td>
                                <asp:Label ID="lblProgress" runat="server" Text="Processing..." /></td>
                        </tr>
                    </table>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </form>
</body>
</html>
