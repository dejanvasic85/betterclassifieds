Here's a list of the latest changes:

**3.0.2 Events Improvements and Feedback implementation**

- [Enhancement - Timezones are now being recorded as part of the event using the google timezone api](https://trello.com/c/t36EeUrZ/374-event-dates-should-be-considering-timezones)
- [Enhancement - Now we have icons for each parent category](https://trello.com/c/Og378p0U/405-category-icons-so-they-can-be-used-on-the-home-page-and-no-image-ads)
- [Enhancement - Event details page redesign](https://trello.com/c/h7Kedb0G/378-event-details-page-redesign)
- [Enhancement - Event image can be cropped now to our specifications to make attractive ads](https://trello.com/c/14nMRlUv/404-event-event-ad-needs-specific-sizing-to-suit-the-event-page-redesign)
- [Enhancement - Updating event details is now available](https://trello.com/c/WTutmyLD/371-editing-event-details-needs-to-be-separate-from-regular-ads)
- [Enhancement - Home page redesign including layout and footer](https://trello.com/c/6D8gYRo1/379-home-page-redesign)
- [Enhancement - Transaction database table is now capturing all the event booking ticket purchases](https://trello.com/c/KjmmimJD/367-events-capture-transaction-for-ticket-purchases)
- [Change - Better handling of html descriptions and security hardening](https://trello.com/c/TcP5tbER/366-description-vs-html-description)
- [Change - Replace time selection with dropdowns instead of clock picker](https://trello.com/c/5zFnJ7zj/400-replace-time-selection-with-dropdowns-instead-of-clock-picker)
- [Fix - When uploading images for an event one after another its adding instead of replacing](https://trello.com/c/TzKE2WCd/406-when-uploading-multiple-event-images-they-keep-adding-instead-of-replacing)

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
