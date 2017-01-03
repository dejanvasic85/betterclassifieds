-- EventGuest - Kandobay
exec [EmailTemplate_Upsert]
	@DocType = 'EventGuestResend',
	@Brand = 'TheMusic',
	@From = 'classies@themusic.com.au',
	@SubjectTemplate = 'Tickets resent for [/EventName/]',
	@BodyTemplate = '
<h3>Hello [/GuestFullName/]</h3>
    <p>On behalf of the event organiser for <em>[/EventName/]</em> we are resending your tickets due to potential changes to the event or your tickets.</p> <p>Please use this email (tickets included) for entry and discard the old one(s).</p>
    <div>
        <p>
            <h3>Location : [/Location/]</h3>

            <h3>Starts : [/EventStartDate/]</h3>

            <h3>Finishes : [/EventEndDate/]</h3>

            <h3>Ticket Type : [/TicketType/]</h3>

			<h3>Group : [/GroupName/]</h3>
        </p>
    </div>
	<div class="text-centre">
		Simply show this barcode on entry: <br />
		<img class="thumbnail" alt="Barcode" src="[/BarcodeImgUrl/]"></img>
	</div>
			
    <p>We have included a calendar invite for your convenience.</p>  Full 
    event details <a href="[/EventUrl/]">here.</a></p>
</div>';