<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="SpecialRates.aspx.vb" Inherits="BetterclassifiedAdmin.SpecialRates" 
    title="Classified Special Rates" %>
    
<%@ Register Src="~/classified/UserControls/RateNavigation.ascx" TagName="RateNavigation" TagPrefix="ucx" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

     //  toggle checkboxes in gridview javascript function 
     function SelectAllCheckboxes(spanChk){
     
       var oItem = spanChk.children;
       var theBox= (spanChk.type=="checkbox") ? 
            spanChk : spanChk.children.item[0];
       xState=theBox.checked;
       elm=theBox.form.elements;

       for(i=0;i<elm.length;i++)
         if(elm[i].type=="checkbox" && 
                  elm[i].id!=theBox.id)
         {
           if(elm[i].checked!=xState)
             elm[i].click();
         }
     }
     </script>
     
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
    Special Rates
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:RateNavigation ID="ucxRateNavigation" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">

    <asp:Label ID="lblTitle" runat="server" Text="Special rates" Font-Bold="true" /><br />
    <p>Special Rates can be assigned to a Publication Category only. This can be done via the 
        Publication menu and Categories option.</p>
    <p class="spanWarning">Please note: Description is displayed in the price list section in the Customer Website. 
        Title is used for internal reference only.</p>    
    <br />
    
  
    <div style="width: 600px; margin-bottom: 10px">

        <%-- Actions toolbar --%>
        <div class="membersToggle" style="padding-bottom: 1px;">
            <asp:HyperLink  ID="lnkCreateNew" runat="server" Text="Create New" ToolTip="Create a new Special Rate"
                            rel="gb_page_center[525, 600]" Title="Create New Special Rate" NavigateUrl="~/classified/ModalDialog/Create_SpecialRate.aspx" />
        </div> <br />
        <div style="float:right; margin-bottom: 5px;">
            <asp:LinkButton ID="btnRefreshList" runat="server" Text="Refresh List"></asp:LinkButton></div>
        <div>
       
        <asp:GridView ID="grdSpecialRates" runat="server" AutoGenerateColumns="false" DataKeyNames="SpecialRateId" 
                    EmptyDataText="No records found.">
            <Columns>
            
            <asp:TemplateField>
                <HeaderStyle CssClass="gridheaderBG" Width="1px" />
                <ItemStyle CssClass="gridheaderBG" Width="1px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Del">
                <HeaderTemplate>
                    <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" title="Check all checkboxes" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkRows" runat="server" ToolTip="Select for deletion" />
                </ItemTemplate>
                <ItemStyle Width="25px" HorizontalAlign="Center" />
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="Rate Name">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkSpecialRate" runat="server" rel="gb_page_center[525, 600]"
                        NavigateUrl='<%# String.Format("~/classified/ModalDialog/Edit_SpecialRate.aspx?specialRateId={0}", Eval("SpecialRateId")) %>' 
                        Text='<%# Eval("Title") %>' Title='<%# String.Format("Edit {0}", Eval("Title")) %>'>
                        <asp:Image ID="imgIcon" runat="server" Height="20" Width="20" ImageUrl="~/images/right.png" />
                    </asp:HyperLink>
                    <asp:HiddenField ID="hdnSpecialRateId" runat="server" Value='<%# Eval("SpecialRateId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField HeaderText="Description" DataField="Description" />
            <asp:BoundField HeaderText="Set Price" DataField="SetPrice" DataFormatString="{0:C}" />
            
            <asp:TemplateField HeaderText="Assign Rate">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkAssignRate" runat="server" rel="gb_page_center[525, 600]"
                        NavigateUrl='<%# String.Format("~/classified/ModalDialog/Assign_SpecialRates.aspx?specialRateId={0}", Eval("SpecialRateId")) %>' 
                        Title='<%# String.Format("Assign Special Rate - {0}", Eval("Title")) %>'>
                        <asp:Image ID="imgRate" runat="server" ImageUrl="~/images/right.png" Width="30" Height="30" />
                    </asp:HyperLink>
                    <asp:HiddenField ID="hdnSpecialRateId2" runat="server" Value='<%# Eval("SpecialRateId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:ImageButton ID="btnDeleteEdition" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/blue/images/delete.gif" 
                        OnClientClick="javascript:return confirm('Are you sure you want to delete?');" />
                </ItemTemplate>
            </asp:TemplateField>
        
            </Columns>
        </asp:GridView>


        <%-- delete checked publications --%>
        <div class="membersToggle">

            <asp:LinkButton ID="btnDeleteSelected" runat="server" ToolTip="Click to delete the selected special rates." 
                OnClientClick="return confirm('Are you sure you want to delete the selected special rates?');">Delete Selected</asp:LinkButton>
        
        </div>
        <br /><br />
        <div style="margin: 5px; ">
            <asp:Label ID="msgRates" runat="server" ForeColor="Red" />
        </div>
        
    </div>
       

</asp:Content>
