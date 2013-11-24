Feature: UserRegistration
	In order become a member of betterclassifieds
	As an anonymous user
	I want to be able to register a new account

@web @membership
Scenario: Create new account successfully
	Given The user with username "bdduser" does not exist
	And I navigate to the registration page
	And I have entered my personal details "Bdd FirstName", "Bdd Lastname", "1 Anderson Road", "Sydney", "NSW", "2000", "02 9999 9999"
	And I click Next to proceed to account details
	And I have entered my account details "bdduser", "password123", "dejanvasic@outlook.com"
	And I click check availability button 
	Then user availability message should display "Username Available"
	When I click Create User button
	Then I should see registration message displayed "Your account has been successfully created"
	Then the user "bdduser" should be created successfully
	And a registration email should be sent to "dejanvasic@outlook.com"
