Feature: EditAd
	In order to have my ad relevant for target audience
	As an advertiser
	I want to be able to udpate my existing ad details

@EditAd
Scenario: Edit online ad details successfully
	Given I am a registered user with username "bdd-edit-ad" and password "password123" and email "bdd-edit-ad@email.com"
	And I am logged in as "bdd-edit-ad" with password "password123"
	And I have an online ad titled "Ad for editing" in parent category "Selenium Parent" and sub category "Selenium Child"
	When I navigate to edit online ad "editAd/details/{adId}"
	And I update the title to "Ad has been updated"
	Then I should see a success message