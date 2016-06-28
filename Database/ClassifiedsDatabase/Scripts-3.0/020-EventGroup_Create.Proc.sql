
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventGroup_Create]
       @eventId				INT,
	   @groupName			VARCHAR(50),
	   @maxGuests			INT = NULL,
	   @createdDate			DATETIME = NULL,
	   @createdDateUtc		DATETIME = NULL,
	   @createdBy			VARCHAR(50) = NULL,
	   @availableToAllTickets BIT = NULL,
	   @tickets				VARCHAR(MAX) = NULL,
	   @eventGroupId		INT = NULL OUTPUT
AS

BEGIN TRANSACTION
	INSERT INTO EventGroup (EventId, GroupName, MaxGuests, CreatedDateTime, CreatedDateTimeUtc, CreatedBy, AvailableToAllTickets)
	VALUES (@eventId, @groupName, @maxGuests, @createdDate, @createdDateUtc, @createdBy, @availableToAllTickets);

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