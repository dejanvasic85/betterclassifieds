
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookedAd_GetById]
       @adId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	b.* 
			, mc.Title as ParentCategoryName
			, STUFF((	select	', ' + DocumentID 
						from		dbo.AdGraphic gr
						where gr.AdDesignId = b.AdDesignId										 
						FOR XML PATH('')
						)	,1,2,'' ) as DocumentIds

			, STUFF((	select	', ' + p.Title
						from		dbo.BookEntry be
						inner join	dbo.Publication p on p.PublicationId = be.PublicationId and p.PublicationTypeId != 3
						where be.AdBookingId = b.AdId
						group by p.Title
						FOR XML PATH('')
						)	,1,2,'' ) as Publications
	FROM		BookedAds b
	INNER JOIN	MainCategory mc 
			ON	mc.MainCategoryId = b.ParentCategoryId
	WHERE b.AdId = @adId
END