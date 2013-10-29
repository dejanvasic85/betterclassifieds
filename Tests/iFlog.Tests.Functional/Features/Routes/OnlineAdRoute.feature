﻿Feature: OnlineAdRoute
	In order to view details of an ad online
	As a an anonymous user
	I want to be to navigate to a SEO friendly URL

@web
Scenario: Open Online Ad Url Successfully
	Given The online ad titled "Ad with cool route"
	When I navigate to "Ad/ad-with-cool-route/{0}"
	Then the page title should start with "Ad with cool route"