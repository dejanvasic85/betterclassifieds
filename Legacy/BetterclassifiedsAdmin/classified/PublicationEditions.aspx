<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="PublicationEditions.aspx.vb" Inherits="BetterclassifiedAdmin.PublicationEditions" %>

<%@ Register Src="~/classified/UserControls/PubNavigation.ascx" TagName="Navigation" TagPrefix="ucx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
     Publication Management
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <ucx:Navigation ID="ucxNavigationManagement" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Manage Editions" Font-Bold="true" />
    
    <p>Welcome to Edition management tool. Every publication needs to have upcoming editions generated in order for users to place a booking.</p>
    <p>Please note that if an edition date already exists, it will not be overriden with a new one. The edition must be deleted first.</p>
    <p class="spanWarning">
        
        <asp:Label ID="Label5" runat="server" Text="Important:" Font-Bold="true"></asp:Label>
        <asp:Label ID="Label4" runat="server" Text="Please ensure that the edition dates are correct. If users place a booking for an edition, the entry will be locked in." /></p>
        
    <div style="margin-bottom: 20px;">
        <table cellpadding="4">
            <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Select Publication" />: </td>
                <td><asp:DropDownList ID="ddlPublications" runat="server" DataValueField="PublicationId" DataTextField="Title" Width="300px" AutoPostBack="true" /></td>
            </tr>
        </table>
        <br />
        
        <ajax:TabContainer ID="tabContainer" runat="server" ActiveTabIndex="0" 
            Width="100%" Font-Size="10px">
        
            <%-- FIRST TAB (View existing editions) --%>
            <ajax:TabPanel ID="tabViewExisting" runat="server" HeaderText="View Upcoming Editions">
                <ContentTemplate>
                    <div style="font-size: 11px;">
                        <table cellpadding="5">
                            <tr>
                                <td><asp:Label ID="Label6" runat="server" Text="View Range" />:</td>
                                <td><asp:DropDownList ID="ddlDays" runat="server">
                                        <asp:ListItem Text="One Month" Value="30" />
                                        <asp:ListItem Text="Three Months" Value="90" />
                                        <asp:ListItem Text="Six Months" Value="180" />
                                        <asp:ListItem Text="One Year" Value="365" /> 
                                    </asp:DropDownList></td>
                                <td><asp:Button ID="btnView" runat="server" Text="View Editions" /></td>
                            </tr>
                        </table>
                        
                        <div style="width: 400px;" id="divEditions" runat="server" visible="False">
                            <asp:UpdatePanel ID="pnlUpdateEditions" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdEditions" runat="server" AutoGenerateColumns="False" DataKeyNames="EditionId" EmptyDataText="No records found.">
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
                                                <ItemStyle Width="10px" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EditionDate" HeaderText="Edition Date" DataFormatString="{0:ddd dd-MMM-yy}" />
                                            <asp:BoundField DataField="Deadline" HeaderText="Deadline" DataFormatString="{0:ddd dd-MMM-yy hh:mm}" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDeleteEdition" runat="server" CommandName="Delete" ImageUrl="~/App_Themes/blue/images/delete.gif" 
                                                        OnClientClick="javascript:return confirm('Are you sure you want to delete edition?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                            
                                    
                                    <%-- delete checked editions --%>
                                    <div class="membersToggle">
                                        
                                        <asp:LinkButton ID="btnDeleteSelected" runat="server" Text="Delete Selected" OnClientClick="return confirm('Are you sure you want to delete selected editions? Users will not be able to view these options when placing an ad.');" />
                                            
                                    </div>

                                    <br /><br />
                                    <br />
                                     <%-- delete success label  --%>
                                    <asp:Label ID="lblDeleteSuccess" runat="server" />

                                </ContentTemplate>            
                            </asp:UpdatePanel>
                        </div>
                        
                    </div>
                </ContentTemplate>
            </ajax:TabPanel>
        
            <%-- SECOND TAB (Auto Generation tool) --%>
            <ajax:TabPanel ID="tabGenerate" runat="server" HeaderText="Auto Generate">
                <ContentTemplate>
                <div style="font-size: 11px;">
                    <asp:UpdatePanel ID="pnlUpdateGenerateEditions" runat="server">
                        <ContentTemplate>
                            <p>Please ensure that publication frequency is set up correctly before 
                                proceeding with the auto generation method.</p>
                            <br />
                            
                            <table cellpadding="5">
                                <tr>
                                    <th colspan="2" style="background-color: #CCCCFF;">Length and Frequency Details</th>
                                </tr>
                                <tr>
                                    <td align="right">Frequency:</td>
                                    <td><asp:Label ID="lblFrequency" runat="server" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td align="right">Edition Day:</td>
                                    <td><asp:Label ID="lblEditionDay" runat="server" Font-Bold="true" /></td>
                                </tr>
                                <tr>
                                    <td valign="top" align="right">Start Date:</td>
                                    <td><asp:Calendar ID="calStartDate" runat="server" BackColor="White" 
                                            BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" 
                                            DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" 
                                            ForeColor="#003399" Height="200px" Width="220px">
                                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                        <WeekendDayStyle BackColor="#CCCCFF" />
                                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" 
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        </asp:Calendar>
                                        <asp:CustomValidator ID="validateStartDate" runat="server"
                                         ErrorMessage="Please provide a start date." OnServerValidate="calStartDate_ServerValidate"
                                         Display="Dynamic" /></td>
                                </tr>
                                <tr>
                                    <td align="right">Number of Editions:</td>
                                    <td><asp:TextBox ID="txtNumberOfEditions" runat="server" Width="50"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <th colspan="2" style="background-color: #CCCCFF;">Deadline Info</th>
                                </tr>
                                <tr>
                                    <td align="right">Days before Editions:</td>
                                    <td><asp:DropDownList ID="ddlDaysBeforeEdition" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td align="right">Time of day (hours)</td>
                                    <td><asp:DropDownList ID="ddlDeadlineHours" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td align="right">Time of day (mins)</td>
                                    <td><asp:DropDownList ID="ddlDeadlineMins" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:Button ID="btnGenerate" runat="server" Text="Generate Editions" /></td>
                                </tr>
                            </table>
                            <br />
                            <asp:Label ID="lblGenerateSuccess" runat="server" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="pnlUpdateGenerateEditions">
                        <ProgressTemplate>
                            <table>
                                <tr><td align="center"><asp:Image ID="imgProcessing" runat="server" 
                                    ImageUrl="~/App_Themes/blue/Images/progress_circle.gif" 
                                    Height="20" Width="20" /></td>
                                <td><asp:Label ID="lblProgress" runat="server" Text="Processing..." /></td></tr>
                            </table>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                </div>
                </ContentTemplate>
            </ajax:TabPanel>
            
        </ajax:TabContainer>
                        
        
    </div>
</asp:Content>
