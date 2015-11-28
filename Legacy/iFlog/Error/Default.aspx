<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Default.Master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedsWeb._Default7" 
    title="Error" %>
<asp:Content ID="cntMain" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- General Error Message -->
    <asp:Panel ID="pnlGeneralError" runat="server" Visible="false">
    
        <h3>Server Error</h3>
    
        <p>Your request has raised an unexpected error. </p>
       
        <p>The issue has been logged to our system administrators.
        Please try again later or if problem persists, contact our friendly staff.</p>
        
        <p>Thank you.</p>
    </asp:Panel>
    
    <!-- Connection and Settings Error Message -->
    <asp:Panel ID="pnlDatabaseConnection" runat="server" Visible="false">
        
        <h3>Server Error</h3>
    
        <p>We are currently experiencing some technical difficulties. 
        This issue has been logged with our technical staff who are already looking into the problem.
        Please try your request again very soon. We apologise for any inconvenience caused.</p>
        
        <p>Thank you.</p>
    </asp:Panel>
    
    <!-- Size of the Request was too large (user possible uploading large file size) -->
    <asp:Panel ID="pnlRequestSize" runat="server" Visible="false">
        
        <h3>Server Error</h3>
    
        <p>Your request was too large to handle. If uploading a document please choose a smaller size file.</p>
        
        <p>Thank you.</p>
    </asp:Panel>
    
</asp:Content>
