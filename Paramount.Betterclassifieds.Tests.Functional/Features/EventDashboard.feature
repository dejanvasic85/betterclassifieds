@eventAd
@eventDashboard
Feature: EventDashboard
	In order to keep the event details up to date
	As an event organiser	
	I want to have ability to manage the event

@EventGuest
Scenario: Guest is added
	Given I am a registered user with username "bddEventOrganiser" and password "password123" and email "fakeorganiser@yahoo.com"
	And I am logged in as "bddEventOrganiser" with password "password123"
	And an event ad titled "Event for adding guests manually" exists for user "bddEventOrganiser"
	And with a ticket option "General Admission" for "5" dollars each and "100" available
	And with a ticket option "Early Bird" for "2" dollars each and "100" available
	When I navigate to event "event/event-for-adding-guests-manually/{0}"
	And I choose to add guest manually from the dashboard with details "Foo Guest" "foo@email.com" "Early Bird"
	Then the new guest with email "foo@email.com" should have a ticket "Early Bird" to the event

@EventGuest
Scenario: Guest is removed
	Given I am a registered user with username "bddEventOrganiser" and password "password123" and email "fakeorganiser@yahoo.com"
	And I am logged in as "bddEventOrganiser" with password "password123"
	And an event ad titled "Event for removing guests" exists for user "bddEventOrganiser"
	And with a ticket option "General Admission" for "0" dollars each and "100" available
	And a guest name "Guest One" and email "guestone@email.com" with a "General Admission" ticket to "Event for removing guests"
	When I go to the event dashboard for the current ad
	And I go to edit the guest "guestone@email.com" from the dashboard
	And I remove the guest from the event
	Then I should see the remove guest success message "The guest has been removed successfully."
	And the guest email "guestone@email.com" should be "not active" for the current event
	And the guest email "guestone@email.com" event booking should not be active	
	When I click Event Dashboard button
	Then I should be back to event dashboard page
	And the guest count should be 0 and 0 guests are in search results

@EventGuest
Scenario: Guest is updated
	Given I am a registered user with username "bddEventOrganiser" and password "password123" and email "fakeorganiser@yahoo.com"
	And I am logged in as "bddEventOrganiser" with password "password123"
	And an event ad titled "Update Guest event" exists for user "bddEventOrganiser"
	And with a ticket option "Ticket x" for "0" dollars each and "10" available
	And a guest name "Tobias Dev" and email "tobias@dev.com" with a "Ticket x" ticket to "Update Guest event"
	When I go to the event dashboard for the current ad
	And I go to edit the guest "tobias@dev.com" from the dashboard
	And I update the guest name to "Tobias Banana" and email to "tobias@banana.com"
	Then the guest email "tobias@dev.com" should be "not active" for the current event
	Then the guest email "tobias@banana.com" should be "active" for the current event


@EventGroups
Scenario: Create event groups from event dashboard
	Given an event ad titled "The Opera with tables" exists 
	And I am logged in as "bdduser" with password "password123"
	And with a ticket option "Free entry" for "0" dollars each and "100" available
	And with a ticket option "VIP" for "10" dollars each and "100" available
	When I go to the event dashboard for the current ad
	And I select to manage groups
	And I create a new group "Table For All" for all tickets and unlimited guests
	Then the group "Table For All" should be created
	When I create a new group "Special Table" for ticket "VIP" and "10" guests
	Then the group "Special Table" should be created

@EventTickets
Scenario: Create event ticket with custom fields
	Given an event ad titled "Ticket Management Event" exists
	And I am logged in as "bdduser" with password "password123"
	When I go to the event dashboard for the current ad
	And I select to manage tickets
	And a new ticket is created titled "Ticket One" with price "0" and quantity "100" and field "Custom Field 1"
	Then a ticket "Ticket One" exists with price "0" quantity "100" and field "Custom Field 1"