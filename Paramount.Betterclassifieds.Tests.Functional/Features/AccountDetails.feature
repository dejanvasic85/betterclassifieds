Feature: AccountDetails
	In order to keep my details up to date
	As an advertiser
	I want to be able to login and update my personal details

@accountDetails
Scenario: Update details successfully
	Given I am a registered user with username "accountTesting" and password "accountTesting123" and email "account@testing.com"
	And I am logged in as "accountTesting" with password "accountTesting123"
	When I go to MyAccountDetails page
	And Update my address "1 AutomatedTesting Street", PhoneNumber "0399991111"
	And Update my paypal email "fake@paypal.com"
	And Update my direct debit details "fake bank" "fake account" "003444" "894728371"
	And Update preferred payment method to be "Direct Debit"
	And Submit my account changes
	Then I should see details updated message

