Feature: EventAd
	In order to find book tickets for an event
	As an anonymous user
	I want to be find the event online and purchase the ticket

@EventAd
@ignore
Scenario: View event and book free tickets successfully
	Given an event ad titled "The Opera" exists
	And I navigate to "/Events/the-opera/adId"
	When I select "2" "General Admission" tickets 
	And confirm tickets and provide personal details
	And choose to email all guests
	Then i should see a ticket purchased success page
	And the tickets should be booked
	And the ticket could for the event should be deducted
	And email is sent to all guests
	And email is sent to ticket purchaser