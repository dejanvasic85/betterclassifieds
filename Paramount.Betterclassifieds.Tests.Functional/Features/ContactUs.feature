@web @contactUs
Feature: ContactUs
	In order to submit a complaint, feedback or just a suggestion
	As a member or anonymous visitor
	I want the ability to submit the request to the support team

Scenario: Submit form without completing required fields
	Given I navigate to the contact us page
	When I submit the contact us form
	Then I should see a required validation message for First Name, Email and Comment

Scenario: Submit form without captcha
	Given I navigate to the contact us page
	When I provide my comment and contact details
	And I submit the contact us form
	Then I should see a human test validation message