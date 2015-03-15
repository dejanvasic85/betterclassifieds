

ALTER PROCEDURE [dbo].[BookEntry_Create]
       @adBookingId			INT,
	   @startDate			DATETIME,
	   @insertions			INT,
	   @publicationId		INT,
	   @editionPrice		MONEY = NULL,
	   @publicationPrice	MONEY = NULL,
	   @ratecardId			INT = NULL,
	   @ratecardType		VARCHAR(10) = 'Ratecard'  -- Obsolete but just default it for sake of data integrity
AS

BEGIN TRANSACTION

	INSERT INTO [dbo].[BookEntry]
           ([EditionDate]
           ,[AdBookingId]
           ,[PublicationId]
           ,[EditionAdPrice]
           ,[PublicationPrice]
           ,[RateType]
		   ,[BaseRateId]
           )
    	
	SELECT	TOP (@insertions) 
			EditionDate,
			@adBookingId,
			@publicationId,
			@editionPrice,
			@publicationPrice,
			@ratecardType,
			@ratecardId
	FROM	Edition
	WHERE	PublicationId = @publicationId
	AND		EditionDate >= @startDate
	

COMMIT