
	ALTER TABLE Registration
	ALTER COLUMN Token NVARCHAR(50) NULL 

	GO 

	DROP INDEX Registration.IX_Registration_Token