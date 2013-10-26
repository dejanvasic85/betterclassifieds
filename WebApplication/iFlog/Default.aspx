<%@ Page Title="Classies Home - Australia's # 1 print and online classifieds network – currently free!" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master"
    CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default4" Theme="iflog" %>

<%@ Register Src="~/Controls/RecentOnlineAdList.ascx" TagName="RecentlyAdded" TagPrefix="ucx" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="sidebar">
        <div id="sidebarHeader">
            <span class="content-header">Categories</span>
        </div>
        <div id="sidebarContent">
             <paramountIt:CategorySelector ID="categorySelector" runat="server" />
        </div>
    </div>
    
    <div id="main">
        <div id="mainHeader">
            <span class="content-header">Latest Posts</span>
        </div>
        
        <div id="mainContent">
            <ucx:RecentlyAdded ID="ucxRecentlyAdded" runat="server" />            
        </div>
    </div>

  
</asp:Content>
