-- =============================================
-- Author:		Dejan Vasic
-- Create date: 3rd June 2010
-- Modifications
-- Date			Author			Description
-- ==================================================================
CREATE PROCEDURE [dbo].[spPublicationSpecialRateAdd]

	  @PublicationId		INT
	, @MainCategoryId		INT
	, @SpecialRateId		INT
	, @ClearCurrentRates	BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION

    declare @publicationCategoryId	int
    declare @publicationAdTypeId	int
    declare @specialRateCount		bit
    
    SELECT	  @publicationCategoryId = pc.PublicationCategoryId
			, @publicationAdTypeId = pat.PublicationAdTypeId
    FROM	PublicationCategory pc
	JOIN	PublicationAdType pat
		ON	pat.PublicationId = @PublicationId			
    WHERE	pc.MainCategoryId = @MainCategoryId
		AND	pc.PublicationId = @PublicationId;
	
	IF	@ClearCurrentRates = 1
	begin
		DELETE PublicationSpecialRate 
		WHERE	PublicationCategoryId = @publicationCategoryId;
	end
	
	-- Check whether we need to add (if already assigned then ignore)
	SELECT	@specialRateCount = COUNT(*) 
	FROM	PublicationSpecialRate psr
	WHERE	psr.SpecialRateId = @SpecialRateId 
		AND psr.PublicationAdTypeId = @publicationAdTypeId
		AND psr.PublicationCategoryId = @publicationCategoryId;
	
	IF @specialRateCount = 0
	begin
		-- Create the new Publication Special Rate
		INSERT INTO PublicationSpecialRate
			(SpecialRateId, PublicationAdTypeId, PublicationCategoryId)
		VALUES
			(@SpecialRateId, @publicationAdTypeId, @publicationCategoryId);
	end
	COMMIT TRANSACTION
	
END



