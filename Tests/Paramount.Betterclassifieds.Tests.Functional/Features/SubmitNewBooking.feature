@booking
Feature: SubmitNewBooking
	In order to have an ad appear on betterclassifieds
	As a registered user
	I want to be able to go through the booking steps and submit a new booking

@booking @web
Scenario: Submit free booking with print and online ad
	Given the publication "Selenium Publication" has at least 10 editions
	And I am a registered user with username "bdduser" and password "password123" and email "bdd@somefakeaddress.com"
	When I navigate to the login page
	And I login with username "bdduser" and password "password123"
	And I click on the Place New Ad button
	And I select the bundle option
	And I click Next in booking Navigation
	And I select categories "Selenium Parent" and "Selenium child"
	And I select publication "Selenium Publication"
	And I provide line ad header "This is a sample ad" and description "This is a sample ad"
	And I provide online ad header "This is a sample ad" and description "This is a sample ad"
	And I click Next in booking Navigation
	And I select 5 insertions
	And I click Next in booking Navigation
	And I confirm Details and confirm to terms and conditions
	And I click Next in booking Navigation
	Then I should see a booking successful page
	And the status of the booking should be 1