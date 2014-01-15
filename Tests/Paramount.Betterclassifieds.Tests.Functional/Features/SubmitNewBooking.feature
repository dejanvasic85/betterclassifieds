@booking
Feature: SubmitNewBooking
	In order to have an ad appear on betterclassifieds
	As a registered user
	I want to be able to go through the booking steps and submit a new booking

@booking @web
Scenario: Submit online ad
	Given I am logged in as "bdduser" with password "password123"
	When I submit a new Online Ad titled "This is a Selenium Ad" starting from today
	Then the booking should be successful
	And I should see the ad on top of the search results