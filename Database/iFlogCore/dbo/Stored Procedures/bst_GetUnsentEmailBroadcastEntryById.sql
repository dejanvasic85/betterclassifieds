CREATE proc [dbo].[bst_GetUnsentEmailBroadcastEntryById] 
    @BroadcastId uniqueidentifier
as 
	set nocount on 
	set xact_abort on  

	begin tran

	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry] 
	where  ([SentDateTime] is  null) and		
	([BroadcastId] = @BroadcastId) 

	commit