<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/Application.Master" CodeBehind="Bookings.aspx.vb" Inherits="BetterclassifiedAdmin.Bookings"  theme="Default" stylesheettheme="Default"%>
<%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:content>
<asp:content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" 
    runat="server">
    <asp:objectdatasource id="BookingSource" runat="server" 
        selectmethod="SearchBooking" typename="BetterclassifiedAdmin.Controller" 
        oldvaluesparameterformatstring="original_{0}">
            <selectparameters >
                <asp:controlparameter controlid="txtSearchKey" defaultvalue="0" name="key" 
                    propertyname="Text" type="String" />
                <asp:controlparameter controlid="rbSerachType" defaultvalue="" 
                    name="searchType" propertyname="SelectedValue" type="Int32" />
            </selectparameters>
        </asp:objectdatasource>
       
       
              <%--      <asp:detailsview ID="dvCustomerDetail" runat="server"  
                        CssClass="detailgrid" GridLines="None" DefaultMode="Edit" AutoGenerateRows="false" 
                        Visible="false" Width="100%">
                        <Fields>
                            <asp:BoundField HeaderText="ID" DataField="Username" ReadOnly="true" />
                            <asp:TemplateField HeaderText="Company">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCompany" runat="server" 
                                        Text='<%# Bind("BusinessName") %>' />
                                    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" 
                                        ControlToValidate="txtCompany" ErrorMessage="Required" Display="Static" 
                                        SetFocusOnError="true" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtContact" runat="server" Text='<%# Bind("FirstName") %>' />
                                    <asp:RequiredFieldValidator ID="rfvContactName" runat="server" 
                                        ControlToValidate="txtContact" ErrorMessage="Required" Display="Static" 
                                        SetFocusOnError="true" />
                                </EditItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Title">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("LastName") %>' />
                                    <asp:RequiredFieldValidator ID="rfvTitle" runat="server" 
                                        ControlToValidate="txtTitle" ErrorMessage="Required" Display="Static" 
                                        SetFocusOnError="true" />
                                </EditItemTemplate>
                            </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Address">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAddress" runat="server" Text='<%# Bind("Address1") %>' />
                                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server" 
                                        ControlToValidate="txtAddress" ErrorMessage="Required" Display="Static" 
                                        SetFocusOnError="true" />
                                </EditItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="City">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtCity" runat="server" Text='<%# Bind("City") %>' />
                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" 
                                        ControlToValidate="txtCity" ErrorMessage="Required" Display="Static" 
                                        SetFocusOnError="true" />
                                </EditItemTemplate>
                            </asp:TemplateField>                                                                                                                
                        </Fields>
                    </asp:detailsview>--%>
       
       
    <table width="100%">
    <tr>
        <td>
               <div style=" margin:4px 10px 10px 10px; width:auto" runat="server" id="imme">
        <cc1:roundedcornersextender id="RoundedCornersExtender1" runat="server" 
            targetcontrolid="imme" bordercolor="ActiveBorder">
        </cc1:roundedcornersextender>
            <div style="margin:0 0 0 7px">
                <asp:label id="Label1" runat="server" >Search by</asp:label>
                 <span style=" margin-left:100px;"/>
                    <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" 
                        errormessage="RequiredFieldValidator" controltovalidate="txtSearchKey" 
                        setfocusonerror="True" validationgroup="searchValidate">You must enter a search criteria</asp:requiredfieldvalidator>
                
            </div>
           <div style="float:left; margin:5px 10px 5px 5px">
               
              
            <asp:radiobuttonlist id="rbSerachType" runat="server" repeatdirection="Horizontal" repeatlayout="Flow">
               <asp:listitem value="1">Ref Number</asp:listitem>
               <asp:listitem value="5" selected="True">Username</asp:listitem>
           </asp:radiobuttonlist>
           </div>
           <div style="margin:5px 10px 5px 5px">
                <asp:textbox id="txtSearchKey" runat="server" width="300px"></asp:textbox>
                <asp:button id="btnSearch" runat="server" text="Search" 
                    validationgroup="searchValidate" />
           </div>
           <div style="clear:both"></div>
        
    </div>
   </td>
    </tr>
    
    <tr>
            <td>
                   <div>
                   
                    <asp:updatepanel ID="updatePanel" runat="server" UpdateMode="Conditional">
            <ContentTemplate>            
               <%-- <asp:GridView ID="gvBooking" runat="server" DataSourceID="BookingSource"
                    CssClass="datagrid" GridLines="None" AutoGenerateColumns="False">
                    			
                    <columns>
                        <asp:boundfield datafield="AdBookingId" headertext="AdBookingId" 
                            sortexpression="AdBookingId" />
                        <asp:boundfield datafield="StartDate" headertext="StartDate" 
                            sortexpression="StartDate" />
                        <asp:boundfield datafield="EndDate" headertext="EndDate" 
                            sortexpression="EndDate" />
                        <asp:boundfield datafield="TotalPrice" headertext="TotalPrice" 
                            sortexpression="TotalPrice" />
                        <asp:boundfield datafield="BookReference" headertext="BookReference" 
                            sortexpression="BookReference" />
                        <asp:boundfield datafield="AdId" headertext="AdId" sortexpression="AdId" />
                        <asp:boundfield datafield="UserId" headertext="UserId" 
                            sortexpression="UserId" />
                        <asp:boundfield datafield="BookingStatus" headertext="BookingStatus" 
                            sortexpression="BookingStatus" />
                        <asp:boundfield datafield="MainCategoryId" headertext="MainCategoryId" 
                            sortexpression="MainCategoryId" />
                    </columns>
                    			
                </asp:GridView>--%>
                <telerik:radgrid id="RadGrid1" runat="server" autogeneratecolumns="False" 
                    datasourceid="BookingSource" gridlines="None">
                    <headercontextmenu enabletheming="True">
                        <collapseanimation duration="200" type="OutQuint" />
                    </headercontextmenu>
                    <mastertableview datasourceid="BookingSource">
                        <rowindicatorcolumn>
                            <headerstyle width="20px" />
                        </rowindicatorcolumn>
                        <expandcollapsecolumn>
                            <headerstyle width="20px" />
                        </expandcollapsecolumn>
                        <columns>
                            <telerik:gridboundcolumn datafield="AdBookingId" datatype="System.Int32" 
                                headertext="AdBookingId" sortexpression="AdBookingId" uniquename="AdBookingId">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="StartDate" datatype="System.DateTime" 
                                headertext="StartDate" sortexpression="StartDate" uniquename="StartDate">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="EndDate" datatype="System.DateTime" 
                                headertext="EndDate" sortexpression="EndDate" uniquename="EndDate">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="TotalPrice" datatype="System.Decimal" 
                                headertext="TotalPrice" sortexpression="TotalPrice" uniquename="TotalPrice">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="BookReference" headertext="BookReference" 
                                sortexpression="BookReference" uniquename="BookReference">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="AdId" datatype="System.Int32" 
                                headertext="AdId" sortexpression="AdId" uniquename="AdId">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="UserId" headertext="UserId" 
                                sortexpression="UserId" uniquename="UserId">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="BookingStatus" headertext="BookingStatus" 
                                sortexpression="BookingStatus" uniquename="BookingStatus">
                            </telerik:gridboundcolumn>
                            <telerik:gridboundcolumn datafield="MainCategoryId" datatype="System.Int32" 
                                headertext="MainCategoryId" sortexpression="MainCategoryId" 
                                uniquename="MainCategoryId">
                            </telerik:gridboundcolumn>
                        </columns>
                    </mastertableview>
                    <filtermenu enabletheming="True">
                        <collapseanimation duration="200" type="OutQuint" />
                    </filtermenu>
                </telerik:radgrid>
            </ContentTemplate>
        </asp:updatepanel>     
       <%--
           <asp:panel ID="pnlPopup" runat="server" CssClass="detail" Width="500px" 
                            style="display:none;">
            <asp:UpdatePanel ID="updPnlCustomerDetail" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button id="btnShowPopup" runat="server" style="display:none" />
            		<cc1:ModalPopupExtender ID="mdlPopup" runat="server" 
            		    TargetControlID="btnShowPopup" PopupControlID="pnlPopup" 
            		    CancelControlID="btnClose" BackgroundCssClass="modalBackground" 
            		/>
                    <div class="footer">
                        <asp:LinkButton ID="btnSave" runat="server" Text="Save" CausesValidation="true"/>
                        <asp:LinkButton ID="btnClose" runat="server" 
                            Text="Close" CausesValidation="false" 
                        />
                    </div>                    
                </ContentTemplate>                
            </asp:UpdatePanel>             
        </asp:panel>  --%>             
    </div>
            </td>
    </tr>
</table>
   
 
    
</asp:content>
