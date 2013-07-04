<%@ Page Title="Classies Home" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master"
    CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default4" Theme="iflog" %>

<%@ Register Src="~/Controls/Search/RecentOnlineAdList.ascx" TagName="RecentlyAdded" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Search/FlogID.ascx" TagName="FlogSearch" TagPrefix="ucx" %>

<asp:Content ID="cntHead" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="sidebar">
        <div id="sidebarHeader">
            <p>
                categories header</p>
        </div>
        <div id="sidebarContent">
             <paramountIt:CategorySelector ID="categorySelector" runat="server" />
        </div>
        
        <ucx:FlogSearch ID="ucxFlogSearch" runat="server" />
    </div>
    
    <div id="main">
        <div id="mainHeader">
            <p>
                mainHeader</p>
        </div>
        
        <div id="mainContent">
            <ucx:RecentlyAdded ID="ucxRecentlyAdded" runat="server" />            
        </div>
    </div>

  
</asp:Content>
