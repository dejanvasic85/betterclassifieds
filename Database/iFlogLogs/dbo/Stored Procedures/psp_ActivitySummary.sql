CREATE PROCEDURE psp_ActivitySummary 
	@ReportDate DATETIME
AS
BEGIN

DECLARE @endDate	DATETIME;

SET @endDate = DATEADD(MINUTE, 1439, @ReportDate)

SELECT TOP 10 COUNT (1) Occurrances, [Application], Data1 AS ErrorMessage
FROM [dbo].[Log]
WHERE DateTimeCreated BETWEEN @ReportDate AND @endDate
AND Category = 'EventLog'
GROUP BY [Application], Data1
ORDER BY COUNT(1) DESC

END
