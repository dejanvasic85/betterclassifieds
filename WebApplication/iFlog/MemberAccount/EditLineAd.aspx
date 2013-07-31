<%@ Page Title="Edit Print Ad" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="EditLineAd.aspx.vb" Inherits="BetterclassifiedsWeb.EditLineAd" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">

    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Edit Print Ad" />
    <div class="clearFloat" />

    <div runat="server" id="divBodyEdit" style="padding-left: 10px; width: 700px; padding-top: 5px;">
        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
            <asp:Label ID="lblUserMsg" runat="server" Text="Details have been updated successfully" />
            <asp:HyperLink ID="lnkPreview" runat="server" NavigateUrl="#" Text="Preview Changes" />
        </asp:Panel>

        <%--Telerik RadWindow for displaying and managing online ad images--%>
        <telerik:RadWindow ID="radWindowImages" runat="server"
            NavigateUrl="~/Common/UploadManager.aspx" Title="Manage Print Image"
            Width="680px" Height="580px" Modal="true" Behaviors="Close" ReloadOnShow="true" VisibleStatusbar="false" />

        <%--Telerik popup window for previewing an ad--%>
        <telerik:RadWindow ID="radWindowPreview" runat="server"
            Title="Line Ad Preview" Modal="true" Width="240px" Height="400px"
            ReloadOnShow="true" Behaviors="Close" VisibleStatusbar="false" />

        <paramountIt:LineAdEditDetails runat="server" ID="lineAdDetails"
            HelpContextImageUrl="~/Resources/Images/question_button.gif"
            CssClass="lineAdEdit" IsAdminMode="false"
            QueryParamType="AdBookingId" QueryParamName="bkId"
            CancelNavigateUrl="~/MemberAccount/Bookings.aspx"
            Style="display: block" />

    </div>
</asp:Content>
