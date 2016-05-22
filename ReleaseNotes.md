Here's a list of the latest changes:

**3.9.0**

- Change Request - Minimum password length is now only set to 1 character! Until better messaging / comms is in place for users.

**3.8.1**

- Fix - The console runner unity container was not configured with the ILogService

**3.8.0 Event invitations**

- [Enhancement - Event invitations are now available.](https://trello.com/c/rZ4Qfe4B/474-event-invitations-invitations-using-the-user-networking)
- [Tech - Added log4Net logging and logentries.com](https://trello.com/c/7RTbKAIz/333-review-logging-infrastructure-and-logentries-com)
- [Tech - Clean lots of tables and objects that were redundant](https://trello.com/c/GQVcsLon/128-clean-databases-and-remove-unused-objects)

**3.7.0 Payment config and Fee Capture**

- [Enhancement - Payment methods can now be configured](https://trello.com/c/dV698u68/461-events-make-payment-methods-optional)
- [Enhancement - The transaction fee needs to be captured for tickets booked and the parent event booking.](https://trello.com/c/QcFBGDZO/462-the-transaction-fee-needs-to-be-captured-for-tickets-booked-and-the-parent-event-booking)
- [Fix - Event was closed but tickets could still be booked](https://trello.com/c/2UyfhWrn/463-the-event-was-closed-but-tickets-could-still-be-booked)
- [Fix - Events can be closed from the dashboard again](https://trello.com/c/PohF9zHJ/464-closing-an-event-was-throwing-an-error-when-attaching-to-the-db-model)
- [Change Request - No longer expiring the ad when event is closed](https://trello.com/c/LC27lgu4/465-do-not-expire-the-ad-when-closing-event-ticket-purchases)

**3.6.0 Extra 30 cents added to ticket fees**

- [Change Request - Add an additional transaction fee](https://trello.com/c/yG9gsR9U/453-add-30-cents-to-the-transaction-fee)
- [Change Request - Removing the disabled inputs during booking](https://trello.com/c/RaS0abV1/443-the-default-guest-full-name-and-email-extra-fields-shouldn-t-be-displayed-as-disabled-input-tags)
- [Change Request - Update the home page title](https://trello.com/c/uQeTid2d/451-change-the-title-for-kandobay)
- [Change Request - The book tickets page is now displaying text with new lines - proper html](https://trello.com/c/qLAT3poS/450-the-book-tickets-page-could-display-html)
- [Change Request - Warning alert is much better looking with orange text and white background](https://trello.com/c/QlQO9RGy/452-the-warning-colour-in-kandobay-needs-to-be-a-little-lighter-and-friendlier-with-blue-links)
- [Fix - Calculating the tickets remaining is not considering unpaid or cancelled tickets.](https://trello.com/c/UllWDORn/441-calculating-the-tickets-remaining-is-not-considering-unpaid-or-cancelled-tickets)
- [Fix - Credit card payment type was set to None for event booking](https://trello.com/c/T5ROAAoX/454-event-booking-was-not-storing-credit-card-as-the-payment-type)
- [Enhancement - New page explaining the ticketing fees](https://trello.com/c/gEnSOpl6/455-add-a-help-screen-explaining-the-ticketing-fees)

**3.5.0 Fixes**

- [Change Request - Remove the TBC text on the ticket preview](https://trello.com/c/ehyOjnf4/448-remove-ticket-id-tbc)
- [Fix - Home page shows no items when there's less than 3](https://trello.com/c/UwAKylLR/446-kandobay-whats-new-items-is-empty)
- [Fix - Clicking back on confirmation step was not going to ticketing setup](https://trello.com/c/8L1Jx5XW/444-clicking-back-on-final-step-goes-to-event-detail-instead-of-event-ticketing)
- [Fix - The facebook share and twitter were not configured properly and no location](https://trello.com/c/kSPUvTnn/447-fix-the-twitter-sharing-and-facebook-sharing)

**3.4.0 Credit card payments and Fixes**

- [Enhancement - Integrate Stripe credit card payments for tickets](https://trello.com/c/COALP2bw/334-events-credit-card-payment-integration-stripe)
- [Enhancement - Success page displays the URL to the advertiser for easier sharing](https://trello.com/c/3A9qryVw/439-on-booking-success-page-we-should-display-the-url-so-it-can-be-shared-easier)
- [Fix - Ticket names were not being loaded during the ad booking](https://trello.com/c/kIjY6teN/429-ticket-information-is-not-being-saved-in-the-session)

**3.3.0 Events Improvements**

- [Enhancement - Transaction fee can now be absorbed by the consumer](https://trello.com/c/wVlRodlK/419-transaction-fee-to-be-absorbing-by-ticket-purchasers-and-automatically-set)
- [Enhancement - Replacing barcodes with QR Codes](https://trello.com/c/NmZv041t/424-qr-code-and-change-the-ticket-display)
- [Enhancement - Ticket validation for QR codes for event entry](https://trello.com/c/SxWNpvKQ/421-ticket-validator-at-the-entry)
- [Enhancement - New Brand, Welcome to Sri Lankan Events](https://trello.com/c/BZhUGkZR/407-on-boarding-the-sri-lanka-events-branding)
- [Change Request - Customers should no longer be able to book ads for print if new configuration for Print is False](https://trello.com/c/jse03IDF/428-customer-should-no-longer-be-able-to-recycle-ads-to-go-print-again)

**3.2.0 Bug fixes**

- [Fix - User bookings screen not loading with ad blocker](https://trello.com/c/jpebouSD/417-cannot-load-user-bookings-screen-using-chrome)
- [Fix - Success page with user networking validation and notification is fixed](https://trello.com/c/39onStno/390-booking-success-screen-validation-for-user-network)

**3.1.0 Framework upgrades**

- [Upgrade to .Net framework 4.6.1](https://trello.com/c/HIvGM3im/414-upgrade-to-net-framework-4-6)
- [Upgrade to Asp.Net MVC 5.2.3](https://trello.com/c/En4Z23rh/413-upgrade-to-mvc-5)
- [Upgrade to Entity Framework 6.1.3](https://trello.com/c/Z8k2HMfS/416-upgrade-entity-framework-to-version-6-1-3)
- [Upgrade Selenium Web Driver](https://trello.com/c/H3Nzs6t4/412-update-selenium-web-driver)
- [Upgrade the PayPal library](https://trello.com/c/Sdc9OpZe/415-upgrade-paypal-sdk)

**3.0.2 Events Improvements and Feedback implementation**

- [Enhancement - Social buttons are now available on the event details page](https://trello.com/c/JpIUpRf3/387-events-social-network-integration-to-share-the-even-with-friends)
- [Enhancement - Floorplans can be uploaded and viewed by ticket buyers](https://trello.com/c/Vu8C25zU/399-upload-floor-plan-for-ticketing-management-booking-page-and-event-dashboard)
- [Enhancement - Timezones are now being recorded as part of the event using the google timezone api](https://trello.com/c/t36EeUrZ/374-event-dates-should-be-considering-timezones)
- [Enhancement - Now we have icons for each parent category](https://trello.com/c/Og378p0U/405-category-icons-so-they-can-be-used-on-the-home-page-and-no-image-ads)
- [Enhancement - Event details page redesign](https://trello.com/c/h7Kedb0G/378-event-details-page-redesign)
- [Enhancement - Event image can be cropped now to our specifications to make attractive ads](https://trello.com/c/14nMRlUv/404-event-event-ad-needs-specific-sizing-to-suit-the-event-page-redesign)
- [Enhancement - Updating event details is now available](https://trello.com/c/WTutmyLD/371-editing-event-details-needs-to-be-separate-from-regular-ads)
- [Enhancement - Home page redesign including layout and footer](https://trello.com/c/6D8gYRo1/379-home-page-redesign)
- [Enhancement - Transaction database table is now capturing all the event booking ticket purchases](https://trello.com/c/KjmmimJD/367-events-capture-transaction-for-ticket-purchases)
- [Enhancement - Cancelling a payment with paypal comes back to the booking page again](https://trello.com/c/gRqIbgFW/397-events-display-ticket-purchase-cancellation-screen-for-user-when-cancelling-payment-with-paypal)
- [Enhancement - Full address details are now being captured](https://trello.com/c/JCBXK4ox/410-capture-full-address-all-lines-from-google-maps-instead-of-just-one-line)
- [Change - Changing from kandobay-support to events@kandobay.com.au](https://trello.com/c/ffaXj1Hf/384-email-sender-kandobay-support-is-not-a-nice-name)
- [Change - Better handling of html descriptions and security hardening](https://trello.com/c/TcP5tbER/366-description-vs-html-description)
- [Change - Replace time selection with dropdowns instead of clock picker](https://trello.com/c/5zFnJ7zj/400-replace-time-selection-with-dropdowns-instead-of-clock-picker)
- [Fix - When uploading images for an event one after another its adding instead of replacing](https://trello.com/c/TzKE2WCd/406-when-uploading-multiple-event-images-they-keep-adding-instead-of-replacing)
- [Fix - Search results were not aligning properly](https://trello.com/c/MbGVb0Y9/393-images-not-aligned-for-search-results)
- [Fix - We are now no-image friendly](https://trello.com/c/43WZLOTD/370-handle-no-images)

**3.0.1 Events Bug Bash**

- [Fix - Preventing a page break in the middle of a table row](https://trello.com/c/tfbX3lRZ/380-large-guest-list-pdf-does-not-render-well-for-printing-when-the-data-overflows-to-next-page)
- [Fix - Limited the barcode size to 200px now so it fix nicely for printing](https://trello.com/c/tt3RnjWa/385-ticket-barcode-is-too-big-in-the-ticket-printing)
- [Fix - Coming back to the event ticketing setup screen should be fine now](https://trello.com/c/EIH2XS43/388-error-eventticketfield-is-not-defined-when-coming-back-to-the-ticketing-setup-screen)
- [Fix - Event dates are now being stored and converted properly](https://trello.com/c/2WBf3PCu/389-event-dates-are-being-converted-to-utc-in-mongo-database-we-should-be-storing-both-utc-and-server-date)
- [Fix - Date validation was not working when submitting the event details](https://trello.com/c/8F3uKga4/373-date-validation-doesn-t-seem-to-be-working-when-designing-an-event)
- [Fix - Enter button on the design event page is now defaulting to submit the form](https://trello.com/c/G6L2cmHl/368-enter-button-on-design-event-page-is-screwed-up-when-submitting-form)
- [Fix - Creating an account instantly when booking tickets page was broken](https://trello.com/c/2mxsZ7Na/383-creating-an-account-on-book-tickets-page-throws-an-error-existing-logged-in-users-are-fine)

**3.0.0 Events Alive!**

- [Events - Dashboard for editing event details for organisers/advertisers](https://trello.com/c/OdYvdLcx/340-events-event-dashboard-page-ability-to-change-and-add-tickets)
- [Events - Displaying bunch of nice stats for the dashboard.](https://trello.com/c/IvOYgBxu/343-events-event-dashboard-page-should-contain-information-on-all-sold-tickets-and-pricing-summary)
- [Events - Generate printable tickets for event](https://trello.com/c/O3n6hIJt/336-events-generate-tickets-for-event-booking)
- [Events - Capture guest name and email per ticket](https://trello.com/c/nEm83hZT/341-events-ability-to-specify-guests-details-per-ticket-e-g-email-name)
- [Events - Dashboard now contains a guest list view](https://trello.com/c/Ia6Od3Wz/347-events-guest-list-in-events-dashboard)
- [Events - Printable guest list](https://trello.com/c/qw6tccjV/346-events-printable-guest-list)
- [Events - Request payment by event organiser](https://trello.com/c/RQTzQxoe/344-events-organiser-needs-ability-to-request-payment-for-all-the-ticket-fees)
- [Events - Organiser can explicitly close the event](https://trello.com/c/Yifd04gX/360-events-close-event-so-no-more-tickets-can-be-booked-and-payment-can-be-requested)
- [Events - Organiser can request payment with direct debit or paypal](https://trello.com/c/1Y6sOeyG/345-events-organiser-needs-to-specify-bank-details-when-requesting-payment-or-paypal-email)
- [Events - Closing date can be used to automatically disable the purchasing of tickets](https://trello.com/c/eSGhQT1Q/363-allow-the-event-organiser-to-setup-a-closing-date-for-tickets)
- [Events - Each guest should get an email about the event unless purchaser opts out](https://trello.com/c/rWz5XKOv/365-each-guest-should-receive-an-email-for-the-event)
- [Events - Calendar invite is now included in the email for each guest](https://trello.com/c/OFOT0BBd/324-add-calendar-invite-for-each-guest)
- [Events - Organisers will see the percentage of ticket fees when booking an event](https://trello.com/c/4v8Ty9q0/364-events-specify-the-charging-fee-on-the-event-ticketing-setup-booking-page)
- [Events - PDF invoices for the purchaser](https://trello.com/c/N5YQvAjm/348-events-pdf-invoice-for-the-purchaser)
- [Events - Members can update their profile with payment details](https://trello.com/c/5jfeFNQ7/362-user-ability-to-update-their-profile-with-payment-details)
- [Change - Contact advertiser form now requires a login rather than a CAPTCHA](https://trello.com/c/5bxvSRBU/329-contact-advertiser-with-a-login-only-little-counter-intuitive-but-beats-the-captcha-usage-and-more-secure)
- [Tech - No more captcha for contacting support team](https://trello.com/c/BBLPYpTa/331-remove-the-captcha-from-the-contact-us-page-no-need)
- [Tech - Integrated security so we are no longer using super sql admin account](https://trello.com/c/pJOw5IIl/325-all-the-connection-strings-to-be-integrated-security)
- [Tech - Removed the dependency on the old task scheduler Powershell library](https://trello.com/c/FnvaUjRX/327-setup-the-deployment-to-use-the-new-create-scheduled-task-step-to-remove-dependency-on-the-powershell-modules)
- [Tech - Replaced the old task scheduler console application with better console and argument handling](https://trello.com/c/GgVokvAZ/326-replace-task-scheduler-with-bc-exe)
- [Tech - Removed exposed urls on every page for slightly better security and weight](https://trello.com/c/5zcQ1HrS/335-remove-exposed-urls-on-every-page)
