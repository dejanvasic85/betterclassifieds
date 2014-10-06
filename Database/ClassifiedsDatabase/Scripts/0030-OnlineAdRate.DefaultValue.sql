
/****** Data:  Table [dbo].[OnlineAdRate]    Script Date: 6/10/2014 10:06:46 PM ******/
IF NOT EXISTS ( select 1 from OnlineAdRate where MainCategoryId IS NULL and MinimumCharge = 0 ) 
begin	
	INSERT INTO OnlineAdRate 
		( MinimumCharge )
	VALUES
		( 0 )
end