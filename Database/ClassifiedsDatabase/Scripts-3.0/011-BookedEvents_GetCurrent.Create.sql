SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BookedEvent_GetCurrent]
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

END