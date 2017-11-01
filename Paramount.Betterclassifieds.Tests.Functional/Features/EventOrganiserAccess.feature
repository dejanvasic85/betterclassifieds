@Events @EventOrganisers
Feature: Multiple event organiser access
	In order to manage an event more efficiently
	As an event organiser
	I want the ability to provide access to other organisers for the same event

Scenario: Event Admin Organiser Viewing and opening event management
	Given a registered user "eventOrganiser" with password "password" and email "email@org.com" exists
	And I am logged in as "eventOrganiser" with password "password"
	And an event ad titled "Event with a single admin organiser" exists for user "eventOrganiser"
	When I view the user ads page
	Then I should see the ad "Event with a single admin..."
	When selecting to manage the ad "Event with a single admin..."
	Then the event dashboard for "Event with a single admin organiser" should display 

Scenario: Event Organiser Viewing and opening event management
	Given a registered user "eventOrganiser" with password "password" and email "email@org.com" exists
	And a registered user "eventAssistant" with password "password123" and email "assistant@org.com" exists
	And I am logged in as "eventAssistant" with password "password123"
	And an event ad titled "Event with multiple organisers" exists for user "eventOrganiser"
	And event titled "Event with multiple organisers" is assigned to organiser "eventAssistant"
	When I view the user ads page
	Then I should see the ad "Event with multiple organisers"
	When selecting to manage the ad "Event with multiple organisers"
	Then the event dashboard for "Event with multiple organisers" should display 