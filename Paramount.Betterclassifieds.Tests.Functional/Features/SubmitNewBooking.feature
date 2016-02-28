@booking 
Feature: SubmitNewBooking
	In order to have an ad appear on betterclassifieds
	As a registered user
	I want to be able to go through the booking steps and submit a new booking

@OnlineBooking @UserNetwork
Scenario: Submit online ad and notify friends
	Given I am logged in as "bdduser" with password "password123"
	When I submit a new Online Ad titled "This is a Selenium Ad" starting from today
	And the booking details are confirmed
	And I notify my friend "Emmanual Adebayor" "ade@spurs.com" about my add
	Then the booking should be successful
	And my friends email "ade@spurs.com" should receive the notification

@EventBooking
@EventAd
Scenario: Simple Event with no ticketing
	Given I am logged in as "bdduser" with password "password123"
	And I start a new booking
	When I book an event ad titled "Event with no tickets" starting from today and ticketing "is not" enabled
	And the booking details are confirmed
	Then the booking should be successful
	When checking out the ad on success page
	Then the url should be "/Event/event-with-no-tickets/adId" with adId in scenario

@EventBooking
@EventAd
Scenario: Event with ticketing
	Given I am logged in as "bdduser" with password "password123"
	And I start a new booking
	When I book an event ad titled "Ticketing event" starting from today and ticketing "is" enabled
	And event is setup with 2 tickets and 2 fields
	And the booking details are confirmed
	Then the booking should be successful
	When checking out the ad on success page
	Then the url should be "/Event/ticketing-event/adId" with adId in scenario