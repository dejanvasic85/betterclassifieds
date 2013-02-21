<%@ Page Title="Back up all databases" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Backup.aspx.vb" Inherits="BetterclassifiedAdmin.SystemAdmin.Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBodyTitle" runat="server">
    System Administration Utility
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphUserNavigation" runat="server">
    <div class="userCategories">
        <asp:HyperLink ID="lnkNav1" runat="server" Text="Data Backup" NavigateUrl="~/SystemAdmin/Backup.aspx" />
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphContentBody" runat="server">
    <asp:Label ID="lblHeader" runat="server" Text="Backup Database Files" Font-Bold="true" 
        Font-Size="14px" />
    <p>Note: Using this utility will back up all the databases on the default SQL Server machine.</p>
    <p>Please specify the output path on local server where you want to place all the database back-up files.
    Each backup will be put into a folder with the format of year/month/day.
    Try to use a path that will point to an ftp address so that these files can be moved to another location 
    for extra security.</p>
    
    <div style="padding: 10px; margin-bottom: 30px;">
        <div style="float: left;">
            <asp:UpdatePanel ID="pnlUpdateBackup" runat="server">
                <ContentTemplate>
                
                    <div style="padding-bottom: 5px;">
                        <asp:Label ID="lblMessage" runat="server" ForeColor="Green" />
                    </div>
                
                    <asp:TextBox ID="txtPath" runat="server" Width="400px"
                        Text="C:\Betterclassifieds\sqlbackup\" />
                    <asp:Button ID="btnBackup" runat="server" Text="Backup"  />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div style="float:left; margin-top: 20px;">
            <%--Progress Update--%>  
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <table>
                        <tr>
                            <td><img alt="" src="../App_Themes/blue/Images/progress_circle.gif" height="20" width="20" /></td>
                            <td><asp:Label ID="lblWait" runat="server" Text="Processing" /></td>
                        </tr>
                    </table>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
    </div>
    
</asp:Content>
