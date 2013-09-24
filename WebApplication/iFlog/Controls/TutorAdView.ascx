<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TutorAdView.ascx.vb" Inherits="BetterclassifiedsWeb.TutorAdView" %>

<div class="formcontrol-container">
    <label>Subjects</label>
    <label class="helptext">What will be covered by the tutor?</label>
    <span class="control" id="lblSubjects" runat="server"></span>
</div>

<div class="formcontrol-container" runat="server" id="Objective">
    <label>Objective</label>
    <label class="helptext">What you should expect to learn</label>
    <span class="control" id="lblObjective" runat="server"></span>
</div>

<div class="formcontrol-container">
    <label>Age Group</label>
    <label class="helptext">Suitable ages</label>
    <span class="control" id="lblAge" runat="server"></span>
</div>

<div class="formcontrol-container">
    <label>Level</label>
    <label class="helptext">What skill level is required</label>
    <span class="control" id="lblLevel" runat="server"></span>
</div>

<div class="formcontrol-container">
    <label>Travel Option</label>
    <label class="helptext">Student required to travel or tutor will come to you.</label>
    <span class="control" id="lblTravelOption" runat="server"></span>
</div>

<div class="formcontrol-container">
    <label>Pricing Option</label>
    <label class="helptext">How will the lessons be charged</label>
    <span class="control" id="lblPricingOption" runat="server"></span>
</div>

<div class="formcontrol-container" runat="server" id="whatToBring">
    <label>What to bring</label>
    <label class="helptext">You will need to supply the following</label>
    <span class="control" id="lblWhatToBring" runat="server"></span>
</div>