<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InsertText.aspx.vb" Inherits="BetterclassifiedAdmin.InsertText" %>
<%@ Import Namespace="System.ComponentModel"%>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:detailsview id="DetailsView1" runat="server" height="50px" 
            autogenerateinsertbutton="True" autogenerateeditbutton="True" DefaultMode="Edit"
            datasourceid="SqlDataSource1" autogeneraterows="False" datakeynames="Id" 
            width="100%">
            <fields>
                <asp:boundfield datafield="EntityGroup" headertext="EntityGroup" 
                    sortexpression="EntityGroup" />
                <asp:boundfield datafield="CultureCode" headertext="CultureCode" 
                    sortexpression="CultureCode" />
                <asp:boundfield datafield="EntityKey" headertext="Entity Key" 
                    sortexpression="EntityKey" />
                 <asp:templatefield headertext="Entity Type" >
                    <insertitemtemplate  >
                           <asp:textbox id="TextBox1" runat="server" readonly="true" text='<%# Bind("EntityType")%>'>TEXT</asp:textbox> 
                    </insertitemtemplate>
                  </asp:templatefield>
               <asp:templatefield headertext="EntityValue" >
               <headerstyle  cssclass=""/>
                <insertitemtemplate >
                       <asp:textbox id="TextBox2" runat="server" text='<%# Bind("EntityValue")%>' />
                </insertitemtemplate>
               </asp:templatefield>
            </fields>
        </asp:detailsview>
    </div>

    <br />
    <asp:sqldatasource id="SqlDataSource1" runat="server" 
        conflictdetection="CompareAllValues" 
        connectionstring="<%$ ConnectionStrings:WebContentProviderConnectionString %>" 
        deletecommand="DELETE FROM [WebContent] WHERE [Id] = @original_Id AND [EntityGroup] = @original_EntityGroup AND [CultureCode] = @original_CultureCode AND [EntityKey] = @original_EntityKey AND [EntityType] = @original_EntityType AND [EntityValue] = @original_EntityValue" 
        insertcommand="INSERT INTO [WebContent] ([EntityGroup], [CultureCode], [EntityKey], [EntityType], [EntityValue]) VALUES (@EntityGroup, @CultureCode, @EntityKey, @EntityType, @EntityValue)" 
        oldvaluesparameterformatstring="original_{0}" 
        selectcommand="SELECT Id, EntityGroup, CultureCode, EntityKey, EntityType, EntityValue FROM dbo.WebContent WHERE (Id = @id)" 
        
        updatecommand="UPDATE [WebContent] SET [EntityGroup] = @EntityGroup, [CultureCode] = @CultureCode, [EntityKey] = @EntityKey, [EntityType] = @EntityType, [EntityValue] = @EntityValue WHERE [Id] = @original_Id AND [EntityGroup] = @original_EntityGroup AND [CultureCode] = @original_CultureCode AND [EntityKey] = @original_EntityKey AND [EntityType] = @original_EntityType AND [EntityValue] = @original_EntityValue">
        <selectparameters>
            <asp:querystringparameter defaultvalue="-1" name="id" querystringfield="id" />
        </selectparameters>
        <deleteparameters>
            <asp:parameter name="original_Id" type="Int32" />
            <asp:parameter name="original_EntityGroup" type="String" />
            <asp:parameter name="original_CultureCode" type="String" />
            <asp:parameter name="original_EntityKey" type="String" />
            <asp:parameter name="original_EntityType" type="String" />
            <asp:parameter name="original_EntityValue" type="String" />
        </deleteparameters>
        <updateparameters>
            <asp:parameter name="EntityGroup" type="String" />
            <asp:parameter name="CultureCode" type="String" />
            <asp:parameter name="EntityKey" type="String" />
            <asp:parameter name="EntityType" type="String" />
            <asp:parameter name="EntityValue" type="String" />
            <asp:parameter name="original_Id" type="Int32" />
            <asp:parameter name="original_EntityGroup" type="String" />
            <asp:parameter name="original_CultureCode" type="String" />
            <asp:parameter name="original_EntityKey" type="String" />
            <asp:parameter name="original_EntityType" type="String" />
            <asp:parameter name="original_EntityValue" type="String" />
        </updateparameters>
        <insertparameters>
            <asp:parameter name="EntityGroup" type="String" />
            <asp:parameter name="CultureCode" type="String" />
            <asp:parameter name="EntityKey" type="String" />
            <asp:parameter name="EntityType" type="String" />
            <asp:parameter name="EntityValue" type="String" />
        </insertparameters>
    </asp:sqldatasource>
    </form>
</body>
</html>
