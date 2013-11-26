SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_AdBookings_Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 24-06-2010
-- Modifications
-- Date			Author			Description
-- 26-07-2013	Dejan Vasic		Replacing AdDesignId with AdBookingId
-- ==================================================================
CREATE proc [dbo].[psp_AdBookings_Select]

	-- Add the parameters for the stored procedure here
	@AdBookingId		int			= null,
	@BookReference		varchar(10) = null,
	@Username			varchar(50) = null,
	@BookingDateStart	datetime	= null,
	@BookingDateEnd		datetime	= null,
	@BookingStatus		int			= null,
	@PublicationId		int			= null,
	@ParentCategoryId	int			= null,
	@SubCategoryId		int			= null,
	@EditionStartDate	datetime	= null,
	@EditionEndDate		datetime	= null,
	@AdSearchText		varchar(50) = null,
	
	-- Pagination parameters
	@StartRowIndex			int				= 0,
	@MaximumRows			numeric			= 20,
	@SortExpression			varchar(max)	= null,
	@FilterExpression		varchar(max)	= null,	
	@TotalRowCount			int				= null output,
	
	@DebugMode				bit				= 0
as

set nocount on;

declare @SelectSQL		nvarchar(max),
		@CountSQL		nvarchar(max),
		@Params			nvarchar(max)


-- Book Reference Filter
IF @BookReference IS NOT NULL AND @BookReference <> ''''
begin 
	SELECT	@AdBookingId = ISNULL(bk.AdBookingId, 0) 
	FROM	AdBooking bk
	WHERE	bk.BookReference = @BookReference;
	-- If no records returned then set BookingId to 0 
	SELECT	@AdBookingId = ISNULL(@AdBookingId, 0);
end

-- Publication and BookEntry Filter
declare @BookEntryPub	table (AdbookingId int)
INSERT INTO @BookEntryPub (AdbookingId)
	SELECT  DISTINCT be.AdbookingId
	FROM	BookEntry be
	WHERE	(@PublicationId IS NULL) OR (@PublicationId IS NOT NULL AND be.PublicationId = @PublicationId)
		AND	((@EditionStartDate IS NULL AND @EditionEndDate IS NULL)
				OR	(@EditionStartDate <= be.EditionDate AND @EditionEndDate >= be.EditionDate));

-- Ad Search Text
declare @OnlineTextSearch table (AdbookingId int)
INSERT INTO @OnlineTextSearch (AdbookingId)
	SELECT DISTINCT bk.AdBookingId
	FROM	AdDesign ds 
	LEFT JOIN	OnlineAd onl 
		ON	onl.AdDesignId = ds.AdDesignId
	LEFT JOIN	LineAd lin 
		ON	lin.AdDesignId = ds.AdDesignId
	JOIN	AdBooking bk
		ON	bk.AdId = ds.AdId
	WHERE	(@AdSearchText IS NULL OR @AdSearchText = '''')
			OR (onl.Heading LIKE ''%'' + @AdSearchText + ''%'') 
			OR (onl.[Description] LIKE ''%'' + @AdSearchText + ''%'')
			OR (lin.AdText LIKE ''%'' + @AdSearchText + ''%'') 
			OR (lin.AdHeader LIKE ''%'' + @AdSearchText + ''%'')

-- *********************************************************
-- Prepare the temporary table to store the filtered results
-- *********************************************************
create table #BookingResults 
		( RowNumber int
		, AdBookingId int
		, BookReference varchar(10)
		, ParentCategory varchar(50)
		, SubCategory varchar(50)
		, BookingDate datetime
		, BookingType varchar(20)
		, TotalPrice money
		, UserId varchar(50)
		, BookingStatus int)

INSERT INTO #BookingResults
	SELECT 
			ROW_NUMBER() OVER (order by bk.AdBookingId desc)
		,	bk.AdBookingId
		,	bk.BookReference
		,	(SELECT pc.Title FROM MainCategory pc WHERE pc.MainCategoryId = mc.ParentId) As ParentCategory
		,	mc.Title as SubCategory
		,	bk.BookingDate
		,	bk.BookingType
		,	bk.TotalPrice
		,	bk.UserId
		,	bk.BookingStatus
	FROM	AdBooking bk
	JOIN	MainCategory mc
		ON	mc.MainCategoryId = bk.MainCategoryId
	JOIN	@BookEntryPub bep
		ON	bep.AdbookingId = bk.AdBookingId
	JOIN	@OnlineTextSearch ots
		ON	ots.AdbookingId = bk.AdBookingId
	WHERE	((@Username IS NULL) OR (bk.UserId LIKE @Username + ''%''))
		AND	bk.AdBookingId = ISNULL(@AdBookingId, bk.AdBookingId)
		AND ((@BookingDateStart IS NULL AND @BookingDateEnd IS NULL) OR 
			 (@BookingDateStart <= bk.BookingDate AND @BookingDateEnd >= bk.BookingDate))
		AND	bk.BookingStatus = ISNULL(@BookingStatus, bk.BookingStatus)
		AND	((@ParentCategoryId IS NULL) OR (mc.ParentId = @ParentCategoryId))
		AND	((@SubCategoryId IS NULL) OR (mc.MainCategoryId = @SubCategoryId))
		
set @Params = ''@StartRowIndex int''

-- ****************************************************
-- Generate the Dynamic sql used for Paging and Sorting
-- ****************************************************
set @SelectSQL = 
''
	SELECT TOP '' + CONVERT(nvarchar, @MaximumRows) + '' 
		RowNumber,
		AdBookingId,
		BookReference,
		ParentCategory,
		SubCategory,
		BookingDate,
		BookingType,
		TotalPrice,
		UserId,
		BookingStatus
	FROM #BookingResults
	WHERE	RowNumber > @StartRowIndex
	ORDER BY '' + @SortExpression + '';
''

-- *********************
-- ** TOTAL ROW COUNT **
-- *********************
SELECT @TotalRowCount = COUNT(1) FROM #BookingResults

-- *******************
-- ** FETCH RESULTS **
-- *******************
execute sys.sp_executesql 
		@stmt		= @SelectSql,
		@params		= @params,
		
		-- Parameterized Pagination control properties
		@StartRowIndex					= @StartRowIndex
		
drop table #BookingResults' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_Betterclassified_GetActivitySummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[psp_Betterclassified_GetActivitySummary]
	@ReportDate DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @endDate	DATETIME;

	SET @endDate = DATEADD(MINUTE, 1439, @ReportDate)

	SELECT COUNT(1) AS TotalBookings, ISNULL(SUM(TotalPrice), 0) AS TotalIncome
	FROM dbo.AdBooking
	WHERE BookingDate BETWEEN @ReportDate AND @endDate
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_Betterclassified_GetAdBookingByEndDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Uche Njoku
-- Create date:		14-Jly-2009
-- Modifications
-- =============================================
Create procedure [dbo].[psp_Betterclassified_GetAdBookingByEndDate]
	@endDate dateTime
	
as
begin
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	set nocount on
	select b.* from dbo.AdBooking b
	where b.EndDate =@endDate
end
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_Betterclassified_GetLineAdBookingByLastEdition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[psp_Betterclassified_GetLineAdBookingByLastEdition]
	@EditionDate dateTime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select  
	UserId, 
	ab.BookReference, 
		(
		select AdDesignId from dbo.AdDesign ad2
		inner join dbo.AdType at2
			on ad2.AdTypeId = at2.AdTypeId
		and ad2.AdId = dg.AdId
		and at2.Code=''ONLINE''
	) as AdDesignId,
	g.LastEditionDate, 
	ab.MainCategoryId,
	ab.AdBookingId, 
	ab.BookingDate, 
	ab.StartDate, 
	ab.EndDate, 
	ab.TotalPrice 

	from  dbo.AdBooking ab
	inner join dbo.AdDesign dg
		on dg.AdId = ab.AdId 
	inner join dbo.AdType at
		on at.AdTypeId = dg.AdTypeId
		and at.Code = ''LINE''
	inner join 
	(
		select MAX (e1.EditionDate) as LastEditionDate, e1.AdBookingId 
		from dbo.BookEntry e1			
	 group by e1.AdBookingId
	) g
	on g.AdBookingId = ab.AdBookingId 
	and  g.LastEditionDate = @EditionDate
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_Betterclassified_GetOnlineAd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Uche Njoku
-- Create date:		16-Jun-2009
-- Modifications
-- Date		Author		Description
-- 5-11-09	Dejan V		Added check for ad design status to be pending or approved only.	
-- 24-07-13 Dejan V		Added Adbooking Id to the search result	
-- =============================================
CREATE procedure [dbo].[psp_Betterclassified_GetOnlineAd]
	@keyword varchar(25) = '''',
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
	set @keyword = ''%'' + @keyword + ''%'';
			
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
		LEFT(oad.[Description], 100) + ''...'' as [AdText],
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
end' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAdBookingSelectUserActive]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		28-Jun-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spAdBookingSelectUserActive]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@BookingStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	bk.AdBookingId, bk.StartDate, bk.EndDate, bk.TotalPrice, bk.BookReference, cat.Title,
			(SELECT count(AdDesignId) 
			FROM AdDesign inner join AdBooking bk1 on bk1.AdId = AdDesign.AdId 
			WHERE bk1.AdBookingId = bk.AdBookingId) as NumOfAds
	FROM AdBooking bk
	INNER JOIN MainCategory cat ON cat.MainCategoryId = bk.MainCategoryId
	WHERE bk.UserId = @UserId AND bk.EndDate > GETDATE() AND bk.BookingStatus = @BookingStatus
	ORDER BY bk.StartDate DESC
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAdBookingsSearch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 1st April 2009
-- Modifications
-- Date			Author			Description

-- 10-06-09		Dejan Vasic		Booking Status column accepts 4 now. So we query for only 1 and 3.
-- 15-11-09		Dejan Vasic		Refined entire search routine to allow more search criteria
-- ==================================================================
CREATE PROCEDURE [dbo].[spAdBookingsSearch]

	-- Add the parameters for the stored procedure here
	@AdDesignId			INT = null,
	@BookReference		NVARCHAR(10) = null,
	@Username			NVARCHAR(50) = null,
	@BookingDateStart	DATETIME = null,
	@BookingDateEnd		DATETIME = null,
	@BookingStatus		INT = null,
	@PublicationId		INT = null,
	@ParentCategoryId	INT = null,
	@MainCategoryId		INT = null,
	@AdSearchText		NVARCHAR(50) = null,
	@EditionStartDate	DATETIME,
	@EditionEndDate		DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT	
	bk.AdBookingId, bk.BookReference,
	(	select		MainCategory.Title 
		from		MainCategory 
		where		MainCategory.MainCategoryId = mc.ParentId) 
	AS ParentCategory,
	mc.Title as SubCategory, bk.BookingDate, bk.BookingType,
	bk.TotalPrice, bk.UserId, bk.BookingStatus
	, 0 AS Editions
	, 0 AS Designs	
	FROM AdBooking bk
	INNER JOIN	MainCategory mc		ON mc.MainCategoryId = bk.MainCategoryId
	LEFT JOIN	BookEntry en		ON en.AdBookingId = bk.AdBookingId
	INNER JOIN	AdDesign ds			ON ds.AdId = bk.AdId
	LEFT JOIN	LineAd la			ON ds.AdDesignId = la.AdDesignId
	LEFT JOIN	OnlineAd oa			ON oa.AdDesignId = oa.AdDesignId
	
	WHERE 
			((@AdDesignId IS NULL) 
				OR (@AdDesignId = ds.AdDesignId))
		AND ((@BookReference IS NULL) 
				OR (@BookReference = bk.BookReference))
		AND ((@Username IS NULL) 
				OR (bk.UserId LIKE @Username + ''%''))
		AND ((@Username IS NULL) 
				OR (bk.UserId LIKE @Username + ''%''))
		AND ((@BookingDateStart IS NULL 
				AND @BookingDateEnd IS NULL) 
				OR (@BookingDateStart <= bk.BookingDate 
					AND @BookingDateEnd >= bk.BookingDate))
		AND ((@BookingStatus IS NULL) 
				OR (@BookingStatus = bk.BookingStatus))
		AND ((@PublicationId IS NULL) 
				OR (@PublicationId = en.PublicationId))
		AND ((@MainCategoryId IS NULL) 
				OR (@MainCategoryId = bk.MainCategoryId))
		AND (	(@AdSearchText IS NULL) 
				OR (la.AdText LIKE ''%'' + @AdSearchText + ''%'') 
				OR (la.AdHeader LIKE ''%'' + @AdSearchText + ''%'') )
		AND (	(@AdSearchText IS NULL) 
				OR (oa.Heading LIKE ''%'' + @AdSearchText + ''%'') 
				OR (oa.[Description] LIKE ''%'' + @AdSearchText + ''%'') )	
		AND	((@ParentCategoryId IS NULL) OR (mc.ParentId = @ParentCategoryId))
		AND	((@EditionStartDate IS NULL AND @EditionEndDate IS NULL)
				OR	(@EditionStartDate <= en.EditionDate AND @EditionEndDate >= en.EditionDate))
	
	ORDER BY ParentCategory, SubCategory , bk.BookingDate
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGetMainParentCategories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 19th January 2009
-- Modifications
-- Date			Author			Description
--
-- 20-3-2009	Dejan Vasic		Added ORDER BY Title clause
-- =============================================
CREATE PROCEDURE [dbo].[spGetMainParentCategories]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	SELECT * FROM MainCategory WHERE ParentId IS NULL
	ORDER BY Title
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spGetPublicationsByAdType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 19th Nov 2008
-- Description:	Returns all publications that are 
--				able to print or contain the specified
--				ad type. If showOnline parameter is true,
--				it will also include online publications.
-- Date			Author			Description
-- 28-NOV-09	Dejan Vasic		Adding sort order logic
-- =============================================
CREATE PROCEDURE [dbo].[spGetPublicationsByAdType]
	-- Add the parameters for the stored procedure here
	@adTypeId int,
	@showOnline bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @showOnline = 1 
	
	-- If we want to list all publications including online. Execute this.
	SELECT pub.PublicationId, pub.Title, pub.Description, pub.ImageUrl
	FROM Publication pub
	INNER JOIN PublicationAdType typ ON pub.PublicationId = typ.PublicationId
	WHERE typ.AdTypeId = @adTypeId AND pub.Active = 1
	ORDER BY pub.SortOrder
	
ELSE
	SELECT pub.PublicationId, pub.Title, pub.Description, pub.ImageUrl
	FROM Publication pub
	INNER JOIN PublicationType ptp ON pub.PublicationTypeId = ptp.PublicationTypeId
	INNER JOIN PublicationAdType typ ON pub.PublicationId = typ.PublicationId
	WHERE 
		typ.AdTypeId = @adTypeId AND 
		(ptp.Code = ''NEWS'' OR ptp.Code = ''MAG'') AND
		pub.ACTIVE = 1
	ORDER BY pub.SortOrder
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spLineAdById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 16th March 2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- =============================================
CREATE PROCEDURE [dbo].[spLineAdById]
	-- Add the parameters for the stored procedure here
	@LineAdId INT
AS

BEGIN
	SELECT	la.LineAdId, la.AdHeader, la.AdText, la.NumOfWords, la.UsePhoto, la.UseBoldHeader, gr.DocumentId
	FROM LineAd la
	LEFT JOIN AdGraphic gr ON gr.AdDesignId = la.AdDesignId
	WHERE la.LineAdId = @LineAdId
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spLineAdExportList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 18th August 2009
-- Modifications
-- Date			Author			Description
-- 29-11-2009	Dejan Vasic		Added Sorting on concatenated header and text
-- 21-12-2009	Dejan Vasic		Added ISNULL on the concetanation so it always appends text
-- 21-05-2010	Dejan Vasic		Reconstructed entire proc to display the right iFlog id.
-- 30-05-2010	Dejan Vasic		Added ISNULL on the Use Photo to ensure it''s coming back with value always
-- =============================================
/*
	e.g 
	exec spLineAdExportList
		@publicationId = 1,
		@editionDate = ''30-JUL-2013'';
*/
CREATE PROCEDURE [dbo].[spLineAdExportList]
	-- Add the parameters for the stored procedure here
	@publicationId INT,
	@editionDate DATETIME
AS
declare @lineAdTypeId	int
declare @onlineAdTypeId	int
declare @statusBooked	int

set @lineAdTypeId	= 1;
set @onlineAdTypeId	= 2;
set @statusBooked	= 1;

DECLARE @onlineAds TABLE (
	AdBookingId INT,
	OnlineAdId INT
);

INSERT INTO @onlineAds
SELECT	bk.AdBookingId, o.OnlineAdId
FROM	dbo.AdBooking bk
JOIN	dbo.AdDesign ds
	ON	ds.AdId = bk.AdId
JOIN	dbo.OnlineAd o
	ON	o.AdDesignId = ds.AdDesignId
JOIN	dbo.BookEntry be
	ON  be.AdBookingId = bk.AdBookingId
WHERE	be.PublicationId = @publicationId
AND		be.EditionDate = @editionDate

BEGIN
	SELECT DISTINCT 
		ISNULL((	SELECT	AdDesignId 
					FROM	AdDesign ks
					JOIN	Ad 
						ON	Ad.AdId = ks.AdId
					WHERE	Ad.AdId = ak.AdId and ks.AdTypeId = @onlineAdTypeId
				), ds.AdDesignId) AS iflogId
		, bk.AdBookingId
		, bk.TotalPrice
		, ds.AdDesignId
		, la.LineAdId
		, o.OnlineAdId
		, (	SELECT	mc.MainCategoryId 
			FROM	MainCategory mc 
			WHERE	mc.MainCategoryId = sc.ParentId) AS MainCategoryId
		, (	SELECT	mc.Title 
			FROM	MainCategory mc 
			WHERE	mc.MainCategoryId = sc.ParentId) as MainCategory
		, sc.MainCategoryId AS SubCategoryId
		, sc.Title as SubCategory
		, (ISNULL(la.AdHeader,'''') + la.AdText) as HeaderAndText
		, la.AdText
		, la.AdHeader
		, la.NumOfWords
		, (ISNULL(la.UsePhoto, 0)) as UsePhoto
		, la.UseBoldHeader
		, bk.UserId
		, en.BookEntryId
		, la.BoldHeadingColourCode
		, la.BackgroundColourCode
		, la.BorderColourCode
		, la.IsSuperBoldHeading
	FROM	AdBooking bk
	JOIN	BookEntry en 
		ON	en.AdBookingId = bk.AdBookingId
	JOIN	AdDesign ds 
		ON	ds.AdId = bk.AdId
	JOIN	LineAd la 
		ON	la.AdDesignId = ds.AdDesignId
	JOIN	AdType tp 
		ON	tp.AdTypeId = ds.AdTypeId
	JOIN	MainCategory sc 
		ON	sc.MainCategoryId = bk.MainCategoryId
	JOIN	Ad ak 
		ON	ak.AdId = ds.AdId
	LEFT JOIN @onlineAds o
		ON	o.AdBookingId = bk.AdBookingId
	WHERE	en.PublicationId = @publicationId
			AND en.EditionDate = @editionDate
			AND tp.AdTypeId = @lineAdTypeId
			AND bk.BookingStatus = @statusBooked

	ORDER BY MainCategory, sc.Title, HeaderAndText
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spLineAdSelectUserCurrent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		28-Jun-2009
-- Modifications
-- Date		Author			Description
-- 24-1-10	Dejan Vasic		Altered procedure not to consider the online publication
-- =============================================
CREATE PROCEDURE [dbo].[spLineAdSelectUserCurrent]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@BookingStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT ds.AdDesignId
		, bk.BookReference
		, LEFT(la.AdHeader, 25) + ''...'' as Title
		, mc.Title as Category
		, bk.AdBookingId
		, bk.EndDate
	FROM dbo.AdBooking bk
		
		INNER JOIN (	SELECT MIN(be.EditionDate) AS EditionDate, be.AdBookingId
						FROM BookEntry be
						INNER JOIN Publication pb ON pb.PublicationId = be.PublicationId
						INNER JOIN PublicationType pbt ON pbt.PublicationTypeId = pb.PublicationTypeId
						WHERE pbt.Code <> ''ONLINE''
						GROUP BY be.AdBookingId ) AS FirstDateEntry ON FirstDateEntry.AdBookingId = bk.AdBookingId

		INNER JOIN (	SELECT MAX(be.EditionDate) AS EditionDate, be.AdBookingId 
						FROM BookEntry be
						INNER JOIN Publication pb ON pb.PublicationId = be.PublicationId
						INNER JOIN PublicationType pbt ON pbt.PublicationTypeId = pb.PublicationTypeId
						WHERE pbt.Code <> ''ONLINE''
						GROUP BY be.AdBookingId ) AS LastDateEntry ON LastDateEntry.AdBookingId = bk.AdBookingId
						
		INNER JOIN MainCategory mc ON mc.MainCategoryId = bk.MainCategoryId
		INNER JOIN AdDesign ds ON ds.AdId = bk.AdId
		INNER JOIN AdType tp ON tp.AdTypeId = ds.AdTypeId
		INNER JOIN Ad ad ON ad.AdId = bk.AdId
		INNER JOIN LineAd la ON la.AdDesignId = ds.AdDesignId
	WHERE	LastDateEntry.EditionDate >= GETDATE() 
			AND FirstDateEntry.EditionDate <= GETDATE()
			AND bk.UserId = @UserId
			AND bk.BookingStatus = @BookingStatus
			AND tp.Code = ''LINE''
	ORDER BY FirstDateEntry.EditionDate DESC

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spLineAdSelectUserExpired]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		30-Jun-2009
-- Modifications
-- Date		Author			Description
-- 19-1-10	Dejan Vasic		Adjusted the end date so that it doesn''t use Booking table but BookEntry instead.
-- =============================================
CREATE PROCEDURE [dbo].[spLineAdSelectUserExpired]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@EndDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET NOCOUNT ON;
	SELECT ds.AdDesignId
			, bk.BookReference
			, LEFT(la.AdHeader, 25) + ''...'' as Title
			, mc.Title as Category, bk.AdBookingId
			, ds.AdTypeId
			, BookEntry.EditionDate as EndDate
	FROM dbo.AdBooking bk
		INNER JOIN (	SELECT MAX(be.EditionDate) as EditionDate, be.AdBookingId 
						FROM BookEntry be
						INNER JOIN Publication pb ON pb.PublicationId = be.PublicationId
						INNER JOIN PublicationType pbt ON pbt.PublicationTypeId = pb.PublicationTypeId
						WHERE pbt.Code <> ''ONLINE''
						GROUP BY be.AdBookingId ) as BookEntry ON BookEntry.AdBookingId = bk.AdBookingId
		INNER JOIN MainCategory mc ON mc.MainCategoryId = bk.MainCategoryId
		INNER JOIN AdDesign ds ON ds.AdId = bk.AdId
		INNER JOIN AdType tp ON tp.AdTypeId = ds.AdTypeId
		INNER JOIN Ad ad ON ad.AdId = bk.AdId
		INNER JOIN LineAd la ON la.AdDesignId = ds.AdDesignId
	WHERE	
			bk.UserId = @UserId
			AND tp.Code = ''LINE''
			AND BookEntry.EditionDate < GETDATE()
			AND BookEntry.EditionDate > @EndDate
	ORDER BY BookEntry.EditionDate

END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spLineAdSelectUserScheduled]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		28-Jun-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spLineAdSelectUserScheduled]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@BookingStatus INT
AS
/*
	exec [spLineAdSelectUserScheduled]
		@UserId = ''dvasic'',
		@BookingStatus = 1
*/
BEGIN

	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT ds.AdDesignId, bk.BookReference, LEFT(la.AdHeader, 25) + ''...'' as Title, mc.Title as Category, bk.AdBookingId, ds.AdTypeId, BookEntry.EditionDate as StartDate
	FROM dbo.AdBooking bk
		INNER JOIN (	SELECT MIN(be.EditionDate) as EditionDate, be.AdBookingId 
						FROM BookEntry be
						INNER JOIN Publication pb ON pb.PublicationId = be.PublicationId
						INNER JOIN PublicationType pbt ON pbt.PublicationTypeId = pb.PublicationTypeId
						WHERE pbt.Code <> ''ONLINE''
						GROUP BY be.AdBookingId ) as BookEntry ON BookEntry.AdBookingId = bk.AdBookingId
		INNER JOIN MainCategory mc ON mc.MainCategoryId = bk.MainCategoryId
		INNER JOIN AdDesign ds ON ds.AdId = bk.AdId
		INNER JOIN AdType tp ON tp.AdTypeId = ds.AdTypeId
		INNER JOIN Ad ad ON ad.AdId = bk.AdId
		INNER JOIN LineAd la ON la.AdDesignId = ds.AdDesignId
	WHERE	BookEntry.EditionDate > GETDATE() 
			AND bk.UserId = @UserId
			AND bk.BookingStatus = @BookingStatus
			AND tp.Code = ''LINE''
	ORDER BY BookEntry.EditionDate
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spMainCategoriesUnassigned]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 9th April 2009
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spMainCategoriesUnassigned]
	-- Add the parameters for the stored procedure here
	@isParent BIT,
	@publicationCategoryId INT,
	@publicationId INT
AS

BEGIN
	
	IF @isParent = 1 
		SELECT MainCategoryId, Title FROM MainCategory
		WHERE	NOT EXISTS (	SELECT * FROM PublicationCategory 
								WHERE PublicationCategory.MainCategoryId = MainCategory.MainCategoryId
								AND PublicationCategory.PublicationId = @publicationId)
				AND (	(@isParent = 1 AND MainCategory.ParentId IS NULL) 
						OR 
						(@isParent = 0  AND MainCategory.ParentId = @publicationCategoryId))
	ELSE
		BEGIN
			DECLARE @tempTb TABLE (MainCategoryId INT, Title NVARCHAR(100))

			BEGIN
				INSERT INTO @tempTb 
					SELECT mc.MainCategoryId, mc.Title FROM MainCategory mc
					INNER JOIN PublicationCategory pc ON pc.MainCategoryId = mc.MainCategoryId
					WHERE pc.ParentId = @publicationCategoryId
			END

			BEGIN
				SELECT mc.MainCategoryId, mc.Title FROM MainCategory mc
				INNER JOIN PublicationCategory pc ON pc.MainCategoryId = mc.ParentId
				WHERE pc.PublicationCategoryId = @publicationCategoryId
					AND NOT EXISTS (SELECT * FROM @tempTb tb WHERE tb.MainCategoryId = mc.MainCategoryId)
			END
		END
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spMainCategoryAdCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 30th January 2009
-- Modifications
-- Date			Author			Description
--
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- =============================================
CREATE PROCEDURE [dbo].[spMainCategoryAdCount]
	-- Add the parameters
	@bookStatus INT
AS

-- use a temporary table to store all categories with the number of ad bookings in them
DECLARE @tmp TABLE (MainCategoryId INT, Title NVARCHAR(100), ParentId INT, [Count] INT)

BEGIN
INSERT INTO @tmp
	-- Do the insert into the temporary table.
	SELECT MainCategory.MainCategoryId, MainCategory.Title, ParentId, (
								SELECT COUNT(OnlineAdId) 
								FROM OnlineAd
								INNER JOIN AdDesign ON AdDesign.AdDesignId = OnlineAd.AdDesignId
								INNER JOIN AdBooking ON AdBooking.AdId = AdDesign.AdId
								WHERE 
								AdBooking.MainCategoryId = MainCategory.MainCategoryId
								AND AdBooking.EndDate > GETDATE()
								AND AdBooking.StartDate < GETDATE()
								AND AdDesign.[Status] <> 3
								AND AdBooking.BookingStatus = @bookStatus) AS [Count]
								
	FROM MainCategory
END


SELECT mc.MainCategoryId, mc.Title + '' ('' + CAST(SUM(t.[COUNT]) AS NVARCHAR(MAX))  + '')'' AS Title
FROM @tmp t
	INNER JOIN MainCategory mc ON mc.MainCategoryId = t.ParentId
GROUP BY mc.Title, mc.MainCategoryId
ORDER BY mc.Title

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spOnlineAdSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- ==========================================================================================
-- Author:		Dejan Vasic
-- Create date: 6th March 2009

-- Modifications
-- Date			Author			Description
--
-- 11-3-2009	Dejan Vasic		Added parameters @ParentCategoryId, @SubCategoryId, @AreaId,
--								@Keyword. Also created the categories variable table and
--								adjusted the WHERE clause to perform appropriate search.
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- ==========================================================================================
CREATE PROCEDURE [dbo].[spOnlineAdSelect]
	-- Add the parameters for the stored procedure here
	@ParentCategoryId INT,
	@SubCategoryId INT,
	@LocationId INT,
	@AreaId INT,
	@KeyWord NVARCHAR(100),
	--@StartDate DATETIME,
	@BookStatus INT
AS

-- ** IMAGES VAR TABLE ** --
DECLARE @tempTb TABLE (AdDesignId INT, DocumentId NVARCHAR(100))

BEGIN
	INSERT INTO @tempTb
		SELECT			AdDesign.AdDesignId, 
						(SELECT TOP 1 DocumentId FROM AdGraphic WHERE AdGraphic.AdDesignId = AdDesign.AdDesignId)
		FROM			AdDesign 
		INNER JOIN		AdBooking bk ON bk.AdId = AdDesign.AdId
		WHERE			bk.BookingStatus = @BookStatus
						AND bk.StartDate < GETDATE() AND bk.StartDate < GETDATE() AND AdDesign.[Status] <> 3
		ORDER BY		bk.StartDate DESC
END
-- ** END IMAGES VAR TABLE ** --

-- ** CATEGORIES VAR TABLE ** --
DECLARE @tmpCat TABLE (MainCategoryId INT, ParentId INT)

BEGIN
	IF @SubCategoryId IS NULL 
		IF @ParentCategoryId IS NULL
			INSERT INTO @tmpCat SELECT MainCategoryId, ParentId FROM Maincategory
		ELSE
			INSERT INTO @tmpCat SELECT MainCategoryId, ParentId FROM MainCategory WHERE MainCategory.ParentId = @ParentCategoryId
	ELSE
		INSERT INTO @tmpCat
		SELECT		MainCategoryId, ParentId FROM MainCategory WHERE MainCategory.MainCategoryId = @SubCategoryId
END
-- ** END CATEGORIES VAR TABLE ** --

-- ** SEARCHING ROUTINE ** --
BEGIN
	SELECT	ad.OnlineAdId, ad.Heading, 
			ad.Price, ad.NumOfViews, bk.StartDate as Posted, bk.EndDate as Ending, ar.Title as Location, 
			LEFT(ad.[Description], 50) + ''...'' as [AdText], mc.Title As Category, 
			gr.DocumentId AS DocumentId
	FROM OnlineAd ad
		INNER JOIN	AdDesign ds			ON	ds.AdDesignId = ad.AdDesignId
		INNER JOIN	AdBooking bk		ON	bk.AdId = ds.AdId
		INNER JOIN	LocationArea ar		ON	ar.LocationAreaId = ad.LocationAreaId
		INNER JOIN	MainCategory mc		ON	mc.MainCategoryId = bk.MainCategoryId
		LEFT JOIN	@tempTb gr			ON	gr.AdDesignId = ad.AdDesignId
	WHERE 
		(	((@KeyWord = '''' AND 1 = 1) OR (@KeyWord <> '''' AND ad.[Description] LIKE ''% '' + @KeyWord + ''%'' )) 
		OR 
			((@KeyWord = '''' AND 1 = 1) OR (@KeyWord <> '''' AND ad.Heading LIKE ''% '' + @KeyWord + ''%''))	) 
		AND 
		(	((@LocationId IS NULL AND 1=1) OR (@LocationId IS NOT NULL AND ad.LocationId = @LocationId))	)
		AND
		(	((@AreaId IS NULL AND 1=1) OR (@AreaId IS NOT NULL AND ad.LocationAreaId = @AreaId))	)
		AND bk.MainCategoryId IN ( SELECT ct.MainCategoryId FROM @tmpCat ct )
		AND bk.EndDate > GETDATE() AND bk.StartDate <= GETDATE()
		AND bk.BookingStatus = @BookStatus AND ds.[Status] <> 3
		
	ORDER BY bk.StartDate DESC
END
-- ** END SEARCHING ROUTINE ** --


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spOnlineAdSelectByCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Dejan Vasic
-- Create date: 6th March 2009

-- Modifications
-- Date			Author			Description
--
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- ==========================================================================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectByCategory]
	-- Add the parameters for the stored procedure here
	@CategoryId INT,
	--@startDate DATETIME,
	@bookStatus INT
AS

DECLARE @tempTb TABLE (AdDesignId INT, DocumentId NVARCHAR(100))

BEGIN
	-- INSERT INTO TABLE ONLY ONE IMAGE FOR EACH AD DESIGN
	INSERT INTO @tempTb
		SELECT			AdDesign.AdDesignId, 
						(SELECT TOP 1 DocumentId FROM AdGraphic WHERE AdGraphic.AdDesignId = AdDesign.AdDesignId)
		FROM			AdDesign 
		INNER JOIN		AdBooking bk ON bk.AdId = AdDesign.AdId
		WHERE			bk.BookingStatus = @bookStatus AND AdDesign.[Status] <> 3
						AND bk.StartDate < GETDATE() 
		ORDER BY		bk.StartDate DESC
END

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT	ad.OnlineAdId, ad.Heading, 
			--ad.[Description] as AdText,
			ad.Price, ad.NumOfViews, bk.StartDate as Posted, bk.EndDate as Ending, ar.Title as Location, 
			LEFT(ad.[Description], 50) + ''...'' as [AdText], mc.Title as Category,
			gr.DocumentId AS DocumentId
	FROM OnlineAd ad
		INNER JOIN	AdDesign ds			ON	ds.AdDesignId = ad.AdDesignId
		INNER JOIN	AdBooking bk		ON	bk.AdId = ds.AdId
		INNER JOIN	LocationArea ar		ON	ar.LocationAreaId = ad.LocationAreaId
		INNER JOIN	MainCategory mc		ON	mc.MainCategoryId = bk.MainCategoryId
		LEFT JOIN	@tempTb gr		ON	gr.AdDesignId = ad.AdDesignId
	WHERE 
		bk.EndDate > GETDATE()
		AND bk.StartDate <= GETDATE()
		AND ds.[Status] <> 3
		AND bk.BookingStatus = @bookStatus
		AND bk.MainCategoryId IN (	SELECT ct.MainCategoryId 
									FROM MainCategory ct
									WHERE ct.ParentId = @CategoryId )
	ORDER BY bk.StartDate DESC
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spOnlineAdSelectByLineAdDesign]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		24-Nov-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectByLineAdDesign]
	-- Add the parameters for the stored procedure here
	@LineDesignId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @adId int

	select @adId = lds.AdId from AdDesign lds where lds.AdDesignId = @LineDesignId

	select onl.AdDesignId, onl.ContactName, onl.ContactType,
			onl.ContactValue, onl.[Description], onl.Heading, 
			onl.HtmlText, onl.LocationAreaId, onl.LocationId,
			onl.NumOfViews, onl.OnlineAdId, onl.Price
	from OnlineAd onl
		inner join  AdDesign ds on ds.AdDesignId = onl.AdDesignId
	where ds.AdId = @adId

END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spOnlineAdSelectRecentlyAdded]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 9th March 2009

-- Modifications
-- Date			Author			Description
--
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- 21-5-2010	Dejan Vasic		Selecting only the graphics that contain a graphic
-- 25-7-2013	Dejan Vasic		Added AdbookingId to the result set
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectRecentlyAdded]
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
						LEFT(ad.Heading, 40) + ''...'' as Heading,
						LEFT(ad.[Description], 80) + ''...'' as [AdText],  
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
END' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spOnlineAdSelectUserCurrent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		29-Jun-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectUserCurrent]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@BookingStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 	des.AdDesignID, bk.BookReference, bk.AdBookingId, bk.EndDate,
			LEFT(onl.Heading, 25) + ''...'' as Title,
			CASE des.[Status]	WHEN 1 THEN ''Pending'' 
								WHEN 2 THEN ''Approved'' 
								WHEN 3 THEN ''Cancelled'' 
			END AS Status, 
			cat.Title as Category, bk.StartDate, bk.EndDate
	FROM AdBooking bk
	INNER JOIN Ad ad ON ad.Adid = bk.Adid
	INNER JOIN AdDesign des ON des.AdId = bk.AdId
	INNER JOIN MainCategory cat ON cat.MainCategoryId = bk.MainCategoryId
	INNER JOIN AdType typ ON typ.AdTypeId = des.AdTypeId
	INNER JOIN OnlineAd onl ON onl.AdDesignId = des.AdDesignId
	WHERE	bk.UserId = @UserId
			AND bk.StartDate <= GETDATE()
			AND bk.EndDate >= GETDATE()
			AND bk.BookingStatus = @BookingStatus
			AND typ.Code = ''ONLINE''
	ORDER BY bk.StartDate
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spOnlineAdSelectUserExpired]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		30-Jun-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectUserExpired]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@EndDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 	des.AdDesignID, bk.BookReference, bk.AdBookingId, des.AdTypeId, bk.EndDate,
			LEFT(onl.Heading, 25) + ''...'' as Title,
			CASE des.[Status]	WHEN 1 THEN ''Pending'' 
								WHEN 2 THEN ''Approved'' 
								WHEN 3 THEN ''Cancelled'' 
			END AS Status, 
			cat.Title as Category, bk.StartDate, bk.EndDate
	FROM AdBooking bk
	INNER JOIN Ad ad ON ad.Adid = bk.Adid
	INNER JOIN AdDesign des ON des.AdId = bk.AdId
	INNER JOIN MainCategory cat ON cat.MainCategoryId = bk.MainCategoryId
	INNER JOIN AdType typ ON typ.AdTypeId = des.AdTypeId
	INNER JOIN OnlineAd onl ON onl.AdDesignId = des.AdDesignId
	WHERE	bk.UserId = @UserId
			AND bk.EndDate < GETDATE()
			AND bk.EndDate >= @EndDate
			AND typ.Code = ''ONLINE''
			
	ORDER BY bk.EndDate DESC
END






' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spOnlineAdSelectUserScheduled]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		28-Jun-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectUserScheduled]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@BookingStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 	des.AdDesignID, bk.BookReference, bk.AdBookingId, bk.StartDate,
			LEFT(onl.Heading, 25) + ''...'' as Title,
			CASE des.[Status]	WHEN 1 THEN ''Pending'' 
								WHEN 2 THEN ''Approved'' 
								WHEN 3 THEN ''Cancelled'' 
			END AS Status, 
			cat.Title as Category
	FROM AdBooking bk
	INNER JOIN Ad ad ON ad.Adid = bk.Adid
	INNER JOIN AdDesign des ON des.AdId = bk.AdId
	INNER JOIN MainCategory cat ON cat.MainCategoryId = bk.MainCategoryId
	INNER JOIN AdType typ ON typ.AdTypeId = des.AdTypeId
	INNER JOIN OnlineAd onl ON onl.AdDesignId = des.AdDesignId
	WHERE	bk.UserId = @UserId
			AND bk.StartDate > GETDATE()
			AND bk.EndDate > GETDATE()
			AND bk.BookingStatus = @BookingStatus
			AND typ.Code = ''ONLINE''
	ORDER BY bk.StartDate
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPublicationCategories]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27th March 2009
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationCategories]
	-- Add the parameters for the stored procedure here
	@PublicationID INT,
	@ParentID INT,
	@MainCategoryID INT
AS

BEGIN
	SELECT pc.PublicationCategoryId, pc.Title, pc.[Description], pc.ImageUrl, pc.ParentId, pc.MainCategoryId, pc.PublicationId 
	FROM PublicationCategory pc
	WHERE 
			pc.PublicationId = @PublicationID
			AND pc.MaincategoryId = @MainCategoryID
			AND ((@ParentID IS NULL AND 1=1) OR (@ParentID IS NOT NULL AND pc.ParentId = @ParentID))
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPublicationCategoriesByPubId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27th March 2009
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationCategoriesByPubId]
	-- Add the parameters for the stored procedure here
	@PublicationID INT,
	@ParentID INT
AS

BEGIN
	IF @ParentID IS NULL 
		SELECT pc.PublicationCategoryId, pc.Title, pc.[Description], pc.ImageUrl, pc.ParentId, pc.MainCategoryId, pc.PublicationId 
		FROM PublicationCategory pc
		WHERE pc.PublicationId = @PublicationID AND pc.ParentId IS NULL
		ORDER BY pc.Title
	ELSE
		SELECT pc.PublicationCategoryId, pc.Title, pc.[Description], pc.ImageUrl, pc.ParentId, pc.MainCategoryId, pc.PublicationId 
		FROM PublicationCategory pc
		WHERE pc.PublicationId = @PublicationID AND pc.ParentId = @ParentID	
		ORDER BY pc.Title
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPublicationDeadlineSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 13th Jan 2010
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationDeadlineSelect]
	-- Add the parameters for the stored procedure here
	@PublicationID	INT
AS

BEGIN
	SELECT TOP 1 ed.PublicationId
	, ed.EditionId
	, ed.EditionDate
	, ed.Deadline
	, pb.Title as Publication
	FROM Edition ed
	INNER JOIN Publication pb ON pb.PublicationId = ed.PublicationId
	WHERE 
		ed.PublicationId = @PublicationID
		AND ed.Active = 1
		AND ed.EditionDate >= GETDATE()
		AND ed.Deadline >= GETDATE()
	ORDER BY ed.EditionDate
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPublicationEditionAndDeadlines]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 16th March 2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- 29-11-2009	Dejan Vasic		Added Sort order logic
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationEditionAndDeadlines]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT pub.PublicationId, pub.Title, pub.[Description], pub.ImageUrl, 
		(	SELECT TOP 1 ed.EditionDate 
			FROM Edition ed 
			WHERE ed.PublicationId = pub.PublicationId AND ed.EditionDate >= GETDATE()
			ORDER BY ed.EditionDate) As NextEdition,
			
		(	SELECT TOP 1 ed.Deadline 
			FROM Edition ed 
			WHERE ed.PublicationId = pub.PublicationId AND ed.EditionDate >= GETDATE()
			ORDER BY ed.EditionDate) As NextDeadline
			
	FROM	Publication pub
	INNER JOIN	PublicationType typ ON typ.PublicationTypeId = pub.PublicationTypeId
	WHERE	typ.Code <> ''ONLINE'' 
			AND pub.Active = 1
	ORDER BY pub.SortOrder
END

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPublicationSpecialRateAdd]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 3rd June 2010
-- Modifications
-- Date			Author			Description
-- ==================================================================
CREATE PROCEDURE [dbo].[spPublicationSpecialRateAdd]

	  @PublicationId		INT
	, @MainCategoryId		INT
	, @SpecialRateId		INT
	, @ClearCurrentRates	BIT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	BEGIN TRANSACTION

    declare @publicationCategoryId	int
    declare @publicationAdTypeId	int
    declare @specialRateCount		bit
    
    SELECT	  @publicationCategoryId = pc.PublicationCategoryId
			, @publicationAdTypeId = pat.PublicationAdTypeId
    FROM	PublicationCategory pc
	JOIN	PublicationAdType pat
		ON	pat.PublicationId = @PublicationId			
    WHERE	pc.MainCategoryId = @MainCategoryId
		AND	pc.PublicationId = @PublicationId;
	
	IF	@ClearCurrentRates = 1
	begin
		DELETE PublicationSpecialRate 
		WHERE	PublicationCategoryId = @publicationCategoryId;
	end
	
	-- Check whether we need to add (if already assigned then ignore)
	SELECT	@specialRateCount = COUNT(*) 
	FROM	PublicationSpecialRate psr
	WHERE	psr.SpecialRateId = @SpecialRateId 
		AND psr.PublicationAdTypeId = @publicationAdTypeId
		AND psr.PublicationCategoryId = @publicationCategoryId;
	
	IF @specialRateCount = 0
	begin
		-- Create the new Publication Special Rate
		INSERT INTO PublicationSpecialRate
			(SpecialRateId, PublicationAdTypeId, PublicationCategoryId)
		VALUES
			(@SpecialRateId, @publicationAdTypeId, @publicationCategoryId);
	end
	COMMIT TRANSACTION
	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spRatecardsByCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 16th March 2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- 9-12-2009	Dejan Vasic		Added order by sort order on publications
-- =============================================
CREATE PROCEDURE [dbo].[spRatecardsByCategory]
	-- Add the parameters for the stored procedure here
	@categoryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	pub.Title as Publication, br.Title as Rate, br.[Description],
		ra.MinCharge, ra.MaxCharge, ra.RatePerMeasureUnit, ra.MeasureUnitLimit, ra.PhotoCharge, ra.BoldHeading, ra.OnlineEditionBundle, pubType.Code
	FROM Ratecard ra
	INNER JOIN PublicationRate pubRate ON pubRate.RatecardId = ra.RatecardId
	INNER JOIN PublicationCategory pubCat ON pubCat.PublicationCategoryId = pubRate.PublicationCategoryId
	INNER JOIN PublicationAdType pubAdType ON pubAdType.PublicationAdTypeId = pubRate.PublicationAdTypeId
	INNER JOIN Publication pub ON pub.PublicationId = pubAdType.PublicationId
	INNER JOIN PublicationType pubType ON pubType.PublicationTypeId = pub.PublicationTypeId
	INNER JOIN BaseRate br ON br.BaseRateId = ra.BaseRateId
	WHERE	pubCat.MainCategoryId = @categoryId 
			AND pub.Active = 1
	ORDER BY pub.SortOrder
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spReportIncomeReport]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 13-OCT-09
-- Modifications
-- Date			Author			Description
-- =============================================
create procedure [dbo].[spReportIncomeReport]
	@startDate datetime,
	@endDate datetime
as 
begin

select	tr.TransactionDate, tr.UserId, tr.Title as BookReference, 
		bk.BookingType, 
		case tr.TransactionType 
			when 1 then ''Credit Card''
			when 2 then ''PayPal''
		end as [TransactionType],
		(tr.Amount * 0.9) as PriceExGST,(tr.Amount * 0.1) as GST, tr.Amount as TotalPrice
from [Transaction] tr
	inner join AdBooking bk on bk.BookReference = tr.Title 
	where tr.TransactionDate >= @startDate and tr.TransactionDate <= @endDate
end

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spReportWeeklySales]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 13-OCT-09
-- Modifications
-- Date			Author			Description
-- =============================================
create procedure [dbo].[spReportWeeklySales]
	@pubid int,					
	@ed datetime,
	@status int = null,			-- optional
	@subCategoryId int = null	-- optional
as 
begin
select ds.AdDesignId as iFlogID, 
		(select mc.Title from MainCategory mc where mc.MainCategoryId = sc.ParentId) AS Category,
		sc.Title as SubCategory, 
		bk.UserId, bk.BookReference, bk.BookingType,
		case bk.BookingStatus 
			when 1 then ''Booked''
			when 2 then ''Expired''
			when 3 then ''Cancelled''
			when 4 then ''Unpaid''
		end as [Status],
		ln.NumOfWords, 
		case ln.UseBoldHeader 
			when 0 then 0
			when 1 then 1
		end as BoldHeadings,
		case ln.UsePhoto	
			when 0 then 0
			when 1 then 1
		end as Photos,
		pb.Title as Publication,
		(en.EditionAdPrice * 0.9) as PriceExGST,
		(en.EditionAdPrice * 0.1) as GST,
		(en.EditionAdPrice) as TotalPrice
		
from AdBooking bk
	inner join BookEntry en ON en.AdBookingId = bk.AdBookingId
	inner join AdDesign ds on ds.AdId = bk.AdId
	inner join LineAd ln on ln.AdDesignId = ds.AdDesignId
	inner join MainCategory sc on sc.MainCategoryId = bk.MainCategoryId
	inner join Publication pb on pb.PublicationId = en.PublicationId
where 
	en.PublicationId = @pubId 
	and en.EditionDate = @ed
	and (@status is null or (@status is not null and bk.BookingStatus = @status))
	and (@subCategoryId is null or (@subCategoryId is not null and bk.MainCategoryId = @subCategoryId))
order by Category, SubCategory

end 

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSpecialRateDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 23rd June 2010
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spSpecialRateDelete]
	-- Add the parameters for the stored procedure here
	 @specialRateId		INT
	,@isCascade			BIT = 0
AS
BEGIN
	BEGIN TRANSACTION
	
	DECLARE @baseRateId INT;
	SELECT @baseRateId = sr.BaseRateId FROM SpecialRate sr WHERE sr.SpecialRateId = @specialRateId;
	
	IF @isCascade = 1
	begin
		DELETE PublicationSpecialRate WHERE SpecialRateId = @specialRateId;
	end
	
	DELETE SpecialRate WHERE SpecialRateId = @specialRateId;
	DELETE BaseRate WHERE BaseRateId = @baseRateId;
	
	COMMIT TRANSACTION
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSpecialRatePublications]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 28th January 2009
-- Modifications
-- Date			Author			Description
-- 18-03-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- 29-11-2009	Dejan Vasic		Added Sort order logic
-- =============================================
CREATE PROCEDURE [dbo].[spSpecialRatePublications]
	-- Add the parameters for the stored procedure here
	@specialRateId INT
AS
BEGIN
	SELECT DISTINCT p.Title as Publication, at.Code as AdType, p.PublicationId, p.ImageUrl, p.SortOrder
	FROM SpecialRate sr
	INNER JOIN PublicationSpecialRate psr ON psr.SpecialRateId = sr.SpecialRateId
	INNER JOIN PublicationAdType pat ON pat.PublicationAdTypeId = psr.PublicationAdTypeId
	INNER JOIN Publication p ON p.PublicationId = pat.PublicationId
	INNER JOIN AdType at ON at.AdTypeId = pat.AdTypeId
	WHERE	sr.SpecialRateId = @specialRateId
			AND p.Active = 1
	ORDER BY p.SortOrder
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSpecialRatesByCategory]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 26-1-2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- =============================================
CREATE PROCEDURE [dbo].[spSpecialRatesByCategory]
	-- Add the parameters for the stored procedure here
	@mainCategoryId INT
AS
BEGIN
	SELECT DISTINCT BaseRate.Title, BaseRate.Description, SpecialRate.SpecialRateId, SpecialRate.NumOfInsertions,
		SpecialRate.MaximumWords, SpecialRate.SetPrice, SpecialRate.Discount, SpecialRate.NumOfAds,
		mc.MainCategoryId
	FROM SpecialRate 
	INNER JOIN BaseRate ON BaseRate.BaseRateId = SpecialRate.BaseRateId
	INNER JOIN PublicationSpecialRate spr ON spr.SpecialRateId = SpecialRate.SpecialRateId
	INNER JOIN PublicationCategory pc ON pc.PublicationCategoryId = spr.PublicationCategoryId
	INNER JOIN MainCategory mc ON mc.MainCategoryId = pc.MainCategoryId
	INNER JOIN Publication pub ON pub.PublicationId = pc.PublicationId
	WHERE	mc.MainCategoryId = @mainCategoryId
			AND pub.Active = 1
	ORDER BY BaseRate.Title
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spTransactionsByUser]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:			Dejan Vasic
-- Create date:		7-Jul-2009
-- Modifications
-- Date		Author			Description
-- 18-10-09	Dejan Vasic		Added Where clause not to display unpaid transactions	
-- =============================================
CREATE PROCEDURE [dbo].[spTransactionsByUser]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@StartDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
SELECT	tr.Title as [Ref No], tr.TransactionDate, tr.TransactionType, tr.Amount, tr.UserId,
		(SELECT mc.Title FROM MainCategory mc WHERE mc.MainCategoryId = bk.MainCategoryId) as Category,
		(SELECT count(AdDesignId) 
			FROM AdDesign inner join AdBooking bk1 on bk1.AdId = AdDesign.AdId 
			WHERE bk1.AdBookingId = bk.AdBookingId) as NumOfAds
		
FROM dbo.[Transaction] tr
	INNER JOIN AdBooking bk on bk.BookReference = tr.Title
WHERE	tr.[TransactionDate] > @StartDate AND tr.UserId = @UserId
		and bk.BookingStatus <> 4
ORDER BY tr.[TransactionDate] DESC

END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LineAdTheme__FetchBackgroundColour]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27-FEB-2011
-- =============================================
CREATE PROCEDURE [dbo].[usp_LineAdTheme__FetchBackgroundColour]
	@HeaderColourCode		varchar(10) = NULL,
	@BorderColourCode	varchar(10) = NULL
AS
/*
	[usp_LineAdTheme__FetchBackgroundColour]
		@HeaderColourCode	= NULL,
		@BorderColourCode	= NULL
*/
BEGIN

	declare @true	 tinyint = 1;

	-- Fetch the possible colour coes first
	SELECT TOP 1
			lat.BackgroundColourCode, lat.BackgroundColourName
	FROM	LineAdTheme lat
	WHERE	lat.HeaderColourCode = ISNULL(@HeaderColourCode, lat.HeaderColourCode)
		AND	lat.BorderColourCode = ISNULL(@BorderColourCode, lat.BorderColourCode)
		AND	lat.IsActive = @true;
	
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LineAdTheme__FetchBorderColour]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27-FEB-2011
-- =============================================
CREATE PROCEDURE [dbo].[usp_LineAdTheme__FetchBorderColour]
	@HeaderColourCode		varchar(10) = NULL,
	@BackgroundColourCode	varchar(10) = NULL
AS
/*
	usp_LineAdTheme__FetchBorderColour
		@HeaderColourCode		= NULL,
		@BackgroundColourCode	= NULL
*/
BEGIN

	declare @true	 tinyint = 1;

	-- Fetch the possible colour coes first
	SELECT TOP 1
			lat.BorderColourCode, lat.BorderColourName
	FROM	LineAdTheme lat
	WHERE	lat.HeaderColourCode = ISNULL(@HeaderColourCode, lat.HeaderColourCode)
		AND	lat.BackgroundColourCode = ISNULL(@BackgroundColourCode, lat.BackgroundColourCode)
		AND	lat.IsActive = @true;
	
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_LineAdTheme__FetchHeaderColour]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27-FEB-2011
-- =============================================
CREATE PROCEDURE [dbo].[usp_LineAdTheme__FetchHeaderColour]
	@BorderColourCode		varchar(10) = NULL,
	@BackgroundColourCode	varchar(10) = NULL
AS
/*
	usp_LineAdTheme__FetchHeaderColour
		@BorderColourCode		= NULL,
		@BackgroundColourCode	= NULL
*/
BEGIN

	declare @true	 tinyint = 1;

	-- Fetch the possible colour coes first
	SELECT TOP 1
			lat.HeaderColourCode, lat.HeaderColourName
	FROM	LineAdTheme lat
	WHERE	lat.BorderColourCode = ISNULL(@BorderColourCode, lat.BorderColourCode)
		AND	lat.BackgroundColourCode = ISNULL(@BackgroundColourCode, lat.BackgroundColourCode)
		AND	lat.IsActive = @true;
	
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_MainCategory__SelectForRateCard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 03-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_MainCategory__SelectForRateCard]
(
	@RateCardId		INT
)
AS
/*
	exec usp_RateCard__SelectMainCategories
		@RateCardId	= 15;
		
*/
BEGIN

	SELECT	DISTINCT pc.MainCategoryId
	FROM	PublicationCategory pc
	JOIN	PublicationRate pr
		ON	pr.PublicationCategoryId = pc.PublicationCategoryId
	WHERE	pr.RatecardId = @RateCardId;
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_Publication__SelectForRateCard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 03-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_Publication__SelectForRateCard]
(
	@RateCardId		INT
)
AS
/*
	exec usp_RateCard__SelectPublications
		@RateCardId	= 13;
		
*/
BEGIN

	SELECT	DISTINCT p.PublicationId
	FROM	Publication p
	JOIN	PublicationCategory pc
		ON	pc.PublicationId = p.PublicationId
	JOIN	PublicationRate pr
		ON	pr.PublicationCategoryId = pc.PublicationCategoryId
	WHERE	pr.RatecardId = @RateCardId;
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_PublicationRateCard__Add]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 02-JAN-2010
-- =============================================

CREATE PROCEDURE [dbo].[usp_PublicationRateCard__Add]

	  @PublicationId		INT
	, @MainCategoryId		INT
	, @RatecardId			INT
	, @ClearCurrentRates	BIT = 1
AS
/*
	exec dbo.spPublicationRateCardAdd
		@PublicationId		= 1, -- Drum media
		@MainCategoryId		= 8, -- Administration
		@RatecardId			= 1,
		@ClearCurrentRates	= 1;
		
*/
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION

    declare @publicationCategoryId	int,
			@publicationAdTypeId	int,
			@ratecardCount			int;
    
    SELECT	  @publicationCategoryId = pc.PublicationCategoryId
			, @publicationAdTypeId = pat.PublicationAdTypeId
    FROM	PublicationCategory pc
	JOIN	PublicationAdType pat
		ON	pat.PublicationId = @PublicationId			
    WHERE	pc.MainCategoryId = @MainCategoryId
		AND	pc.PublicationId = @PublicationId;
	
	IF	@ClearCurrentRates = 1
	begin
		DELETE	PublicationRate 
		WHERE	PublicationCategoryId = @publicationCategoryId;
	end
	
	-- Check whether we need to add (if already assigned then ignore)
	SELECT	@ratecardCount = COUNT(*) 
	FROM	PublicationRate pr
	WHERE	pr.RatecardId				= @RatecardId
		AND pr.PublicationAdTypeId		= @publicationAdTypeId
		AND pr.PublicationCategoryId	= @publicationCategoryId;
	
	IF @ratecardCount = 0
	begin
		-- Create the new Publication Special Rate
		INSERT INTO PublicationRate
			(RatecardId, PublicationAdTypeId, PublicationCategoryId)
		VALUES
			(@RatecardId, @publicationAdTypeId, @publicationCategoryId);
	end
	
	
	COMMIT TRANSACTION
	
END



' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_PublicationRateCard__Select]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 01-JAN-2010
-- Modifications
-- 20-FEB-2011	DV	Including all rate details
-- 20-FEB-2011	DV	Allowing ad type id to be passed in
-- =============================================
CREATE PROCEDURE [dbo].[usp_PublicationRateCard__Select]
	 @PublicationId		INT,
	 @MainCategoryId	INT,
	 @AdTypeId			INT = 1   -- Default To Line Ad Type
AS
/*
	e.g.
		exec usp_PublicationRateCard__Select
			@PublicationId	= 1,
			@MainCategoryId = 8;
*/
BEGIN
	
	declare @publicationCategoryId	int,
			@publicationAdTypeId	int,
			@publicationName		varchar(50);
    
    -- Fetch the publication information first
    SELECT	  @publicationCategoryId = pc.PublicationCategoryId
			, @publicationAdTypeId = pat.PublicationAdTypeId
			, @publicationName = p.Title
    FROM	PublicationCategory pc
	JOIN	PublicationAdType pat
		ON	pat.PublicationId = @PublicationId			
		AND	pat.AdTypeId = @AdTypeId
	JOIN	Publication p
		ON	p.PublicationId = pc.PublicationId
    WHERE	pc.MainCategoryId = @MainCategoryId
		AND	pc.PublicationId = @PublicationId;
		
	select	
			@PublicationId as PublicationId,
			@publicationName as PublicationName,
			pr.PublicationRateId, 
			pr.PublicationAdTypeId, 
			pr.PublicationCategoryId, 
			pc.Title as PublicationCategoryName,
			br.Title as RatecardName,
			r.RatecardId, 
			r.MinCharge,
			r.MaxCharge,
			r.RatePerMeasureUnit,
			r.MeasureUnitLimit,
			r.PhotoCharge,
			r.BoldHeading,
			r.LineAdSuperBoldHeading,
			r.LineAdColourHeading,
			r.LineAdColourBorder,
			r.LineAdColourBackground,
			r.LineAdExtraImage,
			r.OnlineEditionBundle
	from	dbo.PublicationRate pr
	join	dbo.PublicationCategory pc on pc.PublicationCategoryId = pr.PublicationCategoryId
	join	dbo.Ratecard r on r.RatecardId = pr.RatecardId
	join	dbo.BaseRate br on br.BaseRateId = r.BaseRateId
	where	pr.PublicationAdTypeId = @publicationAdTypeId
		and	pr.PublicationCategoryId	= @publicationCategoryId
	
	
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RateCard__Delete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 01-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_RateCard__Delete]
	 @RateCardId		INT
	,@IsCascade			BIT = 1
AS
BEGIN
	BEGIN TRANSACTION
	
	declare @baseRateId int;
	SELECT	@baseRateId = r.BaseRateId 
	FROM	Ratecard r 
	WHERE	r.RatecardId = @RateCardId;
	
	IF @IsCascade = 1
	begin
		DELETE PublicationRate WHERE RatecardId = @RateCardId;
	end
	
	DELETE Ratecard WHERE RatecardId = @RateCardId;
	DELETE BaseRate WHERE BaseRateId = @baseRateId;
	
	COMMIT TRANSACTION
END


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RateCard__Search]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 01-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_RateCard__Search]
AS
/*
	exec usp_RateCard__Search
*/
BEGIN

	declare @publicationRateCount	table (	PublicationId	INT,
											RatecardId		INT);
											
	-- Publication Count
	INSERT INTO @publicationRateCount
	select	p.PublicationId, pr.RatecardId
	from	Publication p
	JOIN	PublicationCategory pc
		ON	pc.PublicationId = p.PublicationId
	JOIN	PublicationRate pr
		ON	pr.PublicationCategoryId = pc.PublicationCategoryId
	GROUP BY	p.PublicationId, pr.RatecardId
	
	-- MAIN RESULTS
	SELECT	  r.RatecardId
			, br.Title
			, r.MinCharge
			, r.MaxCharge
			, r.CreatedDate
			, r.CreatedByUser
			, COUNT(prc.PublicationId) as PublicationCount
	FROM	Ratecard r
	JOIN	BaseRate br
		ON	br.BaseRateId = r.BaseRateId
	LEFT JOIN	@publicationRateCount prc
		ON	prc.RatecardId = r.RatecardId
	GROUP BY
			 r.RatecardId
			, br.Title
			, r.MinCharge
			, r.MaxCharge
			, r.CreatedDate
			, r.CreatedByUser
END


' 
END
GO
