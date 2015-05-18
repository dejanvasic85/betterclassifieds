@onlineAd
Feature: OnlineAdView
	In order to view details of an ad online
	As a an anonymous user
	I want to be to navigate to a SEO friendly URL

Scenario: Open Online Ad Url Successfully
	Given I have an online ad titled "Ad with cool route" in parent category "Selenium parent" and sub category "Selenium child"
	When I navigate to online ad "Ad/ad-with-cool-route/{0}"
	Then the page title should start with "Ad with cool route"
	And the online ad contact name should be "Sample Contact"
	And the online ad contact email should be "sample@fake.com"
	And the online ad contact phone should be "0455555555"
