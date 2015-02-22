GO

/****** Object:  StoredProcedure [dbo].[Booking_Create]    Script Date: 22/02/2015 3:44:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Ratecard_FetchForPublicationCategory]
       @publicationId	int,
	   @categoryId		int
AS

SELECT	r.*
FROM	Ratecard r
JOIN	PublicationRate pr
	ON	pr.RatecardId = r.RatecardId
JOIN	PublicationCategory pc
	ON	pr.PublicationCategoryId = pc.PublicationCategoryId
	AND pc.MainCategoryId = @categoryId 
	AND pc.PublicationId = @publicationId

GO


