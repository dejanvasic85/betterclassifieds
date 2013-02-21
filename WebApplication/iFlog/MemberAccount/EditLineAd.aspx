<%@ Page Title="Edit Print Ad" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master"
    CodeBehind="EditLineAd.aspx.vb" Inherits="BetterclassifiedsWeb.EditLineAd" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/Booking/DesignLineAd.ascx" TagName="LineAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageErrors" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">

    <div id="mainHeaderMyAccount">
        <asp:Image ID="imgAccHeader" runat="server" ImageUrl="~/Resources/Images/my_account_header.gif" AlternateText="My Account" />
        <h2>
            Welcome, 
            <asp:LoginName ID="LoginName1" runat="server"  />
        </h2>
        
        <h3> Edit Print Ad - iFlog <asp:Literal ID="lblAdDesignId" runat="server" /></h3>
    </div>
    
    <div class="clearFloat" />
    
    <div runat="server" id="divBodyEdit" style="padding-left: 10px; width: 700px; padding-top: 5px;">
                
        <div class="aerobuttonmenu" style="margin-bottom: 10px;">
            <asp:HyperLink ID="lnkPreview" runat="server" CssClass="aero" NavigateUrl="#" ToolTip="Please submit changes before trying to preview"><span>Preview</span></asp:HyperLink>
            <asp:HyperLink ID="lnkPrintImages" runat="server" CssClass="aero" NavigateUrl="#"><span>Manage Image</span></asp:HyperLink>
        </div>
     
        <%--Telerik RadWindow for displaying and managing online ad images--%>
        <telerik:RadWindow ID="radWindowImages" runat="server" 
            NavigateUrl="~/Common/UploadManager.aspx" Title="Manage Print Image" 
            Width="680px" Height="580px" Modal="true" Behaviors="Close" ReloadOnShow="true" VisibleStatusbar="false" />
            
        <%--Telerik popup window for previewing an ad--%>
        <telerik:RadWindow ID="radWindowPreview" runat="server"
            Title="Line Ad Preview" Modal="true" Width="240px" Height="400px"
            ReloadOnShow="true" Behaviors="Close"  VisibleStatusbar="false" />
        
        <paramountItCommon:GenericMessagePanel ID="msgPanel" runat="server"  />

        <paramountIt:LineAdEditDetails runat="server" ID="lineAdDetails"
            HelpContextImageUrl="~/Resources/Images/question_button.gif"
            CssClass="lineAdEdit" IsAdminMode="false" 
            QueryParamType="AdBookingId" QueryParamName="bkId"
            style="display:block" />
    </div>
</asp:Content>
