/*
So yeah... 
This is not ideal at all but we have to add these really bad values because of the search ads stored procedure / view
One day... we may just implement proper location services and remove these tables altogether?
*/
SET ANSI_PADDING ON

GO
DECLARE @anyLocationId AS INT,
		@anyAreaId AS INT;

SELECT @anyLocationId = LocationId
FROM Location
WHERE Title like '%Any Location%'

IF @anyLocationId IS NULL
begin
	INSERT INTO Location (Title) VALUES (' Any Location');
	SET @anyLocationId = @@IDENTITY;
end



SELECT @anyAreaId = LocationAreaId
FROM LocationArea
WHERE Title like '%Any Area%'

IF @anyAreaId IS NULL
begin
	INSERT INTO LocationArea (LocationId, Title) VALUES (@anyLocationId, ' Any Area');
	SET @anyLocationId = @@IDENTITY;
end

GO
SET ANSI_PADDING OFF
GO