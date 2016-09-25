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
 testRunner.And("I am a registered user with username \"bddTicketBuyer\" and password \"bddTicketBuye" +
                    "r\" and email \"bdd@TicketBuyer.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("I am logged in as \"bddTicketBuyer\" with password \"bddTicketBuyer\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("an event ad titled \"The Opera\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
 testRunner.And("the event does not include a transaction fee", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.And("the event has a group \"Table 1\" for ticket \"General Admission\" and allows up to \"" +
                    "10\" guests", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 18
 testRunner.And("the event has a group \"Table 2\" for ticket \"General Admission\" and allows up to \"" +
                    "10\" guests", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.When("I select \"2\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
 testRunner.And("enter the email \"guest@event.com\" and name \"Guest FoEvent\" for the second guest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And("my details are prefilled so I proceed to payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.And("paypal payment is completed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.Then("I should see a ticket purchased success page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 25
 testRunner.And("the tickets should be booked", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
 testRunner.When("When selecting \"Table 1\" for guest \"bddTicketBuyer bddTicketBuyer\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
 testRunner.Then("ticket with full name \"bddTicketBuyer bddTicketBuyer\" should be assigned to a gro" +
                    "up", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
#line 31
this.ScenarioSetup(scenarioInfo);
#line 32
 testRunner.Given("an event ad titled \"Register before buying tickets\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 33
 testRunner.And("with a ticket option \"General Admission\" for \"0\" dollars each and \"50\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.When("I navigate to \"/Event/register-before-buying-tickets/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.And("I select \"2\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.Then("I should be on page with url \"/Account/Login\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
#line 40
this.ScenarioSetup(scenarioInfo);
#line 41
 testRunner.Given("an event ad titled \"The Opera 2\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 42
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("with a ticket option \"Free entry\" for \"0\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.When("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("the ticket \"General Admission\" price should be \"$5.41\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 47
 testRunner.And("the ticket \"VIP\" price should be \"$10.51\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And("the ticket \"Free entry\" price should be \"Free\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
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
#line 52
this.ScenarioSetup(scenarioInfo);
#line 53
 testRunner.Given("an event ad titled \"The Opera 3\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 54
 testRunner.And("the event does not include a transaction fee", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.When("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
 testRunner.Then("the ticket \"General Admission\" price should be \"$5.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 59
 testRunner.Then("the ticket \"VIP\" price should be \"$10.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
#line 63
this.ScenarioSetup(scenarioInfo);
#line 64
 testRunner.Given("an event ad titled \"Event with Invite\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 65
 testRunner.And("the event has an invite for \"Foo Bar\" with email \"foo@bar.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
 testRunner.When("I navigate to the page for the new invitaton", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 68
 testRunner.Then("It should display the invitation page with event title \"Event with Invite\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 69
 testRunner.When("selecting to purchase the \"VIP\" ticket", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
 testRunner.Then("I should be on page with url \"/Account/Login\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
