DROP FUNCTION [dbo].[SplitStringToInt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create FUNCTION [dbo].[SplitStringToInt]
(
    @String NVARCHAR(4000),
    @Delimiter NCHAR(1)
)
RETURNS TABLE 
AS
RETURN 
(
    WITH Split(stpos,endpos) 
    AS(
        SELECT 0 AS stpos, CHARINDEX(@Delimiter,@String) AS endpos
        UNION ALL
        SELECT endpos+1, CHARINDEX(@Delimiter,@String,endpos+1)
            FROM Split
            WHERE endpos > 0
    )
    SELECT 'Id' = ROW_NUMBER() OVER (ORDER BY (SELECT 1)),
        'Data' = CONVERT(INT,SUBSTRING(@String,stpos,COALESCE(NULLIF(endpos,0),LEN(@String)+1)-stpos))
    FROM Split
)

GO

DROP FULLTEXT INDEX ON [dbo].[OnlineAdView]

GO

DROP INDEX [ClusteredIndex-OnlineAdId] ON [dbo].[OnlineAdView] WITH ( ONLINE = OFF )
GO

DROP FULLTEXT CATALOG [OnlineAdSearch]
GO

DROP VIEW [dbo].[OnlineAdView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create VIEW [dbo].[OnlineAdView]
WITH SCHEMABINDING 
AS
SELECT        dbo.OnlineAd.OnlineAdId, dbo.OnlineAd.AdDesignId, dbo.OnlineAd.Heading, dbo.OnlineAd.Description, dbo.OnlineAd.HtmlText, dbo.OnlineAd.Price, dbo.OnlineAd.LocationId, dbo.OnlineAd.LocationAreaId, 
                         dbo.OnlineAd.ContactName, dbo.OnlineAd.ContactType, dbo.OnlineAd.ContactValue, dbo.OnlineAd.NumOfViews, dbo.OnlineAd.OnlineAdTag, dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, 
                         dbo.AdBooking.AdBookingId, dbo.AdBooking.BookReference, dbo.AdBooking.UserId, dbo.AdBooking.BookingStatus, dbo.AdBooking.MainCategoryId AS CategoryId, dbo.AdBooking.BookingDate, 
                         dbo.LocationArea.Title AS AreaTitle, dbo.Location.Title AS LocationTitle, dbo.MainCategory.Title AS CategoryTitle, dbo.MainCategory.ParentId AS CategoryParentId, dbo.Ad.AdId
FROM            dbo.AdDesign AS AdDesign INNER JOIN
                         dbo.LocationArea INNER JOIN
                         dbo.OnlineAd ON dbo.LocationArea.LocationAreaId = dbo.OnlineAd.LocationAreaId INNER JOIN
                         dbo.Location ON dbo.LocationArea.LocationId = dbo.Location.LocationId AND dbo.OnlineAd.LocationId = dbo.Location.LocationId ON AdDesign.AdDesignId = dbo.OnlineAd.AdDesignId INNER JOIN
                         dbo.Ad ON AdDesign.AdId = dbo.Ad.AdId INNER JOIN
                         dbo.MainCategory INNER JOIN
                         dbo.AdBooking ON dbo.MainCategory.MainCategoryId = dbo.AdBooking.MainCategoryId ON dbo.AdBooking.AdId = dbo.Ad.AdId 
WHERE        (dbo.AdBooking.BookingStatus = 1) AND (AdDesign.AdTypeId = 2)


GO

SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO

CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-OnlineAdId] ON [dbo].[OnlineAdView]
(
	[OnlineAdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE FULLTEXT CATALOG [OnlineAdSearch]WITH ACCENT_SENSITIVITY = OFF

GO

CREATE FULLTEXT INDEX ON [dbo].[OnlineAdView](
[AreaTitle] LANGUAGE [English], 
[BookReference] LANGUAGE [English], 
[CategoryTitle] LANGUAGE [English], 
[Description] LANGUAGE [English], 
[Heading] LANGUAGE [English], 
[LocationTitle] LANGUAGE [English], 
[OnlineAdTag] LANGUAGE [English], 
[UserId] LANGUAGE [English])
KEY INDEX [ClusteredIndex-OnlineAdId]ON ([OnlineAdSearch], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)


GO

DROP PROCEDURE [dbo].[spSearchOnlineAdFREETEXT]
GO

-- =============================================
-- Author:		Uche Njoku
-- Create date: 1/02/2014
-- Description:	Search for ad using FREETEXT 
-- =============================================
CREATE PROCEDURE [dbo].[spSearchOnlineAdFREETEXT]
	@searchTerm nvarchar(50) = '~',
	@categoryIds varchar(20) = null,
	@locationIds varchar(20) = null,
	@areaIds varchar(20) = null

AS
BEGIN
	SET NOCOUNT ON;

	SET @searchTerm=isnull(@searchTerm,'~')

	select  t1.*, ( 
				select top 1 DocumentID from dbo.AdGraphic
					inner join dbo.AdDesign
						on AdDesign.AdDesignId = AdGraphic.AdDesignId
					and AdDesign.AdDesignId = t1.AdDesignId
		) as DocumentId 
	from	
		(select top 128 o.* from [dbo].[OnlineAdview] o
	
		INNER JOIN 
			FREETEXTTABLE([dbo].[OnlineAdview],
		(Description, Heading), @searchTerm) AS KEY_TBL
		ON  (o.OnlineAdId = KEY_TBL.[KEY]) 
		and KEY_TBL.Rank >2
		ORDER BY RANK desc
		union ( select * from [dbo].[OnlineAdview] 
		 where @searchTerm = '~')	
		) as t1
	where
	(@categoryIds is null or t1.CategoryId in (select [data] from dbo.[SplitStringToInt](@categoryIds,',')) or t1.CategoryParentId in (select data from dbo.[SplitStringToInt](@categoryIds,',')))
	and (@locationIds is null or t1.LocationId in (select data from dbo.[SplitStringToInt](@locationIds,',')))
	and (@areaIds is null or t1.LocationAreaId in (select data from dbo.[SplitStringToInt](@areaIds,',')))
	
	ORDER BY OnlineAdId desc
	
	END
GO






