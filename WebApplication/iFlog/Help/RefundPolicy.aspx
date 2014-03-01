<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master"
    CodeBehind="RefundPolicy.aspx.vb" Inherits="BetterclassifiedsWeb.RefundPolicy"
    Title="Refund Policy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="mainInfo">
        <div id="mainHeaderInfo">
            <h2>
                Refund Policy</h2>
        </div>
        <div id="mainContentInfo">
            <p>
                Cancellation of advertisement placement online or in print can be made 7 days prior
                the the advertisement running. Otherwise, normal Terms and Conditions apply.</p>
        </div>
        <div id="mainFooterInfo">
            <em>RETURN TO:</em> <a href="#0">TOP</a> |
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/">HOME</asp:HyperLink>
        </div>
    </div>
</asp:Content>
