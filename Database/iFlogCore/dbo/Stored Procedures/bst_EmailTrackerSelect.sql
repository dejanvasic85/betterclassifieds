CREATE PROC [dbo].[bst_EmailTrackerSelect] 
    @EmailTrackerId INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTrackerId], [EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone] 
	FROM   [dbo].[EmailTracker] 
	WHERE  ([EmailTrackerId] = @EmailTrackerId OR @EmailTrackerId IS NULL) 

	COMMIT
