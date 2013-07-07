<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithMessage.Master"
    CodeBehind="AdView.aspx.vb" Inherits="BetterclassifiedsWeb.AdView"  %>

<%@ Register Src="~/Controls/OnlineAdView.ascx" TagName="OnlineAd" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <paramountIt:FrameAutoSizeControl ID="frameControl" runat="server" />
    <div id="sidebar">
        <div id="sidebarHeader">
            <p>
                categories header
            </p>
        </div>
        <div id="sidebarContent">
            <paramountIt:CategorySelector ID="categorySelector" runat="server"  />
        </div>
        
        <div style="clear:both"></div>
    </div>

    <div id="mainItemPage">        
        <ucx:OnlineAd ID="ucxOnlineAd" runat="server" />
        <asp:Panel ID="pnlResult" runat="server" Visible="false">
            <div id="mainHeaderItemPage">
                <h2>
                    Cannot find requested iFlog Ad</h2>
            </div>
            <div id="mainBodyItemPage" style="margin-top: 10px;">
                <div id="mainBodyItemPageBlock">
                    <h6>
                        iFlog ID '<asp:Label ID="lblIFlogID" runat="server" Text="" />' cannot be found
                        because it does not exist or has expired.
                    </h6>
                </div>
            </div>
        </asp:Panel>
    </div>
    <div style="height:100%; ">
          <paramountIt:SendMessageControl runat="server" ID="SendMessageControl" CssClass ="rightSidebarMessage" /> 
   </div>
</asp:Content>