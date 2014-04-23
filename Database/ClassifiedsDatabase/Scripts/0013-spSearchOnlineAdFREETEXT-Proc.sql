
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSearchOnlineAdFREETEXT]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Uche Njoku
-- Create date: 1/02/2014
-- Description:	Search for ad using FREETEXT 
-- Todo (23rd-04-2014) - needto add paging
-- =============================================
CREATE PROCEDURE [dbo].[spSearchOnlineAdFREETEXT]
	@searchTerm nvarchar(50) = ''~'',
	@categoryIds varchar(20) = null,
	@locationIds varchar(20) = null,
	@areaIds varchar(20) = null

AS
BEGIN
	SET NOCOUNT ON;

	SET @searchTerm=isnull(@searchTerm,''~'')

	select top 120  t1.*, ( 
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
		 where @searchTerm = ''~'')	
		) as t1
	where
	(@categoryIds is null or t1.CategoryId in (select [data] from dbo.[SplitStringToInt](@categoryIds,'','')) or t1.CategoryParentId in (select data from dbo.[SplitStringToInt](@categoryIds,'','')))
	and (@locationIds is null or t1.LocationId in (select data from dbo.[SplitStringToInt](@locationIds,'','')))
	and (@areaIds is null or t1.LocationAreaId in (select data from dbo.[SplitStringToInt](@areaIds,'','')))
	
	ORDER BY OnlineAdId desc
	
	END' 
END
GO
