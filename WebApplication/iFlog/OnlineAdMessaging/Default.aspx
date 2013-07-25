<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/MasterWithRightBar.Master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default" 
    title="Contact advertiser" %>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<div >
    <fieldset class="fieldset">
        <legend >Contact Advertiser</legend>
        <paramountIt:SendMessageControl runat="server" ID="sendMessage1" CssClass="message-control"/>
    </fieldset>
</div>
     
</asp:Content>
