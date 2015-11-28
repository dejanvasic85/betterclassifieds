<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="LocationManagement.aspx.vb" Inherits="BetterclassifiedAdmin.LocationManagement" %>

<%@ Register Src="~/classified/UserControls/AppSettingsNavigation.ascx" TagPrefix="ucx" TagName="AppNav" %>

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
Location Management
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
<ucx:AppNav ID="ucxAppNav" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Online Ad Location and Areas" Font-Bold="true"></asp:Label>
    <p>Each Online Ad has ability to capture where ad item/service is located regardless who is posting the ad. This
    is the management page to allow the user to select the location and area to where the ad belongs.</p>
    <p>Each Location has a Location Area assigned to it. Every location will have "Any Area" item associated to it for customers to choose.</p>
    
    <div style="margin-bottom: 10px; height: 300px;">
        <div style="width: 700px; margin-top: 15px; padding: 5px; height: 10px;" class="gridColumnHeaderBG">
            <div style="font-weight: bold; width: 350px; float:left">Locations</div>
            <div style="font-weight: bold; width: 350px; float: left">Areas</div>
        </div>
        
        <div class="categoryOuter">
            <div class="categoryInner">
                <table>
                    <tr>
                        <td><asp:Label ID="Label5" runat="server" Text="Title" />:</td>
                        <td><asp:TextBox ID="txtLocation" runat="server" Width="200px" 
                                AutoCompleteType="Disabled"></asp:TextBox></td>
                        <td><asp:Button ID="btnAddLocation" runat="server" Text="Add" /></td>
                    </tr>
                </table>
            </div>
            <div class="categoryInner">
                <table cellpadding="4">
                    <tr>
                        <td><asp:Label ID="Label6" runat="server" Text="Title" />:</td>
                        <td><asp:TextBox ID="txtLocationArea" runat="server" Width="200px" 
                                AutoCompleteType="Disabled"></asp:TextBox></td>
                        <td><asp:Button ID="btnAddArea" runat="server" Text="Add" /></td>
                    </tr>
                </table>
            </div>
            <div class="categoryInner">
                <asp:HyperLink ID="lnkEditLocation" runat="server" Text="Edit" rel="gb_page_center[525, 600]" />&nbsp;
                <asp:LinkButton ID="btnDeleteLocation" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshLocations" runat="server" Text="Refresh" />
            </div>
            <div class="categoryInner">
                <asp:HyperLink ID="lnkEditArea" runat="server" Text="Edit" rel="gb_page_center[525, 600]" />&nbsp;
                <asp:LinkButton ID="btnDeleteArea" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?');" />&nbsp;
                <asp:LinkButton ID="lnkRefreshAreas" runat="server" Text="Refresh" />
            </div>
        </div>
        
        <div class="categoryOuter">
            <div class="categoryInner">
                <asp:ListBox ID="lstLocations" runat="server" DataTextField="Title" 
                        DataValueField="LocationId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblLocation" runat="server" Text=""></asp:Label>
            </div>
            <div class="categoryInner">
                <asp:ListBox ID="lstAreas" runat="server" DataTextField="Title" 
                        DataValueField="LocationAreaId" Width="90%" AutoPostBack="true" 
                        Height="175px" /><br />
                <asp:Label ID="lblLocationArea" runat="server" Text=""></asp:Label>
            </div>
        </div>   
    </div>
       
</asp:Content>
