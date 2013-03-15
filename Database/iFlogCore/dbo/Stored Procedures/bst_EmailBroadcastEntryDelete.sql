create proc [dbo].[bst_EmailBroadcastEntryDelete] 
    @EmailBroadcastEntryId int
as 
	set nocount on 
	set xact_abort on  
	
	begin tran

	delete
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId

	commit
