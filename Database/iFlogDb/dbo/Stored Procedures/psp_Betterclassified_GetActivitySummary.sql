CREATE PROCEDURE psp_Betterclassified_GetActivitySummary
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
