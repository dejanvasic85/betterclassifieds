<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="ApplicationSettings.aspx.vb" Inherits="BetterclassifiedAdmin.ApplicationSettings" 
    title="Classified Application Settings" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
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
    Classified Application Settings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:AppNav ID="ucxAppNav" runat="server" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cphContentHeading" runat="server">
    Manage Application Settings
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    
    <p>Welcome to Betterclassifieds. This application is set up as an end user Ad booking system. If you have not been set up with this 
    package, please contact Paramount IT Solutions immediately.</p>
    <p>Configuring application settings needs to be done with extreme care. System relies on the Module name and Application Key
    in order to read the setting value so these details are read only. Please read this     
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/classified/ModalDialog/DataTypeDetails.aspx" rel="gb_page_center[400, 400]" Title="Setting Data Types">
    explanation on data types</asp:HyperLink> before updating.</p>

    <p class="spanWarning"> 
       <asp:Label ID="Label9" runat="server" CssClass="spanWarning" Text="Warning:" Font-Bold="true" />&nbsp;
       <asp:Label ID="Label10" runat="server" CssClass="spanWarning" Text="Changing an application setting may cause system failure. Please read the instruction/description of each setting carefully before saving." /></p>

    <asp:UpdatePanel ID="pnlUpdateSetting" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            Select the module and setting name to view and edit.<br />
            <table cellpadding="5">
                <tr>
                    <td>Module: </td>
                    <td><asp:DropDownList ID="ddlModules" runat="server" Width="250px" AutoPostBack="true" /></td>
                    <td>Setting Name:</td>
                    <td><asp:DropDownList ID="ddlSetting" runat="server" Width="250px" /></td>
                    <td><asp:Button ID="btnView" runat="server" Text="View Details" /></td>
                </tr>
            </table>
            <div style="width: 500px;">
                <asp:DetailsView ID="dtlSetting" runat="server" AutoGenerateRows="False" 
                    DefaultMode="Edit" DataKeyNames="Module,AppKey"
                                 HeaderText="Setting Details" DataSourceID="linqSourceSetting">
                    <Fields>
                        <asp:BoundField DataField="Description" HeaderText="Description" 
                            ReadOnly="true" HeaderStyle-VerticalAlign="Top" >
                            <HeaderStyle VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Module" HeaderText="Module" ReadOnly="true" 
                            ItemStyle-Font-Bold="true" >
                            <ItemStyle Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AppKey" HeaderText="Setting Name" ReadOnly="true" 
                            ItemStyle-Font-Bold="true" >
                            <ItemStyle Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DataType" HeaderText="Data Type" ReadOnly="true" 
                            ItemStyle-Font-Bold="true" >
                            <ItemStyle Font-Bold="True" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SettingValue" HeaderText="Value" />
                        <asp:TemplateField ShowHeader="False" ItemStyle-HorizontalAlign="Right">
                            <EditItemTemplate>
                                <asp:Button ID="btnSave" runat="server" CausesValidation="True" CommandName="Update" Text="Save" />
                            </EditItemTemplate>
                            <ControlStyle Font-Size="11px" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                    </Fields>
                </asp:DetailsView>
                <asp:LinqDataSource ID="linqSourceSetting" runat="server" ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext" 
                    TableName="AppSettings" EnableUpdate="True" />
            </div>
            <asp:Label ID="lblSettingMsg" runat="server" Text="" ForeColor="Red" />
            
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
    
</asp:Content>
