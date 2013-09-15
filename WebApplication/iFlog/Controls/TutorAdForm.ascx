﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TutorAdForm.ascx.vb" ClientIDMode="Predictable" Inherits="BetterclassifiedsWeb.TutorAdForm" %>

<div class="well">
    <strong>New:</strong> These tutoring details that will be displayed 
    and searchable online!
</div>

<%--Subjects--%>
<div class="formcontrol-container">
    <label>Subject(s)</label>
    <label class="helptext">What areas are covered in the course?</label>
    <paramountItCommon:HelpContextControl runat="server" Position="Bottom" ID="hlpTutorSubject" ImageUrl="~/Resources/Images/question_button.gif">
        <ContentTemplate>
            <span class="text-wrapper">Type a subject and press TAB or comma to save.</span>
        </ContentTemplate>
    </paramountItCommon:HelpContextControl>
    <div class="control">
        <paramountIt:TagsInput runat="server" ID="txtSubjects" />
    </div>
</div>

<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_txtObjective">Course Objective</label>
    <label class="helptext">What should the expectations be for the student?</label>
    <div class="control">
        <asp:TextBox runat="server" ID="txtObjective" Rows="3" TextMode="MultiLine" Width="90%"></asp:TextBox>
    </div>
</div>

<div class="formcontrol-container">
    <label for="ContentPlaceHolder1_tutorForm_txtWhatToBring">What to bring</label>
    <label class="helptext">List any equipment/materials students should bring.</label>
    <div class="control">
        <asp:TextBox runat="server" ID="txtWhatToBring" Width="90%"></asp:TextBox>
    </div>
</div>