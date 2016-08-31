@eventAd
@bookTicketsFromInvite
Feature: bookTicketsFromInvite
	In order to book tickets to an event
	As a consumer
	I want to navitate to a page with all ticket options and proceed to checkout

@EventInvite
Scenario: Invitation exists and is used to book tickets
	Given an event ad titled "Event with Invite" exists
	And the event has an invite for "Foo Bar" with email "foo@bar.com"
	And with a ticket option "VIP" for "10" dollars each and "100" available
	When I navigate to the page for the new invitaton
	Then It should display the invitation page with event title "Event with Invite"
	When selecting to purchase the "VIP" ticket
	Then I should be on the book tickets page 