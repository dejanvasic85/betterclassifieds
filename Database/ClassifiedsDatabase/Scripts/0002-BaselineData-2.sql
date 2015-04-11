GO
	IF NOT EXISTS ( SELECT 1 FROM PublicationType WHERE Code = 'NEWS' )
	begin
		INSERT INTO [dbo].[PublicationType]
				   ([PublicationTypeId]
				   ,[Code]
				   ,[Title]
				   ,[Description])
			 VALUES
				   ( 1 -- Have to hard code this value to make it backward compatible
				   , 'NEWS'
				   , 'Newspaper'
				   , 'Newspaper');
	end

	IF NOT EXISTS ( SELECT 1 FROM PublicationType WHERE Code = 'ONLINE' )
	begin
		INSERT INTO [dbo].[PublicationType]
				   ([PublicationTypeId]
				   ,[Code]
				   ,[Title]
				   ,[Description])
			 VALUES
				   ( 3 -- Have to hard code this value to make it backward compatible
				   , 'ONLINE'
				   , 'Online'
				   , 'Online - soon to be redundant');
	end

	IF NOT EXISTS ( SELECT 1 FROM EnquiryType WHERE Title = 'General')
	begin

	INSERT INTO [dbo].[EnquiryType]
           ([Title]
           ,[Active]
           ,[CreatedDate])
     VALUES
           (
		   'General',
           1,
           GETDATE())

	end
GO