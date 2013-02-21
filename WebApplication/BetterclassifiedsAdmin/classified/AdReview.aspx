<%@ Page Title="Ad Review" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="AdReview.aspx.vb" Inherits="BetterclassifiedAdmin.AdReview" %>

<%@ Register Src="~/classified/UserControls/BookingNavigation.ascx" TagName="BookingNavigation" TagPrefix="ucx" %>

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
    Approve or Cancel Current Online Ads
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:BookingNavigation ID="ucxBookingNavigation" runat="server" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Online Ad Review" />
    
    <p>This feature allows you to select the Online Ads which you would like to Approve or Cancel. </p>
    
    <p>
        <span class="spanWarning">
            <asp:Label ID="Label2" runat="server" Text="Please Note:" Font-Bold="true" />
            Cancelling an Ad will not delete the ad, it will simply change its status and 
            alert the user by email to adjust and publish again which will place it back in this queue.</span>
    </p>
    
    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red" />
    
    <div style="padding: 10px; margin-bottom: 15px;">
        <div style="float:right; margin-bottom: 5px;">
            <asp:LinkButton ID="btnRefreshList" runat="server" Text="Refresh List"></asp:LinkButton></div>
        <div>
            <asp:Label ID="lblRecords" runat="server" Text=""></asp:Label></div>
        <asp:GridView ID="grdSearchResults" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="No records found." DataKeyNames="OnlineAdId"
            AllowPaging="true" PageSize="10">
            <Columns>
                <asp:TemplateField HeaderText="Del">
                    <HeaderTemplate>
                        <input id="chkAll" onclick="javascript:SelectAllCheckboxes(this);" runat="server" type="checkbox" title="Check all checkboxes" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRows" runat="server" ToolTip="Select" />
                        <asp:HiddenField ID="hdnOnlineId" runat="server" Value='<%# Eval("OnlineAdId") %>' />
                    </ItemTemplate>
                    <ItemStyle Width="25px" HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="AdDesignId" HeaderText="iFlog ID" />
                <asp:BoundField DataField="BookingReference" HeaderText="Book Reference" />
                <asp:TemplateField HeaderText="Heading Preview">
                    <ItemTemplate>
                        <asp:Label ID="lblHeading" runat="server" Text='<%# Eval("Heading") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description Preview">
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserId" HeaderText="UserID" />
                <asp:BoundField DataField="MainCategory" HeaderText="Category" />
                <asp:BoundField DataField="NumOfViews" HeaderText="Views" />
                <asp:TemplateField HeaderText="Preview">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnkPreview" runat="server" Text="Ad Preview" rel="gb_page_center[800, 600]" 
                            NavigateUrl='<%# String.Format("~/classified/ModalDialog/Preview_OnlineAd.aspx?onlineAdId={0}&adDesignId={1}", Eval("OnlineAdId"), Eval("AdDesignId")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
        <br />
        
        <div class="membersToggle">
            <asp:LinkButton ID="btnApprove" runat="server" Text="Approve" ToolTip="This changes ad status to be approved and removes from this list." />
            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" ToolTip="This changes ad status to be cancelled and removes from this list."
                            OnClientClick="return confirm('Are you sure you want to cancel? The user will be notified through email.');" />
        </div>
    </div>
    
</asp:Content>
