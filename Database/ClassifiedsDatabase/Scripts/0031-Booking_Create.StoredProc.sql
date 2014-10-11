GO
/****** Object:  StoredProcedure [dbo].[Booking_Create]    Script Date: 11/10/2014 3:47:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Booking_Create]
       @startDate			DATETIME,
	   @endDate				DATETIME,
	   @totalPrice			MONEY,
	   @bookReference		VARCHAR(10),
	   @userId				VARCHAR(50),
	   @mainCategoryId		INT,
	   @insertions			INT,
	   @locationAreaId		INT = NULL,
	   @onlineAdHeading		VARCHAR(255),
	   @onlineAdDescription	VARCHAR(max),
	   @onlineAdHtml		VARCHAR(max),
	   @onlineAdPrice		MONEY = NULL,
	   @contactName			VARCHAR(200),
	   @contactEmail		VARCHAR(100),
	   @contactPhone		VARCHAR(50),
	   @adBookingId			INT = NULL OUTPUT,
	   @onlineDesignId		INT = NULL OUTPUT
AS
BEGIN TRANSACTION
	
	declare	@adId INT;
	declare @lineAdDesignId INT;
	declare @locationId INT;
	declare @createdDateTime DATETIME = GETDATE();
	declare @onlineAdTypeId INT;
	declare @lineAdTypeId INT;
	declare @bookedStatusId INT = 1; -- Booked/Completed
	declare @regularBookingType VARCHAR(20) = 'Regular';

	-- Default the location area ID to any (if none was provided)
	IF		@locationAreaId IS NULL
	begin
		SELECT	@locationAreaId = l.LocationAreaId
		FROM	LocationArea l
		WHERE	Title = ' Any Area';
	end
	
	-- Get Location and Area
	SELECT	@locationId = l.LocationId FROM LocationArea l WHERE l.LocationAreaId = @locationAreaId ;
	
	-- Get Ad Type Id
	SELECT	@onlineAdTypeId = at.AdTypeId FROM AdType at WHERE at.Code = 'ONLINE';
	SELECT  @lineAdTypeId = at.AdTypeId FROM AdType at WHERE at.Code = 'LINE';

	-- Ad Table
	INSERT INTO Ad	( Title ) 
	VALUES			( 'NextGen' );
	set	@adId = @@IDENTITY;
	
	-- Online Ad
	INSERT INTO AdDesign ( AdId, AdTypeId, CreatedDate )
	VALUES				 ( @adId, @onlineAdTypeId, @createdDateTime );
	set	@onlineDesignId = @@IDENTITY;

	INSERT INTO OnlineAd	
		( AdDesignId
		, Heading
		, [Description]
		, HtmlText
		, Price
		, LocationId
		, LocationAreaId
		, ContactName
		, NumOfViews
		, OnlineAdTag
		, ContactEmail
		, ContactPhone )
	VALUES
		( @onlineDesignId
		, @onlineAdHeading
		, @onlineAdDescription
		, @onlineAdHtml
		, @onlineAdPrice
		, @locationId
		, @locationAreaId
		, @contactName
		, 1
		, null
		, @contactEmail
		, @contactPhone );


	-- Ad Booking
	INSERT INTO AdBooking 
		( StartDate
		, EndDate
		, TotalPrice
		, BookReference
		, AdId
		, UserId
		, BookingStatus
		, MainCategoryId
		, BookingType
		, BookingDate
		, Insertions )
	VALUES 
		( @startDate
		, @endDate
		, @totalPrice
		, @bookReference
		, @adId
		, @userId
		, @bookedStatusId
		, @mainCategoryId
		, @regularBookingType
		, @createdDateTime
		, @insertions )


	set	@adBookingId = @@IDENTITY;

COMMIT