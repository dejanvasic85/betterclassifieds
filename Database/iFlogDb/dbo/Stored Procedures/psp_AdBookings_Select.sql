-- =============================================
-- Author:		Dejan Vasic
-- Create date: 24-06-2010
-- Modifications
-- Date			Author			Description
-- ==================================================================
CREATE proc [dbo].[psp_AdBookings_Select]

	-- Add the parameters for the stored procedure here
	@AdDesignId			int			= null,
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
		@Params			nvarchar(max),
		@AdBookingId	int

set		@AdBookingId = NULL;

-- AddesignId filter
IF @AdDesignId IS NOT NULL AND @AdDesignId > 0
begin
	SELECT   @AdBookingId = ISNULL(bk.AdBookingId, 0)
	FROM	AdDesign ds
	JOIN	AdBooking bk
		ON	bk.AdId = ds.AdId
	WHERE	ds.AdDesignId = @AdDesignId;
	-- If no records returned then set BookingId to 0 
	SELECT	@AdBookingId = ISNULL(@AdBookingId, 0)
end

-- Book Reference Filter
IF @BookReference IS NOT NULL AND @BookReference <> ''
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
	WHERE	(@AdSearchText IS NULL OR @AdSearchText = '')
			OR (onl.Heading LIKE '%' + @AdSearchText + '%') 
			OR (onl.[Description] LIKE '%' + @AdSearchText + '%')
			OR (lin.AdText LIKE '%' + @AdSearchText + '%') 
			OR (lin.AdHeader LIKE '%' + @AdSearchText + '%')

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
	WHERE	((@Username IS NULL) OR (bk.UserId LIKE @Username + '%'))
		AND	bk.AdBookingId = ISNULL(@AdBookingId, bk.AdBookingId)
		AND ((@BookingDateStart IS NULL AND @BookingDateEnd IS NULL) OR 
			 (@BookingDateStart <= bk.BookingDate AND @BookingDateEnd >= bk.BookingDate))
		AND	bk.BookingStatus = ISNULL(@BookingStatus, bk.BookingStatus)
		AND	((@ParentCategoryId IS NULL) OR (mc.ParentId = @ParentCategoryId))
		AND	((@SubCategoryId IS NULL) OR (mc.MainCategoryId = @SubCategoryId))
		
set @Params = '@StartRowIndex int'

-- ****************************************************
-- Generate the Dynamic sql used for Paging and Sorting
-- ****************************************************
set @SelectSQL = 
'
	SELECT TOP ' + CONVERT(nvarchar, @MaximumRows) + ' 
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
	ORDER BY ' + @SortExpression + ';
'

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
		
drop table #BookingResults