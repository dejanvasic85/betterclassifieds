<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="AssignRateCardControl.ascx.vb" Inherits="BetterclassifiedAdmin.AssignRateCardControl" %>

<%--<telerik:RadAjaxManager ID="RadScriptManager" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="trCategories">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="trCategories" LoadingPanelID="radAjaxLoading" />
            </UpdatedControls>
        </telerik:AjaxSetting>     
    </AjaxSettings>
</telerik:RadAjaxManager>
        
<telerik:RadAjaxLoadingPanel runat="server" ID="radAjaxLoading" BackgroundPosition="Center" Skin="WebBlue" />--%>

<div class="detailsviewheaderBG">Publications</div>
<div style="width:100%;">
    <asp:CheckBoxList ID="chkListPublications" runat="server" DataTextField="Title" DataValueField="PublicationId" />
</div>

<div class="detailsviewheaderBG">Categories</div>
<div style="height: 320px; overflow:auto;">
    <telerik:RadTreeView ID="trCategories" runat="server"
        CheckBoxes="true" AllowNodeEditing="false" 
        DataFieldID="MainCategoryId" DataFieldParentID="ParentId" DataValueField="MainCategoryId"
        DataTextField="Title" />
</div>