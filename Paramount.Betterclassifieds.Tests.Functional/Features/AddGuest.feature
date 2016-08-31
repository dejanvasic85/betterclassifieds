@eventAd
Feature: AddGuest
	In order to maintain a reconciliated guest list
	As an event organiser	
	I want to add a guest from the dashboard without any payment

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