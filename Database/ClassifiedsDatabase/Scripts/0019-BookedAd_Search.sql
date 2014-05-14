
-- This is just a simple renaming of a proc
DROP PROCEDURE [dbo].[spSearchBookedAds]


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BookedAd_Search]
       @searchTerm nvarchar(50) = '~',
       @categoryIds varchar(20) = null,
       @locationIds varchar(20) = null,
       @areaIds varchar(20) = null,
       @order int = 0,
       @pageIndex int = 0,
       @pageSize int = -1

AS
BEGIN
	SET NOCOUNT ON;

	-- Set default values
	SET @searchTerm=isnull(@searchTerm,'~');
    if (@pageSize <= 0)
    BEGIN
        SET @pageIndex = 0
        SET @pageSize = 20
    END

	;WITH Results
	AS 
	(
		SELECT	ROW_NUMBER() OVER ( ORDER BY
			CASE WHEN	@order = 0
				THEN		[OnlineAdId]
			END DESC,
			CASE WHEN	@order = 1
				THEN	OnlineAdId
			END ASC,
			CASE WHEN	@order = 2
				THEN	[Price]
			END ASC,
			CASE WHEN	@order = 3
				THEN	[Price]
			END DESC
			) AS RowNumber
		, COUNT(*) OVER() AS TotalCount
		, t1.* FROM 
			( 
				SELECT		b.* , KEY_TBL.[Rank] 
				FROM		dbo.BookedAds b
				INNER JOIN	FREETEXTTABLE(dbo.BookedAds, ([Description], [Heading]), @searchTerm) AS KEY_TBL
					ON		b.OnlineAdId = KEY_TBL.[KEY]
				UNION
				SELECT	*, OnlineAdId As [Rank]
				FROM	BookedAds 
				WHERE	@searchTerm = '~'
					
			) AS t1

		WHERE	( t1.StartDate <= GETDATE() AND t1.EndDate > GETDATE() )
		AND		(@categoryIds IS NULL OR t1.CategoryId in (select [data] from dbo.SplitStringToInt(@categoryIds, ',')) or t1.ParentCategoryId in (select data from dbo.SplitStringToInt(@categoryIds, ',')))
		AND		(@locationIds IS NULL OR t1.LocationId in (select data from dbo.SplitStringToInt(@locationIds, ',')))
		AND		(@areaIds IS NULL OR t1.LocationAreaId in (select data from dbo.SplitStringToInt(@areaIds, ','))) 
	)

	SELECT	r.* 
			, mc.Title as ParentCategoryName
			, STUFF((	select	', ' + DocumentID 
						from		dbo.AdGraphic gr
						where gr.AdDesignId = r.AdDesignId										 
						FOR XML PATH('')
						)	,1,2,'' ) as DocumentIds

			, STUFF((	select	', ' + p.Title
						from		dbo.BookEntry be
						inner join	dbo.Publication p on p.PublicationId = be.PublicationId and p.PublicationTypeId != 3
						where be.AdBookingId = r.AdId
						group by p.Title
						FOR XML PATH('')
						)	,1,2,'' ) as Publications
	FROM		Results r
	INNER JOIN	MainCategory mc 
			ON	mc.MainCategoryId = r.ParentCategoryId
	WHERE RowNumber between ((@pageSize * @pageIndex) + 1) and (@pageSize * (@pageIndex + 1))
END

