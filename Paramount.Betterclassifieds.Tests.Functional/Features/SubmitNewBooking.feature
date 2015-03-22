@booking 
Feature: SubmitNewBooking
	In order to have an ad appear on betterclassifieds
	As a registered user
	I want to be able to go through the booking steps and submit a new booking

@OnlineBooking @UserNetwork
Scenario: Submit online ad and notify friends
	Given I am logged in as "bdduser" with password "password123"
	When I submit a new Online Ad titled "This is a Selenium Ad" starting from today
	And I notify my friend "Emmanual Adebayor" "ade@spurs.com" about my add
	Then the booking should be successful
	And my friends email "ade@spurs.com" should receive the notification
	
@ignore @BundleBooking 
Scenario: Submit online and line ad bundle
	Given I am logged in as "bdduser" with password "password123"
	When I submit a new Bundled Ad titled "This is a bundled ad" starting from the next edition
	Then the booking should be successful