@booking  @web
Feature: SubmitNewBooking
	In order to have an ad appear on betterclassifieds
	As a registered user
	I want to be able to go through the booking steps and submit a new booking

Scenario: Submit online ad
	Given I am logged in as "bdduser" with password "password123"
	When I submit a new Online Ad titled "This is a Selenium Ad" starting from today
	Then the booking should be successful


Scenario: Submit online and line ad bundle
	Given I am logged in as "bdduser" with password "password123"
	When I submit a new Bundled Ad titled "This is a bundled ad" starting from the next edition
	Then the booking should be successful