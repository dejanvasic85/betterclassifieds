<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="DefaultOld.aspx.vb" Inherits="BetterclassifiedAdmin._Default1" %>
<asp:content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
       //elm[i].click();

       if(elm[i].checked!=xState)
         elm[i].click();
       //elm[i].checked=xState;

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
</asp:content>
<asp:content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    Web Content
</asp:content>

<asp:content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    
             <%-- dropdown menu for search type --%>
        <asp:dropdownlist ID="ddlUserSearchTypes" runat="server" 
                 ToolTip="Click to select which database column you want to search">
            <asp:ListItem Selected="true" Text="Content Key" />
         </asp:dropdownlist>
        
        <%-- divider --%>
        &nbsp;Starts with&nbsp;
        
        <%-- search box --%>
        <asp:textbox ID="txtSearchText" runat="server" MaxLength="150" 
                 ToolTip="Type your search term" width="250" />
        
        <%-- search button --%>
        <asp:button ID="btnSearch" runat="server" Text="Search" 
                 ToolTip="Click to start search." />
        
        &nbsp;
        <asp:requiredfieldvalidator ID="RequiredFieldValidator2" runat="server" 
                 ControlToValidate="txtSearchText" EnableViewState="False" 
                 ErrorMessage="Type search term first..." SetFocusOnError="True"></asp:requiredfieldvalidator>
        
        <br />
        <br />
        
        <%-- gridview to display Content --%>      
        <asp:gridview ID="ContentGridView" runat="server" AutoGenerateColumns="False"  EmptyDataText="No records found."  datasourceid="ObjectDataSource1"  
                 datakeynames="EntityGroup,CultureCode,EntityKey,Id">
          <Columns>
           <asp:TemplateField>
                    <HeaderStyle CssClass="gridheaderBG" Width="1px" />
                    <ItemStyle CssClass="gridheaderBG" Width="1px" />
                </asp:TemplateField>
            <asp:templatefield headertext="Entity Group">
             <ItemStyle HorizontalAlign="Center" />
                <itemtemplate >
                    <a href='Edit_webContent.aspx?id=<%# Eval("Id")%>' rel="gb_page_fs[]" title="Edit Web Content"><%# Eval("EntityGroup") %></a>
                </itemtemplate>
            </asp:templatefield>
            
              <asp:boundfield datafield="CultureCode" headertext="CultureCode" 
                  readonly="True" sortexpression="CultureCode" 
                  itemstyle-horizontalalign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                
               </asp:boundfield>
              <asp:boundfield datafield="EntityKey" headertext="Entity Key" readonly="True" 
                  sortexpression="EntityKey" itemstyle-horizontalalign="Center">
              </asp:boundfield>
              
              <asp:boundfield datafield="EntityType" headertext="Entity Type" 
                  sortexpression="EntityType" itemstyle-horizontalalign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
              </asp:boundfield>
            <%--  <asp:templatefield   headertext="Entity Value">
                <itemtemplate >
                    <div style="clear:both">
                        <asp:literal id="Literal1" runat="server" text='<%#Eval("EntityValue") %>'/>
                    </div>
                </itemtemplate>
                  <ItemStyle HorizontalAlign="Center" cssclass=""></ItemStyle>
              </asp:templatefield>--%>
            
              <asp:templatefield >
                    <ItemStyle HorizontalAlign="Center" />
                    <itemtemplate >
                        <asp:imagebutton id="Linkbutton1" runat="server" commandname="Delete"   commandargument='<%#Eval("Id") %>' ImageUrl="~/App_Themes/blue/images/delete.gif" />
                    </itemtemplate>
              </asp:templatefield>
             
                  <asp:TemplateField>
                    <HeaderStyle CssClass="gridheaderBG" Width="1px" />
                    <ItemStyle CssClass="gridheaderBG" Width="1px" />
                </asp:TemplateField>
        </Columns>
        
    </asp:gridview>
     <%-- delete checked users --%>
        <div class="membersToggle">
            <a href='InsertText.aspx' rel="gb_page_center[525, 600]" title="Insert New Content">Insert New Text</a>
            <a href='InsertHtml.aspx' rel="gb_page_center[525, 600]" title="Insert New Html Content">Insert New Html</a>
      
        </div>
    <asp:objectdatasource id="ObjectDataSource1" runat="server" selectmethod="SearchContent" 
                 typename="WebContentProvider.DAL">
        <selectparameters>
            <asp:controlparameter controlid="txtSearchText" name="key" propertyname="Text" 
                type="String" />
        </selectparameters>
             </asp:objectdatasource>
  
    <div style="clear:both"/>
            
</asp:content>
