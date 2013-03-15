create proc [dbo].[bst_EmailBroadcastEntryUpdate] 
    @EmailBroadcastEntryId int,
    @Email nvarchar(100),
    @EmailContent nvarchar(max),
    @LastRetryDateTime datetime = null,
    @SentDateTime datetime = null,
    @RetryNo int = 0,
    @Subject varchar(50),
    @Sender nvarchar(50),
    @IsBodyHtml bit = 'true',
    @Priority int = 0
as 
	set nocount on 
	set xact_abort on  
	
	begin tran

	update [dbo].[EmailBroadcastEntry]
	set    [Email] = @Email, [EmailContent] = @EmailContent, [LastRetryDateTime] = @LastRetryDateTime, [SentDateTime] = @SentDateTime, [RetryNo] = @RetryNo, [Subject] = @Subject, [Sender] = @Sender, [IsBodyHtml] = @IsBodyHtml, [Priority] = @Priority
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId
	
	-- Begin Return Select <- do not remove
	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority]
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId	
	-- End Return Select <- do not remove

	commit tran
