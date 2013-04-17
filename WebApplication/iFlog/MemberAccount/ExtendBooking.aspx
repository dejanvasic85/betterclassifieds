<%@ Page Title="Extend Booking" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MemberDetails.master" CodeBehind="ExtendBooking.aspx.vb" Inherits="BetterclassifiedsWeb.ExtendBooking" %>

<%@ Register Src="~/MemberAccount/MemberHeading.ascx" TagName="MemberHeading" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="memberHeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="memberContentMain" runat="server">
    <ucx:MemberHeading ID="ucxHeading" runat="server" HeadingText="Booking Extension" />

    <%--Form goes here--%>
    <paramountIt:FormDropDownList runat="server" ID="ddlEditions"
        Text="Insertions"
        HelpText="Number of weeks to extend the booking" />

    <asp:Repeater runat="server" ID="rptEditions">
        <HeaderTemplate>
            Publication Editions
        </HeaderTemplate>
        <ItemTemplate>
            <div>
                <asp:Label runat="server" ID="lblPublicationName" Text='<%# Eval("PublicationName") %>'></asp:Label>
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
