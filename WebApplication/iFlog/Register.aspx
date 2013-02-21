<%@ Page Title="Register with iFlog" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="Register.aspx.vb" Inherits="BetterclassifiedsWeb.Register" %>

<%@ Register src="~/Controls/Member/Register.ascx" tagname="Register" tagprefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
    <div class="clearFloat">
        
        <div id="contentBodyAccounts">
            <div id="mainLoginRegister">
                
                <div id="mainHeaderMyAccount">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/Images/register_header.jpg"
                         AlternateText="Register header" />
                    <h2>
                        Register your new account with iFlog...</h2>
                </div>
                
                <div id="mainContentMyAccount">
                    <ucx:Register ID="ucxRegister" runat="server" />
                </div>
            </div>
            
            
        </div>
        
        <br />
    </div> 
    
</asp:Content>
