@eventAd
@bookEventTickets
Feature: bookEventTickets
	In order to book tickets for an event
	As a logged in user
	I want to be find the event online and purchase the ticket
	So that I can go to the event

@BookTickets
Scenario: View event and book two tickets with successful payment
	Given client setting "Events.EnableCreditCardPayments" is set to "true"
	And I am logged in as "bddTicketBuyer" with password "bddTicketBuyer"
	And an event ad titled "The Opera" exists 
	And the event does not include a transaction fee
	And with a ticket option "General Admission" for "5" dollars each and "100" available
	When I navigate to "Event/the-opera/adId"
	And I select "2" "General Admission" tickets
	And proceed to order the tickets
	And enter the email "guest@event.com" and name "Guest FoEvent" for the second guest
	And my details are prefilled so I proceed to checkout and payment
	And credit card payment is complete
	Then I should see a ticket purchased success page
	And the tickets should be booked with total cost "10" and ticket count "2"

@BookTickets @GroupsRequired
Scenario: View event and book ticket by selecting group first
	Given client setting "Events.EnablePayPalPayments" is set to "true"
	And I am logged in as "bddTicketBuyer" with password "bddTicketBuyer"
	And an event ad titled "The Opera" exists 
	And the event does not include a transaction fee
	And the event requires group selection
	And with a ticket option "General Admission" for "0" dollars each and "100" available
	And the event has a group "Table 1" for ticket "General Admission" and allows up to "10" guests
	And the event has a group "Table 2" for ticket "General Admission" and allows up to "10" guests
	And I navigate to "Event/the-opera/adId"
	When I select group "Table 1"
	And I select "2" "General Admission" tickets
	And proceed to order the tickets
	And enter the email "guest@event.com" and name "Guest FoEvent" for the second guest
	And my details are prefilled so I proceed to checkout
	Then I should see a ticket purchased success page
	And the tickets should be booked with total cost "0" and ticket count "2"

@BookTickets
Scenario: Register before booking tickets	
	Given an event ad titled "Register before buying tickets" exists
	And with a ticket option "General Admission" for "0" dollars each and "50" available
	When I navigate to "Event/register-before-buying-tickets/adId"
	And I select "2" "General Admission" tickets
	And proceed to order the tickets
	Then I should be on the registration page


@IncludeTransaction
@EventTicketFee
Scenario: View event ad with transaction fee should increase the price of tickets
	Given I am logged in as "bddTicketBuyer" with password "bddTicketBuyer"
	And the event ticket fee is "2.1" percent and "30" cents
	And an event ad titled "The Opera 2" exists
	And with a ticket option "General Admission" for "5" dollars each and "100" available
	And with a ticket option "VIP" for "10" dollars each and "100" available
	And with a ticket option "Free entry" for "0" dollars each and "100" available
	When I navigate to "/Event/the-opera/adId"
	Then the ticket "General Admission" price should be "$5.00"
	And the ticket "VIP" price should be "$10.00" 
	And the ticket "Free entry" price should be "Free"	
	When I select "1" "General Admission" tickets
	When I select "1" "VIP" tickets
	And proceed to order the tickets
	Then the booking page should display total tickets "15.00" total fees "0.61" and sub total "15.62"


@DoesNotIncludeTransactionFee
Scenario: View event ad with no transaction fee 
	Given I am logged in as "bddTicketBuyer" with password "bddTicketBuyer"
	And an event ad titled "The Opera 3" exists
	And the event does not include a transaction fee
	And with a ticket option "General Admission" for "5" dollars each and "100" available
	And with a ticket option "VIP" for "10" dollars each and "100" available
	When I navigate to "/Event/the-opera/adId"
	Then the ticket "General Admission" price should be "$5.00"
	Then the ticket "VIP" price should be "$10.00"
	When I select "1" "General Admission" tickets
	When I select "1" "VIP" tickets
	And proceed to order the tickets
	Then the booking page should display total tickets "15.00" total fees "0.00" and sub total "15.00"


@EventInvite
Scenario: Invitation exists and is used to book tickets
	Given an event ad titled "Event with Invite" exists
	And the event has an invite for "Foo Bar" with email "foo@bar.com"
	And with a ticket option "VIP" for "10" dollars each and "100" available
	When I navigate to the page for the new invitaton
	Then It should display the invitation page with event title "Event with Invite"
	When selecting to purchase the "VIP" ticket
	Then I should be on the registration page
