<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Email_Broadcast.aspx.vb" Inherits="BetterclassifiedAdmin.CRM.Email_Broadcast" %>
<%@ Register src="~/UserControls/UserNavLinks.ascx" tagname="adminUserNavLinks" tagprefix="uc1" %>

<%-- content placeholder for head section --%>
<asp:content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <%-- greybox modal popup javascript sources --%>
<script language="javascript" type="text/javascript">
    var GB_ROOT_DIR = "../greybox/";
</script>
<script type="text/javascript" src="../greybox/AJS.js"></script>
<script type="text/javascript" src="../greybox/AJS_fx.js"></script>
<script type="text/javascript" src="../greybox/gb_scripts.js"></script>
<link href="../greybox/gb_styles.css" rel="stylesheet" type="text/css" />

</asp:content>

<%-- content placeholder for body title --%>
<asp:content ID="Content5" runat="server" contentplaceholderid="cphBodyTitle">
    Quick Email
</asp:content>

<%-- content placeholder for user navigation --%>
<asp:content ID="Content6" runat="server"     contentplaceholderid="cphUserNavigation">
    <%-- user navigation include --%>
    <div class="userCategories"><a href="#">Broadcast Email</a></div>
    <div class="userCategories"><a href="email_options.aspx" title="edit and modify email templates">Modify Email Templates</a></div>
</asp:content>

<%-- content placeholder for content body --%>
<asp:content ID="Content7" runat="server" contentplaceholderid="cphContentBody">

    <%-- a-z email recipients --%>
    <div align="center" class="aTozNavigaion">
  
    <asp:LinkButton ToolTip="Show all users" runat="server" PostBackUrl="email_broadcast.aspx?letter=" ID="showAll" Text="All"></asp:LinkButton>
    </div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=A" title="Email starts with A">A</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=B" title="Email starts with B">B</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=C" title="Email starts with C">C</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=D" title="Email starts with D">D</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=E" title="Email starts with E">E</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=F" title="Email starts with F">F</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=G" title="Email starts with G">G</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=H" title="Email starts with H">H</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=I" title="Email starts with I">I</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=J" title="Email starts with J">J</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=K" title="Email starts with K">K</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=L" title="Email starts with L">L</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=M" title="Email starts with M">M</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=N" title="Email starts with N">N</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=O" title="Email starts with O">O</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=P" title="Email starts with P">P</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=Q" title="Email starts with Q">Q</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=R" title="Email starts with R">R</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=S" title="Email starts with S">S</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=T" title="Email starts with T">T</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=U" title="Email starts with U">U</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=V" title="Email starts with V">V</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=W" title="Email starts with W">W</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=X" title="Email starts with X">X</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=Y" title="Email starts with Y">Y</a></div>
    <div align="center" class="aTozNavigaion"><a href="email_broadcast.aspx?letter=Z" title="Email starts with Z">Z</a></div>
    <div align="center" ><a href="email_broadcast.aspx?letter=sb" title="Subscribed User">Subscribed User</a></div>
    <asp:label ID="lbl_SendResults" runat="server" EnableViewState="False" 
        Style="color: #FF0000"></asp:label>
    <br />
    <br />
    
    <%-- email header  --%>
    <div class="EmailHeader">
        <asp:button ID="btnSendEmail" runat="server" Font-Size="10px" Text="Send Email" 
            OnClick="btnSendEmail_Click" 
            ToolTip="Click to send email. Remember to select at least one recipient checkbox!" />
        <asp:button ID="btnSelectAll" runat="server" Font-Size="10px" Text="Check All" 
            CausesValidation="False" onclick="btnSelectAll_Click" 
            ToolTip="Click to select all checkboxes visible on page." />
        <asp:button ID="btnUnselectAll" runat="server" Font-Size="10px" 
            Text="Uncheck All" CausesValidation="False" onclick="btnUnselectAll_Click" 
            ToolTip="Click to uncheck all checkboxes." />
        <asp:radiobuttonlist ID="rbt_BodyTextType" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow" 
            Style="font-size: xx-small; color: #000000;" 
            ToolTip="HTML or Palin Text Email?">
            <asp:ListItem Selected="True" Value="True">html</asp:ListItem>
            <asp:ListItem Value="False">plain text</asp:ListItem>
        </asp:radiobuttonlist>
        &nbsp;|
        <asp:radiobuttonlist ID="rbt_Importance" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow" 
            Style="font-size: xx-small; color: #000000;" ToolTip="Select Email Priority">
            <asp:ListItem Value="High">high</asp:ListItem>
            <asp:ListItem Selected="True" Value="Normal">default</asp:ListItem>
            <asp:ListItem Value="Low">low</asp:ListItem>
        </asp:radiobuttonlist>
        <asp:CheckBox ID="selectAllUsers" runat="server" ToolTip="Include Unsubscribed Users" Text="include all users" />

    </div>
    
    <%-- gridview to display users by email  --%>
    <asp:gridview ID="UsersGridView" runat="server" AllowPaging="false" 
        AllowSorting="True" AutoGenerateColumns="False" 
        DataKeyNames="LoweredUserName" PageSize="25" 
        EmptyDataText="No records found for the indicated search. Try another letter or create users first.">
        <PagerSettings Mode="NumericFirstLast" FirstPageText="&amp;lt;&amp;lt; First" 
            LastPageText="&amp;gt;&amp;gt; Last" NextPageText="&amp;gt; Next" 
            PreviousPageText="&amp;lt; Previous" />
        <Columns>
            <asp:TemplateField>
                <HeaderStyle CssClass="gridheaderBG" Width="1px" />
                <ItemStyle CssClass="gridheaderBG" Width="1px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="@">
                <ItemTemplate>
                    <asp:CheckBox ID="chkRows" runat="server" ToolTip="Select as recipient" />
                </ItemTemplate>
                <ItemStyle Width="25px" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User Name" SortExpression="LoweredUserName">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%#Bind ("LoweredUserName")%>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <a href='Edit_CustomerModal.aspx?username=<%#Eval ("LoweredUserName")%>' rel="gb_page_center[525, 600]" title="Edit User Details"><%#Eval ("LoweredUserName")%></a>
                </ItemTemplate>
                <HeaderStyle CssClass="gridColumnHeaderBG" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="User Email" SortExpression="LoweredEmail">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%#Bind ("LoweredEmail")%>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <a href='Mailto:<%#Eval ("LoweredEmail")%>' title="click to email from your computer"><%#Eval ("LoweredEmail")%></a>
                </ItemTemplate>
                <HeaderStyle CssClass="gridColumnHeaderBG" />
            </asp:TemplateField>
            <asp:CheckBoxField DataField="IsApproved" HeaderText="Approved?" SortExpression="IsApproved">
                <HeaderStyle CssClass="gridColumnHeaderBG" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Locked Out?" SortExpression="IsLockedOut">
                <HeaderStyle CssClass="gridColumnHeaderBG" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:CheckBoxField>
            <asp:BoundField DataField="CreateDate" HeaderText="Creation Date" SortExpression="CreateDate" >
                <HeaderStyle CssClass="gridColumnHeaderBG" />
            </asp:BoundField>
        </Columns>
    </asp:gridview> 
    
    <%-- object datasource  --%>
    
    
    <%-- email header 2  --%> 
    <div class="EmailHeader2">
    <table cellpadding="3" cellspacing="0" style="width: 100%;">
        <tr>
            <td style="font-size: xx-small; color: #000000; width: 35px;">
                Subject:
            </td>
            <td>
                <asp:textbox ID="txb_Subject" runat="Server" Width="300px" 
                    ToolTip="Type a subject for this email. Subject cannot be left empty.">hello world</asp:textbox>
                <asp:requiredfieldvalidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txb_Subject" ErrorMessage="Subject: cannot be empty.">*</asp:requiredfieldvalidator>
            </td>
          
        </tr>
    </table>
    </div>
    
    <%-- Rich Text Editor embeded in FormView --%>
    <asp:validationsummary ID="ValidationSummary1" runat="server" />
     
    <%-- telerik editor  --%>  
    <telerik:RadEditor ID="radEditor1" runat="server" > 
    
    </telerik:RadEditor> 


</asp:content>