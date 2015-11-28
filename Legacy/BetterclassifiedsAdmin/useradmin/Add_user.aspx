<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/masterpage/MasterPage.master" CodeBehind="Add_user.aspx.vb" Inherits="BetterclassifiedAdmin.Add_user" %>

<%-- content placeholder for head section --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript">

    //  toggle checkboxes in gridview javascript function 
    function SelectAllCheckboxes(spanChk) {

        var oItem = spanChk.children;
        var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
        xState = theBox.checked;
        elm = theBox.form.elements;

        for (i = 0; i < elm.length; i++)
            if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
            //elm[i].click();

            if (elm[i].checked != xState)
                elm[i].click();
            //elm[i].checked=xState;

        }
    }
 
</script>
</asp:Content>

<%-- content placeholder for body title --%>
<asp:Content ID="Content5" runat="server" contentplaceholderid="cphBodyTitle">
    Create User With Roles
</asp:Content>

<%-- content placeholder for user navigation --%>
<asp:Content ID="Content6" runat="server" contentplaceholderid="cphUserNavigation">
 
</asp:Content>

<%-- content placeholder for user content body --%>
<asp:Content ID="Content7" runat="server" contentplaceholderid="cphContentBody">

    <%-- create user wizard with roles --%>
    <asp:CreateUserWizard ID="RegisterUserWithRoles" runat="server" 
        ContinueDestinationPageUrl="~/useradmin/add_user.aspx"
        OnActiveStepChanged="RegisterUserWithRoles_ActiveStepChanged" 
        LoginCreatedUser="False" 
        CompleteSuccessText="The account has been successfully created."        
        UnknownErrorMessage="The account was not created. Please try again." 
        HeaderText="New User Account" 
        CreateUserButtonText="Continue - Step 2" 
        oncreateduser="RegisterUserWithRoles_CreatedUser">
        <WizardSteps>
            <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server" 
                Title="Step 1 - Basic account details">
            </asp:CreateUserWizardStep>
            <asp:WizardStep ID="SpecifyRolesStep" runat="server" 
                StepType="Step" 
                Title="Step 2 -  Specify Roles"
                AllowReturn="False">
                <asp:CheckBoxList ID="RoleList" runat="server">
                </asp:CheckBoxList>
            </asp:WizardStep>
            <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    
</asp:Content>
