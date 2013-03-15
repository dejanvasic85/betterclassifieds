-- =============================================
-- Author:			Dejan Vasic
-- Create date:		24-Nov-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectByLineAdDesign]
	-- Add the parameters for the stored procedure here
	@LineDesignId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @adId int

	select @adId = lds.AdId from AdDesign lds where lds.AdDesignId = @LineDesignId

	select onl.AdDesignId, onl.ContactName, onl.ContactType,
			onl.ContactValue, onl.[Description], onl.Heading, 
			onl.HtmlText, onl.LocationAreaId, onl.LocationId,
			onl.NumOfViews, onl.OnlineAdId, onl.Price
	from OnlineAd onl
		inner join  AdDesign ds on ds.AdDesignId = onl.AdDesignId
	where ds.AdId = @adId

END


