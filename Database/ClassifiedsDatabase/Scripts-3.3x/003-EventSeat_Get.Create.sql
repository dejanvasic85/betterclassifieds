GO

/****** Object:  StoredProcedure [dbo].[EventSeat_Get]    Script Date: 3/08/2017 5:29:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventSeat_Get]
	@eventId		INT,
	@eventTicketId	INT = NULL,
	@seatNumber		VARCHAR(50) = NULL,
	@requestId		VARCHAR(50) = NULL
AS
BEGIN

	/*
		exec EventSeat_Get @eventId = 8
	*/

	SET NOCOUNT ON;
	
	declare @activeBookingStatus varchar(20) = 'Active';
	declare @reservedTicketStatus varchar(20) = 'Reserved';
	declare @currentUtcDate DATETIME = GETUTCDATE();
	declare @bookedSeats table (SeatNumber VARCHAR(50));
	declare @true bit = 1;
	declare @false bit = 0;

	INSERT INTO @bookedSeats (SeatNumber)
	(
		SELECT	t.SeatNumber
		FROM	EventBookingTicket t
		JOIN	EventBooking b
			ON	b.EventBookingId = t.EventBookingId
		WHERE	t.SeatNumber IS NOT NULL
		AND b.EventId = @eventId
		AND	t.EventTicketId = ISNULL(@eventTicketId, t.EventTicketId)
		AND	t.SeatNumber = ISNULL(@seatNumber, t.SeatNumber)
		AND t.IsActive = 1
		AND b.[Status] = @activeBookingStatus
	
		UNION
	
		SELECT	r.SeatNumber
		FROM	EventTicketReservation r
		JOIN	EventTicket t
			ON	t.EventTicketId = r.EventTicketId
			AND	t.EventTicketId = ISNULL(@eventTicketId, t.EventTicketId)
			AND	t.EventId = @eventId
		WHERE	r.[Status] = @reservedTicketStatus
			AND	r.ExpiryDateUtc > @currentUtcDate
			AND r.SeatNumber IS NOT NULL
			AND r.SeatNumber = ISNULL(@seatNumber, r.SeatNumber)
			AND	r.SessionId = ISNULL(@requestId, r.SessionId)
	);


	SELECT 	s.*,
			CASE WHEN b.SeatNumber IS NULL THEN @false ELSE @true END AS IsBooked
	FROM 	EventSeat s
	JOIN 	EventTicket t
		ON t.EventTicketId = s.EventTicketId
	LEFT JOIN 	@bookedSeats b
		ON b.SeatNumber = s.SeatNumber
	WHERE 
		s.SeatNumber = ISNULL(@seatNumber, s.SeatNumber)
	AND t.EventId = @eventId
	AND t.EventTicketId = ISNULL(@eventTicketId, t.EventTicketId)

END
