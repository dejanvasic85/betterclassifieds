ALTER TABLE EventGroup
ADD IsDisabled BIT NULL

GO

UPDATE EventGroup 
SET		IsDisabled = 0
WHERE	IsDisabled IS NULL

GO

ALTER TABLE EventGroup
ALTER COLUMN IsDisabled BIT NOT NULL


GO


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
		gr.IsDisabled,
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
		gr.AvailableToAllTickets,
		gr.IsDisabled
END

GO
/****** Object:  StoredProcedure [dbo].[EventGroups_GetByEventId]    Script Date: 26/06/2016 7:44:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[EventGroups_GetById]
	@eventGroupId		INT
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
		gr.IsDisabled,
		COUNT(ebt.EventBookingTicketId) AS GuestCount
	FROM	EventGroup gr
	LEFT JOIN	EventBookingTicket ebt ON	ebt.EventGroupId = gr.EventGroupId
	WHERE gr.EventGroupId = @eventGroupId
	GROUP BY gr.EventGroupId, 
		gr.GroupName, 
		gr.MaxGuests,
		gr.EventId,
		gr.CreatedBy,
		gr.CreatedDateTime,
		gr.CreatedDateTimeUtc,
		gr.AvailableToAllTickets,
		gr.IsDisabled
END

GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[EventGroup_Create]
       @eventId				INT,
	   @groupName			VARCHAR(50),
	   @maxGuests			INT = NULL,
	   @createdDate			DATETIME = NULL,
	   @createdDateUtc		DATETIME = NULL,
	   @createdBy			VARCHAR(50) = NULL,
	   @availableToAllTickets BIT = NULL,
	   @isDisabled			BIT = 0,
	   @tickets				VARCHAR(MAX) = NULL,
	   @eventGroupId		INT = NULL OUTPUT
AS

BEGIN TRANSACTION
	INSERT INTO EventGroup (EventId, GroupName, MaxGuests, CreatedDateTime, CreatedDateTimeUtc, CreatedBy, AvailableToAllTickets, IsDisabled)
	VALUES (@eventId, @groupName, @maxGuests, @createdDate, @createdDateUtc, @createdBy, @availableToAllTickets, @isDisabled);

	SELECT @eventGroupId = @@IDENTITY;

	IF ((@availableToAllTickets IS NOT NULL) 
		AND (@availableToAllTickets = 0) 
		AND (@tickets IS NOT NULL))
	begin
		INSERT INTO EventGroupTicket
		SELECT @eventGroupId, [data]
		FROM	dbo.SplitStringToInt(@tickets, ',')
	end

COMMIT