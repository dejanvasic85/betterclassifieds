@bookTicketsFromInvite
Feature: bookTicketsFromInvite
	In order to book tickets to an event
	As a consumer
	I want to navitate to a page with all ticket options and proceed to checkout

@EventAd
@EventInvite
Scenario: Invitation exists and is used to book tickets
	Given an event ad titled "Event with Invite" exists
	And the event has an invite for "Foo Bar" with email "foo@bar.com"	
