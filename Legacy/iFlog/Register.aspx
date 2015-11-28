<%@ Page Title="Register to start placing Classifieds" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="Register.aspx.vb" Inherits="BetterclassifiedsWeb.Register" %>

<%@ Register Src="~/Controls/Member/Register.ascx" TagName="Register" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="clearFloat">
        <div id="contentBodyAccounts">
            <div id="mainLoginRegister">
                <div id="mainHeaderMyAccount">
                    <h2>Register your new account...</h2>
                </div>
                <div id="mainContentMyAccount">
                    <ucx:Register ID="ucxRegister" runat="server" />
                </div>
            </div>
        </div>
        <br />
    </div>
</asp:Content>
