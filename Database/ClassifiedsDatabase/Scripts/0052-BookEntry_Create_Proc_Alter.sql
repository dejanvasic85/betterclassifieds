
/****** Object:  StoredProcedure [dbo].[Booking_Create]    Script Date: 28/07/2015 8:22:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Booking_Create]
       @startDate			DATETIME,
	   @endDate				DATETIME,
	   @totalPrice			MONEY,
	   @bookReference		VARCHAR(10),
	   @userId				VARCHAR(50),
	   @mainCategoryId		INT,
	   @insertions			INT,
	   @locationId			INT = NULL,
	   @locationAreaId		INT = NULL,
	   @onlineAdHeading		VARCHAR(255),
	   @onlineAdDescription	VARCHAR(max),
	   @onlineAdHtml		VARCHAR(max),
	   @onlineAdPrice		MONEY = NULL,
	   @contactName			VARCHAR(200),
	   @contactEmail		VARCHAR(100),
	   @contactPhone		VARCHAR(50),
	   @transactionType		INT = 2, -- Paypal = 2
	   @adBookingId			INT = NULL OUTPUT,
	   @onlineDesignId		INT = NULL OUTPUT,
	   @onlineAdId			INT = NULL OUTPUT
AS
/*
	28-JUL-2015	Adding OnlineAdId to the output list
*/
BEGIN TRANSACTION
	
	declare	@adId INT;
	declare @lineAdDesignId INT;
	declare @createdDateTime DATETIME = GETDATE();
	declare @onlineAdTypeId INT;
	declare @lineAdTypeId INT;
	declare @bookedStatusId INT = 1; -- Booked/Completed
	declare @regularBookingType VARCHAR(20) = 'Regular';

	IF @totalPrice = 0 
	begin
		SET	@transactionType = 3 -- Free  = 3
	end

	-- Default the location and location area to ALL
	IF @locationId IS NULL
	begin
		SELECT	@locationId = LocationId
		FROM	Location
		WHERE	LTRIM(Title) = 'Any Location'
	end

	IF @locationAreaId IS NULL
	begin
		SELECT	@locationAreaId = LocationAreaId
		FROM	LocationArea
		WHERE	LTRIM(Title) = 'Any Area'
	end
	
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

	SET	@onlineAdId = @@IDENTITY;

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
		, @insertions );


	set	@adBookingId = @@IDENTITY;


	INSERT INTO [dbo].[Transaction]
           ([TransactionType]
           ,[UserId]
           ,[Title]
           ,[Description]
           ,[Amount]
           ,[TransactionDate])
     VALUES
           (@transactionType, 
		    @userId,
			@bookReference,
			'Classified Booking',
			@totalPrice,
			@createdDateTime);

COMMIT