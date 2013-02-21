<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="ClassifiedReports.aspx.vb" Inherits="BetterclassifiedAdmin.ClassifiedReports" 
    title="Classified Reports" %>
    
<%@ Register Src="~/report/navigation/ClassifiedNav.ascx" TagName="WeeklyPublication" TagPrefix="ucx" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Classified Reports
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:WeeklyPublication runat="server" ID="ucxNavigation" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    Welcome to Classified Reports
    <ul>
        <li><asp:HyperLink ID="lnkWeekly" runat="server" Text="Weekly Publication Report"
                NavigateUrl="~/report/WeeklyPublication.aspx" /></li>
        <li><asp:HyperLink ID="HyperLink1" runat="server" Text="Transaction Report"
                NavigateUrl="~/report/TransactionReport.aspx" /></li>
    </ul>
</asp:Content>