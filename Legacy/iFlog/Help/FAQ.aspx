<%@ Page Title="Frequently Asked Questions" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master" CodeBehind="FAQ.aspx.vb" Inherits="BetterclassifiedsWeb.FAQ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="mainInfo">
        <div id="mainHeaderInfo">
            <h2>
                FAQ</h2>
        </div>
        <div id="mainContentInfo">
            <div id="faqContent" runat="server">
                <%--database driven content goes here--%>
            </div>
        </div>
    </div>
</asp:Content>
