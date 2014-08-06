@web
Feature: NotFound
	In order to handle not found error messages
	As a member or anonymous visitor
	I want to see a user-friendly 404 not found message

@404
Scenario: Resource does not exist
	Given I navigate to a page that does not exist
	Then I should see a not found page
