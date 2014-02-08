

GO
/****** Object:  StoredProcedure [dbo].[psp_Betterclassified_GetActivitySummary]    Script Date: 2/02/2014 10:11:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[psp_Betterclassified_GetActivitySummary]
	@ReportDate DATETIME = NULL
AS
BEGIN
	/*e.g

		exec psp_BetterClassified_GetActivitySummary '2013-11-01'
	*/

	SET NOCOUNT ON;

	IF @ReportDate IS NULL
	begin
		-- Set todays date without the time
		SET @ReportDate = DATEADD(dd, 0, DATEDIFF(dd, 0, GETDATE()))
	end

	DECLARE @startDate DATETIME = @ReportDate;
	DECLARE @endDate DATETIME = DATEADD(MINUTE, 1439, @ReportDate);
	DECLARE @results TABLE ( StatisticName VARCHAR(100), StatisticValue VARCHAR(MAX) );
	DECLARE @bookedStatus INT = 1;


	-- Number of all bookings records inserted for the day	
	DECLARE @totalBookings INT;
	SELECT @totalBookings = COUNT(1) FROM AdBooking WHERE BookingStatus = @bookedStatus AND BookingDate BETWEEN @startDate AND @endDate;
	INSERT INTO @results VALUES ('Bookings Completed', @totalBookings);

	
	-- Number of all bookings records inserted for the day	
	DECLARE @totalBookingsNotCompleted INT;
	SELECT @totalBookingsNotCompleted = COUNT(1) FROM AdBooking WHERE BookingStatus != @bookedStatus AND BookingDate BETWEEN @startDate AND @endDate;
	INSERT INTO @results VALUES ('Bookings Not Completed', @totalBookingsNotCompleted);


	-- Number of Bookings that were extended
	DECLARE @numberOfExtensions INT;
	SELECT @numberOfExtensions = COUNT(1) FROM AdBookingExtension WHERE LastModifiedDate BETWEEN @startDate AND @endDate;
	INSERT INTO @results VALUES ('Bookings Extended', @numberOfExtensions);

	
	-- Number of tutor ads booked for the day
	DECLARE @numberOfTutorAdsBooked INT;	
	SELECT @numberOfTutorAdsBooked = COUNT(1) 
	FROM AdBooking 
	WHERE BookingDate BETWEEN @startDate AND @endDate
	AND BookingStatus = @bookedStatus
	AND	MainCategoryId IN ( SELECT MainCategoryId FROM MainCategory WHERE OnlineAdTag = 'Tutors' );
	INSERT INTO @results VALUES ('Tutor Ads', @numberOfTutorAdsBooked);
	

	-- Total income ( from transactions )declare @totalIncome DECIMAL
	DECLARE @totalIncome DECIMAL;
	SELECT @totalIncome = SUM(Amount) FROM [Transaction] where TransactionDate BETWEEN @startDate AND @endDate;
	INSERT INTO @results VALUES ('Income', @totalIncome);
	

	-- Number of times an advertiser was Contacted
	DECLARE @totalContactCount INT;
	SELECT @totalContactCount = COUNT(1) FROM [OnlineAdEnquiry] where CreatedDate BETWEEN @startDate AND @endDate;
	INSERT INTO @results VALUES ('Contact Advertiser', @totalContactCount);
			

	SELECT * FROM @results;
END
