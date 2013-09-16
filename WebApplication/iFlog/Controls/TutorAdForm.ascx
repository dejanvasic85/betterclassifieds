<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TutorAdForm.ascx.vb" ClientIDMode="Predictable" Inherits="BetterclassifiedsWeb.TutorAdForm" %>

<%--<div class="well">
    <strong>New:</strong> These tutoring details that will be displayed 
    and searchable online!
</div>--%>

<%--Hidden fields--%>
<asp:HiddenField runat="server" ID="hdnOnlineAdId"/>
<asp:HiddenField runat="server" ID="hdnTutorAdId"/>

<%--Subjects--%>
<div class="formcontrol-container">
    <label>Subject/Themes</label>
    <label class="helptext">What areas are covered in the course?</label>
    <div class="control">
        <paramountIt:TagsInput runat="server" ID="txtSubjects" />
    </div>
</div>

<%--Course Objective--%>
<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_txtObjective">Course Objective</label>
    <label class="helptext">What should the expectations be for the student?</label>
    <div class="control">
        <asp:TextBox runat="server" ID="txtObjective" Rows="3" TextMode="MultiLine" Width="90%"></asp:TextBox>
    </div>
</div>

<%--What to bring--%>
<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_txtWhatToBring">What to bring</label>
    <label class="helptext">List any equipment/materials students should bring.</label>
    <div class="control">
        <asp:TextBox runat="server" ID="txtWhatToBring" Width="90%"></asp:TextBox>
    </div>
</div>

<%--Age group ( min max )--%>
<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_txtAgeMin">Age Group</label>
    <label class="helptext">Specify minimum and maximum suitable ages.</label>
    <div class="control">
        <asp:TextBox runat="server" ID="txtAgeMin" TextMode="Number" placeholder="Min" Width="80px"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtAgeMax" TextMode="Number" placeholder="Max" Width="80px"></asp:TextBox>    
    </div>
</div>

<%--Expertise level--%>
<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_ddlExpertLevel">Level</label>
    <label class="helptext">Expected level of skill or knowledge</label>
    <div class="control">
        <asp:DropDownList runat="server" ID="ddlExpertLevel" />
    </div>
</div>

<%--Travel options--%>
<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_ddlTravelOptions">Travel</label>
    <label class="helptext">Where can lessons be taken?</label>
    <div class="control">
        <asp:DropDownList runat="server" ID="ddlTravelOptions"/>
    </div>
</div>

<%--Pricing options--%>
<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_ddlTravelOptions">Pricing Type</label>
    <label class="helptext">How will the student be charged?</label>
    <div class="control">
        <asp:DropDownList runat="server" ID="ddlPricingOptions"/>
    </div>
</div>