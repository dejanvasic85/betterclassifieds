
/****** Object: SqlProcedure [dbo].[BookedEvent_GetCurrent] Script Date: 15/08/2017 7:22:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[BookedEvent_GetCurrent]
	@eventId INT = NULL
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @today as DATETIME = GETDATE();

	SELECT		be.*
			,	mc.FontIcon AS CategoryFontIcon
			,	mc.Title AS ParentCategoryName
			,	STUFF((	select	', ' + DocumentID 
						from		dbo.AdGraphic gr
						where gr.AdDesignId = be.AdDesignId										 
						FOR XML PATH('')
						)	,1,2,'' ) as DocumentIds

	FROM	dbo.BookedEvents be
	INNER JOIN	MainCategory mc 
		ON	mc.MainCategoryId = be.ParentCategoryId
	WHERE	be.StartDate <= @today 
		AND be.EndDate >= @today
		AND	be.EventId = ISNULL(@eventId, be.EventId)
	ORDER BY be.[Priority] DESC, be.BookingDate DESC
END
