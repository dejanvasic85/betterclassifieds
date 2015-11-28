<%@ Page Title="Manage Line Ad Themes" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="ManageThemes.aspx.vb" Inherits="BetterclassifiedAdmin.ManageThemes" %>

<%@ Register Src="~/classified/UserControls/LineAdThemeNavigation.ascx" TagName="ThemeNavigation" TagPrefix="ucx" %>
<%@ Register Src="~/controls/UserMessage.ascx" TagName="UserMessage" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        .lstThemes-item
        {
            border: 1px solid #6D7B8D;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Line Ad Themes
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:ThemeNavigation ID="ucxThemeNavigation" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">

    <ucx:UserMessage ID="ucxMessage" runat="server" />

    <div style="margin-bottom: 10px;">
        <asp:Button ID="btnCreateNewTheme" runat="server" Text="Create New Theme" />
    </div>

    <asp:DataList ID="lstThemes" runat="server" RepeatDirection="Horizontal" 
        CellPadding="5"
        Width="85%" RepeatColumns="4">
        <ItemTemplate>
            <table cellpadding="2" cellspacing="0">
                <tr><td class="lstThemes-item">
                    <paramountIt:LineAdView ID="lineAdView" runat="server"
                        AdText="Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut 
                            labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris" 
                        HeaderText='<%# Eval("ThemeName") %>'
                        ImageUrl="~/Resources/Images/PreviewPlaceholder.jpg"

                        HeaderColourCode='<%# Eval("HeaderColourCode") %>'
                        BackgroundColourCode='<%# Eval("BackgroundColourCode") %>' 
                        BorderColourCode='<%# Eval("BorderColourCode") %>' 
                        IsHeadingSuperBold='<%# Eval("IsHeadingSuperBold") %>' />
                    </td></tr>
                <tr align="center"><td class="lstThemes-item">

                    <asp:ImageButton ID="btnEdit" runat="server"
                        ImageUrl="~/App_Themes/blue/Images/Edit.png"
                        CommandArgument='<%# Eval("LineAdThemeId") %>'
                        CommandName="Edit"
                        ToolTip="Edit Theme" />
                    &nbsp;
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/App_Themes/blue/images/delete.gif"
                        CommandArgument='<%# Eval("LineAdThemeId") %>'
                        CommandName="Disable"
                        OnClientClick="javascript:return confirm('Are you sure you want to Delete Theme?')"
                        ToolTip="Delete Theme" />
                    </td></tr>
            </table>

        </ItemTemplate>
    </asp:DataList>
    
</asp:Content>
