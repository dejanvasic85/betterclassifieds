CREATE proc [dbo].[bst_GetUnsentEmailBroadcastEntry] 
as 
	set nocount on 
	set xact_abort on  

	begin tran

	select top 1000 [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry] 
	where  ([SentDateTime] is null)

	commit
	
	
	SET ANSI_NULLS ON
