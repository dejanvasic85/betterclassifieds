<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Email_Options.aspx.vb" Inherits="BetterclassifiedAdmin.CRM.Email_Options" %>
<%@ Register src="~/UserControls/UserNavLinks.ascx" tagname="adminUserNavLinks" tagprefix="uc1" %>


<%-- content placeholder for head section --%>
<asp:content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:content>

<%-- content placeholder for body title --%>
<asp:content ID="Content5" runat="server" contentplaceholderid="cphBodyTitle">
    Manage Email Templates
</asp:content>

<%-- content placeholder for user navigation --%>
<asp:content ID="Content6" runat="server" 
    contentplaceholderid="cphUserNavigation">

    <%-- user navigation include --%>
    
</asp:content>

<%-- content placeholder for content body --%>
<asp:content ID="Content7" runat="server" contentplaceholderid="cphContentBody">

    <%-- ajax update panel start --%>
    <asp:updatepanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
        <%-- help description --%>
    <div class="adminHelp2">
        Please note that changes made on templates will not affect emails already on the queue
    </div>
    <%-- section header --%>
            
                <asp:Panel ID="successPanel" runat="server"  Visible="false" CssClass="adminHelp2">
                    <b>Your changes has been applied</b>
                </asp:Panel>
  
    
    <div class="formSectionTitle">Select Template</div>
    <div>
    
        <telerik:RadComboBox id="ddlEmailTemplate1" runat="server" width="250px" DataSourceID="emailTemplateSource1"  AutoPostBack="true" 
                     datatextfield="Description" 
                    datavaluefield="Name">
                </telerik:RadComboBox>
                <asp:objectdatasource ID="emailTemplateSource1" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetTemplatesForEntity" 
        TypeName="Paramount.Broadcast.Components.EmailBroadcastController">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="P000000005" Name="clientCode" Type="String" />
                    </SelectParameters>
                </asp:objectdatasource>
    </div>
    <br />
    
    <div class="formSectionTitle">Template Name</div>
    <div>
         <asp:TextBox id="templateName" runat="server" width="250px" ReadOnly="true"></asp:TextBox>
    </div>
    <br /> 
    
    <div class="formSectionTitle">Email Subject</div>
    <div>
         <asp:TextBox id="subjectBox" runat="server" width="250px"></asp:TextBox>
    </div>
    <br />
     
    <div class="formSectionTitle">Template Description</div>
    <div>
         <asp:TextBox id="templateDescription" runat="server"  TextMode="MultiLine" width="677px" Height="100px"></asp:TextBox>
    </div>
    <br /> 
   
     <div class="formSectionTitle">Email Content</div>
    <div>
         <Telerik:RadEditor id="emailContentBox1" runat="server"></Telerik:RadEditor>
    </div>
    <br /> 
    
    <div class="formSectionTitle">Sender</div>
    <div>
         <asp:TextBox id="senderBox" runat="server" width="250px"></asp:TextBox>
    </div>
    <br />      
    <div>
        <asp:Button ID="updateBtn" runat="server" Text="Update" Enabled="false"  />
    </div>
    <%-- ajax update panel end --%>
    
    <%-- Email Template--%>
      <%-- section header
    <div class="formSectionTitle">New Email Template</div>

    <br /> 
        <asp:detailsview id="DetailsView1" runat="server" width="100%"  defaultmode="Insert" HeaderText="Create New &quot;Email&quot; Template" 
            autogeneraterows="False" 
            datakeynames="EmailTemplateId">
             <EmptyDataTemplate>
            No return address found.
        </EmptyDataTemplate>

            <fields>
           
                     <asp:TemplateField HeaderText="&nbsp;Email Header:" SortExpression="EmailHeader">
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("EmailHeader") %>' MaxLength="200"  Width="98%"></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>
            
                    <asp:templatefield  headertext="Email Body" >
                        <insertitemtemplate>
                            <asp:textbox id="TextBox3" runat="server" textmode="MultiLine" text='<%#Bind("EmailBody") %>' width="98%" rows="6"></asp:textbox>
                        </insertitemtemplate>
                    </asp:templatefield>
               <asp:TemplateField HeaderText="&nbsp;Description:" SortExpression="Description">
                <InsertItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("Description") %>' MaxLength="200" Rows="3" TextMode="MultiLine" Width="98%"></asp:TextBox>
                </InsertItemTemplate>
            </asp:TemplateField>
                <asp:boundfield datafield="EmailTemplateId" headertext="EmailTemplateId" 
                    insertvisible="False" readonly="True" sortexpression="EmailTemplateId" visible="false" />
                     <asp:CommandField ButtonType="Button" ShowInsertButton="True" ValidationGroup="New Email">
                <ControlStyle Font-Size="10px" />
            </asp:CommandField>
            </fields>
        </asp:detailsview>
        <br />
        <asp:gridview id="GridView3" runat="server" 
            autogeneratecolumns="False" datakeynames="EmailTemplateId">
            <columns>
               <asp:TemplateField>
                <HeaderStyle CssClass="gridheaderBG" Width="1px" />
                <ItemStyle CssClass="gridheaderBG" Width="1px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Details" ShowHeader="False">
                <EditItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="True" CommandName="Update" Text="Update" Width="45px" />
                    <asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" Width="45px" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="Button1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" Width="45px" />
                    <asp:Button ID="Button2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" Width="45px" OnClientClick="return confirm('Do you want to delete the selected item? This action connot be reversed.');" />
                </ItemTemplate>
                <ControlStyle Font-Size="10px" />
                <HeaderStyle Width="100px" />
                <ItemStyle Width="100px" />
            </asp:TemplateField>
                
                <asp:boundfield datafield="EmailHeader" headertext="Email  Header" 
                    sortexpression="EmailHeader" />
                <asp:boundfield datafield="EmailBody" headertext="EmailBody" 
                    sortexpression="EmailBody" />
                <asp:boundfield datafield="Description" headertext="Description" 
                    sortexpression="Description" />
                <asp:boundfield datafield="EmailTemplateId" headertext="EmailTemplateId" 
                    insertvisible="False" readonly="True" sortexpression="EmailTemplateId"  visible="false"/>
            </columns>
        </asp:gridview>
 --%>
    </ContentTemplate>
    </asp:updatepanel>

</asp:content>

