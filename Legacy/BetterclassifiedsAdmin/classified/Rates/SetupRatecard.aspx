<%@ Page Title="Set up new casual rate" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="SetupRatecard.aspx.vb" Inherits="BetterclassifiedAdmin.SetupRatecard" %>

<%@ Register Src="~/classified/UserControls/RateNavigation.ascx" TagName="RateNavigation" TagPrefix="ucx" %>
<%@ Register Src="~/classified/UserControls/CreateRatecardControl.ascx" TagName="CreateNewRate" TagPrefix="ucx" %>
<%@ Register Src="~/classified/UserControls/AssignRateCardControl.ascx" TagName="AssignRatesToPublications" TagPrefix="ucx" %>
<%@ Register Src="~/controls/UserMessage.ascx" TagName="UserMessage" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Ratecards
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:RateNavigation ID="ucxRateNavigation" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphContentHeading" runat="server">
    Configure New Casual Ratecard
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <telerik:RadMultiPage ID="multiPageSetup" runat="server" SelectedIndex="0" Width="600px" 
        RenderSelectedPageOnly="true">
        <telerik:RadPageView ID="page1" runat="server">
            <ucx:CreateNewRate ID="ucxCreateRate" runat="server" />
            <div class="page-commandPanel">
                <asp:Button ID="btnCancelPage1" runat="server" Text="Cancel" />
                <asp:Button ID="btnCreateRatecard" runat="server" Text="Next" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="page2" runat="server">
            <ucx:AssignRatesToPublications ID="ucxAssignRates" runat="server" />
            <div class="page-commandPanel">
                <asp:Button ID="btnCancelPage2" runat="server" Text="Cancel" />
                <asp:Button ID="btnAssignRates" runat="server" Text="Next" />
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="pageFinal" runat="server">
            <ucx:UserMessage ID="ucxUserMessage" runat="server" />
            <div class="page-commandPanel">
                <asp:Button ID="btnFinish" runat="server" Text="Done" />
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</asp:Content>
