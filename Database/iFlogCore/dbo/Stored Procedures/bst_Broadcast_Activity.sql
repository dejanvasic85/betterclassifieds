CREATE PROCEDURE bst_Broadcast_Activity
	@ReportDate DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @endDate	DATETIME;
	SET @endDate = DATEADD(MINUTE, 1439, @ReportDate)

	SELECT	COUNT(1) AS NumberOfEmailsSent
	FROM	dbo.Broadcast
	WHERE CreatedDateTime BETWEEN @ReportDate AND @endDate
END
