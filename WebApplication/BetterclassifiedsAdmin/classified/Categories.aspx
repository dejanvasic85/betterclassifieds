<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Categories.aspx.vb" Inherits="BetterclassifiedAdmin.Categories" 
    title="Classified Categories" %>
    
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
    Main Category Management
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <%-- enter category sub menu's here --%>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">

    <asp:Label ID="Label1" runat="server" Text="Main Categories" Font-Bold="true"></asp:Label>
    <p>Main Categories are items that users interact with during a booking. Every publication category needs to be mapped 
    to a main category so that the feature of booking into multiple publications is available.</p>
    <p>
       <asp:Label ID="Label3" runat="server" CssClass="spanWarning" Text="<b>Warning:</b> Categories are vital to the operation of the system. Deleting a category may cause loss of related and sensitive data. Please speak to your administrator before performing such an operation."></asp:Label></p>
   
    <div style="height: 300px;">
   
        <div style="width: 700px; margin-top: 15px; padding: 5px; height: 15px;" class="gridColumnHeaderBG">
            <div style="font-weight: bold; width: 350px; float:left">Parent Categories</div>
            <div style="font-weight: bold; width: 350px; float: left">Sub Categories</div>
        </div>
        
        <div class="categoryOuter">
            <div class="categoryInner">
                <table>
                    <tr>
                        <td><asp:Label ID="Label5" runat="server" Text="Title" />:</td>
                        <td><asp:TextBox ID="txtMainCategoryTitle" runat="server" Width="201px" 
                                AutoCompleteType="Disabled"></asp:TextBox></td>
                        <td><asp:Button ID="btnAddMc" runat="server" Text="Add" /></td>
                    </tr>
                </table>
            </div>
            <div class="categoryInner">
                <table cellpadding="4">
                    <tr>
                        <td><asp:Label ID="Label6" runat="server" Text="Title" />:</td>
                        <td><asp:TextBox ID="txtSubCategoryTitle" runat="server" Width="200px" 
                                AutoCompleteType="Disabled"></asp:TextBox></td>
                        <td><asp:Button ID="btnAddSc" runat="server" Text="Add" /></td>
                    </tr>
                </table>
            </div>
            <div class="categoryInner">
                <asp:HyperLink ID="lnkEditMc" runat="server" Text="Edit" rel="gb_page_center[525, 600]" />&nbsp;
                <asp:LinkButton ID="btnDeleteMc" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshParents" runat="server" Text="Refresh" />
            </div>
            <div class="categoryInner">
                <asp:HyperLink ID="lnkEditSc" runat="server" Text="Edit" rel="gb_page_center[525, 600]" />&nbsp;
                <asp:LinkButton ID="btnDeleteSc" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshSubCategories" runat="server" Text="Refresh" />
            </div>
        </div>
        
        <div class="categoryOuter">
            <div class="categoryInner">
                <asp:ListBox ID="lstParentCategories" runat="server" DataTextField="Title" 
                        DataValueField="MainCategoryId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblParentConfirm" runat="server" Text=""></asp:Label>
            </div>
            <div class="categoryInner">
                <asp:ListBox ID="lstSubCategories" runat="server" DataTextField="Title" 
                        DataValueField="MainCategoryId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblSubCategoryConfirm" runat="server" Text=""></asp:Label>
            </div>
        </div>   
    
    </div>
    
</asp:Content>
