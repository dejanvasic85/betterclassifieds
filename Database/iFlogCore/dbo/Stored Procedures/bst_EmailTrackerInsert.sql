CREATE PROC [dbo].[bst_EmailTrackerInsert] 
    @EmailBroadcastEntryId uniqueidentifier,
    @Page nvarchar(MAX) = null,
    @IpAddress nvarchar(50) = null,
    @Browser nvarchar(50) =null,
    @Country nvarchar(150)= null,
    @Region nvarchar(150) =null,
    @City nvarchar(150) = null,
    @Postcode nvarchar(50) =null,
    @Latitude nvarchar(50)=null,
    @Longitude nvarchar(50)=null,
    @TimeZone nchar(10) =null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[EmailTracker] ([EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone])
	SELECT @EmailBroadcastEntryId, @Page, @IpAddress, @Browser, GETDATE(), @Country, @Region, @City, @Postcode, @Latitude, @Longitude, @TimeZone
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTrackerId], [EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone]
	FROM   [dbo].[EmailTracker]
	WHERE  [EmailTrackerId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
