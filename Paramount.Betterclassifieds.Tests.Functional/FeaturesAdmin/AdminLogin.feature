Feature: AdminLogin
	In order to perform administrative tasks
	As an administrator
	I want the ability to login

@admin @login
Scenario: User exists login successful
	Given I have a registered admin account with username "autoadmin@email.com" and password "password"
	When I login to administration with username "autoadmin@email.com" and password "password"
	Then I should see the admin home page