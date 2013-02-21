<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Assign_SpecialRates.aspx.vb"
    Inherits="BetterclassifiedAdmin.AssignSpecialRatesToCategories" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript" language="javascript">
    function clientNodeChecked(sender, eventArgs)
    {
       var node = eventArgs.get_node();        
       if (!node.get_checked())
       {       
          while (node.get_parent().set_checked != null)
          {
              node.get_parent().set_checked(false);
              node = node.get_parent();
          }
       }
  }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 10px;">
        <asp:ScriptManager ID="scriptManager" runat="server" />
        
        <telerik:RadAjaxManager ID="RadScriptManager" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="CategoryTree">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="CategoryTree" LoadingPanelID="radAjaxLoading" />
                    </UpdatedControls>
                </telerik:AjaxSetting>     
            </AjaxSettings>
        </telerik:RadAjaxManager>
        
        <telerik:RadAjaxLoadingPanel runat="server" ID="radAjaxLoading" BackgroundPosition="Center" Skin="WebBlue" />

        <asp:Label ID="lblMessage" runat="server" />
        <div style="clear: both;" />
        <div style="margin-bottom: 10px;">
            <asp:CheckBox ID="chkRemoveCurrentSpecials" runat="server" Font-Bold="true" Checked="true"
                Text="Remove all currently assigned special rates to selected Publications and Categories." />
        </div>
        <div class="detailsviewheaderBG">Publications</div>
        <div style="width:100%;height: 150px; overflow: auto;">
            <asp:CheckBoxList ID="chkList" runat="server" DataTextField="Title" DataValueField="PublicationId" DataSourceID="lnqSourcePublications" />
        </div>
        <asp:LinqDataSource ID="lnqSourcePublications" runat="server" ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext"
            Select="new (Title, PublicationId)" TableName="Publications">
        </asp:LinqDataSource>
    
        <div class="detailsviewheaderBG">Categories</div>
        <div style="height: 320px; overflow:auto;">
            <telerik:RadTreeView ID="CategoryTree" runat="server" DataSourceID="lnqSourceCategories"
                CheckBoxes="true" AllowNodeEditing="false" 
                DataFieldID="MainCategoryId" DataFieldParentID="ParentId" DataValueField="MainCategoryId"
                DataTextField="Title" OnClientNodeChecked="clientNodeChecked" />
        </div>
        <asp:LinqDataSource ID="lnqSourceCategories" runat="server" ContextTypeName="BetterclassifiedsCore.DataModel.BetterclassifiedsDataContext"
            Select="new (Title, MainCategoryId, ParentId)" TableName="MainCategories">
        </asp:LinqDataSource>
        
        <div class="controlDiv">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
        </div>
    </div>
    </form>
</body>
</html>
