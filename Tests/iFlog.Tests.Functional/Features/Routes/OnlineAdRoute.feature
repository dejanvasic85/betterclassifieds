Feature: OnlineAdRoute
	In order to view details of an ad online
	As a an anonymous user
	I want to be to navigate to an Ad URL

@web	
Scenario: Open Online Ad Url Successfully
	Given The online ad titled "WOOHOO IT’S TAX TIME!"
	When I navigate to "Ad/woohoo-its-tax-time/12925"
	Then the page title should start with "WOOHOO IT’S TAX TIME!"