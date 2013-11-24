
GO
IF NOT EXISTS (SELECT * FROM aspnet_SchemaVersions)
begin
	INSERT INTO aspnet_SchemaVersions (Feature, CompatibleSchemaVersion, IsCurrentVersion)
		VALUES	( 'common', 1, 1 ),
				( 'membership', 1, 1 ),
				( 'role manager', 1, 1 ),
				( 'profileVersion', 1, 1 )
end