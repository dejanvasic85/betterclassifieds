-- EventGuest - Kandobay
exec [EmailTemplate_Upsert]
	@DocType = 'EventGuestTransferFrom',
	@Brand = 'TheMusic',
	@From = 'classies@themusic.com.au',
	@SubjectTemplate = 'Tickets transferred for [/EventName/]',
	@BodyTemplate = '
 <div class="row">
    <h3>Ticket Transferred for Event <em>[/EventName/]</em> on [/EventStartDate/]</h3>
    <p>We are sorry your ticket <em>[/TicketName/]</em> has been transferred to [/NewGuestName/] [/NewGuestEmail/]</p>

    <p>If you believe this happened in error, please visit the <a href="[/EventUrl/]">event details here </a> and contact the event organiser.</p>
</div>';