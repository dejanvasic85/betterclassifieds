<%@ Page Title="Line Ad Theme Details" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="CreateLineAdTheme.aspx.vb" Inherits="BetterclassifiedAdmin.CreateLineAdTheme" %>

<%@ Register Src="~/classified/UserControls/LineAdThemeNavigation.ascx" TagName="ThemeNavigation" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .createLineAd-commandDiv
        {
            float:right;
            margin: 10px 5px 10px 5px;
            display: inline;
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
    
    <div style="float:left">
        <asp:DetailsView ID="dtlLineAdThemeDetail" runat="server" HeaderText="Line Ad Theme Details"
            DefaultMode="Insert" AutoGenerateRows="false" DataKeyNames="LineAdThemeId"
            DataSourceID="srcLinq">
            <Fields>
                <asp:BoundField DataField="ThemeName" HeaderText="Theme Name" />
                <asp:TemplateField HeaderText="Header Colour">
                    <ItemTemplate>
                        <paramountIt:LineAdColourPicker ID="headerColourPicker" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Border Colour">
                    <ItemTemplate>
                        <paramountIt:LineAdColourPicker ID="borderColourPicker" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Background Colour">
                    <ItemTemplate>
                        <paramountIt:LineAdColourPicker ID="backgroundColourPicker" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>

        <asp:LinqDataSource ID="srcLinq" runat="server" EnableObjectTracking="false"
            ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext"
            TableName="LineAdThemes" 
            EnableInsert="true"
            EnableUpdate="true" />

        <div class="createLineAd-commandDiv">
            <asp:Button ID="btnCreate" runat="server" Text="Save" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
        </div>

    </div>

    <div style="clear:both;"></div>
</asp:Content>
