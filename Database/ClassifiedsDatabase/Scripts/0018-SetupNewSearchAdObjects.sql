
/****** Object:  View [dbo].[BookedAds]    Script Date: 5/05/2014 9:35:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[BookedAds]
/*
	select * from BookedAds
*/
WITH SCHEMABINDING 
AS
SELECT	  o.[OnlineAdId]
		, o.[AdDesignId]
		, o.[Heading]
		, o.[Description]
		, o.[HtmlText]
		, o.[Price]
		, o.[LocationId]
		, o.[LocationAreaId]
		, o.[ContactName]
		, o.[ContactType]
		, o.[ContactValue]
		, o.[NumOfViews]
		, o.[OnlineAdTag]
		, bk.AdBookingId as AdId
		, bk.BookingDate
		, bk.EndDate
		, bk.UserId
		, bk.StartDate 		
		, c.Title as CategoryName
		, bk.MainCategoryId as CategoryId
		, c.ParentId as ParentCategoryId
		, lo.Title as LocationName
		, la.Title as LocationAreaName
FROM    dbo.OnlineAd o
	INNER JOIN	dbo.AdDesign ds ON ds.AdDesignId = o.AdDesignId
	INNER JOIN	dbo.AdBooking bk ON bk.AdId = ds.AdId
	INNER JOIN	dbo.MainCategory c ON c.MainCategoryId = bk.MainCategoryId
	INNER JOIN	dbo.Location lo ON lo.LocationId = o.LocationId
	INNER JOIN	dbo.LocationArea la ON la.LocationAreaId = o.LocationAreaId
WHERE bk.BookingStatus = 1


GO

SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE name = 'ClusteredIndex-BookedAds-OnlineAdId' AND object_id = OBJECT_ID('BookedAds'))
begin
	CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-BookedAds-OnlineAdId] ON [dbo].[BookedAds]
	(
		[OnlineAdId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
end

GO


CREATE FULLTEXT CATALOG [BookedAdsFullText] WITH ACCENT_SENSITIVITY = OFF

CREATE FULLTEXT INDEX ON [dbo].[BookedAds](
[LocationAreaName] LANGUAGE [English], 
[CategoryName] LANGUAGE [English], 
[Description] LANGUAGE [English], 
[Heading] LANGUAGE [English], 
[LocationName] LANGUAGE [English], 
[OnlineAdTag] LANGUAGE [English], 
[UserId] LANGUAGE [English])
KEY INDEX [ClusteredIndex-BookedAds-OnlineAdId] ON ([BookedAdsFullText], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)


GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spSearchBookedAds]
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

