<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="EditOnlineAd.aspx.vb" Inherits="BetterclassifiedsWeb.EditOnlineAd" Title="Edit Online Ad" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/DesignOnlineAd.ascx" TagName="OnlineAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageError" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberContentMain" runat="server">

    <%--Heading--%>
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Edit Online Ad" />
    <div class="clearFloat" />
    <ucx:PageError ID="ucxPageError" runat="server" />

    <div runat="server" id="divBodyEdit" style="padding-left: 10px; padding-top: 5px; width: 700px;">
        <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
            <asp:Label ID="lblUserMsg" runat="server" />
            <asp:HyperLink ID="lnkPreview" runat="server" NavigateUrl="#" Text="Preview Changes" />
        </asp:Panel>

        <%--Telerik RadWindow for displaying and managing online ad images--%>
        <telerik:RadWindow ID="radWindowImages" runat="server"
            NavigateUrl="~/Common/UploadManager.aspx" Title="Manage Online Images"
            Width="680px" Height="580px" Modal="true" Behaviors="Close" ReloadOnShow="true" VisibleStatusbar="false" />

        <%--Telerik popup window for previewing an ad--%>
        <telerik:RadWindow ID="radWindowPreview" runat="server"
            Title="Online Ad Preview" Modal="true" Width="650px" Height="500px"
            ReloadOnShow="true" Behaviors="Close" VisibleStatusbar="false" />

        <ucx:OnlineAd ID="ucxOnlineAd" runat="server" />
        
        <div class="accountRow">
            <div class="alert alert-info">
                Your pictures will be resized to 900 pixel width and 500 pixel height. 
                                        If you prefer no automatic resizing, please upload an image with this 
                                        size ratio. Click 'Upload' to begin.
            </div>

            <div class="btn-group pull-right" style="padding-top: 10px;">
                <asp:HyperLink runat="server" ID="lnkCancel" CssClass="btn btn-warning" Text="Cancel" NavigateUrl="~/MemberAccount/Bookings.aspx"></asp:HyperLink>
                <asp:HyperLink ID="lnkOnlineImages" runat="server" NavigateUrl="#" Text="Manage Images" CssClass="btn btn-default" />
                <asp:LinkButton runat="server" ID="btnUpdate" CssClass="btn btn-default" Text="Update"></asp:LinkButton>
            </div>
        </div>
    </div>

</asp:Content>
