@bookEventTickets
Feature: BookEventTickets
	In order to book tickets for an event
	As a logged in user
	I want to be find the event online and purchase the ticket
	So that I can go to the event

@EventAd
@BookTickets
Scenario: View event and book free tickets successfully
	Given I am logged in as "bdduser" with password "password123"
	And an event ad titled "The Opera" exists 
	And with a ticket option "General Admission" for "5" dollars each and "100" available
	And I navigate to "/Event/the-opera/adId"
	When I select "2" "General Admission" tickets
	And enter the email "guest@event.com" and name "Guest FoEvent" for the second guest
	And choose to email all guests
	And my details are prefilled so I proceed to payment
	And paypal payment is completed
	Then i should see a ticket purchased success page
	And the tickets should be booked
	#And the ticket could for the event should be deducted
	#And email is sent to all guests
	#And email is sent to ticket purchaser