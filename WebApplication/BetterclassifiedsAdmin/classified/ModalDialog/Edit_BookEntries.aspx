<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Edit_BookEntries.aspx.vb" Inherits="BetterclassifiedAdmin.Edit_BookEntries" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Book Entries</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding: 20px">
        <div style="padding: 20px; height:600; overflow:auto">
            <%-- ajax script manager --%>
            <asp:ScriptManager ID="ScriptManager1" runat="server" />
            
            <%-- ajax update panel start --%>
            <asp:UpdatePanel ID="pnlUpdateGeneralDetails" runat="server">
                <ContentTemplate>
                
                    <asp:GridView   ID="grdBookEntries" runat="server" AutoGenerateColumns="false"
                                    DataKeyNames="BookEntryId" EmptyDataText="No records found.">
                        <Columns>
                            <asp:BoundField DataField="PublicationTitle" HeaderText="Publication" />
                            <asp:BoundField DataField="EditionDate" HeaderText="Edition" DataFormatString="{0:D}" /> 
                        </Columns>
                    </asp:GridView>
                
                </ContentTemplate>
            </asp:UpdatePanel>
        
        </div>
    </div>
    </form>
</body>
</html>
