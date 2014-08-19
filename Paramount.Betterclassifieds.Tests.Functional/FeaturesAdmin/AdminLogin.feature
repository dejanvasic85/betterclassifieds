Feature: AdminLogin
	In order to perform administrative tasks
	As an administrator
	I want the ability to login

@admin @login @ignore
Scenario: User exists login successful
	Given I have a registered admin account name "Selenium Admin"
	When I login to administration with username "SeleniumAdmin" password "password123"
	Then I should see the admin home page