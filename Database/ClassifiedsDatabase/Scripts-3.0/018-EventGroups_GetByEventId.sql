﻿
/****** Object:  StoredProcedure [dbo].[EventGroups_GetByEventId]    Script Date: 26/06/2016 7:44:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[EventGroups_GetByEventId]
	@eventId		INT,
	@eventTicketId	INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	gr.EventGroupId, 
		gr.GroupName, 
		gr.MaxGuests,
		gr.EventId,
		gr.CreatedBy,
		gr.CreatedDateTime,
		gr.CreatedDateTimeUtc,
		gr.AvailableToAllTickets,
		COUNT(ebt.EventBookingTicketId) AS GuestCount
	FROM	EventGroup gr
	LEFT JOIN	EventBookingTicket ebt ON	ebt.EventGroupId = gr.EventGroupId
	WHERE gr.EventId = @eventId
	AND	 (
			gr.AvailableToAllTickets = 1 OR
			gr.EventGroupId IN (
					SELECT	EventGroupId 
					FROM	EventGroupTicket gt
					WHERE	gt.EventTicketId = ISNULL(@eventTicketId, gt.EventTicketId) )
		 )
		 
	GROUP BY gr.EventGroupId, 
		gr.GroupName, 
		gr.MaxGuests,
		gr.EventId,
		gr.CreatedBy,
		gr.CreatedDateTime,
		gr.CreatedDateTimeUtc,
		gr.AvailableToAllTickets
END