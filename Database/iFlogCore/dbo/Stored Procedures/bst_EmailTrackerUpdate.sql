CREATE PROC [dbo].[bst_EmailTrackerUpdate] 
    @EmailTrackerId int,
    @EmailBroadcastEntryId uniqueidentifier,
    @Page nvarchar(MAX),
    @IpAddress nvarchar(50),
    @Browser nvarchar(50),
    @DateTime datetime,
    @Country nvarchar(150),
    @Region nvarchar(150),
    @City nvarchar(150),
    @Postcode nvarchar(50),
    @Latitude nvarchar(50),
    @Longitude nvarchar(50),
    @TimeZone nchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EmailTracker]
	SET    [EmailBroadcastEntryId] = @EmailBroadcastEntryId, [Page] = @Page, [IpAddress] = @IpAddress, [Browser] = @Browser, [DateTime] = @DateTime, [Country] = @Country, [Region] = @Region, [City] = @City, [Postcode] = @Postcode, [Latitude] = @Latitude, [Longitude] = @Longitude, [TimeZone] = @TimeZone
	WHERE  [EmailTrackerId] = @EmailTrackerId
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTrackerId], [EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone]
	FROM   [dbo].[EmailTracker]
	WHERE  [EmailTrackerId] = @EmailTrackerId	
	-- End Return Select <- do not remove

	COMMIT TRAN
