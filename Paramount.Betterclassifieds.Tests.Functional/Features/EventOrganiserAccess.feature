Feature: Multiple event organiser access
	In order to manage an event more efficiently
	As an event organiser
	I want the ability to provide access to other organisers for the same event

@Events
Scenario: Viewing and opening event management
	Given I am logged in as "eventAssistant" with password "password123"
	And a registered user "eventOrganiser" with password "password" and email "email@admin.com" exists
	And an event ad titled "Event with multiple organisers" exists for user "eventOrganiser"
	And event titled "Event with multiple organisers" is assigned to organiser "eventAssistant"
	When I view the user ads page
	Then I should see the ad "Event with multiple organisers"
	When selecting to manage the ad "Event with multiple organisers"
	Then the event dashboard for "Event with multiple organisers" should display 