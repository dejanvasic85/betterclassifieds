<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Default.aspx.vb" Inherits="BetterclassifiedAdmin._Default3" %>
<asp:content ID="Content1" ContentPlaceHolderID="head" runat="server">

<%-- greybox javascript sources --%>
<script language="javascript" type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
</script>
<script type="text/javascript" src="../greybox/AJS.js"></script>
<script type="text/javascript" src="../greybox/AJS_fx.js"></script>
<script type="text/javascript" src="../greybox/gb_scripts.js"></script>
<link href="../greybox/gb_styles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
Web Content
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:hyperlink id="HyperLink1" runat="server" rel="gb_page_fs[]"  navigateurl="~/cms/admin.cmsx">Manage web Content</asp:hyperlink>
</asp:Content>
