Feature: AccountConfirmation
	In order to start using my classifieds account
	As a newly registered user
	I want the ability to confirm my identity

@membership
Scenario: Confirmation successful 
	Given I have received an email with a confirmation link
	When I navigate to the confirmation link provided in the email
	Then I should see a successful confirmation screen
	And I should be logged in successfully

@membership
Scenario: Registration expired
	Given I have received an email with a confirmation link
	And I wait for the confirmation link to expire
	When I navigate to the confirmation link provided in the email
	Then I should see an expired confirmation message