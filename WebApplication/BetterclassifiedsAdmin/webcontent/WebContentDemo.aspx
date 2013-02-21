<%@ Page  Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/Login.Master" CodeBehind="WebContentDemo.aspx.vb" Inherits="BetterclassifiedAdmin.WebContentDemo"  title='<%$ Resources:WebContentDemo, Page_Title %>' theme="Default"  enableeventvalidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
   <H1 style="margin-top:20px; margin-left:10px; word-spacing:40px; display:block; ">
       <asp:literal id="Literal1" runat="server" text='<%$ Resources:WebContentDemo, Page_Logo %>'></asp:literal>
   </H1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:dropdownlist id="DropDownList1" runat="server" autopostback="true">
        <asp:listitem selected="True" text="English Australia" value="en-AU" />
        <asp:listitem text="Italian" value="it-IT" />
    </asp:dropdownlist>
    <div style="clear:both;" >
      <asp:literal id="Literal2" runat="server" text='<%$ Resources:WebContentDemo, Body %>' />
        
    </div>
   
</asp:Content>

<asp:content ID="Content3" ContentPlaceHolderID="footerPaceHolder" runat="server">
     <asp:literal id="Literal3" runat="server" text='<%$ Resources:WebContentDemo, Footer %>' />
</asp:content>