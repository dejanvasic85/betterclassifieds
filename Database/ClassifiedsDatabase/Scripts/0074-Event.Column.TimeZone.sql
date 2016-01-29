ALTER TABLE [Event]
ADD TimeZoneId VARCHAR(50) NULL
GO

ALTER TABLE [Event]
ADD TimeZoneName VARCHAR(50) NULL
GO

ALTER TABLE [Event]
ADD TimeZoneDaylightSavingsOffsetSeconds BIGINT  NULL
GO

ALTER TABLE [Event]
ADD TimeZoneUtcOffsetSeconds BIGINT  NULL
GO