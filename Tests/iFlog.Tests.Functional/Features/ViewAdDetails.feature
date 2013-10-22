Feature: ViewAdDetails
	In order to view details of an ad online
	As a an anonymous user
	I want to be to navigate to an Ad URL

@web	
Scenario: Navigate to an existing ad successfully
	Given The online ad titled "Music Tutor Available"
	When I navigate to ad URL for "Music Tutor Available"
	Then the page should display tutor ad information
