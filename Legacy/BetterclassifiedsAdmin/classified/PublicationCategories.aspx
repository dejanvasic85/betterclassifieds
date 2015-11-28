<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="PublicationCategories.aspx.vb" Inherits="BetterclassifiedAdmin.PublicationCategories" 
    title="Publication Categories" %>

<%@ Register Src="~/classified/UserControls/PubNavigation.ascx" TagName="PubNavigation" TagPrefix="ucx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    Publication Categories
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:PubNavigation ID="ucxPubNavigation" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Manage Categories Assigned to Publications" Font-Bold="true" />
    <p>Each newspaper/magazine will have their own categories, however each one needs to be mapped
    accordingly to a main category that drives the system. This is so that feature to book for 
    multiple publications is available.</p>
    
    <div style="height: 300px;">
    
        <asp:Label ID="Label3" runat="server" Text="Select Publication" Font-Bold="true"></asp:Label>&nbsp;
        <asp:DropDownList ID="ddlPublications" runat="server" DataTextField="Title" DataValueField="PublicationId" Width="300px" AutoPostBack="true" />
        
        <div style="width: 700px; margin-top: 15px; padding: 5px; height: 15px;" class="gridColumnHeaderBG">
            <div style="font-weight: bold; width: 350px; float:left">Parent Categories</div>
            <div style="font-weight: bold; width: 350px; float: left">Sub Categories</div>
        </div>
        
        <div class="categoryOuter">
            <div class="categoryInner">
                <asp:HyperLink ID="lnkCreate" runat="server" Text="Create" rel="gb_page_center[525, 600]"
                               Title="Create New Publication Category" />&nbsp;
                <asp:HyperLink ID="lnkEdit" runat="server" Text="Edit" rel="gb_page_center[525, 600]"
                               Title="Edit Publication Category Details" NavigateUrl="~/classified/ModalDialog/Edit_PublicationCategory.aspx" />&nbsp;
                <asp:LinkButton ID="btnDeleteMc" runat="server" Text="Delete" 
                                OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshParents" runat="server" Text="Refresh" />
            </div>
            <div class="categoryInner">
                <asp:HyperLink ID="lnkCreateSubCategory" runat="server" Text="Create" rel="gb_page_center[525, 600]"
                                Title="Create New Publication Category" NavigateUrl="~/classified/ModalDialog/Create_PublicationCategory.aspx" />&nbsp;
                <asp:HyperLink ID="lnkEditSub" runat="server" Text="Edit" rel="gb_page_center[525, 600]"
                               Title="Edit Publication Category Details" NavigateUrl="~/classified/ModalDialog/Edit_PublicationCategory.aspx" />&nbsp;
                <asp:LinkButton ID="btnDeleteSc" runat="server" Text="Delete" 
                                OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshSubCategories" runat="server" Text="Refresh" />
            </div>
        </div>
        
        <div class="categoryOuter">
            
            <div class="categoryInner">
                <asp:ListBox ID="lstParentCategories" runat="server" DataTextField="Title" 
                        DataValueField="PublicationCategoryId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblParentConfirm" runat="server" Text="" ForeColor="Red"></asp:Label>
                    
            </div>
            <div class="categoryInner">
                <asp:ListBox ID="lstSubCategories" runat="server" DataTextField="Title" 
                        DataValueField="PublicationCategoryId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblSubCategoryConfirm" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
                
        </div>
    
    </div>
</asp:Content>
        