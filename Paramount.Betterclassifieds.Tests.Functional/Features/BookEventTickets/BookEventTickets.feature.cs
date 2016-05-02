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
namespace Paramount.Betterclassifieds.Tests.Functional.Features.BookEventTickets
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("BookEventTickets")]
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
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "BookEventTickets", "In order to book tickets for an event\r\nAs a logged in user\r\nI want to be find the" +
                    " event online and purchase the ticket\r\nSo that I can go to the event", ProgrammingLanguage.CSharp, new string[] {
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
        [NUnit.Framework.CategoryAttribute("EventAd")]
        [NUnit.Framework.CategoryAttribute("BookTickets")]
        public virtual void ViewEventAndBookTwoTicketsWithSuccessfulPayment()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("View event and book two tickets with successful payment", new string[] {
                        "EventAd",
                        "BookTickets"});
#line 10
this.ScenarioSetup(scenarioInfo);
#line 11
 testRunner.Given("I am a registered user with username \"bddTicketBuyer\" and password \"bddTicketBuye" +
                    "r\" and email \"bdd@TicketBuyer.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
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
 testRunner.And("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
 testRunner.When("I select \"2\" \"General Admission\" tickets", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
 testRunner.And("enter the email \"guest@event.com\" and name \"Guest FoEvent\" for the second guest", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 19
 testRunner.And("choose to email all guests", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("my details are prefilled so I proceed to payment", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("paypal payment is completed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.Then("i should see a ticket purchased success page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 23
 testRunner.And("the tickets should be booked", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
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
#line 26
this.ScenarioSetup(scenarioInfo);
#line 27
 testRunner.Given("an event ad titled \"The Opera 2\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 28
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.When("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 31
 testRunner.Then("the ticket \"General Admission\" price should be \"$5.54\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 32
 testRunner.Then("the ticket \"VIP\" price should be \"$10.79\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
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
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("an event ad titled \"The Opera 3\" exists", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.And("the event does not include a transaction fee", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.And("with a ticket option \"General Admission\" for \"5\" dollars each and \"100\" available" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
 testRunner.And("with a ticket option \"VIP\" for \"10\" dollars each and \"100\" available", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.When("I navigate to \"/Event/the-opera/adId\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
 testRunner.Then("the ticket \"General Admission\" price should be \"$5.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 43
 testRunner.Then("the ticket \"VIP\" price should be \"$10.00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
