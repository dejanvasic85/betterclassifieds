Feature: ChangePassword
	In order to maintain password security
	As a registered user
	I want the ability to change my password

@changePassword
Scenario: Change password successfully
	Given The user with username "usr_change_password" and email "usr_change@email.com" does not exist
	And I am a registered user with username "usr_change_password" and password "usr_change_password" and email "usr_change@email.com"
	And I am logged in as the newly added user
	When I navigate to relative url "Account/ChangePassword"
	And Set the old password "usr_change_password" and new password "usr_password_new"
	And Wait until success message is displayed
	Then the success message contain "Password changed successfully."