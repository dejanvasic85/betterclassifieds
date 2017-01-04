SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].EventBookingTicket_UpdateName
	@eventTicketId		INT,
	@ticketName			VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE	EventBookingTicket
	SET		TicketName = @ticketName
	WHERE	EventTicketId = @eventTicketId;

END