<%@ Page Title="Manage Account Details - iFlog" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="MemberDetails.aspx.vb" Inherits="BetterclassifiedsWeb.MemberDetails1" %>

<%@ Register Src="~/Controls/Member/MemberDetails.ascx" TagName="MemberDetails" TagPrefix="ucx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">

    <div id="mainHeaderMyAccount">
        <asp:Image ID="imgAccHeader" runat="server" ImageUrl="~/Resources/Images/my_account_header.gif" AlternateText="My Account" />
        <h2>
            Welcome, 
            <asp:LoginName ID="LoginName1" runat="server"  />
            <paramountIt:MessageNotifyControl ID="notifyControl" runat="server" />
        </h2>
            <h3>
                <asp:Label ID="lblHeader" runat="server" Text="Manage Account Details"></asp:Label></h3>
    </div>
    
    <div id="mainContentMyAccount">
        <ucx:MemberDetails ID="ucxMemberDetails" runat="server" ShowEmail="true" />    
    </div>
    
</asp:Content>
