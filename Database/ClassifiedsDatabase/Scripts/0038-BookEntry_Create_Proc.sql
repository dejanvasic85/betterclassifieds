
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookEntry_Create]
       @adBookingId			INT,
	   @startDate			DATETIME,
	   @insertions			INT,
	   @publicationId		INT,
	   @editionPrice		MONEY = NULL,
	   @publicationPrice	MONEY = NULL,
	   @ratecardId			INT = NULL,
	   @ratecardType		VARCHAR(10) = 'Ratecard'  -- Obsolete but just default it for sake of data integrity
AS
/*
	exec BookEntry_Create
		@adBookingId = 14447,
		@startDate	= '14-MAR-2015',
		@insertions = 1,
		@publicationId = 1,
		@editionPrice = 10,
		@ratecardId = 4,
		@publicationPrice = 100

*/
BEGIN TRANSACTION

	INSERT INTO [dbo].[BookEntry]
           ([EditionDate]
           ,[AdBookingId]
           ,[PublicationId]
           ,[EditionAdPrice]
           ,[PublicationPrice]
           ,[RateType]
           )
    	
	SELECT	TOP (@insertions) 
			EditionDate,
			@adBookingId,
			@publicationId,
			@editionPrice,
			@publicationPrice,
			@ratecardType
	FROM	Edition
	WHERE	PublicationId = @publicationId
	AND		EditionDate >= @startDate
	

COMMIT