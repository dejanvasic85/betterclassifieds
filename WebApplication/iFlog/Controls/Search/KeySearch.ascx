<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="KeySearch.ascx.vb" Inherits="BetterclassifiedsWeb.Controls.Search.KeySearch1" %>

<asp:Panel ID="pnlFlogSearch" runat="server" DefaultButton="btnSearch">
<asp:UpdatePanel ID="pnlUpdateControls" runat="server">
    <ContentTemplate>
        <div id="searchCategories">
            <table width="717" border="0" cellspacing="5px 20px" cellpadding="0px">
                <tr>
                    <td>
                        <asp:TextBox ID="txtKeyword" runat="server" Text="search keywords..."
                            Width="200px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMainCategory" runat="server" Width="200px"
                            DataTextField="Title" DataValueField="MainCategoryId" AutoPostBack="true" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLocation" runat="server" Width="200px"
                            DataTextField="Title" DataValueField="LocationId" AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtIflogId" runat="server" Text="Ad ID" Width="200px" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubCategory" runat="server" Width="200px"
                            DataTextField="Title" DataValueField="MainCategoryId" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLocationArea" runat="server" Width="200px"
                            DataTextField="Title" DataValueField="LocationAreaId" />
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<div id="searchAndIflogBut">
    <div id="searchButton">
        <p>
            <asp:Button ID="btnSearch" runat="server" Text="Search"></asp:Button></p>
    </div>
</div>
</asp:Panel>