SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventGroups_GetByEventId]
	@eventId	INT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT	eg.EventGroupId, 
		eg.GroupName, 
		eg.MaxGuests,
		eg.EventId,
		eg.CreatedBy,
		eg.CreatedDateTime,
		eg.CreatedDateTimeUtc,
		COUNT(ebt.EventBookingTicketId) AS GuestCount
	FROM	EventGroup eg
	LEFT JOIN	EventBookingTicket ebt ON	ebt.EventGroupId = eg.EventGroupId
	WHERE eg.EventId = @eventId
	GROUP BY eg.EventGroupId, 
		eg.GroupName, 
		eg.MaxGuests,
		eg.EventId,
		eg.CreatedBy,
		eg.CreatedDateTime,
		eg.CreatedDateTimeUtc
END