CREATE proc [dbo].[bst_EmailBroadcastEntryInsert] 
	@BroadcastId uniqueidentifier,
    @Email nvarchar(100),
    @EmailContent nvarchar(max),
    @LastRetryDateTime datetime = null,
    @SentDateTime datetime = null,
    @RetryNo int = 0,
    @Subject varchar(50),
    @Sender nvarchar(50),
    @IsBodyHtml bit = 'true' ,
    @Priority int =0
as 
	set nocount on 
	set xact_abort on  
	
	begin tran
	
	insert into [dbo].[EmailBroadcastEntry] ([Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId])
	select @Email, @EmailContent, @LastRetryDateTime, @SentDateTime, @RetryNo, @Subject, @Sender, @IsBodyHtml, @Priority, @BroadcastId
	
	-- Begin Return Select <- do not remove
	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = scope_identity()
	-- End Return Select <- do not remove
               
	commit
