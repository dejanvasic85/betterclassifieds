<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MasterSideNavigation.ascx.vb" Inherits="BetterclassifiedAdmin.MasterSideNavigation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:sitemapdatasource id="SiteMapDataSource1" runat="server" sitemapprovider="XmlSiteMapProvider" showstartingnode="false" />

<%-- ajax accordion --%>
<cc1:accordion ID="MyAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
    ContentCssClass="accordionContent" FadeTransitions="true" FramesPerSecond="60" datasourceid="SiteMapDataSource1"
    TransitionDuration="50" >
    
    <contenttemplate>
        <asp:datalist id="DataList1" runat="server" datasource='<%# Container.DataItem.ChildNodes %>' width="100%">
     
            <itemtemplate>
                <div class="accordionLink">
                    <asp:hyperlink id="HyperLink1" runat="server" text='<%#Eval("title") %>' navigateurl='<%#Eval("url") %>'/>
                </div>
            </itemtemplate>
        </asp:datalist>
    </contenttemplate>
    <headertemplate> 
        <a href="" onclick="return false;">
        <%--<img src="~/App_Themes/blue/Images/nav-Expand.gif" alt="Customer Membership" border="0px" align="right" />--%>
        <asp:Image ID="imgExpand" runat="server" ImageUrl="~/App_Themes/blue/Images/nav-Expand.gif" align="right" border="0px" />
        <asp:literal id="Literal1" runat="server" text='<%#Eval("Title") %>'></asp:literal></a>
    </headertemplate>
    
</cc1:accordion>

