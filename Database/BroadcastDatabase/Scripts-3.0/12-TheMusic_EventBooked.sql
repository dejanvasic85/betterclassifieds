-- EventGuest - Kandobay
exec [EmailTemplate_Upsert]
	@DocType = 'EventTicketsBooked',
	@Brand = 'TheMusic',
	@BodyTemplate = '
<p>Thank you for purchasing tickets at TheMusic for event <em>[/EventName/]</em>.</p> <p>Each guest should receive an email with their ticket as an attachment and barcode.</p>';