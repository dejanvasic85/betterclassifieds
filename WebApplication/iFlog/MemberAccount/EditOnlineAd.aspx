<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="EditOnlineAd.aspx.vb" Inherits="BetterclassifiedsWeb.EditOnlineAd" 
    title="Edit Online Ad" %>

<%@ Register Src="~/Controls/Booking/DesignOnlineAd.ascx" TagName="OnlineAd" TagPrefix="ucx" %>
<%@ Register Src="~/Controls/ErrorList.ascx" TagName="PageError" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberContentMain" runat="server">
    <div id="mainHeaderMyAccount">
        <asp:Image ID="imgAccHeader" runat="server" ImageUrl="~/Resources/Images/my_account_header.gif" AlternateText="My Account" />
        <h2>
            Welcome, 
            <asp:LoginName ID="LoginName1" runat="server"  />
            <paramountIt:MessageNotifyControl ID="notifyControl" runat="server" />
        </h2>
        
        <h3> Edit Online Ad ID <asp:Literal ID="lblAdDesignId" runat="server" /></h3>
    </div>

    <div class="clearFloat" />

    <ucx:PageError ID="ucxPageError" runat="server" />    
    
    <div runat="server" id="divBodyEdit" style="padding-left: 10px; padding-top:5px; width: 700px;"> 
        
        <asp:Panel ID="pnlSuccess" runat="server" CssClass="message-success" Visible="false">
            <asp:Label ID="lblUserMsg" runat="server" />
        </asp:Panel>
        
        <div class="aerobuttonmenu">
            <asp:HyperLink ID="lnkPreview" runat="server" CssClass="aero" NavigateUrl="#" ToolTip="Please submit changes before trying to preview"><span>Preview</span></asp:HyperLink>
            <asp:HyperLink ID="lnkOnlineImages" runat="server" CssClass="aero" NavigateUrl="#"><span>Manage Images</span></asp:HyperLink>
        </div>

        <%--Telerik RadWindow for displaying and managing online ad images--%>
        <telerik:RadWindow ID="radWindowImages" runat="server" 
            NavigateUrl="~/Common/UploadManager.aspx" Title="Manage Online Images" 
            Width="680px" Height="580px" Modal="true" Behaviors="Close" ReloadOnShow="true" VisibleStatusbar="false" />
            
        <%--Telerik popup window for previewing an ad--%>
        <telerik:RadWindow ID="radWindowPreview" runat="server"
            Title="Online Ad Preview" Modal="true" Width="650px" Height="500px" 
            ReloadOnShow="true" Behaviors="Close" VisibleStatusbar="false" />
       
        <ucx:OnlineAd ID="ucxOnlineAd" runat="server" />
        
        <div id="myAccountTableButtonsAll" style="width: 700px">
            <ul class="accountButtons">
                <div id="myAccountTableButtonsCancel">
                    <li><asp:LinkButton ID="btnCancel" runat="server" Text="CANCEL" /></li>
                </div>
                <div id="myAccountTableButtonsModify">
                    <li><asp:LinkButton ID="btnUpdate" runat="server" Text="MODIFY" /></li>
                </div>
            </ul>
        </div>
    </div>
    
</asp:Content>
