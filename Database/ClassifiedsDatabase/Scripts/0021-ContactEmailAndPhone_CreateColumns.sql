IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[OnlineAd]') 
         AND name = 'ContactEmail'
)
begin
	ALTER TABLE OnlineAd ADD ContactEmail NVARCHAR(100) NULL
end

IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[OnlineAd]') 
         AND name = 'ContactPhone'
)
begin
	ALTER TABLE OnlineAd ADD ContactPhone NVARCHAR(50) NULL
end