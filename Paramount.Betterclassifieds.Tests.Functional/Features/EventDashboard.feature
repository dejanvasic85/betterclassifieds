@eventAd
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
	When I go the event dashboard for the current ad
	And I go to edit the guest "guestone@email.com" from the dashboard
	And I remove the guest from the event
	Then the guest email "guestone@email.com" should be not active for the current event
	And the guest email "guestone@email.com" event booking should not be active
	And I should see the remove guest success message "The guest has been removed successfully."
	When I click Event Dashboard button
	Then I should be back to event dashboard page
	And the guest count should be 0 and 0 guests are in search results