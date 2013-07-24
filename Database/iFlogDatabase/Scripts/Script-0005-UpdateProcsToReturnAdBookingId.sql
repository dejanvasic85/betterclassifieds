
GO
/****** Object:  StoredProcedure [dbo].[psp_Betterclassified_GetOnlineAd]    Script Date: 07/24/2013 21:45:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:			Uche Njoku
-- Create date:		16-Jun-2009
-- Modifications
-- Date		Author		Description
-- 5-11-09	Dejan V		Added check for ad design status to be pending or approved only.	
-- 24-07-13 Dejan V		Added Adbooking Id to the search result	
-- =============================================
ALTER procedure [dbo].[psp_Betterclassified_GetOnlineAd]
	@keyword varchar(25) = '',
	@locationId int = null,
	@areaId int = null,
	@parentCategoryId int= null,
	@subCategoryId int = null,
	@pageSize int = -1,
	@pageIndex int = 0,
	@totalPopulationSize int output
	as
begin
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	set nocount on
	
	declare @categoryId int;
	-- ** CATEGORIES VAR TABLE ** --
	declare @tmpCat table (MainCategoryId int, ParentId int)

	begin
		if @SubCategoryId IS NULL 
			if @parentCategoryId IS NULL
				insert into @tmpCat select MainCategoryId, ParentId from Maincategory
			else
				insert into @tmpCat select MainCategoryId, ParentId from MainCategory where MainCategory.ParentId = @parentCategoryId
		else
			insert into @tmpCat
			select		MainCategoryId, ParentId from MainCategory where MainCategory.MainCategoryId = @subCategoryId
	end

		
	-- Setup  wildcard
	set @keyword = '%' + @keyword + '%';
			
	select @totalPopulationSize = count(*)
	from dbo.AdBooking bk
		inner join MainCategory c
			on c.MainCategoryId = bk.MainCategoryId
				
		inner join ad
			on ad.AdId = bk.AdId
		inner join dbo.AdDesign d
			on d.AdId = bk.AdId
		inner join dbo.OnlineAd oad
			on oad.AdDesignId = d.AdDesignId
		inner join LocationArea la
			on la.LocationAreaId = oad.LocationAreaId
	where 
			((oad.Description like @keyword) or (oad.Heading like @keyword))
		and
			(
				(@locationId is null) or (@LocationId is not null and oad.LocationId = @locationId)
			)
		and 
			(
				((@areaId is null) or (@areaId is not null and oad.LocationAreaId = @areaId))
			)
		and
			(
				bk.EndDate > getdate() AND bk.StartDate <= getdate()
			)
		and
			(
				bk.BookingStatus = 1
			)
		and
			(
				d.[Status] = 1 OR d.[Status] = 2 -- check if Status is approved or pending
			) 
		and
			(
					(bk.MainCategoryId in ( select ct.MainCategoryId from @tmpCat ct ))
					--(@categoryId is null) or (@categoryId is not null and c.MainCategoryId = @categoryId )
			)
			
	-- Setup paging
	if (@pageSize <= 0)
	begin
		set @pageSize = @totalPopulationSize
		set @pageIndex = 0
	end

	;with onlineAds as
	(
	select 
		oad.OnlineAdId,
		c.MainCategoryId,
		oad.Heading,
		oad.NumOfViews,
		oad.Price,
		bk.StartDate as Posted,
		bk.EndDate as Ending,
		bk.AdBookingId,
		la.Title as Location,
		LEFT(oad.[Description], 100) + '...' as [AdText],
		c.Title as Category,
		ROW_NUMBER() OVER(ORDER BY bk.AdBookingId desc, bk.adid) as RowNumber,
		( 
				select top 1 DocumentID from dbo.AdGraphic
					inner join dbo.AdDesign
						on AdDesign.AdDesignId = AdGraphic.AdDesignId
					and AdDesign.AdDesignId = d.AdDesignId
		) as DocumentId

	from dbo.AdBooking bk
		inner join MainCategory c
			on c.MainCategoryId = bk.MainCategoryId
		inner join ad
			on ad.AdId = bk.AdId
		inner join dbo.AdDesign d
			on d.AdId = bk.AdId
		inner join dbo.OnlineAd oad
			on oad.AdDesignId = d.AdDesignId
		inner join LocationArea la
			on la.LocationAreaId = oad.LocationAreaId
			
	where 
			((oad.Description like @keyword) or (oad.Heading like @keyword))
		and
			((@locationId is null) or (@LocationId is not null and oad.LocationId = @locationId))
		and 
			((@areaId is null) or (@areaId is not null and oad.LocationAreaId = @areaId))
		and
			(bk.EndDate > getdate() AND bk.StartDate <= getdate())
		and
			(bk.BookingStatus = 1)
		and
			(d.[Status] = 1 OR d.[Status] = 2) -- check if Status is approved or pending
		and
			(bk.MainCategoryId in ( select ct.MainCategoryId from @tmpCat ct ))
			--(@categoryId is null) or (@categoryId is not null and c.MainCategoryId = @categoryId )
		) 
		
	Select * from onlineAds where RowNumber between ((@pageSize * @pageIndex) + 1) and (@pageSize * (@pageIndex + 1))
end



GO
/****** Object:  StoredProcedure [dbo].[spOnlineAdSelectRecentlyAdded]    Script Date: 07/24/2013 22:44:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Dejan Vasic
-- Create date: 9th March 2009

-- Modifications
-- Date			Author			Description
--
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- 21-5-2010	Dejan Vasic		Selecting only the graphics that contain a graphic
-- 25-7-2013	Dejan Vasic		Added AdbookingId to the result set
-- =============================================
ALTER PROCEDURE [dbo].[spOnlineAdSelectRecentlyAdded]
	-- Add the parameters for the stored procedure here
	@bookStatus INT
AS

DECLARE @tempTb TABLE (AdDesignId INT, DocumentId NVARCHAR(100))

BEGIN
	INSERT INTO @tempTb
		SELECT			AdDesign.AdDesignId, 
						(SELECT TOP 1 DocumentId FROM AdGraphic WHERE AdGraphic.AdDesignId = AdDesign.AdDesignId)
		FROM			AdDesign 
		INNER JOIN		AdBooking bk ON bk.AdId = AdDesign.AdId
		WHERE			bk.BookingStatus = @bookStatus
						AND bk.EndDate > GETDATE()
						AND bk.StartDate < GETDATE() AND AdDesign.[Status] <> 3
		ORDER BY		bk.StartDate DESC
END

BEGIN
	SELECT DISTINCT TOP	9	ad.OnlineAdId,
						bk.AdBookingId, 
						LEFT(ad.Heading, 40) + '...' as Heading,
						LEFT(ad.[Description], 80) + '...' as [AdText],  
						gr.DocumentId, bk.StartDate, bk.AdBookingId, 
						(Select main.Title FROM MainCategory main WHERE main.MainCategoryId = mc.ParentId) AS Category,
						(Select main.MainCategoryId FROM MainCategory main WHERE main.MainCategoryId = mc.ParentId) AS CategoryId

	FROM		OnlineAd ad
	INNER JOIN	AdDesign ds ON ds.AdDesignId = ad.AdDesignId
	INNER JOIN	AdBooking bk ON	bk.AdId = ds.AdId
	INNER JOIN	MainCategory mc ON mc.MainCategoryId = bk.MainCategoryId
	LEFT JOIN	@tempTb gr ON gr.AdDesignId = ad.AdDesignId
	WHERE	bk.BookingStatus = @bookStatus
			AND bk.StartDate < GETDATE() 
			AND bk.EndDate > GETDATE()
			AND ds.[Status] <> 3
			AND gr.DocumentID IS NOT NULL
	ORDER BY bk.AdBookingId DESC
END	

