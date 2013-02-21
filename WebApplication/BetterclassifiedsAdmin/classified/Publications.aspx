<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Publications.aspx.vb" Inherits="BetterclassifiedAdmin.Publications" 
    title="Classified Publications" %>
    
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
    Publication Management
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:PubNavigation ID="ucxPubNavigation" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Existing Publications" Font-Bold="true"></asp:Label>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Please be aware that the system allows only up to one online publication at a time."></asp:Label></p>
    <p>
       <asp:Label ID="Label3" runat="server" CssClass="spanWarning" Text="<b>Warning:</b> Deleting a publication from the system may cause loss of related and sensitive data. Please speak to your administrator before performing such an operation."></asp:Label></p>
       
    <div style="width: 700px; margin-bottom: 10px; margin-top: 5px;">
    
        <div class="membersToggle" style="padding-bottom: 1px;">
            <asp:HyperLink  ID="lnkCreateNew" runat="server" Text="Create New" ToolTip="Create a new Publication"
                            rel="gb_page_center[525, 600]" Title="Create New Publication" 
                            NavigateUrl="~/classified/ModalDialog/Create_Publication.aspx" />
        </div>
        <br />
        <div style="float:right; margin-bottom: 5px;">
            <asp:LinkButton ID="btnRefreshList" runat="server" Text="Refresh List"></asp:LinkButton></div>
        <div>
            <asp:updatepanel id="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <contenttemplate >
               
                <%-- gridview to display publications --%>      
                <asp:GridView ID="grdPublications" runat="server" AutoGenerateColumns="False" DataKeyNames="PublicationId" EmptyDataText="No records found.">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle CssClass="gridheaderBG" Width="1px" />
                            <ItemStyle CssClass="gridheaderBG" Width="1px" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Order" DataField="SortOrder" />
                        <asp:TemplateField HeaderText="Publication Title">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnkPublicationId" runat="server" 
                                    NavigateUrl='<%# String.Format("~/classified/ModalDialog/Edit_Publication.aspx?publicationId={0}", Eval("PublicationId")) %>' 
                                    rel="gb_page_center[525, 600]" Text='<%# Eval("Title") %>' Title="Edit Publication Details" />
                                    
                                <asp:HiddenField ID="hdnPublicationId" runat="server" Value='<%# Eval("PublicationId") %>' />
                                <asp:HiddenField ID="hdnTitle" runat="server" Value='<%# Eval("Title") %>' />
                                    
                            </ItemTemplate>
                        </asp:TemplateField>  
                        
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Label ID="lblPublicationType" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Image">
                            <ItemTemplate>
                                <asp:Image ID="imgPublication" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Frequency" DataField="FrequencyType" />
                        <asp:CheckBoxField HeaderText="Active" DataField="Active" />
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/blue/images/delete.gif" 
                                OnClientClick="javascript:return confirm('Are you sure you want to delete this publication?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
                 
                <br />
                <br />

                <%-- delete success label  --%>
                <asp:Label ID="lblDeleteSuccess" runat="server" ForeColor="Red"></asp:Label>
                 
                </contenttemplate>
            </asp:updatepanel>
            
        </div>
    </div>
</asp:Content>
