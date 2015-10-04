Feature: EventBooking
	In order to advertise my event
	As an advertiser
	I want to the ability to book an ad for the event

@EventBooking
Scenario: Simple Event with no ticketing
	Given I start a new booking 
	And I select a category and sub category for events
	When I complete my event details starting today
	And Confirm my ad details
	Then the booking should be successful
	And the ad should be displayed at the top of the list on the home page
