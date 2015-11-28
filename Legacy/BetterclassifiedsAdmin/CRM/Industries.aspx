<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Industries.aspx.vb" Inherits="BetterclassifiedAdmin.Industries" 
    title="Betterclassified Business Industry Management" %>
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
    Business Industry Management
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Business Industry Management Page" Font-Bold="true"></asp:Label>
    <p>Each customer has the ability to select an industry and category that their business belongs to. 
    Use this page to manage the options which the customer has to select from.</p>
    
    <div style="height: 250px;">
    
        <div style="width: 700px; margin-top: 15px; padding: 5px; height: 10px;" class="gridColumnHeaderBG">
            <div style="font-weight: bold; width: 350px; float:left">Industries</div>
            <div style="font-weight: bold; width: 350px; float: left">Business Categories</div>
        </div>
    
        <div class="categoryOuter">
            <div class="categoryInner">
                <table>
                    <tr>
                        <td><asp:Label ID="Label5" runat="server" Text="Title*" />:</td>
                        <td>
                            <asp:TextBox ID="txtIndustry" runat="server" Width="201px" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valIndustryRequired" runat="server" ControlToValidate="txtIndustry"
                                EnableClientScript="true" Text="*" SetFocusOnError="true" ValidationGroup="industryValidation" />
                        </td>
                        <td><asp:Button ID="btnAddIndustry" runat="server" Text="Add" CausesValidation="true"
                                ValidationGroup="industryValidation" /></td>
                    </tr>
                </table>
            </div>
            <div class="categoryInner">
                <table cellpadding="4">
                    <tr>
                        <td><asp:Label ID="Label6" runat="server" Text="Title*" />:</td>
                        <td>
                            <asp:TextBox ID="txtCategory" runat="server" Width="200px" AutoCompleteType="Disabled"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valRequired" runat="server" ControlToValidate="txtCategory" 
                                EnableClientScript="true" Text="*" SetFocusOnError="true" ValidationGroup="categoryValidation" />
                        </td>
                        <td><asp:Button ID="btnAddCategory" runat="server" Text="Add" CausesValidation="true" 
                                ValidationGroup="categoryValidation" /></td>
                    </tr>
                </table>
            </div>
            <div class="categoryInner">
                <asp:HyperLink ID="lnkEditIndustry" runat="server" Text="Edit" rel="gb_page_center[525, 600]" />&nbsp;
                <asp:LinkButton ID="btnDeleteIndustry" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshIndustry" runat="server" Text="Refresh" />
            </div>
            <div class="categoryInner">
                <asp:HyperLink ID="lnkEditCategory" runat="server" Text="Edit" rel="gb_page_center[525, 600]" />&nbsp;
                <asp:LinkButton ID="btnDeleteCategory" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshCategory" runat="server" Text="Refresh" />
            </div>
        </div>
        
        <div class="categoryOuter">
            <div class="categoryInner">
                <asp:ListBox ID="lstIndustry" runat="server" DataTextField="Title" 
                        DataValueField="IndustryId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblIndustry" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div class="categoryInner">
                <asp:ListBox ID="lstCategory" runat="server" DataTextField="Title" 
                        DataValueField="BusinessCategoryId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblCategory" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </div>  
       
    </div>
       
</asp:Content>
