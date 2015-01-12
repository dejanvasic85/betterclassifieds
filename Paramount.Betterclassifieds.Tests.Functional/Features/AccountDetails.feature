﻿Feature: AccountDetails
	In order to keep my details up to date
	As an advertiser
	I want to be able to login and update my personal details

@accountDetails @ignore
Scenario: Update details successfully
	Given The user with username "accountTesting" does not exist
	And I am a registered user with username "accountTesting" and password "accountTesting123" and email "account@testing.com"
	And I am logged in as "accountTesting" with password "accountTesting123"
	When I go to MyAccountDetails page
	And Update my address "1 AutomatedTesting Street", PhoneNumber "0399991111"
	Then I should see details updated message
	#And my personal details should be updated in the database