Feature: EditAd
	In order to have my ad relevant for target audience
	As an advertiser
	I want to be able to udpate my existing ad details

@EditAd
Scenario: Edit online ad details successfully
	Given I have an online ad titled "Ad for editing" in parent category "Selenium Parent" and sub category "Selenium Child"
	When I navigate to relative url "Account/UserAds"
	And I select to edit the newly placed ad
	And I update the title to "Ad has been updated" and StartDate to tomorrow
	Then I should see a success message
	And the online ad should be updated
