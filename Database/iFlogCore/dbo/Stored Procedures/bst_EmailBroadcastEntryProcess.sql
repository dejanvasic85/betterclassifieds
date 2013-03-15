CREATE proc [dbo].[bst_EmailBroadcastEntryProcess] 
    @EmailBroadcastEntryId int,
    @SentDateTime datetime = null,
    @RetryNo int = 0
    
as 
	set nocount on 
	set xact_abort on  
	
	begin tran

	update [dbo].[EmailBroadcastEntry]
	set    [LastRetryDateTime] = GETDATE(), [SentDateTime] = @SentDateTime, [RetryNo] = [RetryNo] + @RetryNo
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId
	
	-- Begin Return Select <- do not remove
	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority]
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId	
	-- End Return Select <- do not remove

	commit
--===============================================
