exec dbo.EmailTemplate_Upsert 
	@DocType = 'AdEnquiry',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'You have an enquiry',
	@Description = 'Anonymous request made to the advertiser when previewing an Ad.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'AdShare',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = '[/AdTitle/] - by [/AdvertiserName/]',
	@Description = 'Support for advertiser to send an email with ad details to their user network contacts after they place the ad',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'ActivityReport',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'SriLankanEvents HealthCheck',
	@Description = 'Email contains details of what happened for the day (sent at night).',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'EventGuest',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'See you at [/EventName/]',
	@Description = 'Email to each guest with a ticket about the event.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'EventPaymentRequest',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Event Payment Request',
	@Description = 'Email directly to customer who purchased the tickets and should contain attachments so they can print.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'EventTicketsBooked',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Tickets for [/EventName/]',
	@Description = 'Email directly to customer who purchased the tickets and should contain attachments so they can print.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'ExpirationReminder',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Your Sri Lankan Event ad is Expiring',
	@Description = 'Reminder to the user that their booking is about to expire.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'ForgottenPassword',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Password Reset at Sri Lankan Events',
	@Description = 'User will be sent a new temporary password.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';


exec dbo.EmailTemplate_Upsert 
	@DocType = 'NewBooking',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Sri Lankan Events Booking Complete',
	@Description = 'Email containing full details of the new Ad Booking.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'NewRegistration',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Sri Lankan Events Account Confirmation',
	@Description = 'EmailNew User Registration - Welcome and confirmation email.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';

exec dbo.EmailTemplate_Upsert 
	@DocType = 'SupportRequest',
	@Brand = 'SriLankanEvents',
	@SubjectTemplate = 'Sri Lankan Events Support Request',
	@Description = 'Anonymous support request submitted through public website.',
	@From = 'support@srilankanevents.com.au',
	@BodyTemplate = '';