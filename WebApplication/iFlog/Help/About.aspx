<%@ Page Title="iFlog About Us" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master"
    CodeBehind="About.aspx.vb" Inherits="BetterclassifiedsWeb.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="mainInfo">
        <div id="mainHeaderInfo">
            <h2>
                About Us</h2>
        </div>
        <div id="mainContentInfo">
            <p>
                <b>TheMusic Classies are operated under licence by Dharma Media Pty Ltd (ABN: 54 117 132 402). Dharma
                    Media Pty Ltd is part of the Street Press Australia Pty Ltd group of companies.</b>
        </div>
        <div id="mainFooterInfo">
            <em>RETURN TO:</em> <a href="#0">TOP</a> |
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">HOME</asp:HyperLink>
        </div>
    </div>
</asp:Content>
