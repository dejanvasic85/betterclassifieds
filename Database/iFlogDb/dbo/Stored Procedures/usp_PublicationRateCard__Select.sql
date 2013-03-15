-- =============================================
-- Author:		Dejan Vasic
-- Create date: 01-JAN-2010
-- Modifications
-- 20-FEB-2011	DV	Including all rate details
-- 20-FEB-2011	DV	Allowing ad type id to be passed in
-- =============================================
CREATE PROCEDURE [dbo].[usp_PublicationRateCard__Select]
	 @PublicationId		INT,
	 @MainCategoryId	INT,
	 @AdTypeId			INT = 1   -- Default To Line Ad Type
AS
/*
	e.g.
		exec usp_PublicationRateCard__Select
			@PublicationId	= 1,
			@MainCategoryId = 8;
*/
BEGIN
	
	declare @publicationCategoryId	int,
			@publicationAdTypeId	int,
			@publicationName		varchar(50);
    
    -- Fetch the publication information first
    SELECT	  @publicationCategoryId = pc.PublicationCategoryId
			, @publicationAdTypeId = pat.PublicationAdTypeId
			, @publicationName = p.Title
    FROM	PublicationCategory pc
	JOIN	PublicationAdType pat
		ON	pat.PublicationId = @PublicationId			
		AND	pat.AdTypeId = @AdTypeId
	JOIN	Publication p
		ON	p.PublicationId = pc.PublicationId
    WHERE	pc.MainCategoryId = @MainCategoryId
		AND	pc.PublicationId = @PublicationId;
		
	select	
			@PublicationId as PublicationId,
			@publicationName as PublicationName,
			pr.PublicationRateId, 
			pr.PublicationAdTypeId, 
			pr.PublicationCategoryId, 
			pc.Title as PublicationCategoryName,
			br.Title as RatecardName,
			r.RatecardId, 
			r.MinCharge,
			r.MaxCharge,
			r.RatePerMeasureUnit,
			r.MeasureUnitLimit,
			r.PhotoCharge,
			r.BoldHeading,
			r.LineAdSuperBoldHeading,
			r.LineAdColourHeading,
			r.LineAdColourBorder,
			r.LineAdColourBackground,
			r.LineAdExtraImage,
			r.OnlineEditionBundle
	from	dbo.PublicationRate pr
	join	dbo.PublicationCategory pc on pc.PublicationCategoryId = pr.PublicationCategoryId
	join	dbo.Ratecard r on r.RatecardId = pr.RatecardId
	join	dbo.BaseRate br on br.BaseRateId = r.BaseRateId
	where	pr.PublicationAdTypeId = @publicationAdTypeId
		and	pr.PublicationCategoryId	= @publicationCategoryId
	
	
END


