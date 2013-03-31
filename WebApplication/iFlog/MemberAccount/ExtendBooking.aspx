<%@ Page Title="Extend Booking" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="ExtendBooking.aspx.vb" Inherits="BetterclassifiedsWeb.ExtendBooking" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Booking Extensions" />
    <paramountIt:ExtendBookingForm runat="server" ID="formExtendBooking" />
</asp:Content>