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
    [NUnit.Framework.DescriptionAttribute("SubmitNewBooking")]
    [NUnit.Framework.CategoryAttribute("booking")]
    public partial class SubmitNewBookingFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "SubmitNewBooking.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "SubmitNewBooking", "In order to have an ad appear on betterclassifieds\r\nAs a registered user\r\nI want " +
                    "to be able to go through the booking steps and submit a new booking", ProgrammingLanguage.CSharp, new string[] {
                        "booking"});
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
        [NUnit.Framework.DescriptionAttribute("Submit online ad and notify friends")]
        [NUnit.Framework.CategoryAttribute("OnlineBooking")]
        [NUnit.Framework.CategoryAttribute("UserNetwork")]
        public virtual void SubmitOnlineAdAndNotifyFriends()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Submit online ad and notify friends", new string[] {
                        "OnlineBooking",
                        "UserNetwork"});
#line 8
this.ScenarioSetup(scenarioInfo);
#line 9
 testRunner.Given("I am logged in as \"bdduser\" with password \"password123\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
 testRunner.When("I submit a new Online Ad titled \"This is a Selenium Ad\" starting from today", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
 testRunner.And("the booking details are confirmed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("I notify my friend \"Emmanual Adebayor\" \"ade@spurs.com\" about my add", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.Then("the booking should be successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Simple Event with no ticketing")]
        [NUnit.Framework.CategoryAttribute("EventBooking")]
        [NUnit.Framework.CategoryAttribute("EventAd")]
        public virtual void SimpleEventWithNoTicketing()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Simple Event with no ticketing", new string[] {
                        "EventBooking",
                        "EventAd"});
#line 17
this.ScenarioSetup(scenarioInfo);
#line 18
 testRunner.Given("I am logged in as \"bdduser\" with password \"password123\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 19
 testRunner.And("I start a new booking", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.When("I book an event ad titled \"Event with no tickets\" starting from today and ticketi" +
                    "ng \"is not\" enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
 testRunner.And("the booking details are confirmed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.Then("the booking should be successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 23
 testRunner.When("checking out the ad on success page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 24
 testRunner.Then("the url should be \"/Event/event-with-no-tickets/adId\" with adId in scenario", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Event with ticketing")]
        [NUnit.Framework.CategoryAttribute("EventBooking")]
        [NUnit.Framework.CategoryAttribute("EventAd")]
        public virtual void EventWithTicketing()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Event with ticketing", new string[] {
                        "EventBooking",
                        "EventAd"});
#line 28
this.ScenarioSetup(scenarioInfo);
#line 29
 testRunner.Given("I am logged in as \"bdduser\" with password \"password123\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 30
 testRunner.And("I start a new booking", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.When("I book an event ad titled \"Ticketing event\" starting from today and ticketing \"is" +
                    "\" enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
 testRunner.And("event is setup with 2 tickets and 2 fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 33
 testRunner.And("the booking details are confirmed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
 testRunner.Then("the booking should be successful", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 35
 testRunner.When("checking out the ad on success page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 36
 testRunner.Then("the url should be \"/Event/ticketing-event/adId\" with adId in scenario", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
