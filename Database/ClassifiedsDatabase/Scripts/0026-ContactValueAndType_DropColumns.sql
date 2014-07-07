IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[OnlineAd]') 
         AND name = 'ContactType'
)
begin
	ALTER TABLE OnlineAd 
	DROP COLUMN ContactType
end

IF EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[OnlineAd]') 
         AND name = 'ContactValue'
)
begin
	ALTER TABLE OnlineAd 
	DROP COLUMN ContactValue
end
