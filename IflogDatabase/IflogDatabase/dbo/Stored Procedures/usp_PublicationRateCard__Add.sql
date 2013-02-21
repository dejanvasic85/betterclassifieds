-- =============================================
-- Author:		Dejan Vasic
-- Create date: 02-JAN-2010
-- =============================================

CREATE PROCEDURE [dbo].[usp_PublicationRateCard__Add]

	  @PublicationId		INT
	, @MainCategoryId		INT
	, @RatecardId			INT
	, @ClearCurrentRates	BIT = 1
AS
/*
	exec dbo.spPublicationRateCardAdd
		@PublicationId		= 1, -- Drum media
		@MainCategoryId		= 8, -- Administration
		@RatecardId			= 1,
		@ClearCurrentRates	= 1;
		
*/
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

    declare @publicationCategoryId	int,
			@publicationAdTypeId	int,
			@ratecardCount			int;
    
    SELECT	  @publicationCategoryId = pc.PublicationCategoryId
			, @publicationAdTypeId = pat.PublicationAdTypeId
    FROM	PublicationCategory pc
	JOIN	PublicationAdType pat
		ON	pat.PublicationId = @PublicationId			
    WHERE	pc.MainCategoryId = @MainCategoryId
		AND	pc.PublicationId = @PublicationId;
	
	IF	@ClearCurrentRates = 1
	begin
		DELETE	PublicationRate 
		WHERE	PublicationCategoryId = @publicationCategoryId;
	end
	
	-- Check whether we need to add (if already assigned then ignore)
	SELECT	@ratecardCount = COUNT(*) 
	FROM	PublicationRate pr
	WHERE	pr.RatecardId				= @RatecardId
		AND pr.PublicationAdTypeId		= @publicationAdTypeId
		AND pr.PublicationCategoryId	= @publicationCategoryId;
	
	IF @ratecardCount = 0
	begin
		-- Create the new Publication Special Rate
		INSERT INTO PublicationRate
			(RatecardId, PublicationAdTypeId, PublicationCategoryId)
		VALUES
			(@RatecardId, @publicationAdTypeId, @publicationCategoryId);
	end
	
	
	COMMIT TRANSACTION
	
END



