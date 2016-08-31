@eventAd
@eventGroups
Feature: ManageEventGroups
	In order to enable guest grouping
	As an event organiser
	I want to manage groups after creating an event


Scenario: Create event groups from event dashboard
	Given an event ad titled "The Opera with tables" exists 
	And I am logged in as "bdduser" with password "password123"
	And with a ticket option "Free entry" for "0" dollars each and "100" available
	And with a ticket option "VIP" for "10" dollars each and "100" available
	When I go the event dashboard for the current ad
	And I select to manage groups
	And I create a new group "Table For All" for all tickets and unlimited guests
	Then the group "Table For All" should be created
	When I create a new group "Special Table" for ticket "VIP" and "10" guests
	Then the group "Special Table" should be created
