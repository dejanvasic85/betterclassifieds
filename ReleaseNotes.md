Here's a list of the latest changes:

**3.32.1**

- [Fix - The calculation for organiser fees should be much more accurate now](https://trello.com/c/sgSZ3XsR/672-the-fee-calculation-for-the-organiser-is-not-displaying-properly-on-the-event-dashboard)
- Change - Adding ad for the ticket print. Blank images for kandobay and theMusic

**3.32.0**

- [Feature - The email for all organisers and ticket purchasers should contain the guest information as well as the buyer now.](https://trello.com/c/awvsAEyu/664-the-guest-ticket-and-seat-information-on-the-event-booked-email-for-organisers)
- [Feature - New promo codes screen to see how many bookings against each promo code](https://trello.com/c/l85tgdAs/643-view-booked-promo-codes)
- [Feature - Ability to manage promo codes. Create and remove (disable) should be available now](https://trello.com/c/2Ij9SSE9/644-manage-promo-codes-and-discounts)
- [Feature - The purchaser email now contains guest details](https://trello.com/c/ZabHCdD5/661-add-guest-details-on-the-buyer-booking-email)
- [Fix - Attempting to fix the KeyNotFound exception when reserving tickets. Removing the Parallel code](https://trello.com/c/TRUtaGeR/659-exception-keynotfoundexception-occurs-sometimes-when-reserving-tickets)

**3.31.0**

- [Change - Moving the sold ticket legend to the bottom](https://trello.com/c/Um2JbnOi/657-ticket-legend-for-sold-items-should-go-last)
- [Change - Guest email should contain ticket type and price](https://trello.com/c/GgLMqZQs/658-ticket-name-to-be-shown-in-guest-email)
- [Feature - Adding the colour code adding and editing for ticket types.](https://trello.com/c/iaknPPCN/649-colour-editing-for-ticket-types)
- [Feature - On booking tickets, users can give us information on how they found about the event](https://trello.com/c/KqFyIqS5/660-analytics-how-u-heard-about-the-event-on-checkout)

**3.30.0 - Mobile improvements**

- [Feature - Transform zoom allowing for better mobile view](https://trello.com/c/SaHZKms9/620-mobile-pinch-zoom-for-seat-selection)
- [Feature - adding more details to the email body](https://trello.com/c/rHvnsrPk/656-additional-information-like-guest-name-email-and-seat-number-should-be-part-of-the-email-body-to-the-guest)

**3.29.0 - Captcha ... Security!**

- [Feature - Registration form should have a robot (captcha) checker](https://trello.com/c/XeCj5VW2/541-add-google-recaptcha-to-the-registration-form)
- [Feature - General enquiry - contact us form - should have a captcha](https://trello.com/c/rE56P4TS/653-add-google-captcha-for-the-general-enquiry-page)
- [Feature - Contact advertiser / event organiser should have a captcha when not logged in](https://trello.com/c/rE56P4TS/653-add-google-captcha-for-the-general-enquiry-page)
- [Change  - Updating the home page for sri lankan events including new background photo!](https://trello.com/c/niQVNI1l/652-update-the-background-image-for-srilankanevents)

**3.28.5**

- Feature - New button that will use logged in details for all tickets.
- Fix - The server side validation on max selected reservations was incorrect.
- Fix - The contact us form on the general site was not working because the mail service wasn't initialised.

**3.28.4**

- Fix - Removing the guest was not de-allocating the seat
- Fix - Updating the guest was not re-allocating the seat

**3.28.3**

- Fix - The seat was not booked properly when 100 percent discount was applied.
- Fix - The calculation of the 100 percent discount was still adding cents!

**3.28.2**

- Fix - The account login wasn't handling the errors from the server properly

**3.28.1**

- [Feature - Add pay reference to the stripe checkout for easier reconciliation](https://trello.com/c/hieOJIen/646-stripe-description-our-ref-should-include-the-brand-eg-kandobay-along-with-the-booking-id-helps-with-reconciliation)

**3.28.0 - Seating**

- [Feature - We have seating!](https://trello.com/c/A04yyUW7/617-seat-and-ticket-selection-for-event)
- [Feature - Set specific seats as not available with a simple DB flag](https://trello.com/c/LiNtAMnR/623-blocked-seating-by-developer)
- [Feature - Add guest 'offline' with a seat number](https://trello.com/c/omppRjXh/636-add-guest-with-a-seat-number)
- [Feature - Seat number is now included in the guest list report](https://trello.com/c/Y10qtgad/638-guest-report-with-seat-number)
- [Feature - Ticketing should not be displayed until the opening date. And should also respect closing and end dates](https://trello.com/c/o7VIunti/639-the-seating-should-not-be-displayed-until-the-ticketing-opening-date-is-reached)
- [Feature - Add promotional code during checkout](https://trello.com/c/lLoXG0af/619-promo-code-during-checkout)
- [Tech - Adjust the deployment target for SriLankanEvents production server](https://trello.com/c/9Fk6hTZn/624-change-the-production-destination-server-for-sri-lankan-events-brand)
- [Change - Adding new sri lankan events logo](https://trello.com/c/cll2zDHv/629-update-the-sri-lankan-events-logo)
- [Fix - The LogEntries for all the brands](https://trello.com/c/lBjX2BAr/630-fix-the-logentries-configuration-for-themusic-and-sri-lankan-events)
- [Fix - The facebook link for SriLankanEvents should be working now](https://trello.com/c/lBjX2BAr/630-fix-the-logentries-configuration-for-themusic-and-sri-lankan-events)
- [Fix - Home page latest items tiles should now be an even height](https://trello.com/c/vkYRIqGi/637-home-page-tiles-should-align)
- [Fix - The full size image on the event page should load fully now](https://trello.com/c/kH65QUKn/633-load-full-size-image-on-the-event-page-it-doesnt-look-good-with-the-a4-flyers-at-the-moment)
