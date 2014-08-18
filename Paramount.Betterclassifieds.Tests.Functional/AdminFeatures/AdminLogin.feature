Feature: AdminLogin
	In order to perform administrative tasks
	As an administrator
	I want the ability to login

@admin @login @ignore
Scenario: User exists login successful
	Given I have a registered admin account name ""
	When I login with username "" and password
	Then I should see the admin home page