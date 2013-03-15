CREATE proc [dbo].[bst_EmailBroadcastEntrySelect] 
    @EmailBroadcastEntryId int
as 
	set nocount on 
	set xact_abort on  

	begin tran

	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry] 
	where  ([EmailBroadcastEntryId] = @EmailBroadcastEntryId or @EmailBroadcastEntryId is null) 

	commit
