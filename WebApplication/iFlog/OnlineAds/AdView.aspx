﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master"
    CodeBehind="AdView.aspx.vb" Inherits="BetterclassifiedsWeb.AdView" %>

<%@ Register Src="~/Controls/OnlineAdView.ascx" TagName="OnlineAd" TagPrefix="ucx" %>
<%@ Register src="~/Controls/TutorAdView.ascx" tagName="TutorView" tagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="clearFloat">
        <div id="contentBody">
            <paramountIt:FrameAutoSizeControl ID="frameControl" runat="server" />
            <div id="sidebar">
                <div id="sidebarHeader">
                    <p>
                        categories header
                    </p>
                </div>
                <div id="sidebarContent">
                    <paramountIt:CategorySelector ID="categorySelector" runat="server" />
                </div>

                <div style="clear: both"></div>
            </div>

            <div id="mainItemPage" runat="server">
                <ucx:OnlineAd ID="ucxOnlineAd" runat="server" />
                <%--Specific ad type views--%>
                <ucx:TutorView ID="ucxTutors" runat="server" Visible="False"/>
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
