@extendbooking
Feature: ExtendBooking
	In order to avoid having to book an entirely new ad
	As an advertiser
	I want to be able to extend 

@web
Scenario: Extend Online Booking
	Given I am logged in as "bdduser" with password "password"
	And I have a booking bundle with online and line ad titled "Selenium Booking Extension" 
	When I navigate to My Bookings
	And I extend the booking for another 5 insertions
	Then the booking extension should be successful