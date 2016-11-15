﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Paramount.Betterclassifieds.Tests.Functional.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("bookEventTickets")]
    [NUnit.Framework.CategoryAttribute("eventAd")]
    [NUnit.Framework.CategoryAttribute("bookEventTickets")]
    public partial class BookEventTicketsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "BookEventTickets.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "bookEventTickets", "In order to book tickets for an event\r\nAs a logged in user\r\nI want to be find the" +
                    " event online and purchase the ticket\r\nSo that I can go to the event", ProgrammingLanguage.CSharp, new string[] {
                        "eventAd",
                        "bookEventTickets"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("View event and book two tickets with successful payment")]
        [NUnit.Framework.CategoryAttribute("BookTickets")]
        public virtual void ViewEventAndBookTwoTicketsWithSuccessfulPayment()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View event and book two tickets with successful payment", new string[] {
                        "BookTickets"});
#line 10
this.ScenarioSetup(scenarioInfo);
#line 11
 testRunner.Given("client setting \"Events.EnablePayPalPayments\" is set to \"true\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 12
 testRunner.And("I am logged in as \"bddTicketBuyer\" with password \"bddTicketBuyer\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("an event ad titled \"The Opera\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("the event does not include a transaction fee", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.When("I select \"2\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 17
 testRunner.And("proceed to order the tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("enter the email \"guest@event.com\" and name \"Guest FoEvent\" for the second guest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("my details are prefilled so I proceed to payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("paypal payment is completed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.Then("I should see a ticket purchased success page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 22
 testRunner.And("the tickets should be booked", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("View event and book ticket by selecting group first")]
        [NUnit.Framework.CategoryAttribute("BookTickets")]
        [NUnit.Framework.CategoryAttribute("GroupsRequired")]
        public virtual void ViewEventAndBookTicketBySelectingGroupFirst()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View event and book ticket by selecting group first", new string[] {
                        "BookTickets",
                        "GroupsRequired"});
#line 25
this.ScenarioSetup(scenarioInfo);
#line 26
 testRunner.Given("client setting \"Events.EnablePayPalPayments\" is set to \"true\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.And("I am logged in as \"bddTicketBuyer\" with password \"bddTicketBuyer\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And("an event ad titled \"The Opera\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("the event does not include a transaction fee", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("the event requires group selection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.And("the event has a group \"Table 1\" for ticket \"General Admission\" and allows up to \"" +
                    "10\" guests", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("the event has a group \"Table 2\" for ticket \"General Admission\" and allows up to \"" +
                    "10\" guests", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.And("I navigate to \"Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.When("I select group \"Table 1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.And("I select \"2\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 37
 testRunner.And("proceed to order the tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
 testRunner.And("enter the email \"guest@event.com\" and name \"Guest FoEvent\" for the second guest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("my details are prefilled so I proceed to payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("paypal payment is completed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.Then("I should see a ticket purchased success page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 42
 testRunner.And("the tickets should be booked", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Register before booking tickets")]
        [NUnit.Framework.CategoryAttribute("BookTickets")]
        public virtual void RegisterBeforeBookingTickets()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Register before booking tickets", new string[] {
                        "BookTickets"});
#line 45
this.ScenarioSetup(scenarioInfo);
#line 46
 testRunner.Given("an event ad titled \"Register before buying tickets\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 47
 testRunner.And("with a ticket option \"General Admission\" for \"0\" dollars each and \"50\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.When("I navigate to \"Event/register-before-buying-tickets/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 49
 testRunner.And("I select \"2\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 50
 testRunner.And("proceed to order the tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
 testRunner.Then("I should be on the registration page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("View event ad with transaction fee should increase the price of tickets")]
        [NUnit.Framework.CategoryAttribute("IncludeTransaction")]
        public virtual void ViewEventAdWithTransactionFeeShouldIncreaseThePriceOfTickets()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View event ad with transaction fee should increase the price of tickets", new string[] {
                        "IncludeTransaction"});
#line 55
this.ScenarioSetup(scenarioInfo);
#line 56
 testRunner.Given("I am logged in as \"bddTicketBuyer\" with password \"bddTicketBuyer\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 57
 testRunner.And("an event ad titled \"The Opera 2\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 59
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
 testRunner.And("with a ticket option \"Free entry\" for \"0\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.When("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 63
 testRunner.Then("the ticket \"General Admission\" price should be \"$5.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 64
 testRunner.And("the ticket \"VIP\" price should be \"$10.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
 testRunner.And("the ticket \"Free entry\" price should be \"Free\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.When("I select \"1\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 67
 testRunner.When("I select \"1\" \"VIP\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 68
 testRunner.And("proceed to order the tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
 testRunner.Then("the booking page should display total tickets \"15.00\" total fees \"0.92\" and sub t" +
                    "otal \"15.92\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("View event ad with no transaction fee")]
        [NUnit.Framework.CategoryAttribute("DoesNotIncludeTransactionFee")]
        public virtual void ViewEventAdWithNoTransactionFee()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View event ad with no transaction fee", new string[] {
                        "DoesNotIncludeTransactionFee"});
#line 73
this.ScenarioSetup(scenarioInfo);
#line 74
 testRunner.Given("I am logged in as \"bddTicketBuyer\" with password \"bddTicketBuyer\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 75
 testRunner.And("an event ad titled \"The Opera 3\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
 testRunner.And("the event does not include a transaction fee", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 77
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.When("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 80
 testRunner.Then("the ticket \"General Admission\" price should be \"$5.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 81
 testRunner.Then("the ticket \"VIP\" price should be \"$10.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 82
 testRunner.When("I select \"1\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 83
 testRunner.When("I select \"1\" \"VIP\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 84
 testRunner.And("proceed to order the tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
 testRunner.Then("the booking page should display total tickets \"15.00\" total fees \"0.00\" and sub t" +
                    "otal \"15.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Invitation exists and is used to book tickets")]
        [NUnit.Framework.CategoryAttribute("EventInvite")]
        public virtual void InvitationExistsAndIsUsedToBookTickets()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Invitation exists and is used to book tickets", new string[] {
                        "EventInvite"});
#line 89
this.ScenarioSetup(scenarioInfo);
#line 90
 testRunner.Given("an event ad titled \"Event with Invite\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 91
 testRunner.And("the event has an invite for \"Foo Bar\" with email \"foo@bar.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
 testRunner.When("I navigate to the page for the new invitaton", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 94
 testRunner.Then("It should display the invitation page with event title \"Event with Invite\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 95
 testRunner.When("selecting to purchase the \"VIP\" ticket", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 96
 testRunner.Then("I should be on the registration page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
