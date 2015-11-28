<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="AdView.aspx.vb" Inherits="BetterclassifiedsWeb.AdView" %>

<%@ Register Src="~/Controls/OnlineAdView.ascx" TagName="OnlineAd" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="clearFloat">
        <div id="contentBody">
            <paramountIt:FrameAutoSizeControl ID="frameControl" runat="server" />
            <div id="sidebar">
                <div id="sidebarHeader">
                    <span class="content-header">Categories</span>
                </div>
                <div id="sidebarContent">
                    <paramountIt:CategorySelector ID="categorySelector" runat="server" />
                </div>

                <div style="clear: both"></div>
            </div>

            <div id="mainItemPage">
                <ucx:OnlineAd ID="ucxOnlineAdDetailView" runat="server" />

             
                <asp:Panel ID="pnlResult" runat="server" Visible="false">
                    <div id="mainHeaderItemPage">
                        <h2>Cannot find requested Ad</h2>
                    </div>
                    <div id="mainBodyItemPage" style="margin-top: 10px;">
                        <div id="mainBodyItemPageBlock">
                            <h6>Ad '<asp:Label ID="lblIFlogID" runat="server" Text="" />' cannot be found
                        because it does not exist or has expired.
                            </h6>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div style="height: 100%;">
                <paramountIt:SendMessageControl runat="server" ID="SendMessageControl" CssClass="rightSidebarMessage" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="pageScripts" ID="AdViewScript">
    <script type="text/javascript">
        $().ready(function () {
            $('.RadCaptcha').find('img').width('160px');
        });
    </script>
</asp:Content>
