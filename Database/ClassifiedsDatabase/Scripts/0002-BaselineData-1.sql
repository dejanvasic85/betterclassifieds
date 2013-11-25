GO
IF NOT EXISTS ( SELECT 1 FROM AdType WHERE Code = 'LINE' )
begin
	INSERT INTO AdType (AdTypeId, Code, Title, Description ) 	VALUES ( 1, 'LINE', 'Line Ad', 'Simple one column classified' );
end

IF NOT EXISTS ( SELECT 1 FROM AdType WHERE Code = 'ONLINE' )
begin
	INSERT INTO AdType ( AdTypeId, Code, Title, Description )	VALUES ( 2, 'ONLINE', 'Online Ad', 'General online classified' );
end

GO

