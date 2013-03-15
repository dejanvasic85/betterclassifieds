CREATE PROC [dbo].[crm_EntityGetNewId] 
@EntityCode varchar(10) output
AS 
   
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	INSERT INTO [dbo].[EntityCounter] ([DateTimeCreated])
	SELECT GETDATE()
	
	-- Begin Return Select <- do not remove
	
	 SELECT @EntityCode =  'P' + REPLICATE('0', 9 - LEN(CounterId)) + cast(CounterId AS nvarchar) 
	FROM   [dbo].[EntityCounter]
	WHERE  [CounterId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT

