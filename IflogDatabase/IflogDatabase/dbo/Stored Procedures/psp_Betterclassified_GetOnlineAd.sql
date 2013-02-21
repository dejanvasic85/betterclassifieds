-- =============================================
-- Author:			Uche Njoku
-- Create date:		16-Jun-2009
-- Modifications
-- Date		Author		Description
-- 5-11-09	Dejan V		Added check for ad design status to be pending or approved only.		
-- =============================================
CREATE procedure [dbo].[psp_Betterclassified_GetOnlineAd]
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
		oad.OnlineAdId,c.MainCategoryId,
		oad.Heading,
		oad.NumOfViews,
		oad.Price,
		bk.StartDate as Posted,
		bk.EndDate as Ending,
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



