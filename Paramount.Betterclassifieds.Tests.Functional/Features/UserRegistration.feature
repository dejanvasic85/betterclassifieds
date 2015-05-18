@registration
Feature: UserRegistration
	In order become a member of betterclassifieds
	As an anonymous user
	I want to be able to register a new account

Scenario: Create new account successfully
	Given The user with username "bdduser" does not exist
	And I navigate to the registration page
	And I have entered my personal details "Bdd FirstName", "Bdd Lastname", "1 Anderson Road", "Sydney", "NSW", "2000", "02 9999 9999"
	And I have entered my account details "bdd@somefakeaddress.com", "password123"
	When I click register button
	Then the user "bdd@somefakeaddress.com" should be created successfully
	And a registration email should be sent to "bdd@somefakeaddress.com"
	And I should see a code confirmation page
