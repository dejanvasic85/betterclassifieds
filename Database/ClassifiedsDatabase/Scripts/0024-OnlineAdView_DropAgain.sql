
-- Drop the dependencies for the contact column
-- For some reason the Drop current search script never really dropped the view in the first place!

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spSearchOnlineAdFREETEXT]') AND type in (N'P', N'PC'))
begin
	drop procedure dbo.spSearchOnlineAdFREETEXT
end

IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OnlineAdView]'))
begin
	drop view dbo.OnlineAdView
end

drop fulltext catalog OnlineAdSearch